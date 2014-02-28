using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using System.Device.Location;
using System.Windows.Shapes;
using System.Windows.Media;
using Microsoft.Phone.Maps.Controls;
using Microsoft.Phone.Maps.Toolkit;
using System.Windows.Media.Imaging;
using travelroute.DBClasses;
using Microsoft.WindowsAzure.MobileServices;

namespace travelroute
{
    public partial class EditRoute : PhoneApplicationPage
    {
        private Geolocator geolocator = null;
        private Ellipse mapCircle = new Ellipse();

        private MapOverlay myLocationOverlay = new MapOverlay();
        
        private MapLayer myLocationLayer = new MapLayer();
        private MapLayer registerLayer = new MapLayer();

        private List<GeoCoordinate> registerCoordinates = new List<GeoCoordinate>();

        private bool firstMapLoad = true;

        public EditRoute()
        {
            InitializeComponent();

            // Create a small circle to mark the current location.

            mapCircle.Fill = new SolidColorBrush(Colors.Blue);
            mapCircle.Height = 20;
            mapCircle.Width = 20;
            mapCircle.Opacity = 50;

            registerGrid.Visibility = System.Windows.Visibility.Collapsed;

            routeMap.Layers.Add(registerLayer);



            geolocator = new Geolocator();
            geolocator.DesiredAccuracy = PositionAccuracy.High;
            geolocator.MovementThreshold = 1; // The units are meters.

            geolocator.PositionChanged += geolocator_PositionChanged;


            myLocationOverlay.Content = mapCircle;
            myLocationOverlay.PositionOrigin = new Point(0.5, 0.5);
            mapCircle.Visibility = System.Windows.Visibility.Collapsed;

            myLocationLayer.Add(myLocationOverlay);
            routeMap.Layers.Add(myLocationLayer);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            routeName.Text = AzureDBM.selectedRoute.Name;
            RefreshRegisters();
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Home.xaml", UriKind.Relative));
        }

        private void geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            Dispatcher.BeginInvoke(() =>
            {
                double lat = args.Position.Coordinate.Latitude;
                double lon = args.Position.Coordinate.Longitude;

                GeoCoordinate gc = new GeoCoordinate(lat, lon);

                mapCircle.Visibility = System.Windows.Visibility.Visible;
                myLocationOverlay.GeoCoordinate = gc;

                if (firstMapLoad)
                {
                    routeMap.Center = gc;
                    routeMap.ZoomLevel = 15;

                    firstMapLoad = false;
                }
            });
        }

        private async void RefreshRegisters()
        {
            // This code refreshes the entries in the "rutas activas" view querying the Ruta table.
            // The query excludes Rutas that do now belown to the current user
            try
            {
                AzureDBM.registerItems = await AzureDBM.registerTable
                    .Where(reg => reg.RouteId == AzureDBM.selectedRoute.Id)
                    .OrderBy(reg => reg.CreatedAt)
                    .ToCollectionAsync();

                registerCoordinates.Clear();

                foreach (Register r in AzureDBM.registerItems)
                {
                    Image image = new Image();
                    //Define the URI location of the image

                    if(r.Type.Equals("POI"))
                    {
                        image.Source = new BitmapImage(new Uri("Assets/Icons/registerPOI.png", UriKind.Relative));
                    }

                    else if (r.Type.Equals("Sleep"))
                    {
                        image.Source = new BitmapImage(new Uri("Assets/Icons/registerSleep.png", UriKind.Relative));
                    }

                    else if (r.Type.Equals("Restaurant"))
                    {
                        image.Source = new BitmapImage(new Uri("Assets/Icons/registerEat.png", UriKind.Relative));
                    }

                    else if (r.Type.Equals("Transport"))
                    {
                        image.Source = new BitmapImage(new Uri("Assets/Icons/registerTransport.png", UriKind.Relative));
                    }

                    else if (r.Type.Equals("Picture"))
                    {
                        image.Source = new BitmapImage(new Uri("Assets/Icons/registerPicture.png", UriKind.Relative));
                    }

                    else if (r.Type.Equals("Comment"))
                    {
                        image.Source = new BitmapImage(new Uri("Assets/Icons/registerComment.png", UriKind.Relative));
                    }

                    image.Stretch = System.Windows.Media.Stretch.None;

                    MapOverlay registerOverlay = new MapOverlay();

                    registerOverlay.Content = image;
                    registerOverlay.PositionOrigin = new Point(0.5, 0.9);
                    registerOverlay.GeoCoordinate = new GeoCoordinate(r.Latitude, r.Longitude);

                    registerLayer.Add(registerOverlay);

                    registerCoordinates.Add(new GeoCoordinate(r.Latitude, r.Longitude));
                }

                RefreshPolylines();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                MessageBox.Show(e.Message, "Error loading items", MessageBoxButton.OK);
            }
        }

        private void RefreshPolylines()
        {
            //delete polylines from the map
            routeMap.MapElements.Clear();

            if (registerCoordinates.Count > 1)
            {
                //creates n-1 polylines for the route
                for (int i = 0; i < registerCoordinates.Count - 1; i++)
                {
                    MapPolyline polyLine = new MapPolyline();

                    byte R = 0;
                    byte G = 0;
                    byte B = 0;
                    
                    if (AzureDBM.registerItems[i + 1].Appreciation == 1)
                    {
                        R = 200;
                        G = 55;
                        B = 55;
                    }

                    else if (AzureDBM.registerItems[i + 1].Appreciation == 2)
                    {
                        R = 253;
                        G = 125;
                        B = 48;
                    }

                    else if (AzureDBM.registerItems[i + 1].Appreciation == 3)
                    {
                        R = 253;
                        G = 227;
                        B = 69;
                    }

                    else if (AzureDBM.registerItems[i + 1].Appreciation == 4)
                    {
                        R = 193;
                        G = 219;
                        B = 63;
                    }

                    else if (AzureDBM.registerItems[i + 1].Appreciation == 5)
                    {
                        R = 23;
                        G = 176;
                        B = 76;
                    }

                    polyLine.StrokeColor = Color.FromArgb(255, R, G, B);
                    polyLine.StrokeThickness = 6;

                    polyLine.Path.Add(registerCoordinates[i]);
                    polyLine.Path.Add(registerCoordinates[i + 1]);

                    routeMap.MapElements.Add(polyLine);
                }

            }

        }

        private void addRegisterButton_Click(object sender, EventArgs e)
        {
            if (registerGrid.Visibility == System.Windows.Visibility.Collapsed)
            {
                registerGrid.Visibility = System.Windows.Visibility.Visible;
            }

            else
            {
                registerGrid.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void POIButton_Click(object sender, RoutedEventArgs e)
        {
            AzureDBM.selectedRegisterType = "POI";
            AzureDBM.selectedRegisterLat = myLocationOverlay.GeoCoordinate.Latitude;
            AzureDBM.selectedRegisterLon = myLocationOverlay.GeoCoordinate.Longitude;

            NavigationService.Navigate(new Uri("/NewRegister.xaml", UriKind.Relative));
        }

        private void sleepButton_Click(object sender, RoutedEventArgs e)
        {
            AzureDBM.selectedRegisterType = "Sleep";
            AzureDBM.selectedRegisterLat = myLocationOverlay.GeoCoordinate.Latitude;
            AzureDBM.selectedRegisterLon = myLocationOverlay.GeoCoordinate.Longitude;

            NavigationService.Navigate(new Uri("/NewRegister.xaml", UriKind.Relative));
        }

        private void restaurantButton_Click(object sender, RoutedEventArgs e)
        {
            AzureDBM.selectedRegisterType = "Restaurant";
            AzureDBM.selectedRegisterLat = myLocationOverlay.GeoCoordinate.Latitude;
            AzureDBM.selectedRegisterLon = myLocationOverlay.GeoCoordinate.Longitude;

            NavigationService.Navigate(new Uri("/NewRegister.xaml", UriKind.Relative));
        }

        private void transportButton_Click(object sender, RoutedEventArgs e)
        {
            AzureDBM.selectedRegisterType = "Transport";
            AzureDBM.selectedRegisterLat = myLocationOverlay.GeoCoordinate.Latitude;
            AzureDBM.selectedRegisterLon = myLocationOverlay.GeoCoordinate.Longitude;

            NavigationService.Navigate(new Uri("/NewRegister.xaml", UriKind.Relative));
        }

        private void pictureButton_Click(object sender, RoutedEventArgs e)
        {
            Image image = new Image();
            //Define the URI location of the image
            image.Source = new BitmapImage(new Uri("Assets/Icons/registerPicture.png", UriKind.Relative));
            image.Stretch = System.Windows.Media.Stretch.None;

            MapOverlay registerOverlay = new MapOverlay();

            registerOverlay.Content = image;
            registerOverlay.PositionOrigin = new Point(0.5, 0.9);
            registerOverlay.GeoCoordinate = myLocationOverlay.GeoCoordinate;

            registerLayer.Add(registerOverlay);

            Register r = new Register();
            r.Type = "Picture";
            r.Latitude = registerOverlay.GeoCoordinate.Latitude;
            r.Longitude = registerOverlay.GeoCoordinate.Longitude;
            r.RouteId = AzureDBM.selectedRoute.Id;
            r.CreatedByCurrentUser = true;

            registerCoordinates.Add(new GeoCoordinate(r.Latitude, r.Longitude));

            AzureDBM.InsertRegister(r);
        }

        private void commentButton_Click(object sender, RoutedEventArgs e)
        {
            Image image = new Image();
            //Define the URI location of the image
            image.Source = new BitmapImage(new Uri("Assets/Icons/registerComment.png", UriKind.Relative));
            image.Stretch = System.Windows.Media.Stretch.None;

            MapOverlay registerOverlay = new MapOverlay();

            registerOverlay.Content = image;
            registerOverlay.PositionOrigin = new Point(0.5, 0.9);
            registerOverlay.GeoCoordinate = myLocationOverlay.GeoCoordinate;

            registerLayer.Add(registerOverlay);

            Register r = new Register();
            r.Type = "Comment";
            r.Latitude = registerOverlay.GeoCoordinate.Latitude;
            r.Longitude = registerOverlay.GeoCoordinate.Longitude;
            r.RouteId = AzureDBM.selectedRoute.Id;
            r.CreatedByCurrentUser = true;

            registerCoordinates.Add(new GeoCoordinate(r.Latitude, r.Longitude));

            AzureDBM.InsertRegister(r);
        }

        private void reloadRegisterButton_Click(object sender, EventArgs e)
        {
            RefreshRegisters();
        }
    }
}
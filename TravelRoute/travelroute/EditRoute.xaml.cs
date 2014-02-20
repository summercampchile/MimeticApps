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
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            geolocator = new Geolocator();
            geolocator.DesiredAccuracy = PositionAccuracy.High;
            geolocator.MovementThreshold = 1; // The units are meters.

            geolocator.PositionChanged += geolocator_PositionChanged;


            myLocationOverlay.Content = mapCircle;
            myLocationOverlay.PositionOrigin = new Point(0.5, 0.5);
            mapCircle.Visibility = System.Windows.Visibility.Collapsed;

            myLocationLayer.Add(myLocationOverlay);
            routeMap.Layers.Add(myLocationLayer);

            RefreshRegisters();
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
                    routeMap.ZoomLevel = 18;

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
                    .ToCollectionAsync();

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
                }
            }
            catch (MobileServiceInvalidOperationException e)
            {
                MessageBox.Show(e.Message, "Error loading items", MessageBoxButton.OK);
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
            Image image = new Image();
            //Define the URI location of the image
            image.Source = new BitmapImage(new Uri("Assets/Icons/registerPOI.png", UriKind.Relative));
            image.Stretch = System.Windows.Media.Stretch.None;

            MapOverlay registerOverlay = new MapOverlay();

            registerOverlay.Content = image;
            registerOverlay.PositionOrigin = new Point(0.5, 0.9);
            registerOverlay.GeoCoordinate = myLocationOverlay.GeoCoordinate;

            registerLayer.Add(registerOverlay);

            Register r = new Register();
            r.Type = "POI";
            r.Latitude = registerOverlay.GeoCoordinate.Latitude;
            r.Longitude = registerOverlay.GeoCoordinate.Longitude;
            r.RouteId = AzureDBM.selectedRoute.Id;

            AzureDBM.InsertRegister(r);
        }

        private void sleepButton_Click(object sender, RoutedEventArgs e)
        {
            Image image = new Image();
            //Define the URI location of the image
            image.Source = new BitmapImage(new Uri("Assets/Icons/registerSleep.png", UriKind.Relative));
            image.Stretch = System.Windows.Media.Stretch.None;

            MapOverlay registerOverlay = new MapOverlay();

            registerOverlay.Content = image;
            registerOverlay.PositionOrigin = new Point(0.5, 0.9);
            registerOverlay.GeoCoordinate = myLocationOverlay.GeoCoordinate;

            registerLayer.Add(registerOverlay);

            Register r = new Register();
            r.Type = "Sleep";
            r.Latitude = registerOverlay.GeoCoordinate.Latitude;
            r.Longitude = registerOverlay.GeoCoordinate.Longitude;
            r.RouteId = AzureDBM.selectedRoute.Id;

            AzureDBM.InsertRegister(r);
        }

        private void restaurantButton_Click(object sender, RoutedEventArgs e)
        {
            Image image = new Image();
            //Define the URI location of the image
            image.Source = new BitmapImage(new Uri("Assets/Icons/registerEat.png", UriKind.Relative));
            image.Stretch = System.Windows.Media.Stretch.None;

            MapOverlay registerOverlay = new MapOverlay();

            registerOverlay.Content = image;
            registerOverlay.PositionOrigin = new Point(0.5, 0.9);
            registerOverlay.GeoCoordinate = myLocationOverlay.GeoCoordinate;

            registerLayer.Add(registerOverlay);

            Register r = new Register();
            r.Type = "Restaurant";
            r.Latitude = registerOverlay.GeoCoordinate.Latitude;
            r.Longitude = registerOverlay.GeoCoordinate.Longitude;
            r.RouteId = AzureDBM.selectedRoute.Id;

            AzureDBM.InsertRegister(r);
        }

        private void transportButton_Click(object sender, RoutedEventArgs e)
        {
            Image image = new Image();
            //Define the URI location of the image
            image.Source = new BitmapImage(new Uri("Assets/Icons/registerTransport.png", UriKind.Relative));
            image.Stretch = System.Windows.Media.Stretch.None;

            MapOverlay registerOverlay = new MapOverlay();

            registerOverlay.Content = image;
            registerOverlay.PositionOrigin = new Point(0.5, 0.9);
            registerOverlay.GeoCoordinate = myLocationOverlay.GeoCoordinate;

            registerLayer.Add(registerOverlay);

            Register r = new Register();
            r.Type = "Transport";
            r.Latitude = registerOverlay.GeoCoordinate.Latitude;
            r.Longitude = registerOverlay.GeoCoordinate.Longitude;
            r.RouteId = AzureDBM.selectedRoute.Id;

            AzureDBM.InsertRegister(r);
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

            AzureDBM.InsertRegister(r);
        }
    }
}
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
using travelroute.ViewModels;

namespace travelroute
{
    public partial class ViewRoute : PhoneApplicationPage
    {
        private MapLayer registerLayer = new MapLayer();

        private List<GeoCoordinate> registerCoordinates = new List<GeoCoordinate>();

        public ViewRoute()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.RouteViewModel;

            routeMap.Layers.Add(registerLayer);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            routeName.Text = AzureDBM.selectedRoute.Name;

            if (!AzureDBM.isUserLoggedIn)
            {
                this.ApplicationBar.IsVisible = false;
                userComment.IsEnabled = false;
                userRating.IsEnabled = false;
            }

            RefreshRegisters();
            RefreshComments();
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Home.xaml", UriKind.Relative));
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

                Register lastReg = null;
                registerCoordinates.Clear();

                foreach (Register r in AzureDBM.registerItems)
                {
                    Image image = new Image();
                    //Define the URI location of the image

                    if (r.Type.Equals("POI"))
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

                    lastReg = r;
                }

                if(lastReg != null)
                {
                    routeMap.Center = new GeoCoordinate(lastReg.Latitude, lastReg.Longitude);
                    routeMap.ZoomLevel = 12;
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

        private void RouteViewPanorama_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Detects when the user is at the Rutas view and adds or remove buttons from the App Bar
            //in order to show the correct options that the user has on each section.
            if (e.AddedItems.Count < 1)
            {
                return;
            }

            if (!(e.AddedItems[0] is PanoramaItem))
            {
                return;
            }

            PanoramaItem selectedItem = (PanoramaItem)e.AddedItems[0];

            string tag = (string)selectedItem.Tag;

            if (tag.Equals("route"))
            {
                if (this.ApplicationBar.Buttons.Count == 2)
                {
                    //Removes buttons from the app bar. It is always removing from the index 0 because the list gets
                    //shorter and the second item is now the first item.
                    this.ApplicationBar.Buttons.RemoveAt(0);
                    this.ApplicationBar.Buttons.RemoveAt(0);

                    //Adds back the timeline button
                    ApplicationBarIconButton button1 = new ApplicationBarIconButton();
                    button1.IconUri = new Uri("Assets/Icons/clock.png", UriKind.Relative);
                    button1.Text = "timeline";
                    this.ApplicationBar.Buttons.Insert(0, button1);
                    //button1.Click += new EventHandler(button1_Click);

                    //Adds back the share button
                    ApplicationBarIconButton button2 = new ApplicationBarIconButton();
                    button2.IconUri = new Uri("Assets/Icons/share.png", UriKind.Relative);
                    button2.Text = "compartir";
                    this.ApplicationBar.Buttons.Insert(1, button2);
                    //button2.Click += new EventHandler(button2_Click);
                }

                else if (this.ApplicationBar.Buttons.Count == 1)
                {
                    //Adds back the timeline button
                    ApplicationBarIconButton button1 = new ApplicationBarIconButton();
                    button1.IconUri = new Uri("Assets/Icons/clock.png", UriKind.Relative);
                    button1.Text = "timeline";
                    this.ApplicationBar.Buttons.Insert(0, button1);
                    //button1.Click += new EventHandler(button1_Click);
                }
            }

            else if (tag.Equals("stats"))
            {
                //Removes buttons from the app bar. It is always removing from the index 0 because the list gets
                //shorter and the second item is now the first item.
                this.ApplicationBar.Buttons.RemoveAt(0);
                this.ApplicationBar.Buttons.RemoveAt(0);

                //Adds back the share button
                ApplicationBarIconButton button2 = new ApplicationBarIconButton();
                button2.IconUri = new Uri("Assets/Icons/share.png", UriKind.Relative);
                button2.Text = "compartir";
                this.ApplicationBar.Buttons.Insert(0, button2);
                //button2.Click += new EventHandler(button2_Click);


            }

            else if (tag.Equals("comments"))
            {
                if (this.ApplicationBar.Buttons.Count == 2)
                {
                    //Removes buttons from the app bar. It is always removing from the index 0 because the list gets
                    //shorter and the second item is now the first item.
                    this.ApplicationBar.Buttons.RemoveAt(0);
                    this.ApplicationBar.Buttons.RemoveAt(0);

                    //Adds back the check button
                    ApplicationBarIconButton button1 = new ApplicationBarIconButton();
                    button1.IconUri = new Uri("Assets/Icons/check.png", UriKind.Relative);
                    button1.Text = "enviar";
                    this.ApplicationBar.Buttons.Insert(0, button1);
                    button1.Click += new EventHandler(sendCommentButton_Click);

                    //Adds back the cancel button
                    ApplicationBarIconButton button2 = new ApplicationBarIconButton();
                    button2.IconUri = new Uri("Assets/Icons/cancel.png", UriKind.Relative);
                    button2.Text = "cancelar";
                    this.ApplicationBar.Buttons.Insert(1, button2);
                    //button2.Click += new EventHandler(button2_Click);
                }

                else if (this.ApplicationBar.Buttons.Count == 1)
                {
                    //Removes buttons from the app bar. It is always removing from the index 0 because the list gets
                    //shorter and the second item is now the first item.
                    this.ApplicationBar.Buttons.RemoveAt(0);


                    //Adds back the check button
                    ApplicationBarIconButton button1 = new ApplicationBarIconButton();
                    button1.IconUri = new Uri("Assets/Icons/check.png", UriKind.Relative);
                    button1.Text = "enviar";
                    this.ApplicationBar.Buttons.Insert(0, button1);
                    button1.Click += new EventHandler(sendCommentButton_Click);

                    //Adds back the cancel button
                    ApplicationBarIconButton button2 = new ApplicationBarIconButton();
                    button2.IconUri = new Uri("Assets/Icons/cancel.png", UriKind.Relative);
                    button2.Text = "cancelar";
                    this.ApplicationBar.Buttons.Insert(1, button2);
                    //button2.Click += new EventHandler(button2_Click);
                }

            }
        }

        

        private async void RefreshComments()
        {
            // This code refreshes the entries in the "rutas activas" view querying the Ruta table.
            // The query excludes Rutas that do now belown to the current user
            try
            {
                AzureDBM.commentItems = await AzureDBM.commentTable
                    .Where(comentario => comentario.RouteId == AzureDBM.selectedRoute.Id)
                    .ToCollectionAsync();

                App.RouteViewModel.CommentRouteList.Clear();

                foreach (RouteComment c in AzureDBM.commentItems)
                {
                    App.RouteViewModel.CommentRouteList.Add(new CommentViewModel() { Comment = c.Comentario, Appreciation = c.Appreciation });
                }
            }
            catch (MobileServiceInvalidOperationException e)
            {
                MessageBox.Show(e.Message, "Error loading items", MessageBoxButton.OK);
            }

        }

        private void sendCommentButton_Click(object sender, EventArgs e)
        {
            if(!userComment.Text.Equals("") || userRating.Value != 0)
            {
                //Creates the new Comment and then it sends it to Azure so we can store the comment data.
                var c = new RouteComment { UserId = App.MobileService.CurrentUser.UserId, RouteId = AzureDBM.selectedRoute.Id, Comentario = userComment.Text, Appreciation = Convert.ToInt32(userRating.Value) };
                AzureDBM.InsertComment(c);

                userComment.Text = "";
                userRating.Value = 0;

                CommentViewModel cvm = new CommentViewModel() { Comment = c.Comentario, Appreciation = c.Appreciation };
                App.RouteViewModel.CommentRouteList.Insert(0, cvm);
                commentLLS.ScrollTo(cvm);
            }

            else
            {
                MessageBox.Show("Por favor ingrese un comentario o una valoración para esta ruta.");
            }
            
        }
    }
}
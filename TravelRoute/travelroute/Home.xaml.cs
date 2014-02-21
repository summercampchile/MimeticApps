using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.MobileServices;
using travelroute.Resources;
using travelroute.DBClasses;
using System.Windows.Media.Imaging;
using travelroute.ViewModels;

namespace travelroute
{
    public partial class Home : PhoneApplicationPage
    {
        private bool loadedFirstTime = true;
        public Home()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.HomeViewModel;

            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RefreshPopularRouteItems();

            if (AzureDBM.isUserLoggedIn)
            {
                RefreshActiveRouteItems();

                if (!App.HomeViewModel.IsDataLoaded)
                {
                    App.HomeViewModel.LoadData();
                }
            }

            else if(!AzureDBM.isUserLoggedIn  && loadedFirstTime) 
            {
                HomePanorama.Items.RemoveAt(1);
                HomePanorama.Items.RemoveAt(2);
                HomePanorama.Items.RemoveAt(2);

                this.ApplicationBar.IsVisible = false;
                loadedFirstTime = false;
            }

            else
            {
                this.ApplicationBar.IsVisible = false;
            }

            //Rutas Populares de ejemplo, Just In Case
            /*
            Route r1 = new Route();
            r1.CopiedNumber = 0;
            r1.Description = "Ruta por el Parque Nacional Torres del Paine";
            r1.Duration = 7;
            r1.IsPopular = true;
            r1.IsShared = true;
            r1.Name = "Circuito W Torres del Paine";
            r1.OwnerId = "Anonymous";
            r1.Place = "Parque Nacional Torres del Paine, XII Región";
            r1.RoutePicture = "http://travelroutestorage.blob.core.windows.net/routecoverimages/04107852-d5e0-4695-826b-b3883fc8f1dd.jpg";
            r1.Status = "active";
            r1.Price = 326910;

            Route r2 = new Route();
            r2.CopiedNumber = 0;
            r2.Description = "Explorando el Bosque";
            r2.Duration = 3;
            r2.IsPopular = true;
            r2.IsShared = true;
            r2.Name = "Explorando el Bosque";
            r2.OwnerId = "Anonymous";
            r2.Place = "Parque Nacional Conguillio, IX Región";
            r2.RoutePicture = "http://travelroutestorage.blob.core.windows.net/routecoverimages/cb059430-95d7-4995-acd9-c0cec7dc6eb9.jpg";
            r2.Status = "active";
            r2.Price = 123790;

            Route r3 = new Route();
            r3.CopiedNumber = 0;
            r3.Description = "Recorrido en Kayak";
            r3.Duration = 12;
            r3.IsPopular = true;
            r3.IsShared = true;
            r3.Name = "Recorrido en Kayak";
            r3.OwnerId = "Anonymous";
            r3.Place = "Lago LLanquihue, X Región";
            r3.RoutePicture = "http://travelroutestorage.blob.core.windows.net/routecoverimages/231595d4-a1dd-485a-b0f5-3a05b301d25c.jpg";
            r3.Status = "active";
            r3.Price = 97340;

            AzureDBM.InsertRoute(r1, null);
            AzureDBM.InsertRoute(r2, null);
            AzureDBM.InsertRoute(r3, null);
            */
        }

        // Back Button pressed: notify MainPage so it can exit application
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            base.OnBackKeyPress(e);
            if (NavigationService.CanGoBack)
            {
                while (NavigationService.RemoveBackEntry() != null)
                {
                    NavigationService.RemoveBackEntry();
                }
            }
        }

        private async void RefreshActiveRouteItems()
        {
            // This code refreshes the entries in the "rutas activas" view querying the Ruta table.
            // The query excludes Rutas that do now belown to the current user
            try
            {
                AzureDBM.activeRouteItems = await AzureDBM.routeTable
                    .Where(ruta => ruta.OwnerId == App.MobileService.CurrentUser.UserId)
                    .ToCollectionAsync();

                App.HomeViewModel.ActiveRouteList.Clear();

                foreach (Route r in AzureDBM.activeRouteItems)
                {
                    App.HomeViewModel.ActiveRouteList.Add(new RouteViewModel() { Image = new BitmapImage(new Uri(r.RoutePicture, UriKind.Absolute)), Name = r.Name, Duration = "0", Price = "0" });
                }
            }
            catch (MobileServiceInvalidOperationException e)
            {
                MessageBox.Show(e.Message, "Error loading items", MessageBoxButton.OK);
            }

            //ListItems.ItemsSource = items;
            
        }

        private async void RefreshPopularRouteItems()
        {
            // This code refreshes the entries in the "rutas populares" view querying the Ruta table.
            try
            {
                AzureDBM.popularRouteItems = await AzureDBM.routeTable
                    .Where(ruta => ruta.IsPopular == true)
                    .ToCollectionAsync();

                App.HomeViewModel.PopularRouteList.Clear();

                foreach (Route r in AzureDBM.popularRouteItems)
                {
                    App.HomeViewModel.PopularRouteList.Add(new RouteViewModel() { Image = new BitmapImage(new Uri(r.RoutePicture, UriKind.Absolute)), Name = r.Name, Duration = r.Duration.ToString(), Price = r.Price.ToString(), Place = r.Place, Owner = "Por Ignacio Carmach", Star1 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star2 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star3 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star4 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star5 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)) });
                }
            }
            catch (MobileServiceInvalidOperationException e)
            {
                MessageBox.Show(e.Message, "Error loading items", MessageBoxButton.OK);
            }

            //ListItems.ItemsSource = items;

        }

        private void HomePanorama_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
            
            if (tag.Equals("populares"))
            {
                if (this.ApplicationBar.Buttons.Count == 4)
                {
                    //Removes buttons from the app bar. It is always removing from the index 0 because the list gets
                    //shorter and the second item is now the first item.
                    this.ApplicationBar.Buttons.RemoveAt(0);
                    this.ApplicationBar.Buttons.RemoveAt(0);
                    this.ApplicationBar.Buttons.RemoveAt(0);

                    //Adds back the copy button
                    ApplicationBarIconButton button1 = new ApplicationBarIconButton();
                    button1.IconUri = new Uri("Assets/Icons/copy.png", UriKind.Relative);
                    button1.Text = "copiar";
                    this.ApplicationBar.Buttons.Insert(0, button1);
                    //button1.Click += new EventHandler(button1_Click);
                }
            }

            else if (tag.Equals("rutas"))
            {
                //Adds 3 buttons acconrding to this section
                this.ApplicationBar.Buttons.RemoveAt(0);

                ApplicationBarIconButton newButton = new ApplicationBarIconButton();
                newButton.IconUri = new Uri("Assets/Icons/add.png", UriKind.Relative);
                newButton.Text = "agregar";
                this.ApplicationBar.Buttons.Insert(0, newButton);
                newButton.Click += new EventHandler(newButton_Click);

                ApplicationBarIconButton button2 = new ApplicationBarIconButton();
                button2.IconUri = new Uri("Assets/Icons/edit.png", UriKind.Relative);
                button2.Text = "editar";
                this.ApplicationBar.Buttons.Insert(1, button2);
                //button2.Click += new EventHandler(button2_Click);

                ApplicationBarIconButton button3 = new ApplicationBarIconButton();
                button3.IconUri = new Uri("Assets/Icons/delete.png", UriKind.Relative);
                button3.Text = "eliminar";
                this.ApplicationBar.Buttons.Insert(2, button3);
                //button3.Click += new EventHandler(button3_Click);
            }

            else if (tag.Equals("buscar"))
            {
                //Removes buttons from the app bar. It is always removing from the index 0 because the list gets
                //shorter and the second item is now the first item.
                if (this.ApplicationBar.Buttons.Count == 4)
                {
                    this.ApplicationBar.Buttons.RemoveAt(0);
                    this.ApplicationBar.Buttons.RemoveAt(0);
                    this.ApplicationBar.Buttons.RemoveAt(0);

                    ApplicationBarIconButton button1 = new ApplicationBarIconButton();
                    button1.IconUri = new Uri("Assets/Icons/copy.png", UriKind.Relative);
                    button1.Text = "copiar";
                    this.ApplicationBar.Buttons.Insert(0, button1);
                    //button1.Click += new EventHandler(button1_Click);
                }
                
            }

            else if (tag.Equals("perfil"))
            {

            }

            else if (tag.Equals("tienda"))
            {

            }
           
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/NewRoute.xaml", UriKind.Relative));
        }

        private void signOutButton_Click(object sender, RoutedEventArgs e)
        {
            //If there is an authenticated user then logout
            AzureDBM.SignOut();
            NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
        }

        private void popularLLS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AzureDBM.selectedRoute = AzureDBM.popularRouteItems[((LongListSelector)sender).ItemsSource.IndexOf(((LongListSelector)sender).SelectedItem)];
            NavigationService.Navigate(new Uri("/ViewRoute.xaml", UriKind.Relative));
        }

        private void activeLLS_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AzureDBM.selectedRoute = AzureDBM.activeRouteItems[((LongListSelector)sender).ItemsSource.IndexOf(((LongListSelector)sender).SelectedItem)];
            NavigationService.Navigate(new Uri("/EditRoute.xaml", UriKind.Relative));
        }

    }
}
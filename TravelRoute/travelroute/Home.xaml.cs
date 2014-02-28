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

            RefreshPerfilData();
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

        private void RefreshPerfilData()
        {
            try
            {

                if (AzureDBM.isUserLoggedIn == true && Login.variableDePasada == 0)
                {

                    imagePerfil.Source = new BitmapImage(new Uri("http://graph.facebook.com/" + App.MobileService.CurrentUser.UserId.Split(':')[1] + "/picture?type=large", UriKind.Absolute));
                    globalName.Text = AzureDBM.usuarioGlobal;
                }
                else
                {
                    if (AzureDBM.isUserLoggedIn == true && Login.variableDePasada == 5)
                    {
                        imagePerfil.Source = new BitmapImage(new Uri(NewUser.URLProfilePicture, UriKind.Absolute));
                        globalName.Text = NewUser.name;
                        pointUser.Text = "Puntos Travel Route: " + AzureDBM.puntosGlobal;
                    }


                }
                if (AzureDBM.puntosGlobal == null || AzureDBM.puntosGlobal == "0")
                {
                    pointUser.Text = "Puntos Travel Route: Usted aún no posee puntos";

                }
                else
                {
                    pointUser.Text = "Puntos Travel Route: " + AzureDBM.puntosGlobal;

                }
            }
            catch
            {

                MessageBox.Show("Error loading items");

            }

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

        private void shareButton_Click(object sender, EventArgs e)
        {

            TextBox Comentar = new TextBox();

            Comentar.Visibility = Visibility.Visible;
            string descripcion = Comentar.Text;
            string picture = "";

            facebookClass.PublishStory(descripcion, picture);

            NavigationService.Navigate(new Uri("/Home.xaml", UriKind.Relative));
        }

        private void busqueda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AzureDBM.selectedRoute = AzureDBM.searchItems[((LongListSelector)sender).ItemsSource.IndexOf(((LongListSelector)sender).SelectedItem)];
            NavigationService.Navigate(new Uri("/ViewRoute.xaml", UriKind.Relative));
        }

        private async void searchButton_Click(object sender, RoutedEventArgs e)
        {
            string tag = searchText.Text;
            try
            {
                AzureDBM.tagItems = await AzureDBM.tagTable
                    .Where(tag2 => tag2.TagNom == tag)
                    .ToCollectionAsync();
                
                App.HomeViewModel.SearchList.Clear();

                foreach (Tag t in AzureDBM.tagItems)
                {
                    AzureDBM.searchItems = await AzureDBM.routeTable
                        .Where(ruta => ruta.Id == t.RouteId)
                        .ToCollectionAsync();
                    foreach (Route r in AzureDBM.searchItems)
                    {
                        App.HomeViewModel.SearchList.Add(new RouteViewModel() { Image = new BitmapImage(new Uri(r.RoutePicture, UriKind.Absolute)), Name = r.Name, Duration = "0", Price = "0" });

                    }

                }
            }

            catch
            {
                MessageBox.Show("Error loading items");
            }
        }
    }
}
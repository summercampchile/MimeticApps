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
using System.Windows.Media.Imaging;
using travelroute.ViewModels;

namespace travelroute
{
    public partial class Home : PhoneApplicationPage
    {
        public Home()
        {
            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            DataContext = App.HomeViewModel;

            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RefreshRutaItems();

            if (!App.HomeViewModel.IsDataLoaded)
            {
                App.HomeViewModel.LoadData();
            }

        }
        
        private async void RefreshRutaItems()
        {
            // This code refreshes the entries in the "rutas activas" view querying the Ruta table.
            // The query excludes Rutas that do now belown to the current user
            try
            {
                AzureDBM.routeItems = await AzureDBM.routeTable
                    .Where(ruta => ruta.OwnerId == App.MobileService.CurrentUser.UserId)
                    .ToCollectionAsync();

                App.HomeViewModel.ActiveRouteList.Clear();

                foreach (Route r in AzureDBM.routeItems)
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

        private void popularGrid_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/RouteView.xaml", UriKind.Relative));
        }

        private void activeGrid_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/RouteEdit.xaml", UriKind.Relative));
        }

    }
}
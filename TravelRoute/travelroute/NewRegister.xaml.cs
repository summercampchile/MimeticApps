using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using System.IO;
using travelroute.DBClasses;
using System.Windows.Media;

namespace travelroute
{
    public partial class NewRegister : PhoneApplicationPage
    {
        // Using a stream reference to upload the image to blob storage.
        Stream imageStream = null;
        int appreciation = 0;

        public NewRegister()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (AzureDBM.selectedRegisterType.Equals("POI"))
            {
                registerType.Text = "Punto de Interés";
            }

            else if (AzureDBM.selectedRegisterType.Equals("Sleep"))
            {
                registerType.Text = "Alojamiento";
            }

            else if (AzureDBM.selectedRegisterType.Equals("Restaurant"))
            {
                registerType.Text = "Alimentación";
            }

            else if (AzureDBM.selectedRegisterType.Equals("Transport"))
            {
                registerType.Text = "Transporte";
            }

            else if (AzureDBM.selectedRegisterType.Equals("Picture"))
            {
                registerType.Text = "Fotografía";
            }

            else if (AzureDBM.selectedRegisterType.Equals("Comment"))
            {
                registerType.Text = "Comentario";
            }
        }


        private void registerDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Scrolls to the end of the ScrollViewer so the user can see what he is typing.
            registerDescriptionScrollViewer.ScrollToVerticalOffset(200);
        }

        private void registerImage_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //PhotoChooserTask gives you access to the gallery on the phone (and optionally to the camera)
            //to let the user select or take a picture.
            PhotoChooserTask photo = new PhotoChooserTask();
            photo.ShowCamera = true;

            //Shows the gallery app
            photo.Show();

            //When the user selects a picture, he is taken back to the app and the photo.Completed event is triggered.
            photo.Completed += photo_Completed;
        }

        private void photo_Completed(object sender, PhotoResult e)
        {
            //If the user really selected a picture (didn't press back)
            if (e != null && e.ChosenPhoto != null)
            {
                //Get the picture selected and set it as the source to the routeImage
                BitmapImage image = new BitmapImage();
                image.SetSource(e.ChosenPhoto);
                registerImage.Source = image;

                imageStream = e.ChosenPhoto;
            }


        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Register register = null;

            if (registerExpenses.Text.Equals("") && appreciation == 0)
            {
                //Creates the new Register and then it sends it to Azure so we can store the route data.
                register = new Register { Appreciation = 1, CreatedByCurrentUser = true, Description = registerDescription.Text, Expenses = 0, Type = AzureDBM.selectedRegisterType, Latitude = AzureDBM.selectedRegisterLat, Longitude = AzureDBM.selectedRegisterLon, Name = registerName.Text, RouteId = AzureDBM.selectedRoute.Id };
            }

            else if (registerExpenses.Text.Equals("") && appreciation != 0)
            {
                //Creates the new Register and then it sends it to Azure so we can store the route data.
                register = new Register { Appreciation = appreciation, CreatedByCurrentUser = true, Description = registerDescription.Text, Expenses = 0, Type = AzureDBM.selectedRegisterType, Latitude = AzureDBM.selectedRegisterLat, Longitude = AzureDBM.selectedRegisterLon, Name = registerName.Text, RouteId = AzureDBM.selectedRoute.Id };
            }

            else if (!registerExpenses.Text.Equals("") && appreciation == 0)
            {
                //Creates the new Register and then it sends it to Azure so we can store the route data.
                register = new Register { Appreciation = 1, CreatedByCurrentUser = true, Description = registerDescription.Text, Expenses = Convert.ToInt32(registerExpenses.Text), Type = AzureDBM.selectedRegisterType, Latitude = AzureDBM.selectedRegisterLat, Longitude = AzureDBM.selectedRegisterLon, Name = registerName.Text, RouteId = AzureDBM.selectedRoute.Id };
            }
            else
            {
                //Creates the new Register and then it sends it to Azure so we can store the route data.
                register = new Register { Appreciation = appreciation, CreatedByCurrentUser = true, Description = registerDescription.Text, Expenses = Convert.ToInt32(registerExpenses.Text), Type = AzureDBM.selectedRegisterType, Latitude = AzureDBM.selectedRegisterLat, Longitude = AzureDBM.selectedRegisterLon, Name = registerName.Text, RouteId = AzureDBM.selectedRoute.Id };
            }

            AzureDBM.InsertRegister(register);

            NavigationService.Navigate(new Uri("/EditRoute.xaml", UriKind.Relative));
        }

        private void muyMalaImage_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            appreciation = 1;
            muyMalaImage.Source = new BitmapImage(new Uri("/Assets/mms.png", UriKind.Relative));
            malaImage.Source = new BitmapImage(new Uri("/Assets/mns.png", UriKind.Relative));
            regularImage.Source = new BitmapImage(new Uri("/Assets/rns.png", UriKind.Relative));
            buenaImage.Source = new BitmapImage(new Uri("/Assets/bns.png", UriKind.Relative));
            muyBuenaImage.Source = new BitmapImage(new Uri("/Assets/mbns.png", UriKind.Relative));
        }

        private void malaImage_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            appreciation = 2;
            muyMalaImage.Source = new BitmapImage(new Uri("/Assets/mmns.png", UriKind.Relative));
            malaImage.Source = new BitmapImage(new Uri("/Assets/ms.png", UriKind.Relative));
            regularImage.Source = new BitmapImage(new Uri("/Assets/rns.png", UriKind.Relative));
            buenaImage.Source = new BitmapImage(new Uri("/Assets/bns.png", UriKind.Relative));
            muyBuenaImage.Source = new BitmapImage(new Uri("/Assets/mbns.png", UriKind.Relative));
        }

        private void regularImage_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            appreciation = 3;
            muyMalaImage.Source = new BitmapImage(new Uri("/Assets/mmns.png", UriKind.Relative));
            malaImage.Source = new BitmapImage(new Uri("/Assets/mns.png", UriKind.Relative));
            regularImage.Source = new BitmapImage(new Uri("/Assets/rs.png", UriKind.Relative));
            buenaImage.Source = new BitmapImage(new Uri("/Assets/bns.png", UriKind.Relative));
            muyBuenaImage.Source = new BitmapImage(new Uri("/Assets/mbns.png", UriKind.Relative));
        }

        private void buenaImage_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            appreciation = 4;
            muyMalaImage.Source = new BitmapImage(new Uri("/Assets/mmns.png", UriKind.Relative));
            malaImage.Source = new BitmapImage(new Uri("/Assets/mns.png", UriKind.Relative));
            regularImage.Source = new BitmapImage(new Uri("/Assets/rns.png", UriKind.Relative));
            buenaImage.Source = new BitmapImage(new Uri("/Assets/bs.png", UriKind.Relative));
            muyBuenaImage.Source = new BitmapImage(new Uri("/Assets/mbns.png", UriKind.Relative));
        }

        private void muyBuenaImage_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            appreciation = 5;
            muyMalaImage.Source = new BitmapImage(new Uri("/Assets/mmns.png", UriKind.Relative));
            malaImage.Source = new BitmapImage(new Uri("/Assets/mns.png", UriKind.Relative));
            regularImage.Source = new BitmapImage(new Uri("/Assets/rns.png", UriKind.Relative));
            buenaImage.Source = new BitmapImage(new Uri("/Assets/bns.png", UriKind.Relative));
            muyBuenaImage.Source = new BitmapImage(new Uri("/Assets/mbs.png", UriKind.Relative));
        }

        
    }
}
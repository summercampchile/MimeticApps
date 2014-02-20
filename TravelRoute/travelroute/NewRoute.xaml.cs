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

namespace travelroute
{
    public partial class NewRoute : PhoneApplicationPage
    {
        // Using a stream reference to upload the image to blob storage.
        Stream imageStream = null;

        public NewRoute()
        {
            InitializeComponent();
        }

        private void routeImage_Tap(object sender, System.Windows.Input.GestureEventArgs e)
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
                routeImage.Source = image;

                imageStream = e.ChosenPhoto;
            }

            
        }

        private void routeDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Scrolls to the end of the ScrollViewer so the user can see what he is typing.
            routeDescriptionScrollViewer.ScrollToVerticalOffset(200);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            //Creates the new Route and then it sends it to Azure so we can store the route data.
            var route = new Route { Description = routeDescription.Text, Duration = 0, Name = routeName.Text, OwnerId = App.MobileService.CurrentUser.UserId, CopiedNumber = 0, Status = "planned", IsPopular = false, IsShared = false };
            AzureDBM.InsertRoute(route, imageStream);

            NavigationService.Navigate(new Uri("/Home.xaml", UriKind.Relative));
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            //Creates the new Route and then it sends it to Azure so we can store the route data.
            var route = new Route { Description = routeDescription.Text, Duration = 0, Name = routeName.Text, OwnerId = App.MobileService.CurrentUser.UserId, CopiedNumber = 0, Status = "active", IsPopular = false, IsShared = false };
            AzureDBM.InsertRoute(route, imageStream);

            NavigationService.Navigate(new Uri("/Home.xaml", UriKind.Relative));
        }
    }
}
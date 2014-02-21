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

        public NewRegister()
        {
            InitializeComponent();
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
            //Creates the new Register and then it sends it to Azure so we can store the route data.
            var register = new Register { Appreciation = Convert.ToInt32(registerAppreciation.Value), CreatedByCurrentUser = true, Description = registerDescription.Text, Expenses = Convert.ToInt32(registerExpenses.Text), Type = AzureDBM.selectedRegisterType, Latitude = AzureDBM.selectedRegisterLat, Longitude = AzureDBM.selectedRegisterLon, Name = registerName.Text, RouteId = AzureDBM.selectedRoute.Id };
            AzureDBM.InsertRegister(register);

            NavigationService.Navigate(new Uri("/EditRoute.xaml", UriKind.Relative));
        }

        private void registerAppreciation_ValueChanged(object sender, EventArgs e)
        {
            /*
            if(((Rating)sender).Value == 1)
            {
                Style style = new Style(typeof(RatingItem));
                style.Setters.Add(new Setter(RatingItem.BackgroundProperty, new SolidColorBrush(Colors.Red)));
                ((Rating)sender).Style = style;
            }

            else if (((Rating)sender).Value == 2)
            {
                Style style = new Style(typeof(RatingItem));
                style.Setters.Add(new Setter(RatingItem.BackgroundProperty, new SolidColorBrush(Colors.Orange)));
                ((Rating)sender).Style = style;
            }

            else if (((Rating)sender).Value == 3)
            {
                Style style = ((Rating)sender).Style;
                //style.Setters.Clear();
                style.Setters.Add(new Setter(RatingItem.BackgroundProperty, new SolidColorBrush(Colors.Yellow)));
                ((Rating)sender).Style = style;
            }

            else if (((Rating)sender).Value == 4)
            {
                Style style = new Style(typeof(RatingItem));
                style.Setters.Add(new Setter(RatingItem.BackgroundProperty, new SolidColorBrush(Colors.Purple)));
                ((Rating)sender).Style = style;
            }

            else if (((Rating)sender).Value == 5)
            {
                Style style = new Style(typeof(RatingItem));
                style.Setters.Add(new Setter(RatingItem.BackgroundProperty, new SolidColorBrush(Colors.Green)));
                ((Rating)sender).Style = style;
            }
             */
        }
    }
}
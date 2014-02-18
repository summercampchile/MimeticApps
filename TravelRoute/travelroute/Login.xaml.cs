
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.WindowsAzure.MobileServices;

namespace travelroute
{
    public partial class Login : PhoneApplicationPage
    {

        public Login()
        {
            InitializeComponent();
        }
        private async void facebookLoginButton_Click(object sender, RoutedEventArgs e)
        {
            //Awaits for the facebook login and the redirects the user to the Home layout
            await AzureDBM.AuthenticateWithFacebook();
            NavigationService.Navigate(new Uri("/Home.xaml", UriKind.Relative));
            /*
            if(AzureDBM.userItems == null)
            {
                NavigationService.Navigate(new Uri("/NewUser.xaml", UriKind.Relative));
            }

            else
            {
                NavigationService.Navigate(new Uri("/Home.xaml", UriKind.Relative));
            }
            */
        }

        private async void twitterLoginButton_Click(object sender, RoutedEventArgs e)
        {
            //Awaits for the twitter login and the redirects the user to the Home layout
            await AzureDBM.AuthenticateWithTwitter();
            NavigationService.Navigate(new Uri("/Home.xaml", UriKind.Relative));
        }
        
    }
}
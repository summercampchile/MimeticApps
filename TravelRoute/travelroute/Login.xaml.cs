
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

        public static int variableDePasada = 0;

        public Login()
        {
            InitializeComponent();
        }
        private async void facebookLoginButton_Click(object sender, RoutedEventArgs e)
        {
            //Awaits for the facebook login and the redirects the user to the Home layout
            await AzureDBM.AuthenticateWithFacebook();

            //Old code, go to Home
            if (AzureDBM.variableEstado == 10)
            {
                NavigationService.Navigate(new Uri("/Home.xaml", UriKind.Relative));

            }
            else
            {
                if (AzureDBM.variableEstado == 11)
                {

                    NavigationService.Navigate(new Uri("/NewUser.xaml", UriKind.Relative));
                }
                else
                {

                    MessageBox.Show("Ha ocurrido un error, por favor intente más tarde");
                }
            }
        }

        private async void twitterLoginButton_Click(object sender, RoutedEventArgs e)
        {
            //Awaits for the twitter login and the redirects the user to the Home layout
            await AzureDBM.AuthenticateWithTwitter();
            //NavigationService.Navigate(new Uri("/Home.xaml", UriKind.Relative));
        }

        private void skipLoginButton_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Home.xaml", UriKind.Relative));
        }

    }
}
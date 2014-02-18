using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace travelroute
{
    public partial class NewUser : PhoneApplicationPage
    {
        public NewUser()
        {
            InitializeComponent();
        }

        private void Combo1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void facebookDataButton_SessionStateChanged(object sender, Facebook.Client.Controls.SessionStateChangedEventArgs e)
        {

        }
    }
}
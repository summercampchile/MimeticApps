using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Media.Imaging;
using travelroute.DBClasses;
using System.Windows.Threading;

namespace travelroute
{
    public partial class NewUser : PhoneApplicationPage
    {
        public static string help;
        public static long ayuda2;

        public static string name;
        public static string location;
        public static string birthday;
        public static string URLProfilePicture;
        public static string gender;
        public NewUser()
        {
            InitializeComponent();
        }

        private void Combo1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //NameUser
        private string BuildUserInfoDisplay(Facebook.Client.GraphUser user)
        {
            var userInfo = new System.IO.StringWriter();

            // Example: typed access (name)
            // - no special permissions required
            userInfo.WriteLine(string.Format("{0}", user.Name));
            userInfo.WriteLine();

            return userInfo.ToString();
        }

        //Birthday
        private string BuildUserInfoDisplay2(Facebook.Client.GraphUser user)
        {

            var userInfo = new System.IO.StringWriter();

            userInfo.WriteLine(string.Format("{0}", user.Birthday));
            userInfo.WriteLine();

            return userInfo.ToString();
        }

        //Location
        private string BuildUserInfoDisplay3(Facebook.Client.GraphUser user)
        {
            var userInfo = new System.IO.StringWriter();

            userInfo.WriteLine(string.Format("{0}", user.Location.City));
            userInfo.WriteLine();

            return userInfo.ToString();
        }


        private async System.Threading.Tasks.Task RetriveUserInfo()
        {
            var client = new Facebook.FacebookClient(this.facebookDataButton.CurrentSession.AccessToken);

            dynamic result = await client.GetTaskAsync("me");
            var currentUser = new Facebook.Client.GraphUser(result);

            this.userInfo.Text = this.BuildUserInfoDisplay(currentUser);
            
            //se aprovecha el facebook.client.coontrols.userInfoChangedEventArgs
            location = this.BuildUserInfoDisplay3(currentUser);
            birthday = this.BuildUserInfoDisplay2(currentUser);

        }


        private void OnUserInfoChanged(object sender, Facebook.Client.Controls.UserInfoChangedEventArgs e)
        {
            this.userInfo.Text = this.BuildUserInfoDisplay(e.User);


            //string help = BuildUserInfoDisplay2(e.User);

            

            //FIN ->se aprovecha el facebook.client.coontrols.userInfoChangedEventArgs
        }



        private async void facebookDataButton_SessionStateChanged(object sender, Facebook.Client.Controls.SessionStateChangedEventArgs e)
        {
            if (e.SessionState == Facebook.Client.Controls.FacebookSessionState.Opened)
            {

                //string[] ayuda3 = help.Split('/');

                // dd/mm/yyyy
                //this.birDate.Value = new DateTime(Convert.ToInt16(ayuda3[2]), Convert.ToInt16(ayuda3[1]), Convert.ToInt16(ayuda3[0]));


                //Obtener nombre del usuario de facebook

                this.userInfo.Visibility = Visibility.Visible;

                //Obtener fecha de nacimiento usuario de facebook
                this.birthDate.Visibility = Visibility.Visible;

                //Obtener url de la foto de perfil para guardarla en azure
                //ImageUser.Source = new BitmapImage(new Uri("http://graph.facebook.com/" + App.MobileService.CurrentUser.UserId.Split(':')[1] + "/picture?type=large", UriKind.Absolute));

                await this.RetriveUserInfo();
                //this.shareButton.Visibility = Visibility.Visible;

            }
            else if (e.SessionState == Facebook.Client.Controls.FacebookSessionState.Closed)
            {
                //this.shareButton.Visibility = Visibility.Collapsed;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            User u = new User();
            u.Birthdate = birthDate.Value.ToString();
            u.ProfilePicture = "http://graph.facebook.com/" + App.MobileService.CurrentUser.UserId.Split(':')[1] + "/picture?type=large";
            u.Gender = (this.List1.Items[List1.SelectedIndex] as ListPickerItem).Content.ToString();
            u.Name = userInfo.Text;;
            u.FacebookId = App.MobileService.CurrentUser.UserId.Split(':')[1];
            u.Location = location;
            u.Points = "0";

            AzureDBM.InsertUser(u);

            NavigationService.Navigate(new Uri("/Home.xaml", UriKind.Relative));
        }


    }
}
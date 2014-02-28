using Facebook.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using travelroute.DBClasses;
using travelroute.Resources;
using travelroute.ViewModels;

namespace travelroute
{
    public class facebookClass
    {
        FacebookSession session;
        string message = String.Empty;

        public static async void PublishStory(string descriptionPost, string picturePost)
        {

            FacebookSession session;
            string message = String.Empty;

            try
            {
                session = await App.FacebookSessionClient.LoginAsync("publish_stream");
                App.AccessToken = session.AccessToken;

            }

            catch (InvalidOperationException e)
            {
                message = "Login failed! Exception details: " + e.Message;
                MessageBox.Show(message);
                return;
            }


            var facebookClient = new Facebook.FacebookClient(App.AccessToken);

            var postParams = new
            {
                name = "Travel route",
                caption = "Estoy usando travel route",
                description = "Yo estoy usando travel route, no esperes más y comienza a planificar tu viaje",
                link = "https://www.facebook.com/TravelRouteSummerCamp",
                picture = "https://fbcdn-sphotos-e-a.akamaihd.net/hphotos-ak-ash3/t1/19…"
          };

            try
            {
                MessageBoxResult respuesta = MessageBox.Show("¿Desea publicar?", "Alerta", MessageBoxButton.OKCancel);

                if (respuesta == MessageBoxResult.OK)
                {
                    dynamic fbPostTaskResult = await facebookClient.PostTaskAsync("/me/feed", postParams);
                    var result = (IDictionary<string, object>)fbPostTaskResult;

                }

                else
                {


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception during post: " + ex.Message, "Error", MessageBoxButton.OK);
            }
        }


    }
}

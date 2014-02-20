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
    public partial class ViewRoute : PhoneApplicationPage
    {
        public ViewRoute()
        {
            InitializeComponent();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            routeName.Text = AzureDBM.selectedRoute.Name;
        }

        private void RouteViewPanorama_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

            if (tag.Equals("route"))
            {
                if (this.ApplicationBar.Buttons.Count == 2)
                {
                    //Removes buttons from the app bar. It is always removing from the index 0 because the list gets
                    //shorter and the second item is now the first item.
                    this.ApplicationBar.Buttons.RemoveAt(0);
                    this.ApplicationBar.Buttons.RemoveAt(0);

                    //Adds back the timeline button
                    ApplicationBarIconButton button1 = new ApplicationBarIconButton();
                    button1.IconUri = new Uri("Assets/Icons/clock.png", UriKind.Relative);
                    button1.Text = "timeline";
                    this.ApplicationBar.Buttons.Insert(0, button1);
                    //button1.Click += new EventHandler(button1_Click);

                    //Adds back the share button
                    ApplicationBarIconButton button2 = new ApplicationBarIconButton();
                    button2.IconUri = new Uri("Assets/Icons/share.png", UriKind.Relative);
                    button2.Text = "compartir";
                    this.ApplicationBar.Buttons.Insert(1, button2);
                    //button2.Click += new EventHandler(button2_Click);
                }

                else if (this.ApplicationBar.Buttons.Count == 1)
                {
                    //Adds back the timeline button
                    ApplicationBarIconButton button1 = new ApplicationBarIconButton();
                    button1.IconUri = new Uri("Assets/Icons/clock.png", UriKind.Relative);
                    button1.Text = "timeline";
                    this.ApplicationBar.Buttons.Insert(0, button1);
                    //button1.Click += new EventHandler(button1_Click);
                }
            }

            else if (tag.Equals("stats"))
            {
                //Removes buttons from the app bar. It is always removing from the index 0 because the list gets
                //shorter and the second item is now the first item.
                this.ApplicationBar.Buttons.RemoveAt(0);
                this.ApplicationBar.Buttons.RemoveAt(0);

                //Adds back the share button
                ApplicationBarIconButton button2 = new ApplicationBarIconButton();
                button2.IconUri = new Uri("Assets/Icons/share.png", UriKind.Relative);
                button2.Text = "compartir";
                this.ApplicationBar.Buttons.Insert(0, button2);
                //button2.Click += new EventHandler(button2_Click);


            }

            else if (tag.Equals("comments"))
            {
                if (this.ApplicationBar.Buttons.Count == 2)
                {
                    //Removes buttons from the app bar. It is always removing from the index 0 because the list gets
                    //shorter and the second item is now the first item.
                    this.ApplicationBar.Buttons.RemoveAt(0);
                    this.ApplicationBar.Buttons.RemoveAt(0);

                    //Adds back the check button
                    ApplicationBarIconButton button1 = new ApplicationBarIconButton();
                    button1.IconUri = new Uri("Assets/Icons/check.png", UriKind.Relative);
                    button1.Text = "enviar";
                    this.ApplicationBar.Buttons.Insert(0, button1);
                    //button1.Click += new EventHandler(button1_Click);

                    //Adds back the cancel button
                    ApplicationBarIconButton button2 = new ApplicationBarIconButton();
                    button2.IconUri = new Uri("Assets/Icons/cancel.png", UriKind.Relative);
                    button2.Text = "cancelar";
                    this.ApplicationBar.Buttons.Insert(1, button2);
                    //button2.Click += new EventHandler(button2_Click);
                }

                else if (this.ApplicationBar.Buttons.Count == 1)
                {
                    //Removes buttons from the app bar. It is always removing from the index 0 because the list gets
                    //shorter and the second item is now the first item.
                    this.ApplicationBar.Buttons.RemoveAt(0);


                    //Adds back the check button
                    ApplicationBarIconButton button1 = new ApplicationBarIconButton();
                    button1.IconUri = new Uri("Assets/Icons/check.png", UriKind.Relative);
                    button1.Text = "enviar";
                    this.ApplicationBar.Buttons.Insert(0, button1);
                    //button1.Click += new EventHandler(button1_Click);

                    //Adds back the cancel button
                    ApplicationBarIconButton button2 = new ApplicationBarIconButton();
                    button2.IconUri = new Uri("Assets/Icons/cancel.png", UriKind.Relative);
                    button2.Text = "cancelar";
                    this.ApplicationBar.Buttons.Insert(1, button2);
                    //button2.Click += new EventHandler(button2_Click);
                }

            }
        }
    }
}
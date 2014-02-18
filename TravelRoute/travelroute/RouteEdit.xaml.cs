using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using System.Device.Location;
using System.Windows.Shapes;
using System.Windows.Media;
using Microsoft.Phone.Maps.Controls;

namespace travelroute
{
    public partial class RouteEdit : PhoneApplicationPage
    {
        Geolocator geolocator = null;
        Ellipse mapCircle = new Ellipse();
        MapOverlay myLocationOverlay = new MapOverlay();

        MapLayer myLocationLayer = new MapLayer();

        public RouteEdit()
        {
            InitializeComponent();

            // Create a small circle to mark the current location.

            mapCircle.Fill = new SolidColorBrush(Colors.Blue);
            mapCircle.Height = 20;
            mapCircle.Width = 20;
            mapCircle.Opacity = 50;

            registerGrid.Visibility = System.Windows.Visibility.Collapsed;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            geolocator = new Geolocator();
            geolocator.DesiredAccuracy = PositionAccuracy.High;
            geolocator.MovementThreshold = 1; // The units are meters.

            geolocator.PositionChanged += geolocator_PositionChanged;


            myLocationOverlay.Content = mapCircle;
            myLocationOverlay.PositionOrigin = new Point(0.5, 0.5);
            mapCircle.Visibility = System.Windows.Visibility.Collapsed;

            myLocationLayer.Add(myLocationOverlay);
            routeMap.Layers.Add(myLocationLayer);
        }

        private void geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            Dispatcher.BeginInvoke(() =>
            {
                double lat = args.Position.Coordinate.Latitude;
                double lon = args.Position.Coordinate.Longitude;

                GeoCoordinate gc = new GeoCoordinate(lat, lon);

                mapCircle.Visibility = System.Windows.Visibility.Visible;
                myLocationOverlay.GeoCoordinate = gc;

                routeMap.Center = gc;
                routeMap.ZoomLevel = 18;
            });
        }

        private void addRegisterButton_Click(object sender, EventArgs e)
        {
            if(registerGrid.Visibility == System.Windows.Visibility.Collapsed)
            {
                registerGrid.Visibility = System.Windows.Visibility.Visible;
            }

            else
            {
                registerGrid.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
    }
}
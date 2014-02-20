using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using travelroute.Resources;


namespace travelroute.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        public HomeViewModel()
        {
            this.PopularRouteList = new ObservableCollection<RouteViewModel>();
            this.ActiveRouteList = new ObservableCollection<RouteViewModel>();
            this.PlannedRouteList = new ObservableCollection<RouteViewModel>();
            this.EndedRouteList = new ObservableCollection<RouteViewModel>();
        }

        //Collections for RouteViewModel objects.
        public ObservableCollection<RouteViewModel> PopularRouteList { get; private set; }
        public ObservableCollection<RouteViewModel> ActiveRouteList { get; private set; }
        public ObservableCollection<RouteViewModel> PlannedRouteList { get; private set; }
        public ObservableCollection<RouteViewModel> EndedRouteList { get; private set; }


        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            //Popular routes to show
            /*
            this.PopularRouteList.Add(new RouteViewModel() { Image = new BitmapImage(new Uri("/Assets/populares-02.png", UriKind.Relative)), Name = "Circuito " + '"' + "W" + '"' + " Torres del Paine", Place = "Parque Nacional Torres del Paine, XII Región", Owner = "Por Camila Orellana", Duration = "7", Price = "326.910", Star1 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star2 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star3 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star4 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star5 = new BitmapImage(new Uri("/Assets/starEmpty.png", UriKind.Relative)) });
            this.PopularRouteList.Add(new RouteViewModel() { Image = new BitmapImage(new Uri("/Assets/populares-04.png", UriKind.Relative)), Name = "Explorando el Bosque", Place = "Parque Nacional Conguillio, IX Región", Owner = "Por Ignacio Carmach", Duration = "3", Price = "123.790", Star1 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star2 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star3 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star4 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star5 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)) });
            this.PopularRouteList.Add(new RouteViewModel() { Image = new BitmapImage(new Uri("/Assets/populares-03.png", UriKind.Relative)), Name = "Recorrido en Kayak", Place = "Lago Llanquihue, X Región", Owner = "Por Catalina Lagos", Duration = "12", Price = "97.340", Star1 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star2 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star3 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star4 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star5 = new BitmapImage(new Uri("/Assets/starEmpty.png", UriKind.Relative)) });
            this.PopularRouteList.Add(new RouteViewModel() { Image = new BitmapImage(new Uri("/Assets/populares-02.png", UriKind.Relative)), Name = "Circuito " + '"' + "W" + '"' + " Torres del Paine", Place = "Parque Nacional Torres del Paine, XII Región", Owner = "Por Camila Orellana", Duration = "7", Price = "326.910", Star1 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star2 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star3 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star4 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star5 = new BitmapImage(new Uri("/Assets/starEmpty.png", UriKind.Relative)) });
            this.PopularRouteList.Add(new RouteViewModel() { Image = new BitmapImage(new Uri("/Assets/populares-04.png", UriKind.Relative)), Name = "Explorando el Bosque", Place = "Parque Nacional Conguillio, IX Región", Owner = "Por Ignacio Carmach", Duration = "3", Price = "123.790", Star1 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star2 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star3 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star4 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star5 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)) });
            this.PopularRouteList.Add(new RouteViewModel() { Image = new BitmapImage(new Uri("/Assets/populares-03.png", UriKind.Relative)), Name = "Recorrido en Kayak", Place = "Lago Llanquihue, X Región", Owner = "Por Catalina Lagos", Duration = "12", Price = "97.340", Star1 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star2 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star3 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star4 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star5 = new BitmapImage(new Uri("/Assets/starEmpty.png", UriKind.Relative)) });
            this.PopularRouteList.Add(new RouteViewModel() { Image = new BitmapImage(new Uri("/Assets/populares-02.png", UriKind.Relative)), Name = "Circuito " + '"' + "W" + '"' + " Torres del Paine", Place = "Parque Nacional Torres del Paine, XII Región", Owner = "Por Camila Orellana", Duration = "7", Price = "326.910", Star1 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star2 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star3 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star4 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star5 = new BitmapImage(new Uri("/Assets/starEmpty.png", UriKind.Relative)) });
            this.PopularRouteList.Add(new RouteViewModel() { Image = new BitmapImage(new Uri("/Assets/populares-04.png", UriKind.Relative)), Name = "Explorando el Bosque", Place = "Parque Nacional Conguillio, IX Región", Owner = "Por Ignacio Carmach", Duration = "3", Price = "123.790", Star1 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star2 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star3 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star4 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star5 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)) });
            this.PopularRouteList.Add(new RouteViewModel() { Image = new BitmapImage(new Uri("/Assets/populares-03.png", UriKind.Relative)), Name = "Recorrido en Kayak", Place = "Lago Llanquihue, X Región", Owner = "Por Catalina Lagos", Duration = "12", Price = "97.340", Star1 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star2 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star3 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star4 = new BitmapImage(new Uri("/Assets/starFull.png", UriKind.Relative)), Star5 = new BitmapImage(new Uri("/Assets/starEmpty.png", UriKind.Relative)) });
            */
            //Active routes to show
            //this.ActiveRouteList.Add(new RouteViewModel() { Image = new BitmapImage(new Uri("/Assets/populares-03.png", UriKind.Relative)), Name = "San Pedro de Atacama", Duration = "5", Price = "249.735" });


            this.IsDataLoaded = true;
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

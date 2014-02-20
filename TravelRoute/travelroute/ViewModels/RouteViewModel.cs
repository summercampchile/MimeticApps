using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace travelroute.ViewModels
{
    public class RouteViewModel : INotifyPropertyChanged
    {
        private BitmapImage _image;

        public BitmapImage Image
        {
            get
            {
                return _image;
            }
            set
            {
                if (value != _image)
                {
                    _image = value;
                    NotifyPropertyChanged("Image");
                }
            }
        }

        //name, place, owner, duration, price, star1-5

        private string _name;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        private string _place;

        public string Place
        {
            get
            {
                return _place;
            }
            set
            {
                if (value != _place)
                {
                    _place = value;
                    NotifyPropertyChanged("Place");
                }
            }
        }

        private string _owner;

        public string Owner
        {
            get
            {
                return _owner;
            }
            set
            {
                if (value != _owner)
                {
                    _owner = value;
                    NotifyPropertyChanged("Owner");
                }
            }
        }

        private string _duration;

        public string Duration
        {
            get
            {
                return _duration;
            }
            set
            {
                if (value != _duration)
                {
                    _duration = value + " días";
                    NotifyPropertyChanged("Duration");
                }
            }
        }

        private string _price;

        public string Price
        {
            get
            {
                return _price;
            }
            set
            {
                if (value != _price && value.Equals("0") == false)
                {
                    _price = "$ " + value.Insert(value.Length-3, ".");
                    NotifyPropertyChanged("Price");
                }
            }
        }

        private BitmapImage _star1;

        public BitmapImage Star1
        {
            get
            {
                return _star1;
            }
            set
            {
                if (value != _star1)
                {
                    _star1 = value;
                    NotifyPropertyChanged("Star1");
                }
            }
        }

        private BitmapImage _star2;

        public BitmapImage Star2
        {
            get
            {
                return _star2;
            }
            set
            {
                if (value != _star2)
                {
                    _star2 = value;
                    NotifyPropertyChanged("Star2");
                }
            }
        }

        private BitmapImage _star3;

        public BitmapImage Star3
        {
            get
            {
                return _star3;
            }
            set
            {
                if (value != _star3)
                {
                    _star3 = value;
                    NotifyPropertyChanged("Star3");
                }
            }
        }

        private BitmapImage _star4;

        public BitmapImage Star4
        {
            get
            {
                return _star4;
            }
            set
            {
                if (value != _star4)
                {
                    _star4 = value;
                    NotifyPropertyChanged("Star4");
                }
            }
        }

        private BitmapImage _star5;

        public BitmapImage Star5
        {
            get
            {
                return _star5;
            }
            set
            {
                if (value != _star5)
                {
                    _star5 = value;
                    NotifyPropertyChanged("Star5");
                }
            }
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

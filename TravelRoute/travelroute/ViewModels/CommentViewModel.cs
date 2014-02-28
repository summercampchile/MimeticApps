using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace travelroute.ViewModels
{
    public class CommentViewModel : INotifyPropertyChanged
    {
        private string _comment;

        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    NotifyPropertyChanged("Comment");
                }
            }
        }

        private int _appreciation;

        public int Appreciation
        {
            get
            {
                return _appreciation;
            }
            set
            {
                if (value != _appreciation)
                {
                    _appreciation = value;
                    NotifyPropertyChanged("Appreciation");
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

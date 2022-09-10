using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp10.ViewModel
{
    public class FileImage
    {
        public string Path
        {
            get;set;
        }

    }
    public class DataFiles : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<FileImage> _ListImages = new ObservableCollection<FileImage>();

        public ObservableCollection<FileImage> ListImages
        {
            get => _ListImages;
            set
            {
                _ListImages = value;
                OnPropertyChanged();
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}

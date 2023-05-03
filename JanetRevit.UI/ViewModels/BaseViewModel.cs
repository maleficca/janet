using System.ComponentModel;
using JanetRevit.Core.Models;

namespace JanetRevit.UI.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private AddinDataProperties _dataProperties;
        public AddinDataProperties DataProperties
        {
            get { return _dataProperties; }
            set
            {
                _dataProperties = value;
                OnPropertyChanged("DataProperties");
            }
        }
        
        public BaseViewModel(AddinDataProperties addinDataProperties)
        {
            DataProperties = addinDataProperties;
        }

        public void OnPropertyChanged(string param)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(param));
        }
    }
}

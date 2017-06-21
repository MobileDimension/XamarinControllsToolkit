using System;
using System.ComponentModel;
using System.Windows.Input;

namespace testcontrols.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaizePropertyChanged(params string[] propertyNames)
        {
            foreach (var name in propertyNames)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}

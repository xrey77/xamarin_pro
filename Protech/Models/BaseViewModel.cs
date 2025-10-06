using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Protech.Models
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        string servicename;
        public string Servicename
        {
            set
            {
                servicename = value;
                onPropertyChanged();
            }
            get => servicename;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void onPropertyChanged([CallerMemberName] string propertyName= null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }


    //public Dictionary<string, BaseViewModel> dictionary { set; get; }

    //public MainPageViewModel()
    //{
    //    Dictionary<string, BaseViewModel> dic = new Dictionary<string, BaseViewModel>();
    //    dic.Add("key1",new BaseViewModel {Servicename = 0});

    //    dictionary = dic;
    //}
}

using System;
using System.ComponentModel;

namespace Protech.Models
{
    public class ServiceViewModel : INotifyPropertyChanged
    {
        //==========Service Name==========================
        string servicename = string.Empty;
        public string ServiceName
        {
            get => servicename;
            set
            {
                if (servicename == value)
                    return;

                servicename = value;
                OnServiceNamePropertyChanged(nameof(ServiceName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnServiceNamePropertyChanged(string servicename)
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(servicename));
        }

        //=========Descriptions===================================
        string descriptions = string.Empty;
        public string Descriptions
        {
            get => descriptions;
            set
            {
                if (descriptions == value)
                    return;

                descriptions = value;
                OnDescriptionsPropertyChanged(nameof(Descriptions));
            }
        }

        void OnDescriptionsPropertyChanged(string descriptions)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(descriptions));
        }


        //==========Service Fee=======================================
        string servicefee = string.Empty;
        public string ServiceFee
        {
            get => servicefee;
            set
            {
                if (servicefee == value)
                    return;

                servicefee = value;
                OnServiceFeePropertyChanged(nameof(ServiceFee));
            }
        }

        void OnServiceFeePropertyChanged(string servicefee)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(servicefee));
        }


        //=============Payment Mode =====================================
        string paymentmode = string.Empty;
        public string PaymentMode
        {
            get => paymentmode;
            set
            {
                if (paymentmode == value)
                    return;

                paymentmode = value;
                OnPaymentModePropertyChanged(nameof(PaymentMode));
            }
        }

        void OnPaymentModePropertyChanged(string paymentmode)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(paymentmode));
        }


    }
}

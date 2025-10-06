using System;
namespace Protech.Models
{
    public class ServiceListModel
    {

        //private int id;
        //[PrimaryKey]
        //[AutoIncrement]

        public int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string servicename;
        public string ServiceName
        {
            get { return servicename; }
            set { servicename = value; }
        }

        private string descriptions;
        public string Descriptions
        {
            get { return descriptions; }
            set { descriptions = value; }
        }

        private string servicefee;
        public string ServiceFee
        {
            get { return servicefee; }
            set { servicefee = value; }
        }

        private string paymentmode;
        public string PaymentMode
        {
            get { return paymentmode; }
            set { paymentmode = value; }
        }
    }           
}

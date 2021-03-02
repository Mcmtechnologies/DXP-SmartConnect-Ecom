namespace DXP.SmartConnect.Ecom.Core.Entities
{
    public class OrderFulfilment
    {
        public string Name { set; get; }
        public string FulfilmentType { set; get; }
        public string TimeslotReservationReference { set; get; }
        public string NotificationPhoneNumber { set; get; }
        public object Promotions { set; get; }
    }
}
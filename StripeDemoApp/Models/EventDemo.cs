using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StripeDemoApp.Models
{
    public class EventDemo
    {
        public int ID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public bool HasPayment { get; set; }
        public decimal Price { get; set; }

    }

    public class CheckoutOrderResponse
    {
        public string SessionId { get; set; }

        public string PubKey { get; set; }

    }

    public class UserDemo
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
    public class UserInfoCheckoutModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string CheckoutEmail { get; set; }
        public string Email { get; set; }
        public long PaidAmount { get; set; }
        public int ReservedEventId { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
    }

}
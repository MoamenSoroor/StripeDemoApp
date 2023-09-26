using System;

namespace StripeDemoApp.Models
{
    public class AppEventViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EventDate { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public bool HasPayment { get; set; }
        public decimal Price { get; set; }
        public int TenantId { get; set; }
        public string TenantName { get; set; }

    }
}
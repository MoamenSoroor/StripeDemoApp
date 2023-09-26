using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StripeDemoApp.Data
{
    public class AppEvent
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime EventDate { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public bool HasPayment { get; set; }
        public decimal Price { get; set; }
        public int TenantId { get; set; }
        public TenantInfo TenantInfo { get; set; }

    }




}
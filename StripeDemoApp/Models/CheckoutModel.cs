namespace StripeDemoApp.Models
{
    public class CheckoutModel
    {
        public int Id { get; set; }
        public string TenantName { get; set; }
        public string CheckoutEmail { get; set; }
        public long PaidAmount { get; set; }
        public int ReservedEventId { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
    }

}
namespace StripeDemoApp.Models
{
    public class TenantInfoViewModel
    {
        public int Id { get; set; }
        public string TenantName { get; set; }
        public string Email { get; set; }
        public bool IsRegisterationCompleted { get; set; }
        public int StripePaymentInfoId { get; set; }
        public string StripePaymentSecretKey { get; set; }
        public string StripePaymentPublicKey { get; set; }
        public string StripePaymentAccountId { get; set; }


    }
}
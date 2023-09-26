using StripeDemoApp.Services;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StripeDemoApp.Data
{
    public class TenantInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Id { get; set; }
        public string TenantName { get; set; }
        public string Email { get; set; }
        public string AccountId { get; set; }

        //public int StripePaymentInfoId { get; set; }
        //public StripUserPaymentInfo StripePaymentInfo { get; set; }

        //public int PaybalPaymentInfoId { get; set; }
        //public PaybalUserPaymentInfo PaybalPaymentInfo { get; set; }

    }


    //public enum PaymentGatewayType
    //{
    //    Stripe, Paypal
    //}


}
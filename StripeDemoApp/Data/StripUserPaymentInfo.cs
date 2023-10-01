using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StripeDemoApp.Data
{
    public class StripUserPaymentInfo
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public int Id { get; set; }
        public string SecretKey { get; set; }
        public string PublicKey { get; set; }
        public string AccountId { get; set; }
    }


}
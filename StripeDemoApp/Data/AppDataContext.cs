using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StripeDemoApp.Data
{
    public class AppDataContext : DbContext
    {
        public AppDataContext() : base("name=EventsDemoAppDb")
        {

        }

        public virtual DbSet<AppEvent> AppEvents { get; set; }
        public virtual DbSet<TenantInfo> Tenants { get; set; }
        //public virtual DbSet<StripUserPaymentInfo> StripePaymentInfo { get; set; }


    }


    public class MyDBInitializer : DropCreateDatabaseAlways<AppDataContext>
    {
        protected override void Seed(AppDataContext context)
        {
            //var stripePay1 = new StripUserPaymentInfo()
            //{
            //    Id = 1,
            //    PublicKey = "pk_test_51NrBSvJIhi7Tsh1qCE23s8BlErKrKuQ5ryxuBWb4pg9fxMdI2wk793PF2nbZ7z02CKlolnWAiuXfVOW4epgDbouz00BiYA9gCF",
            //    SecretKey = "sk_test_51NrBSvJIhi7Tsh1qMbm6qItfYSLEJVPk9EY7jb0pwsmrOHX0WKN9739TcjNKFvAqNNo0GHyDbwiHNtgMIrBwJoa1002hC2EIcM",
            //    ,AccountId = "acct_1NrBSvJIhi7Tsh1q"
            //};

            //var stripePay2 = new StripUserPaymentInfo()
            //{
            //    Id = 2,
            //    PublicKey = "pk_test_51NtTmKF2NqG2MwCbKmavZ1PKnSVUFTiZjqvDGi4PHqd38ZxhweYRzl3WYRxL5O4Cw8DPeSkqDJ2HHpTPWp6G6yTO00XclsLC28",
            //    SecretKey = "sk_test_51NtTmKF2NqG2MwCbZ61OSe6FzRdxrdtQSAYWK9XNCTPE2U2Gz8zxrL6SEHXWDnq3t6uhUMXUJCPJs917A0mun3w900DjFwo9Yv",
            //    ,AccountId = "acct_1NtTmKF2NqG2MwCb" // moamen
            //};

            //context.StripePaymentInfo.AddRange(new[] { stripePay1, stripePay2 });


            var tenants = new List<TenantInfo>(){
                new TenantInfo()
                {
                    Email = "eng.ahmed@gmail.com",
                    Id = 1,
                    TenantName = "First Tenant Payment",
                    //StripePaymentInfoId = 1,
                    AccountId = "acct_1NrBSvJIhi7Tsh1q"
                },
                    new TenantInfo()
                {
                    Email = "moa2@gmail.com",
                    Id = 2,
                    TenantName = "Second Tenant Payment (moamen)",
                    //StripePaymentInfoId = 2,
                    AccountId = "acct_1NtTmKF2NqG2MwCb"

                }
            };


            context.Tenants.AddRange(tenants);
            //context.SaveChanges();

            List<AppEvent> events = new List<AppEvent>
            {
                new AppEvent
                {
                   // Id = 1,
                    CreatedDate = new DateTime(2023, 09, 20),
                    EventName = "College Event",
                    EventDescription="a long College Event Description that will apears in the strip payment checkout page, \n you will enjoy",
                    HasPayment = true,
                    Price = 50.0M ,// amount in dollar  
                    TenantId = tenants[1].Id
                },
                new AppEvent
                {
                   // Id = 2,
                    CreatedDate = new DateTime(2023, 09, 21),
                    EventName = "Long Day Event",
                    EventDescription="a long Long Day Event Description that will apears in the strip payment checkout page, \n you will enjoy",
                    HasPayment = false,
                    Price = 0.0M, // amount in dollar
                    TenantId = tenants[1].Id
                },
                new AppEvent
                {
                   // Id = 3,
                    CreatedDate = new DateTime(2023, 09, 22),
                    EventName = "Devops Event",
                    EventDescription="a long Devops Event Description that will apears in the strip payment checkout page, \n you will enjoy",
                    HasPayment = true,
                    Price = 25.0M, // amount in dollar
                    TenantId = tenants[1].Id
                },
                new AppEvent
                {
                   // Id = 4,
                    CreatedDate = new DateTime(2023, 09, 23),
                    EventName = "Architecture Big Event",
                    EventDescription="a long Architecture Big Event Description that will apears in the strip payment checkout page, \n you will enjoy",
                    HasPayment = false,
                    Price = 0.0M, // amount in dollar
                    TenantId = tenants[1].Id
                },
                new AppEvent
                {
                   // Id = 5,
                    CreatedDate = new DateTime(2023, 09, 24),
                    EventName = "Dummy Event",
                    EventDescription="a long Dummy Event Description that will apears in the strip payment checkout page, \n you will enjoy",
                    HasPayment = true,
                    Price = 75.0M, // amount in dollar
                    TenantId = tenants[1].Id
                },
                new AppEvent
                {
                   // Id = 3,
                    CreatedDate = new DateTime(2023, 09, 25),
                    EventName = "Software Engineering Event",
                    EventDescription="a long Software Engineering Event Description that will apears in the strip payment checkout page, \n you will enjoy",
                    HasPayment = true,
                    Price = 105.0M, // amount in dollar
                    TenantId = tenants[1].Id
                },
                new AppEvent
                {
                   // Id = 3,
                    CreatedDate = new DateTime(2023, 09, 23),
                    EventName = "Data Analyst Event",
                    EventDescription="a long Data Analyst Event Description that will apears in the strip payment checkout page, \n you will enjoy",
                    HasPayment = true,
                    Price = 250.0M, // amount in dollar
                    TenantId = tenants[1].Id
                },
            };

            context.AppEvents.AddRange(events); 
            base.Seed(context);
        }
    }

}
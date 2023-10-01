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
            //Database.SetInitializer<AppDataContext>(new MyDBInitializer());
        }

        public virtual DbSet<AppEvent> AppEvents { get; set; }
        public virtual DbSet<TenantInfo> Tenants { get; set; }
        public virtual DbSet<StripUserPaymentInfo> StripePaymentInfo { get; set; }


    }


    public class MyDBInitializer : DropCreateDatabaseAlways<AppDataContext>
    {
        protected override void Seed(AppDataContext context)
        {




            var t = context.Tenants.FirstOrDefault();

            List<AppEvent> events = new List<AppEvent>
            {
                new AppEvent
                {
                   // Id = 1,
                    CreatedDate = new DateTime(2023, 09, 20),
                    EventDate = new DateTime(2023, 09, 20),
                    EventName = "College Event",
                    EventDescription="a long College Event Description that will apears in the strip payment checkout page, \n you will enjoy",
                    HasPayment = true,
                    Price = 50.0M ,// amount in dollar  
                    TenantId = t.Id
                },
                new AppEvent
                {
                   // Id = 2,
                    CreatedDate = new DateTime(2023, 09, 21),
                    EventDate = new DateTime(2023, 09, 21),
                    EventName = "Long Day Event",
                    EventDescription="a long Long Day Event Description that will apears in the strip payment checkout page, \n you will enjoy",
                    HasPayment = false,
                    Price = 0.0M, // amount in dollar
                    TenantId = t.Id
                },
                new AppEvent
                {
                   // Id = 3,
                    CreatedDate = new DateTime(2023, 09, 22),
                    EventDate = new DateTime(2023, 09, 22),
                    EventName = "Devops Event",
                    EventDescription="a long Devops Event Description that will apears in the strip payment checkout page, \n you will enjoy",
                    HasPayment = true,
                    Price = 25.0M, // amount in dollar
                    TenantId = t.Id
                },
                new AppEvent
                {
                   // Id = 4,
                    CreatedDate = new DateTime(2023, 09, 23),
                    EventDate = new DateTime(2023, 09, 23),
                    EventName = "Architecture Big Event",
                    EventDescription="a long Architecture Big Event Description that will apears in the strip payment checkout page, \n you will enjoy",
                    HasPayment = false,
                    Price = 0.0M, // amount in dollar
                    TenantId = t.Id
                },
                new AppEvent
                {
                   // Id = 5,
                    CreatedDate = new DateTime(2023, 09, 24),
                    EventDate = new DateTime(2023, 09, 24),
                    EventName = "Dummy Event",
                    EventDescription="a long Dummy Event Description that will apears in the strip payment checkout page, \n you will enjoy",
                    HasPayment = true,
                    Price = 75.0M, // amount in dollar
                    TenantId = t.Id
                },
                new AppEvent
                {
                   // Id = 3,
                    CreatedDate = new DateTime(2023, 09, 25),
                    EventDate = new DateTime(2023, 09, 25),
                    EventName = "Software Engineering Event",
                    EventDescription="a long Software Engineering Event Description that will apears in the strip payment checkout page, \n you will enjoy",
                    HasPayment = true,
                    Price = 105.0M, // amount in dollar
                    TenantId = t.Id
                },
                new AppEvent
                {
                   // Id = 3,
                    CreatedDate = new DateTime(2023, 09, 23),
                    EventDate = new DateTime(2023, 09, 23),
                    EventName = "Data Analyst Event",
                    EventDescription="a long Data Analyst Event Description that will apears in the strip payment checkout page, \n you will enjoy",
                    HasPayment = true,
                    Price = 250.0M, // amount in dollar
                    TenantId = t.Id
                },
            };

            context.AppEvents.AddRange(events); 
            base.Seed(context);
        }
    }

}
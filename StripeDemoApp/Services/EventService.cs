using StripeDemoApp.Data;
using StripeDemoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StripeDemoApp.Services
{
    public class AppEventsService
    {
        private static List<AppEvent> events = new List<AppEvent>
            {
                new AppEvent
                {
                    Id = 1,
                    CreatedDate = new DateTime(2023, 09, 20),
                    EventName = "College Event",
                    EventDescription="a long College Event Description that will apears in the strip payment checkout page, \n you will enjoy",
                    HasPayment = true,
                    Price = 50.0M // amount in dollar  
                    ,TenantId=1

                },
                new AppEvent
                {
                    Id = 2,
                    CreatedDate = new DateTime(2023, 09, 21),
                    EventName = "Long Day Event",
                    EventDescription="a long Long Day Event Description that will apears in the strip payment checkout page, \n you will enjoy",
                    HasPayment = false,
                    Price = 0.0M // amount in dollar
                    ,TenantId=1
                },
                new AppEvent
                {
                    Id = 3,
                    CreatedDate = new DateTime(2023, 09, 22),
                    EventName = "Devops Event",
                    EventDescription="a long Devops Event Description that will apears in the strip payment checkout page, \n you will enjoy",
                    HasPayment = true,
                    Price = 25.0M // amount in dollar
                    ,TenantId=1
                },
                new AppEvent
                {
                    Id = 4,
                    CreatedDate = new DateTime(2023, 09, 23),
                    EventName = "Architecture Big Event",
                    EventDescription="a long Architecture Big Event Description that will apears in the strip payment checkout page, \n you will enjoy",
                    HasPayment = false,
                    Price = 0.0M // amount in dollar
                    ,TenantId=1
                },
                new AppEvent
                {
                    Id = 5,
                    CreatedDate = new DateTime(2023, 09, 24),
                    EventName = "Dummy Event",
                    EventDescription="a long Dummy Event Description that will apears in the strip payment checkout page, \n you will enjoy",
                    HasPayment = true,
                    Price = 75.0M // amount in dollar
                    ,TenantId=1
                }
            };
        private readonly AppDataContext db;

        public AppEventsService(AppDataContext db)
        {
            this.db = db;
        }
        
        public List<AppEventViewModel> GetEvents()
        {
            return db.AppEvents.Select(v=> new AppEventViewModel()
            {
                CreatedDate = v.CreatedDate,
                EventDate = v.EventDate,
                EventName = v.EventName,
                EventDescription = v.EventDescription,
                HasPayment = v.HasPayment,
                Id = v.Id,
                TenantId = v.TenantId,
                Price = v.Price,
                TenantName = v.TenantInfo.TenantName
            }).ToList();
        }

        public AppEvent GetEvent(int eventId)
        {
            return db.AppEvents.Include("TenantInfo").FirstOrDefault(v => v.Id == eventId);
        }

        public void RegisterTestEvents()
        {
            db.AppEvents.AddRange(events);
            db.SaveChanges();
        }
    }




}
using StripeDemoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StripeDemoApp.Services
{
    public class EventService
    {
        private static List<EventDemo> events = new List<EventDemo>
            {
                new EventDemo
                {
                    ID = 1,
                    CreatedDate = new DateTime(2023, 09, 20),
                    EventName = "College Event",
                    EventDescription="a long College Event Description that will apears in the strip payment checkout page, \n you will enjoy",
                    HasPayment = true,
                    Price = 50.0M // amount in dollar  
                },
                new EventDemo
                {
                    ID = 2,
                    CreatedDate = new DateTime(2023, 09, 21),
                    EventName = "Long Day Event",
                    EventDescription="a long Long Day Event Description that will apears in the strip payment checkout page, \n you will enjoy",
                    HasPayment = false,
                    Price = 0.0M // amount in dollar
                },
                new EventDemo
                {
                    ID = 3,
                    CreatedDate = new DateTime(2023, 09, 22),
                    EventName = "Devops Event",
                    EventDescription="a long Devops Event Description that will apears in the strip payment checkout page, \n you will enjoy",
                    HasPayment = true,
                    Price = 25.0M // amount in dollar
                },
                new EventDemo
                {
                    ID = 4,
                    CreatedDate = new DateTime(2023, 09, 23),
                    EventName = "Architecture Big Event",
                    EventDescription="a long Architecture Big Event Description that will apears in the strip payment checkout page, \n you will enjoy",
                    HasPayment = false,
                    Price = 0.0M // amount in dollar
                },
                new EventDemo
                {
                    ID = 5,
                    CreatedDate = new DateTime(2023, 09, 24),
                    EventName = "Dummy Event",
                    EventDescription="a long Dummy Event Description that will apears in the strip payment checkout page, \n you will enjoy",
                    HasPayment = true,
                    Price = 75.0M // amount in dollar
                }
            };
        public List<EventDemo> GetEvents()
        {
            return events;
        }

        public EventDemo GetEvent(int eventId)
        {
            return events.FirstOrDefault(v => v.ID == eventId);
        }


    }




}
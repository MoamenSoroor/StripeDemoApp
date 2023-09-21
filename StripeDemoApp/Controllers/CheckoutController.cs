using StripeDemoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Stripe.Checkout;
using StripeDemoApp.Services;

namespace StripeDemoApp.Controllers
{

    public class CheckoutController : Controller
    {
        UsersService usersService = new UsersService();
        EventService eventService = new EventService();

        [HttpPost]
        public async Task<ActionResult> CheckoutOrder(int eventId)
        {
            var eventDemo = eventService.GetEvent(eventId);
            string thisApiUrl = 
                string.Format("{0}://{1}{2}", 
                Request.Url.Scheme, 
                Request.Url.Authority, 
                Url.Content("~"));

            if (string.IsNullOrWhiteSpace(thisApiUrl) == false)
            {
                var sessionId = await CheckOut(eventDemo, thisApiUrl);
                StripeOptions  stripeOp= Configurations.GetStripeConfig();
                var pubKey = stripeOp.PubKey;

                var checkoutOrderResponse = new CheckoutOrderResponse()
                {
                    SessionId = sessionId,
                    PubKey = pubKey
                };

                return Json(checkoutOrderResponse);
            }
            else
            {
                return Json(new { Error="error" });
            }
        }


        private async Task<string> CheckOut(EventDemo eventDemo, string thisApiUrl)
        {
            // Create a payment flow from the items in the cart.
            // Gets sent to Stripe API.
            var options = new SessionCreateOptions
            {
                // Stripe calls the URLs below when certain checkout events happen such as success and failure.
                SuccessUrl = $"{thisApiUrl}/checkout/CheckoutSuccess?sessionId=" + "{CHECKOUT_SESSION_ID}", // Customer paid.
                CancelUrl = $"{thisApiUrl}/checkout/CheckoutFailed",  // Checkout cancelled.
                PaymentMethodTypes = new List<string> // Only card available in test mode?
                {
                    "card"
                },
                LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions ()
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        //UnitAmount = eventDemo.Price, // Price is in USD cents.
                        UnitAmountDecimal = eventDemo.Price * 100, // Price is in USD cents.
                        Currency = "USD",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = eventDemo.EventName,
                            Description = eventDemo.EventDescription,
                            //Images = new List<string> { eventDemio.ImageUrl }
                        },
                    },
                    Quantity = 1,
                },
            },
                Mode = "payment", // One-time payment. Stripe supports recurring 'subscription' payments.
                Metadata = new Dictionary<string, string>()
                {
                    ["EventID"] = eventDemo.ID.ToString(),
                    ["UserID"] = usersService.GetCurrentUser().Id.ToString(),
                }
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);


            return session.Id;
        }


        // Automatic query parameter handling from ASP.NET.
        // Example URL: https://localhost:7051/checkout/success?sessionId=si_123123123123
        public ActionResult CheckoutSuccess(string sessionId)
        {
            var sessionService = new SessionService();
            var session = sessionService.Get(sessionId);

            // Here you can save order and customer details to your database.
            var total = session.AmountTotal.Value;
            var customerEmail = session.CustomerDetails.Email;
            var userId = int.Parse(session.Metadata["UserID"]);
            var eventID = int.Parse(session.Metadata["EventID"]);
            var user = usersService.GetUser(userId);
            var myEvent = eventService.GetEvent(eventID);

            var userCheckoutModel = new UserInfoCheckoutModel()
            {
                Email = user.Email,
                CheckoutEmail = customerEmail,
                EventDescription = myEvent.EventDescription,
                EventName = myEvent.EventName,
                Id = userId,
                PaidAmount = total,
                ReservedEventId = eventID,
                Username = user.Username
            };


            return View(userCheckoutModel);

        }

        //CheckoutFailed
        public ActionResult CheckoutFailed()
        {
           
            return View();

        }

    }
}

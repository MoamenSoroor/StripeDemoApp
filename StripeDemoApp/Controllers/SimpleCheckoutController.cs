
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Stripe.Checkout;
using StripeDemoApp.Services;
using Stripe;
using StripeDemoApp.Data;
using System.Collections.Generic;
using StripeDemoApp.Models;
using System;

namespace StripeDemoApp.Controllers
{

    public class SimpleCheckoutController : Controller
    {
        private readonly TenantService tenantInfoService;
        private readonly AppEventsService eventService;

        public SimpleCheckoutController(TenantService tenantInfoService, AppEventsService eventService)
        {
            this.tenantInfoService = tenantInfoService;
            this.eventService = eventService;
        }

        [HttpGet]
        public async Task<ActionResult> CheckoutOrder(int eventId)
        {
            try
            {
                var eventDemo = eventService.GetEvent(eventId);

                string thisApiUrl =
                    string.Format("{0}://{1}{2}",
                    Request.Url.Scheme,
                    Request.Url.Authority,
                    Url.Content("~"));


                var tenant = tenantInfoService.GetTenantInfoViewModel(eventDemo.TenantId);


                if (string.IsNullOrWhiteSpace(thisApiUrl) == false)
                {
                    var session = await CheckOut(eventDemo, tenant, thisApiUrl);
                    return Redirect(session.Url);
                }
                else
                {
                    return RedirectToAction("CheckoutFailed");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("CheckoutFailed", ex);
            }
        }


        private async Task<Session> CheckOut(AppEvent eventDemo, TenantInfoViewModel tenantInfo, string thisApiUrl)
        {
            // Create a payment flow from the items in the cart.
            // Gets sent to Stripe API.
            var options = new SessionCreateOptions
            {
                // Stripe calls the URLs below when certain checkout events happen such as success and failure.
                SuccessUrl = $"{thisApiUrl}/SimpleCheckout/CheckoutSuccess?sessionId=" + "{CHECKOUT_SESSION_ID}" + "&tenantId=" + tenantInfo.Id, // Customer paid., // Customer paid.
                CancelUrl = $"{thisApiUrl}/SimpleCheckout/CheckoutFailed",  // Checkout cancelled.
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
                    ["EventID"] = eventDemo.Id.ToString(),
                    //["UserID"] = usersService.GetCurrentUser().Id.ToString(),
                }
            };

            RequestOptions requestOptions = new RequestOptions()
            {
                ApiKey = tenantInfo.StripePaymentSecretKey,
            };



            var service = new SessionService();
            var session = await service.CreateAsync(options, requestOptions);


            return session;
        }


        // Automatic query parameter handling from ASP.NET.
        // Example URL: https://localhost:7051/CheckoutSimple/success?sessionId=si_123123123123
        public ActionResult CheckoutSuccess(string sessionId,int tenantId)
        {
            var t = tenantInfoService.GetTenantInfoViewModel(tenantId);

            var sessionService = new SessionService();
            RequestOptions requestOptions = new RequestOptions()
            {
                ApiKey = t.StripePaymentSecretKey,
            };

            var session = sessionService.Get(sessionId,requestOptions: requestOptions);

            // Here you can save order and customer details to your database.
            var total = session.AmountTotal.Value;
            var customerEmail = session.CustomerDetails.Email;
            var eventID = int.Parse(session.Metadata["EventID"]);
            var myEvent = eventService.GetEvent(eventID);

            var tenantCheckoutModel = new CheckoutModel()
            {
                //Email = tenantInfo.Email,
                CheckoutEmail = customerEmail,
                EventDescription = myEvent.EventDescription,
                EventName = myEvent.EventName,
                //Id = tenantID,
                PaidAmount = total,
                ReservedEventId = eventID,

            };
            return View(tenantCheckoutModel);

        }

        //CheckoutFailed
        public ActionResult CheckoutFailed(Exception ex)
        {

            return View();

        }

    }

}
using StripeDemoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Stripe.Checkout;
using StripeDemoApp.Services;
using Stripe;
using StripeDemoApp.Data;
using StripeDemoApp.Exceptions;

namespace StripeDemoApp.Controllers
{

    public class CheckoutController : Controller
    {
        private readonly TenantService tenantInfoService;
        private readonly AppEventsService eventService;

        public CheckoutController(TenantService tenantInfoService,AppEventsService eventService)
        {
            this.tenantInfoService = tenantInfoService;
            this.eventService = eventService;
        }

        //[HttpPost]
        //public async Task<ActionResult> CheckoutOrder(int eventId)
        //{
        //    var stripOp = Configurations.GetStripeConfig();

        //    var eventDemo = eventService.GetEvent(eventId);
        //    var tenant = eventDemo.TenantInfo;

        //    string thisApiUrl = GetCurrentApiBaseUrl();


        //    if (string.IsNullOrWhiteSpace(thisApiUrl) == false)
        //    {
        //        var sessionId = await CheckOut(eventDemo, tenant, thisApiUrl);
        //        var checkoutOrderResponse = new CheckoutOrderResponse()
        //        {
        //            SessionId = sessionId,
        //            PubKey = stripOp.PubKey,
        //            //AccountId = tenant.AccountId,
        //        };

        //        return Json(checkoutOrderResponse);
        //    }
        //    else
        //    {
        //        return Json(new { Error = "error" });
        //    }
        //}

        public async Task<ActionResult> CheckoutOrder(int eventId)
        {
            //var stripOp = Configurations.GetStripeConfig();

            var eventDemo = eventService.GetEvent(eventId);

            var tenant = tenantInfoService.GetTenantInfo(3);

            string thisApiUrl = GetCurrentApiBaseUrl();


            var session = await CheckOut(eventDemo, tenant, thisApiUrl);

            return Redirect(session.Url);

        }


        private string GetCurrentApiBaseUrl()
        {
            return string.Format("{0}://{1}{2}",
                            Request.Url.Scheme,
                            Request.Url.Authority,
                            Url.Content("~"));
        }

        private async Task<Session> CheckOut(AppEvent eventDemo,TenantInfo tenantInfo, string thisApiUrl)
        {
            //var stripOp = Configurations.GetStripeConfig();


            //var requestOptions = new RequestOptions();
            ////requestOptions.ApiKey = tenantInfo.StripePaymentInfo.SecretKey;
            //requestOptions.ApiKey = stripOp.ApiKey;
            //requestOptions.StripeAccount = tenantInfo.AccountId; // my account id

            var tenantAccount = tenantInfo?.StripePaymentInfo?.AccountId;

            if (tenantAccount == null)
                throw new StripeAccountIdNullException();

            var options = new SessionCreateOptions
            {
                // Stripe calls the URLs below when certain checkout events happen such as success and failure.
                SuccessUrl = $"{thisApiUrl}/checkout/CheckoutSuccess?sessionId=" + "{CHECKOUT_SESSION_ID}&tenantID="+ tenantInfo.Id, // Customer paid.
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
                    ["EventID"] = eventDemo.Id.ToString(),
                    ["TenantID"] = tenantInfo.Id.ToString(),
                },
                PaymentIntentData = new SessionPaymentIntentDataOptions()
                {
                    TransferData = new SessionPaymentIntentDataTransferDataOptions()
                    {
                        Destination = tenantInfo.StripePaymentInfo.AccountId,
                    },
                    ApplicationFeeAmount = 0,
                }
            };
            var stripOp = Configurations.GetStripeConfig();
            var service = new SessionService();
            //var session = await service.CreateAsync(options, requestOptions);
            var session = await service.CreateAsync(options, 
                new RequestOptions() { ApiKey= stripOp.ApiKey });


            return session;
        }



        // Automatic query parameter handling from ASP.NET.
        // Example URL: https://localhost:7051/checkout/success?sessionId=si_123123123123
        public ActionResult CheckoutSuccess(string sessionId, int tenantID)
        {
            var tenantInfo = tenantInfoService.GetTenantInfo(tenantID);

            //var requestOptions = new RequestOptions();
            //requestOptions.ApiKey = tenantInfo.StripePaymentInfo.SecretKey;

            

            var sessionService = new SessionService();
            //var session = sessionService.Get(sessionId, requestOptions: requestOptions);
            var session = sessionService.Get(sessionId);

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
                Id = tenantID,
                PaidAmount = total,
                ReservedEventId = eventID,
                TenantName = tenantInfo.TenantName
            };


            return View(tenantCheckoutModel);

        }

        //CheckoutFailed
        public ActionResult CheckoutFailed()
        {
           
            return View();

        }

    }
}

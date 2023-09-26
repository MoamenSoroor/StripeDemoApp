using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StripeDemoApp.Controllers
{
    public class AccountsController : Controller
    {


        public ActionResult NewUserLanding()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAcc()
        {
            // Create a new standard connected account.
            var accountOptions = new AccountCreateOptions
            {
                Type = "standard",
            };
            var accountService = new AccountService();
            var account = accountService.Create(accountOptions);
            HttpContext.Session["account_id"] =  account.Id;

            // Create a new account link to onboard the new account.
            var options = new AccountLinkCreateOptions
            {
                Account = account.Id,
                RefreshUrl = $@"{GetCurrentApiBaseUrl()}/Accounts/CreateAccRefresh",
                ReturnUrl = $@"{GetCurrentApiBaseUrl()}/Accounts/Success",
                Type = "account_onboarding",
            };
            var service = new AccountLinkService();
            var accountLink = service.Create(options);
            return Redirect(accountLink.Url);

        }

        public ActionResult CreateAccRefresh()
        {
            // Create a new standard connected account.
            var accountId = HttpContext.Session["account_id"].ToString();

            // Create a new account link to onboard the new account.
            var options = new AccountLinkCreateOptions
            {
                Account = accountId,
                RefreshUrl = $@"{GetCurrentApiBaseUrl()}/Accounts/CreateAccRefresh",
                ReturnUrl = $@"{GetCurrentApiBaseUrl()}/Accounts/Success",
                Type = "account_onboarding",
            };
            var service = new AccountLinkService();
            var accountLink = service.Create(options);
            return Redirect(accountLink.Url);

        }

        public ActionResult Success()
        {
            // Create a new standard connected account.
            //var accountId = HttpContext.Session["account_id"].ToString();

            return View();

        }

        private string GetCurrentApiBaseUrl()
        {
            return string.Format("{0}://{1}{2}",
                            Request.Url.Scheme,
                            Request.Url.Authority,
                            Url.Content("~"));
        }

    }
}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;

namespace StripeDemoApp
{
    public static class Configurations
    {

        public static StripeOptions GetStripeConfig()
        {
            StripeOptions op = new StripeOptions();
            NameValueCollection section = (NameValueCollection)ConfigurationManager.GetSection("Stripe");
            op.PubKey = section["PubKey"];
            op.ApiKey = section["SecretKey"];
            return op;
        }



    }

}
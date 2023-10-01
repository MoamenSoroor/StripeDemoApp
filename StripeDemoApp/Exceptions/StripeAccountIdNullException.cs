using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StripeDemoApp.Exceptions
{
    public class StripeAccountIdNullException : Exception
    {
        public StripeAccountIdNullException() { }
        public StripeAccountIdNullException(string message) : base(message) { }
        public StripeAccountIdNullException(string message, Exception inner) : base(message, inner) { }
        protected StripeAccountIdNullException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
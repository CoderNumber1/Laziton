using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Authorizer.Mvc.Helpers
{
    public static class AuthHtmlHelpers
    {
        public static MvcHtmlString GetLogonStatusView(this HtmlHelper Helper)
        {
            return Helper.Partial("~/Areas/Authorization/Views/Shared/_LogOnPartial.cshtml");
        }
    }
}
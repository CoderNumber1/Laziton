using System.Web;
using System.Web.Mvc;
using Website.Filters;

namespace Website
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new InitializeSimpleMembershipAttribute());
            filters.Add(new AuthorizeAttribute());
        }
    }
}
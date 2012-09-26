using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using KarlAnthonyJames.Com.Core.Configuration;

namespace KarlAnthonyJames.Com.Core.Routing
{
    public class RouteIgnoreConstraint
        : IRouteConstraint
    {
        bool IRouteConstraint.Match(System.Web.HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var Controllers = CoreConfiguration.Instance.IgnorableControllers.Split(',');
            var Actions = CoreConfiguration.Instance.IgnorableActions.Split(',');

            return !Controllers.Any(Controller => Controller == values["controller"].ToString()) && !Actions.Any(Action => Action == values["action"].ToString());
        }
    }
}

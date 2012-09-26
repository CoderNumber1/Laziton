using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Reflection;
using System.Diagnostics;

namespace KarlAnthonyJames.Com.Util
{
    public class ExceptionHandlingTest
        : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            ExceptionHandlerAreaAttribute RegisteredExceptionArea = null;

            ReflectedControllerDescriptor controllerDescriptor = new ReflectedControllerDescriptor(filterContext.Controller.GetType());
            ActionDescriptor actionDescriptor = controllerDescriptor.FindAction(filterContext.Controller.ControllerContext, filterContext.RouteData.Values["action"].ToString());

            if (controllerDescriptor.IsDefined(typeof(ExceptionHandlerAreaAttribute), true))
                RegisteredExceptionArea = controllerDescriptor.GetCustomAttributes(typeof(ExceptionHandlerAreaAttribute), true).First() as ExceptionHandlerAreaAttribute;
            else if (actionDescriptor != null && actionDescriptor.IsDefined(typeof(ExceptionHandlerAreaAttribute), true))
                RegisteredExceptionArea = actionDescriptor.GetCustomAttributes(typeof(ExceptionHandlerAreaAttribute), true).First() as ExceptionHandlerAreaAttribute;

            if (RegisteredExceptionArea != null)
                Debug.WriteLine(RegisteredExceptionArea.RegisteredAreaName);

            base.OnException(filterContext);
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ExceptionHandlerAreaAttribute
        : Attribute
    {
        public string RegisteredAreaName { get; set; }
    }
}

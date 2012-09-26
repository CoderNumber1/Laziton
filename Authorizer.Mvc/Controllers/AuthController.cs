using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetOpenAuth;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.RelyingParty;
using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using Authorizer.Security.Session;
using System.Net;
using System.Web.Security;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Authorizer.DataServices;
using Authorizer.DataModels;
using Authorizer.Services;
using KarlAnthonyJames.Com.Util.HtmlHelpers;

namespace Authorizer.Mvc.Controllers
{
    public class AuthController : Controller
    {
        public static OpenIdAjaxRelyingParty _AjaxParty;
        public static OpenIdAjaxRelyingParty AjaxParty { get { if (_AjaxParty == null)_AjaxParty = new OpenIdAjaxRelyingParty(); return _AjaxParty; } }

        public ActionResult LogOn()
        {
            return View();
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToRoute("Default", new { controller = "Home", action = "Index", area = string.Empty });
        }

        public ActionResult Discover(string identifier)
        {
            if (!this.Request.IsAjaxRequest())
            {
                throw new InvalidOperationException();
            }

            UrlHelper Urls = new UrlHelper(HttpContext.Request.RequestContext);
            var Requests = AjaxParty.CreateRequests(identifier, Realm.AutoDetect, Urls.GetFullUri("/auth/PopUpReturn")).ToList();
            Requests.ForEach(req =>
            {
                req.AddExtension(new ClaimsRequest()
                    {
                        BirthDate = DemandLevel.Request,
                        Email = DemandLevel.Require,
                        FullName = DemandLevel.Request,
                        PolicyUrl = new Uri("http://www.google.com")
                    });
            });

            return AjaxParty.AsAjaxDiscoveryResult(Requests).AsActionResult();
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post), ValidateInput(false)]
        public ActionResult PopUpReturn()
        {
            return AjaxParty.ProcessResponseFromPopup().AsActionResult();
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult LogOnPostAssertion(string openid_openidAuthData)
        {
            IAuthenticationResponse Response;

            if (!string.IsNullOrEmpty(openid_openidAuthData))
            {
                Uri Auth = new Uri(openid_openidAuthData);
                WebHeaderCollection Headers = new WebHeaderCollection();

                foreach (string Key in Request.Headers)
                {
                    Headers[Key] = Request.Headers[Key];
                }

                HttpRequestInfo ResponseInfo = new HttpRequestInfo("GET", Auth, Auth.PathAndQuery, Headers, null);
                Response = AjaxParty.GetResponse(ResponseInfo);
            }
            else
                Response = AjaxParty.GetResponse();

            if (Response != null)
            {
                switch(Response.Status)
                {
                    case AuthenticationStatus.Authenticated:
                        var Claim = Response.GetExtension<ClaimsResponse>();
                        Authorizer.DataModels.User UserInfo = null;

                        using (IAuthorizerDataService DataService = DataServiceProvider.Instance.CreateAuthorizerDataService())
                        {
                            DataService.RegisterUserLogIn(new DataModels.User()
                                {
                                    EmailFull = Claim.Email,
                                    EmailUserName = Claim.MailAddress.User,
                                    OpenId = Response.ClaimedIdentifier
                                });

                            UserInfo = DataService.RetrieveUserInfo(Response.ClaimedIdentifier);

                            FormsAuthenticationTicket Authorization = AuthService.Service.CreateAuthTicket(Response.ClaimedIdentifier
                            , UserInfo != null && UserInfo.Alias != null ? UserInfo.Alias.Name : !string.IsNullOrEmpty(Claim.FullName) ? Claim.FullName : !string.IsNullOrEmpty(Claim.MailAddress.User) ? Claim.MailAddress.User : Response.FriendlyIdentifierForDisplay);

                            string EncryptedAuthorization = FormsAuthentication.Encrypt(Authorization);

                            this.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, EncryptedAuthorization));
                        }

                        return RedirectToAction("Index", "Home", new { area = string.Empty });
                    case AuthenticationStatus.Canceled:
                        ModelState.AddModelError("OpenID", "You need to complete the authorization process to be logged in");
                        break;
                    case AuthenticationStatus.Failed:
                        ModelState.AddModelError("OpenID", Response.Exception.Message);
                        break;
                }
            }

            return View("LogOn");
        }

        public ActionResult RegisterAlias()
        {
            return View(new UserAlias());
        }

        [HttpPost]
        public ActionResult RegisterAlias(UserAlias model)
        {
            using (IAuthorizerDataService DataService = DataServiceProvider.Instance.CreateAuthorizerDataService())
            {
                DataService.RegisterUserAlias(AuthService.Service.Identity.OpenId, model);
                UserSessions.CurrentSession.User = DataService.RetrieveUserInfo(AuthService.Service.Identity.OpenId);
                AuthService.Service.UpdateDisplayName();

                return Redirect(UserSessions.CurrentSession.RedirectToUrl);
            }
        }
    }
}

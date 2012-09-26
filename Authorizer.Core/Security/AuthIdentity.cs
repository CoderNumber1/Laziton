using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.Web.Security;
using Authorizer.Security.Session;

namespace Authorizer.Security
{
    public class AuthIdentity : IIdentity
    {
        private FormsAuthenticationTicket Ticket;

        public AuthIdentity(FormsAuthenticationTicket ticket)
        {
            this.Ticket = ticket;
        }

        string IIdentity.AuthenticationType
        {
            get { return "AuthType"; }
        }

        bool IIdentity.IsAuthenticated
        {
            get { return true; }
        }

        string IIdentity.Name
        {
            get { return this.Ticket.UserData; }
        }

        public string OpenId { get { return this.Ticket.Name; } }
    }
}

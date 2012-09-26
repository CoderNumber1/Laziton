using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Authorizer.DataModels;

namespace Authorizer.Security.Session
{
    [DataContract]
    public class UserSession
    {

        [DataMember]
        public bool UserLoggedIn { get; set; }

        [DataMember]
        public User User { get; set; }

        [DataMember]
        public string DisplayName { get; set; }

        [DataMember]
        public UserLevels PermissionLevel { get; set; }

        [DataMember]
        public string RedirectToUrl { get; set; }

        [DataMember]
        public string OpenId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Profile;

namespace Website.Models
{
    public class SiteProfile
        : System.Web.Profile.ProfileBase
    {
        public SiteProfile()
            : base() { }
        
        public string Name
        {
            get { return base.GetPropertyValue("Name") as string; }
            set { base.SetPropertyValue("Name", value); }
        }

        public string Email
        {
            get { return base.GetPropertyValue("Email") as string; }
            set { base.SetPropertyValue("Email", value); }
        }

        public DateTime Birthdate
        {
            get { return (DateTime)base.GetPropertyValue("Birthdate"); }
            set { base.SetPropertyValue("Birthdate", value); }
        }

        public static SiteProfile GetProfile(string username)
        {
            return Create(username) as SiteProfile;
        }
    }
}
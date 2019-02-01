using Isbak.KullaniciYonetimi.Entities.Users;
using Isbak.KullaniciYonetimi.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Isbak.KullaniciYonetimi.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly RoleService _roleService = new RoleService();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        public void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            if (!(HttpContext.Current.User == null))
            {
                if (HttpContext.Current.User.Identity.AuthenticationType == "Forms")
                {
                    System.Web.Security.FormsIdentity id;
                    id = (System.Web.Security.FormsIdentity)HttpContext.Current.User.Identity;

                    FormsAuthenticationTicket ticket = id.Ticket;
                    string userData = ticket.UserData;
                    string[] roles = userData.Split(',');
                    //string[] roles = null;
                    //roles = _roleService.GetRoles(roles);
                    HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(id, roles);
                }
            }
        }
    }
}

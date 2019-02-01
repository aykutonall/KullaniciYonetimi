using Isbak.KullaniciYonetimi.Entities.Roles;
using Isbak.KullaniciYonetimi.Entities.Users;
using Isbak.KullaniciYonetimi.MVC.Models.User;
using Isbak.KullaniciYonetimi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Isbak.KullaniciYonetimi.MVC.Controllers
{
    public class AdminController : Controller
    {

        private readonly UserService _userService;
        private readonly LoginService _loginService;
        private readonly RoleService _roleService;

        public AdminController()
        {
            _userService = new UserService();
            _loginService = new LoginService();
            _roleService = new RoleService();
        }
        // GET: Admin
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(CreateUser model)
        {
            if (ModelState.IsValid)
            {

                User createuser = new Entities.Users.User()
                {

                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password
                };
                _userService.UserCreate(createuser);
            }
            return View(model);
        }

        
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login model)
        {
            User user = new User();
            user.Username = model.Username;
            user.Password = model.Password;
           
          
            User returnModel = _loginService.LoginUser(user);

            List<Role> roles = _roleService.GetRoles(returnModel.RolId);

            string rolesStr = "";

            foreach (Role item in roles)
            {
                rolesStr = rolesStr + item.RoleName + ",";
            }



            if (returnModel != null)
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                    1,
                    returnModel.Username,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(30),
                    true,
                    rolesStr.TrimEnd(' ').ToString(),
                    //returnModel.Role.ToString(),//Sıkıntı burda
                    FormsAuthentication.FormsCookiePath);

                string hash = FormsAuthentication.Encrypt(ticket);

                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                if (ticket.IsPersistent)
                {
                    cookie.Expires = ticket.Expiration;
                }
                Response.Cookies.Add(cookie);

                //FormsAuthentication.SetAuthCookie(returnModel.Username, true);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("Username and Password", "Username and Password not invalid.");
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}
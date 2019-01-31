using Isbak.KullaniciYonetimi.Entities.Users;
using Isbak.KullaniciYonetimi.MVC.Models.User;
using Isbak.KullaniciYonetimi.Services;
using System.Web.Mvc;
using System.Web.Security;

namespace Isbak.KullaniciYonetimi.MVC.Controllers
{
    public class AdminController : Controller
    {

        private readonly UserService _userService;
        private readonly LoginService _loginService;

        public AdminController()
        {
            _userService = new UserService();
            _loginService = new LoginService();
        }
        // GET: Admin
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
            if (returnModel != null)
            {
                FormsAuthentication.SetAuthCookie(returnModel.Username, true);
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
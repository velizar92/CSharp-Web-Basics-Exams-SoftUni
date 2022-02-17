using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using SharedTrip.Contracts;
using SharedTrip.Models;

namespace SharedTrip.Controllers
{
    public class UsersController : Controller
    {
     
        private readonly IUserService userService;


        public UsersController(
            Request request,
            IUserService _userService)
            : base(request)
        {
            this.userService = _userService;
        }


        public Response Login()
        {
            if (User.IsAuthenticated)
            {
                return Redirect("/Trips/All");
            }

            return View(new { IsAuthenticated = false });
        }


        [HttpPost]
        public Response Login(LoginViewModel model)
        {
            Request.Session.Clear();
            string id = userService.Login(model);

            if (id == null)
            {
                return View(new { ErrorMessage = "Incorect Login" }, "/Error");
            }

            SignIn(id);

            CookieCollection cookies = new CookieCollection();
            cookies.Add(Session.SessionCookieName,
                Request.Session.Id);

            return Redirect("/Trips/All");
        }


        public Response Register()
        {
            if (User.IsAuthenticated)
            {
                return Redirect("/Trips/All");
            }

            return View(new { IsAuthenticated = false });
        }


        [HttpPost]
        public Response Register(RegisterViewModel model)
        {
            var (isRegistered, errors) = userService.Register(model);

            if (isRegistered)
            {
                return Redirect("/Users/Login");
            }

            return View(errors, "/Error");
        }


        [Authorize]
        public Response Logout()
        {
            SignOut();

            return Redirect("/Home");
        }

    }
}

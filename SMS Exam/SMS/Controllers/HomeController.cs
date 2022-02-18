using BasicWebServer.Server.Attributes;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using SMS.Contracts;


namespace SMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService productService;
        private readonly IUserService userService;

        public HomeController(
            Request request,
            IProductService _productService,
            IUserService _userService)
            : base(request)
        {
            this.productService = _productService;
            this.userService = _userService;
        }


        [HttpGet]
        public Response Index()
        {
            if (User.IsAuthenticated)
            {
                var products = productService.GetAllProducts(User.Id);
                var userName = userService.GetUsername(User.Id);

                var model = new
                {
                    Username = userName,
                    IsAuthenticated = true,
                    Products = products
                };

                return View(model, "/Home/IndexLoggedIn");
            }

            return this.View(new { IsAuthenticated = false });
        }


    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {

        private readonly ShoppingContext _context;
        public AccountController(ShoppingContext shoppingContext)
        {
            _context = shoppingContext;
        }
        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.Role = new SelectList(_context.Role.Where(x => x.Name != "Admin").ToList(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            user.JoinOn = DateTime.Today;
            user.AccessToken = Guid.NewGuid().ToString();
            _context.User.Add(user);
            _context.SaveChanges();

            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Append("user-access-token", user.AccessToken, cookieOptions);
            return Redirect("/Home/Index");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
       
        [HttpPost]

        public IActionResult Login(User user)
        {
            User DbUser = _context.User.Where(x => x.Email.ToLower().Equals(user.Email) && x.Password.Equals(user.Password)).FirstOrDefault();    
            if(DbUser == null)
            {
                ViewBag.ErrorMessage = "Email or Password is incorrect";
                return View();
            }

            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(30);
            Response.Cookies.Append("user-access-token", DbUser.AccessToken, cookieOptions);
            return Redirect("/Home/Index");
        }
        public IActionResult Logout()
        {
            Response.Cookies.Delete("user-access-token");
            return Redirect("/Home/Index");
        }
    }
}

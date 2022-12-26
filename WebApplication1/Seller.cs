using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication1.Models;

namespace WebApplication1
{
    public class Seller : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            string accessToken = context.HttpContext.Request.Cookies["user-access-token"];
            ShoppingContext _context = context.HttpContext.RequestServices.GetRequiredService<ShoppingContext>();
            User user = _context.User.Where(x => x.AccessToken == accessToken && x.Role.Name == "Seller").FirstOrDefault();

            if (user == null)
            {
                context.Result = new RedirectResult("/Account/Login");
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

    }
}

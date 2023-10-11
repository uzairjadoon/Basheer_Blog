using BasherBlog.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BasherBlog.WebUI
{
    public class AdminOrAuthorAttribute : Attribute, IActionFilter
    {

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string accessToken = context.HttpContext.Request.Cookies["user-access-token"];

            if (!string.IsNullOrEmpty(accessToken))
            {
                BasheerContext db = context.HttpContext.RequestServices.GetRequiredService<BasheerContext>();
                var test = db.Users.Where(x => x.AccessToken.Equals(accessToken)&& x.UserRole.Name.Equals("Admin") || x.UserRole.Name.Equals("Author")).Any();


                if (!test)
                {
                    context.Result = new RedirectResult("/Account/Login");
                }


            }

            else {

                context.Result = new RedirectResult("/Account/Login");
            }
        }
        void IActionFilter.OnActionExecuted(ActionExecutedContext context)
        {
          
        }


    }
}

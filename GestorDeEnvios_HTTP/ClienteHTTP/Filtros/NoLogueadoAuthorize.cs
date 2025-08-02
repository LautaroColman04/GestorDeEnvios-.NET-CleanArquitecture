using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ClienteHTTP.Filtros
{
    public class NoLogueadoAuthorize : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string? token = context.HttpContext.Session.GetString("Token");
            if (token != null)
            {
                context.Result = new RedirectToActionResult("MisEnvios", "Envio", null);
            }
            base.OnActionExecuting(context);
        }
    }
}

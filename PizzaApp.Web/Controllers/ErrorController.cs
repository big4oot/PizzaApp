using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace PizzaApp.Web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            var exceptionMessage = HttpContext.Features.Get<IExceptionHandlerFeature>().Error.Message;
            return View("Error", exceptionMessage);
        }
    }
}

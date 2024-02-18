using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaApp.Application.CQs.Invoices.Queries;
using PizzaApp.Domain.Constants;

namespace PizzaApp.Web.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly IMediator _mediator;

        public InvoicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult> Index()
        {
            var invoices = await _mediator.Send(new GetAllInvoicesQuery());
            return View(invoices);
        }
    }
}

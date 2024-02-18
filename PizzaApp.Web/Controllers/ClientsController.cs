using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaApp.Application.CQs.Clients;
using PizzaApp.Domain.Constants;

namespace PizzaApp.Web.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IMediator _mediator;

        public ClientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = nameof(Roles.Admin))]
        public async Task<IActionResult> Index()
        {
            var clients = await _mediator.Send(new GetAllClientsQuery());
            return View(clients);
        }
    }
}

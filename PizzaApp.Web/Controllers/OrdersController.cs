using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaApp.Application.Common.Interfaces;
using PizzaApp.Application.CQs.Orders.Commands.CreateOrder;
using PizzaApp.Application.CQs.Orders.Commands.PayForAllOrders;
using PizzaApp.Application.CQs.Orders.Commands.PayForOrder;
using PizzaApp.Application.CQs.Orders.Queries;
using PizzaApp.Application.CQs.Pizzas.Queries;
using PizzaApp.Domain.Constants;

namespace PizzaApp.Web.Controllers
{

    public class OrdersController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IPizzaAppDbContext _dbContext;

        public OrdersController(IMediator mediator, IPizzaAppDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var orders = await _mediator.Send(new GetAllOrdersQuery());
            return View(orders);
        }

        #region Create actions

        [Authorize(Roles = Roles.Client)]
        public async Task<IActionResult> Create()
        {
            ViewBag.AvailablePizzas = await _mediator.Send(new GetAvailablePizzasQuery());
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderCommand command)
        {
            try
            {
                var orderId = await _mediator.Send(command);
                return RedirectToAction("Details", "Orders", new { OrderId = orderId });
            }
            catch (ValidationException ex)
            {
                foreach (var error in ex.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                ViewBag.AvailablePizzas = await _mediator.Send(new GetAvailablePizzasQuery());
                return View();
            }
        }

        #endregion

        #region Details action

        [Authorize]
        public async Task<IActionResult> Details(int orderId)
        {
            if (orderId == null || orderId < 0)
            {
                return BadRequest(); 
            }
            var order = await _mediator.Send(new GetOrderByIdQuery() { Id = orderId });
            return View(order);
        }

        #endregion

        #region Pay actions

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Pay(PayForOrderCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction("Details", "Orders", new { OrderId = command.OrderId });
            return Ok();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PayForAll(PayForAllOrdersCommand command)
        {
            await _mediator.Send(command);
            return RedirectToAction("Index", "Orders");
        }

        #endregion

        #region Костыль

        /// <summary>
        /// Core admin по непонятной причине не сохраняет изменения сущности Invoice.
        /// Метод сбрасывает значение даты открытия счета в 00.00.00
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IActionResult> ResetInvoiceCreateDate(int orderId)
        {
            var invoice = _dbContext.Invoices.FirstOrDefault(i => i.OrderId == orderId);
            invoice.Created = DateTimeOffset.MinValue;
            _dbContext.Invoices.Update(invoice);
            await _dbContext.SaveChangesAsync(new CancellationToken());
            return RedirectToAction("Index", "Orders");
        }

        #endregion

    }
}

using FluentValidation;
using PizzaApp.Application.Common.Interfaces;
using PizzaApp.Domain.Entities;

namespace PizzaApp.Application.CQs.Orders.Commands.CreateOrder
{

    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        private readonly IPizzaAppDbContext _context;

        public CreateOrderCommandValidator(IPizzaAppDbContext context)
        {
            _context = context;

            RuleFor(v => v.Pizza)
                .NotNull().WithMessage("Необходимо выбрать пиццу")
                .Must(IsSelectedPizzaExist).WithMessage("Выбранная пицца недоступна");
        }

        private bool IsSelectedPizzaExist(Pizza pizza)
        {
            return _context.Pizzas.Any(p => p.Name == pizza.Name);
        }
    }
}

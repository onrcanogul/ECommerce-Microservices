using FluentValidation;
using Order.Application.Dtos;
using Shared.CQRS;
using System.Windows.Input;

namespace Order.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderCommandRequest(OrderDto Order) : ICommand<CreateOrderCommandResponse>;

    public record CreateOrderCommandResponse(Guid Id); 


    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommandRequest>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Order.CustomerId).NotEmpty().WithMessage("Customer Id is required");
            RuleFor(x => x.Order.OrderItems).NotEmpty().WithMessage("OrderItems should not be empty");
        }
    }
    
}

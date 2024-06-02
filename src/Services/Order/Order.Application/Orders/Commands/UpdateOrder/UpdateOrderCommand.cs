using FluentValidation;

namespace Order.Application.Orders.Commands.UpdateOrder
{
    public record UpdateOrderCommandRequest(OrderDto Order) : ICommand<UpdateOrderCommandResponse>;

    public record UpdateOrderCommandResponse(bool isSuccess);

    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommandRequest>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.Order.OrderName).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Order.CustomerId).NotEmpty().WithMessage("Customer Id is required");
            RuleFor(x => x.Order.Id).NotEmpty().WithMessage("Id is required");
        }
    }

}

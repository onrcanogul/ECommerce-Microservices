

using FluentValidation;

namespace Order.Application.Orders.Commands.DeleteOrder
{
    public record DeleteOrderCommandRequest(Guid OrderId) : ICommand<DeleteOrderCommandResponse>;
    public record DeleteOrderCommandResponse(bool isSuccess);

    public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommandRequest>
    {
        public DeleteOrderCommandValidator() 
        {
            RuleFor(x => x.OrderId).NotEmpty().WithMessage("Order Id is required");
        }
    }


}

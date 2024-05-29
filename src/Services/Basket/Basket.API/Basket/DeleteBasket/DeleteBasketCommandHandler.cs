
using Basket.API.Repositories;

namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommandRequest(string UserName) : ICommand<DeleteBasketCommandResponse>;
    public record DeleteBasketCommandResponse(bool IsSuccess);
    public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommandRequest>
    {
        public DeleteBasketCommandValidator() 
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");
        }

    }
    public class DeleteBasketCommandHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommandRequest, DeleteBasketCommandResponse>
    {
        public async Task<DeleteBasketCommandResponse> Handle(DeleteBasketCommandRequest request, CancellationToken cancellationToken)
        {
            await repository.DeleteBasket(request.UserName, cancellationToken);
            return new(true);
        }
    }
}

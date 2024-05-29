

using Basket.API.Repositories;
using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommandRequest(ShoppingCart Cart) : ICommand<StoreBasketCommandResponse>;
    public record StoreBasketCommandResponse(string UserName);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommandRequest>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart con not be null");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("Username is required");
        }
    }

    public class StoreBasketCommandHandler(IBasketRepository repository, DiscountProtoService.DiscountProtoServiceClient discountProto) : ICommandHandler<StoreBasketCommandRequest, StoreBasketCommandResponse>
    {
        public async Task<StoreBasketCommandResponse> Handle(StoreBasketCommandRequest request, CancellationToken cancellationToken)
        {
            //all operations could be in services 
            await DeductDiscount(request.Cart, cancellationToken);
            await repository.StoreBasket(request.Cart,cancellationToken);
            return new(request.Cart.UserName);
        }

        private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
        {
            foreach (var item in cart.Items)
            {
                var coupon = await discountProto.GetDiscountAsync(new() { ProductName = item.ProductName });
                item.Price -= coupon.Amount;
            }
        }
    }

    
}

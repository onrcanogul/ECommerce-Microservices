using Basket.API.Repositories;

namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQueryRequest(string UserName) : IQuery<GetBasketQueryResponse>;
    public record GetBasketQueryResponse(ShoppingCart Cart);

    public class GetBasketQueryHandler(IBasketRepository repository) : IQueryHandler<GetBasketQueryRequest, GetBasketQueryResponse>
    {
        public async Task<GetBasketQueryResponse> Handle(GetBasketQueryRequest request, CancellationToken cancellationToken)
        {
            ShoppingCart basket = await repository.GetBasket(request.UserName,cancellationToken);
            return new GetBasketQueryResponse(basket);
        }
    }
}

namespace Basket.API.Repositories
{
    public class BasketRepository(IDocumentSession session) : IBasketRepository
    {

        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            ShoppingCart basket = await session.LoadAsync<ShoppingCart>(userName, cancellationToken);
            if(basket == null)
            {
                throw new BasketNotFoundException(userName);
            }
            return basket;
        }
        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            session.Store(basket);
            await session.SaveChangesAsync();
            return basket;
        }
        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            session.Delete<ShoppingCart>(userName);
            await session.SaveChangesAsync();
            return true;
        }

       

        
    }
}

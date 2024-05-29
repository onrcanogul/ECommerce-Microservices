
namespace Basket.API.Basket.StoreBasket
{
    public class StoreBasketEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async (StoreBasketCommandRequest request, IMediator mediator) =>
            {
                StoreBasketCommandResponse response = await mediator.Send(request);
                return Results.Created($"/basket/{response.UserName}",response);
            })
            .WithName("Store Basket")
            .Produces(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Store Basket")
            .WithDescription("Store Basket");
        }
    }
}

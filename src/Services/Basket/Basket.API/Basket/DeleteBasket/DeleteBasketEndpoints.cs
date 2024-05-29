
namespace Basket.API.Basket.DeleteBasket
{
    public class DeleteBasketEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{userName}", async (string userName, IMediator mediator) =>
            {
                DeleteBasketCommandResponse response = await mediator.Send(new DeleteBasketCommandRequest(userName));

                return Results.Ok(response);
            })
            .WithName("DeleteBasket")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("DeleteBasket")
            .WithDescription("DeleteBasket");
        }
    }
}

namespace Basket.API.Basket.GetBasket
{
    public class GetBasketEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{userName}", async (string userName, IMediator mediator) =>
            {
                GetBasketQueryResponse response = await mediator.Send(new GetBasketQueryRequest(userName));
                return Results.Ok(response);
            })
            .WithName("GetBasket")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("GetBasket");
        }
    }
}

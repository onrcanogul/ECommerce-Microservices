
using Order.Application.Orders.Queries.GetOrdersByName;

namespace Order.API.Endpoints
{
    public class GetOrdersByName : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("orders/{orderName}", async (string orderName, IMediator mediator) =>
            {
                GetOrdersByNameQueryResponse response = await mediator.Send(new GetOrdersByNameQueryRequest(orderName));

                return Results.Ok(response); 
            })
            .WithName("GetOrdersByName")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Orders By Name")
            .WithDescription("Get Orders By Name");
        }
    }
}

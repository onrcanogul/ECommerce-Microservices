
using Order.Application.Orders.Queries.GetOrdersByCustomer;

namespace Order.API.Endpoints
{
    public class GetOrdersByCustomer : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/customer/{customerId}", async (Guid customerId, IMediator mediator) =>
            {
                GetOrdersByCustomerQueryResponse response = await mediator.Send(new GetOrdersByCustomerQueryRequest(customerId));

                return Results.Ok(response);
            })
            .WithName("GetOrdersByCustomer")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Orders By Customer")
            .WithDescription("Get Orders By Customer");
        }
    }
}

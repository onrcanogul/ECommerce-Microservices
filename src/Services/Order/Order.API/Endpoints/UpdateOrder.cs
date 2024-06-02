
using Order.Application.Orders.Commands.UpdateOrder;

namespace Order.API.Endpoints
{
    public class UpdateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/orders", async (UpdateOrderCommandRequest request, IMediator mediator) =>
            {
                UpdateOrderCommandResponse response = await mediator.Send(request);
                return Results.Ok(response);
            })
              .WithName("UpdateOrder")
              .Produces(StatusCodes.Status200OK)
              .ProducesProblem(StatusCodes.Status400BadRequest)
              .WithSummary("Update Order")
              .WithDescription("Update Order");
        }
    }
}

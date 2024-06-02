
using Order.Application.Orders.Commands.DeleteOrder;

namespace Order.API.Endpoints
{
    public class DeleteOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/orders/{Id}", async (Guid Id, IMediator mediator) =>
            {
                DeleteOrderCommandResponse response = await mediator.Send(new DeleteOrderCommandRequest(Id));

                return Results.Ok(response);
            })
             .WithName("DeleteOrder")
             .Produces(StatusCodes.Status200OK)
             .ProducesProblem(StatusCodes.Status400BadRequest)
             .WithSummary("DeleteOrder")
             .WithDescription("DeleteOrder");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Order.Application.Orders.Commands.CreateOrder;

namespace Order.API.Endpoints
{
    public class CreateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/orders", async ([FromBody]CreateOrderCommandRequest request, IMediator mediator) =>
            {
                CreateOrderCommandResponse response = await mediator.Send(request);

                return Results.Created($"/orders/{response.Id}", response);
            })
            .WithName("CreateOrder")
            .Produces(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Order")
            .WithDescription("Create Order");           
        }
    }
}

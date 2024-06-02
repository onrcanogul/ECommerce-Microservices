
using Microsoft.AspNetCore.Mvc;
using Order.Application.Orders.Queries.GetOrders;
using Shared.Pagination;

namespace Order.API.Endpoints
{
    public class GetOrders : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders", async ([AsParameters]PaginationRequest request, IMediator mediator) =>
            {
                GetOrdersQueryResponse response = await mediator.Send(new GetOrdersQueryRequest(request));

                return Results.Ok(response);
            })
             .WithName("GetOrders")
             .Produces(StatusCodes.Status200OK)
             .ProducesProblem(StatusCodes.Status400BadRequest)
             .ProducesProblem(StatusCodes.Status404NotFound)
             .WithDescription("Get Orders")
             .WithSummary("Get Orders");
        }
    }
}

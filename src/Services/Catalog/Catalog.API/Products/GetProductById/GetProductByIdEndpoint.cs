
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Products.GetProductById
{
    public class GetProductByIdEndpoint() : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async (string id, IMediator mediator) =>
            {
                GetProductByIdQueryResponse response = await mediator.Send(new GetProductByIdQueryRequest(id));
                return Results.Ok(response);
            }).WithName("GetProductById")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("GetProductById")
            .WithDescription("GetProductById");
        }
    }
}

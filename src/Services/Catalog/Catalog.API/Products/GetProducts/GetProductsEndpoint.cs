
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Products.GetProducts
{ 

    public class GetProductsEndpoint: ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async ([AsParameters]GetProductsQueryRequest request,IMediator mediator) =>
            {
                GetProductsQueryResponse response = await mediator.Send(request);
                return Results.Ok(response);
            })
            .WithName("GetProducts")
            .Produces<GetProductsQueryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithDescription("GetProducts")
            .WithSummary("GetProducts");
        }
    }
}

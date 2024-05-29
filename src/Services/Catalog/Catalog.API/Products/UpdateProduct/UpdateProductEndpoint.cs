
using Catalog.API.DTOs;
using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.UpdateProduct
{
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async (UpdateProductCommandRequest request, IMediator mediator) =>
            {
                UpdateProductCommandResponse response = await mediator.Send(request);
                return Results.Ok(response);
            }).WithName("UpdateProduct")
            .Produces<GetProductsQueryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithDescription("UpdateProduct")
            .WithSummary("UpdateProduct");
        }
    }
}

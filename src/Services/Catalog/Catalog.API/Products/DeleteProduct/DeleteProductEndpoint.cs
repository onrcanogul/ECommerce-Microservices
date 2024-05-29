
using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.DeleteProduct
{
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id}", async (string id, IMediator mediator) =>
            {
                DeleteProductCommandResponse response = await mediator.Send(new DeleteProductCommandRequest(id));
                return Results.Ok(response);
            }).WithName("DeleteProduct")
            .Produces<GetProductsQueryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithDescription("DeleteProduct")
            .WithSummary("DeleteProduct");
        }
    }
}

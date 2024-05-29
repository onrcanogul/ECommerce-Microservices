
namespace Catalog.API.Products.GetProductByCategory
{
    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, IMediator mediator) =>
            {
                GetProductByCategoryQueryResponse response = await mediator.Send(new GetProductByCategoryQueryRequest(category));

                return Results.Ok(response);
            }).WithName("GetProductByCategory")
            .Produces(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("GetProductByCategory")
            .WithDescription("GetProductByCategory"); 
        }
    }
}

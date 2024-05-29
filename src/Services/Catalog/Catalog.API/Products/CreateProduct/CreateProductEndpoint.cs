namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequest(string Name, List<string> Categories, string Description, string ImageFile, decimal Price);

    public record CreateProductResponse(Guid Id);

    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreateProductCommandRequest request, IMediator mediator) =>
            {
                CreateProductCommandResponse response = await mediator.Send(request);
                return Results.Created($"/products/{response.Id}", response);//201

            })
            .WithName("CreatedProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Product")
            .WithDescription("Create Product");
        }
    }
}

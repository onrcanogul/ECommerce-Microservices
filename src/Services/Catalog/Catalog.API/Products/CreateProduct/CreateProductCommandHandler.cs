namespace Catalog.API.Products.CreateProduct
{
    //Request
    public record CreateProductCommandRequest(string Name, List<string> Categories, string Description, string ImageFile, decimal Price) : ICommand<CreateProductCommandResponse>;

    //Response
    public record CreateProductCommandResponse(Guid Id);

    //Validator
    public class CreateProductCommandValidator: AbstractValidator<CreateProductCommandRequest>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Categories).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Name is required").GreaterThan(0).WithMessage("Price must be greater than 0");
           
        }
    }

    //Handler
    public class CreateProductCommandHandler(CatalogDbContext context) : ICommandHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest command, CancellationToken cancellationToken)
        {
            Product product = new Product()
            {
                Name = command.Name,
                Category = command.Categories,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();   

            return new CreateProductCommandResponse(product.Id);
        }
    }
}

using Catalog.API.DTOs;
using Catalog.API.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommandRequest(string Id,string Name, List<string> Categories, string Description, string ImageFile, decimal Price) : ICommand<UpdateProductCommandResponse>;

    public record UpdateProductCommandResponse(bool IsSuccess);


    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommandRequest>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID is required");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Image file is required").GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }

    

    public class UpdateProductCommandHandler(CatalogDbContext context) : ICommandHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {

            Product? product = await context.Products.FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.Id));
            if (product != null)
            {
                product.Price = request.Price;
                product.Name = request.Name;
                product.Category = request.Categories;
                product.Description = request.Description;
                product.ImageFile = request.ImageFile;
                context.Products.Update(product);
                await context.SaveChangesAsync();
            }
            else throw new ProductNotFoundException(request.Id);

            return new(true);
        }
    }
}

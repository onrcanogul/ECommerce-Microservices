
using Catalog.API.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommandRequest(string Id) : ICommand<DeleteProductCommandResponse>;

    public record DeleteProductCommandResponse(bool IsSuccess);


    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommandRequest>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        }
    }


    public class DeleteProductCommandHandler(CatalogDbContext context) : ICommandHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
    {
        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            Product? product = await context.Products.FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.Id));
            if(product != null)
            {
                context.Products.Remove(product);
                await context.SaveChangesAsync();
            }
            else throw new ProductNotFoundException(request.Id);

            return new(true);
        }
    }
}

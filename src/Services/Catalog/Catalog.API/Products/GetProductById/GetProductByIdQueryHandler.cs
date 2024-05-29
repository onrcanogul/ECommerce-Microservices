
using Catalog.API.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Products.GetProductById
{

    public record GetProductByIdQueryRequest(string Id) : IQuery<GetProductByIdQueryResponse>;

    public record GetProductByIdQueryResponse(Product Product);
    public class GetProductByIdQueryHandler(CatalogDbContext context) : IQueryHandler<GetProductByIdQueryRequest, GetProductByIdQueryResponse>
    {
        public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
        {
            Product? product = await context.Products.FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));
            if(product != null)
            {
                return new(product); 
            }
            throw new ProductNotFoundException(request.Id);

        }
    }
}

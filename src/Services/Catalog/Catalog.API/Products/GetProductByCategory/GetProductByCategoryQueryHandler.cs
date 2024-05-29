
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryQueryRequest(string Category)  : IQuery<GetProductByCategoryQueryResponse>; 

    public record GetProductByCategoryQueryResponse(List<Product> Products);



    public class GetProductByCategoryQueryHandler(CatalogDbContext context) : IQueryHandler<GetProductByCategoryQueryRequest, GetProductByCategoryQueryResponse>
    {
        public async Task<GetProductByCategoryQueryResponse> Handle(GetProductByCategoryQueryRequest request, CancellationToken cancellationToken)
        {
            List<Product> products = await  context.Products.Where(x => x.Category.Any(x => x == request.Category)).ToListAsync(cancellationToken);

            return new(products);

        }
    }
}

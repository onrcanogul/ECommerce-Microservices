

using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Products.GetProducts
{

    public record GetProductsQueryRequest(int? Page, int? Size) : IQuery<GetProductsQueryResponse>;


    public record GetProductsQueryResponse(List<Product> Products);

    public class GetProductsQueryHandler(CatalogDbContext context) : IQueryHandler<GetProductsQueryRequest, GetProductsQueryResponse>
    {
        public async Task<GetProductsQueryResponse> Handle(GetProductsQueryRequest request, CancellationToken cancellationToken)
        {
            List<Product> products = await context.Products.Skip((request.Page ?? 0) * (request.Size ?? 5)).Take(request.Size ?? 5).ToListAsync();

            return new(products);
        }
    }
}

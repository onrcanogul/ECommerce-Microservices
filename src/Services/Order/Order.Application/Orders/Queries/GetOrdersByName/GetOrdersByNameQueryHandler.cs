using Microsoft.EntityFrameworkCore;
using Order.Application.Extensions;
using System;
namespace Order.Application.Orders.Queries.GetOrdersByName
{
    public class GetOrdersByNameQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByNameQueryRequest, GetOrdersByNameQueryResponse>
    {
        public async Task<GetOrdersByNameQueryResponse> Handle(GetOrdersByNameQueryRequest request, CancellationToken cancellationToken)
        {
            
            var orders = await dbContext.Orders
                .Include(x => x.OrderItems)
                .AsNoTracking() //optimization to read data.
                .Where(x => x.OrderName.Value.Contains(request.Name))
                .OrderBy(o => o.OrderName.Value)
                .ToListAsync(cancellationToken);


            return new(orders.ToOrderDtoList());
        }

        


    }
}

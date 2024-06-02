using Microsoft.EntityFrameworkCore;
using Order.Application.Extensions;
using Order.Application.Orders.Queries.GetOrdersByName;

namespace Order.Application.Orders.Queries.GetOrdersByCustomer
{
    public class GetOrdersByCustomerQueryHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByCustomerQueryRequest, GetOrdersByCustomerQueryResponse>
    {
        public async Task<GetOrdersByCustomerQueryResponse> Handle(GetOrdersByCustomerQueryRequest request, CancellationToken cancellationToken)
        {
            var orders = await dbContext.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .Where(o => o.CustomerId == CustomerId.Of(request.CustomerId))
                .OrderBy(o => o.OrderName.Value)
                .ToListAsync();

            return new(orders.ToOrderDtoList());
         }
    }
}

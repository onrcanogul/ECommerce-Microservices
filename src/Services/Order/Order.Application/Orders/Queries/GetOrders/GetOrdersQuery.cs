
using Shared.Pagination;

namespace Order.Application.Orders.Queries.GetOrders
{
    public record GetOrdersQueryRequest(PaginationRequest PaginationRequest) : IQuery<GetOrdersQueryResponse>;
    
    public record GetOrdersQueryResponse(PaginatedResult<OrderDto> Orders);
    
}

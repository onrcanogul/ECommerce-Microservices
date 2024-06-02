

namespace Order.Application.Orders.Queries.GetOrdersByName
{
    public record GetOrdersByNameQueryRequest(string Name) : IQuery<GetOrdersByNameQueryResponse>;
    
    public record GetOrdersByNameQueryResponse(IEnumerable<OrderDto> Orders);
}

namespace Order.Application.Orders.Queries.GetOrdersByCustomer
{
    public record GetOrdersByCustomerQueryRequest(Guid CustomerId) : IQuery<GetOrdersByCustomerQueryResponse>;
    public record GetOrdersByCustomerQueryResponse(IEnumerable<OrderDto> Orders);
    
}


using System.Data;

namespace Order.Domain.Models
{
    public class Order : Aggregate<OrderId>
    {
        private readonly List<OrderItem> _orderItems = new();
        public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();


        public CustomerId CustomerId { get; set; } = default!;
        public OrderName OrderName { get; private set; } = default!;
        public Address ShippingAddress { get; private set; } = default!;
        public Address BillingAddress { get; private set; } = default!;
        public Payment Payment { get; private set; } = default!;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public decimal TotalPrice 
        {
            get => OrderItems.Sum(x => x.Price * x.Quantity);
            private set { } 
        }


        public static Order Create(OrderId id, CustomerId customerId, OrderName ordername, Address shippingAdress, Address billingAddress, Payment payment, OrderStatus orderStatus)
        {
            Order order = new()
            {
                Id = id,
                CustomerId = customerId,
                OrderName = ordername,
                Payment = payment,
                BillingAddress = billingAddress,
                ShippingAddress = shippingAdress,
                Status = orderStatus
            };

            order.AddDomainEvent(new OrderCreatedEvent(order));
            return order;
        }

        public void Update(OrderName orderName, Address shippingAddress, Address billingAddress,Payment payment, OrderStatus status)
        {
            OrderName = orderName;
            ShippingAddress = shippingAddress;
            BillingAddress = billingAddress;
            Payment = payment;
            Status = status;


            AddDomainEvent(new OrderUpdatedEvent(this));
        }

        public void Add(ProductId id, int quantity, decimal price)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            OrderItem orderItem = new(Id , id, quantity, price);
            _orderItems.Add(orderItem);     
        }

        public void Remove(ProductId id)
        {
            OrderItem? orderItem = _orderItems.FirstOrDefault(oi => oi.ProductId == id);
            if(orderItem is not null)
            {
                _orderItems.Remove(orderItem);
            }

        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Orders.Commands.DeleteOrder
{
    public  class DeleteOrderCommandHandler(IApplicationDbContext dbContext) : ICommandHandler<DeleteOrderCommandRequest, DeleteOrderCommandResponse>
    {
        public async Task<DeleteOrderCommandResponse> Handle(DeleteOrderCommandRequest request, CancellationToken cancellationToken)
        {
            var orderId = OrderId.Of(request.OrderId);
            
            var order = await dbContext.Orders.FindAsync([orderId],cancellationToken:cancellationToken);

            if(order is null)
            {
                throw new OrderNotFoundException(request.OrderId);
            }

            dbContext.Orders.Remove(order);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new(true);
        }
    }
}

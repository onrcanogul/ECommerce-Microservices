using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.Events
{
    public record OrderUpdatedEvent(Models.Order order) : IDomainEvent; 
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.ValueObjects
{
    public record OrderItemId
    {
        public Guid Value { get; }
        private OrderItemId(Guid value) => Value = value;
        public static OrderItemId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if(value == Guid.Empty)
            {
                throw new DomainException("OrderItem id cannot be null");
            }

            return new OrderItemId(value);
        }
    }
}

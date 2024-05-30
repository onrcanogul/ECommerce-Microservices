using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Models;
using Order.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasConversion(
                orderItemId => orderItemId.Value,
                dbId => OrderItemId.Of(dbId));

            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(oi => oi.ProductId);


            builder.Property(x => x.Quantity).IsRequired();

            builder.Property(x => x.Price).IsRequired();
        }
    }
}

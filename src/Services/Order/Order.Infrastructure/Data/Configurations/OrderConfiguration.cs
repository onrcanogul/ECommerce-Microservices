﻿using Microsoft.EntityFrameworkCore;
using Order.Domain.Enums;
using Order.Domain.Models;
using Order.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Domain.Models.Order>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Domain.Models.Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasConversion(
                orderId => orderId.Value,
                dbId => OrderId.Of(dbId));

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .IsRequired();

            builder.HasMany(o => o.OrderItems)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId);

            builder.ComplexProperty(
                o => o.OrderName, nameBuilder =>
                {
                    nameBuilder.Property(n => n.Value)
                    .HasColumnName(nameof(Domain.Models.Order.OrderName))
                    .HasMaxLength(100)
                    .IsRequired();
                });

            builder.ComplexProperty(
          o => o.ShippingAddress, addressBuilder =>
          {
              addressBuilder.Property(a => a.FirstName)
                  .HasMaxLength(50)
                  .IsRequired();

              addressBuilder.Property(a => a.LastName)
                   .HasMaxLength(50)
                   .IsRequired();

              addressBuilder.Property(a => a.EmailAddress)
                  .HasMaxLength(50);

              addressBuilder.Property(a => a.AddressLine)
                  .HasMaxLength(180)
                  .IsRequired();

              addressBuilder.Property(a => a.Country)
                  .HasMaxLength(50);

              addressBuilder.Property(a => a.State)
                  .HasMaxLength(50);

              addressBuilder.Property(a => a.ZipCode)
                  .HasMaxLength(5)
                  .IsRequired();
          });

            builder.ComplexProperty(
         o => o.BillingAddress, addressBuilder =>
         {
             addressBuilder.Property(a => a.FirstName)
                  .HasMaxLength(50)
                  .IsRequired();

             addressBuilder.Property(a => a.LastName)
                  .HasMaxLength(50)
                  .IsRequired();

             addressBuilder.Property(a => a.EmailAddress)
                 .HasMaxLength(50);

             addressBuilder.Property(a => a.AddressLine)
                 .HasMaxLength(180)
                 .IsRequired();

             addressBuilder.Property(a => a.Country)
                 .HasMaxLength(50);

             addressBuilder.Property(a => a.State)
                 .HasMaxLength(50);

             addressBuilder.Property(a => a.ZipCode)
                 .HasMaxLength(5)
                 .IsRequired();
         });
            builder.ComplexProperty(
               o => o.Payment, paymentBuilder =>
               {
                   paymentBuilder.Property(p => p.CardName)
                       .HasMaxLength(50);

                   paymentBuilder.Property(p => p.CardNumber)
                       .HasMaxLength(24)
                       .IsRequired();

                   paymentBuilder.Property(p => p.Expiration)
                       .HasMaxLength(10);

                   paymentBuilder.Property(p => p.CVV)
                       .HasMaxLength(3);

                   paymentBuilder.Property(p => p.PaymentMethod);
               });

            builder.Property(o => o.Status)
                .HasDefaultValue(OrderStatus.Draft)
                .HasConversion(
                    s => s.ToString(),
                    dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

            builder.Property(x => x.TotalPrice);

    }
    }
}

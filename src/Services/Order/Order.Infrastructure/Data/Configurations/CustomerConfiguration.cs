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
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).HasConversion(
                customerId => customerId.Value, //storing information to database
                dbId => CustomerId.Of(dbId) //reading from database
                );

            builder.Property(c => c.Name).HasMaxLength(100).IsRequired();

            builder.Property(c => c.Email).HasMaxLength(225);

            builder.HasIndex(c => c.Email).IsUnique();
        }
    }
}

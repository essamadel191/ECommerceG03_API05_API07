using ECommerceG03.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Infrastructure.Data.Configurations
{
    public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(d => d.Price).HasColumnType("DECIMAL(8,2)");
            builder.Property(d => d.ShortName).HasMaxLength(50);
            builder.Property(d => d.Description).HasMaxLength(100);
            builder.Property(d => d.DeliveryTime).HasMaxLength(50);
        }
    }
}

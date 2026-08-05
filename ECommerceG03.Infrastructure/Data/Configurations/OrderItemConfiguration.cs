using ECommerceG03.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Infrastructure.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(x => x.Price).HasColumnType("DECIMAL(8,2)");

            // To Prevent creating many to many with ProductItemOrdered
            builder.OwnsOne(x => x.Product, product =>
            {
                product.Property(p => p.ProductName).HasMaxLength(100);
                product.Property(p => p.PictureUrl).HasMaxLength(200);
            });
        }
    }
}

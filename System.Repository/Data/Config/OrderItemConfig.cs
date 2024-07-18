using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Domain.Entities;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Repository.Data.Config
{
    public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasOne(OI => OI.Product)
                .WithMany()
                .HasForeignKey(OI => OI.ProductId);
            builder.Property(OI => OI.UnitPrice).HasColumnType("decimal(18,2)");
            builder.Property(OI => OI.Discount).HasColumnType("decimal(18,2)");

        }
    }
}

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
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasMany(O => O.OrderItems)
                .WithOne();


            builder.Property(O => O.Status)
                .HasConversion(
                    o => o.ToString(),
                    o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o));

            builder.Property(O => O.PaymentMethod)
                .HasConversion(
                    o => o.ToString(),
                    o => (PaymentMethod)Enum.Parse(typeof(PaymentMethod), o)
                    );

            builder.Property(O => O.TotalAmount).HasColumnType("decimal(18,2)");


        }
    }
}

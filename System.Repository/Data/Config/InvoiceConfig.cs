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
    public class InvoiceConfig : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasOne(I => I.Order)
                .WithOne()
                .HasForeignKey<Invoice>(I => I.OrderId);
            builder.Property(I => I.TotalAmount).HasColumnType("decimal(18,2)");
        }
    }
}

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
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasMany(C => C.Orders)
                .WithOne();
            builder.Property(C => C.Name).IsRequired().HasMaxLength(50);
            builder.Property(C => C.Email).IsRequired();
        }
    }
}

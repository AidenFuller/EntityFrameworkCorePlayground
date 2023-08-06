using FrameworkLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCorePlayground
{
    [EntityTypeConfiguration(typeof(NewTestEntityConfiguration))]
    public class NewTestEntity : IEntity
    {
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }

        public int QuantitySold { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTimeOffset TimeAdded { get; set; }
    }

    public class NewTestEntityConfiguration : IEntityTypeConfiguration<NewTestEntity>
    {
        public void Configure(EntityTypeBuilder<NewTestEntity> builder)
        {
            builder
                .HasKey(x => new { x.ProductCode, x.ProductDescription });

            builder.Property(x => x.ProductCode)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.ProductDescription)
                .IsRequired();

            builder.Property(x => x.UnitPrice)
                .HasPrecision(18, 6);

            builder.Property(x => x.TotalPrice)
                .HasPrecision(18, 6);

            builder.Property(x => x.TimeAdded);                
        }
    }
}

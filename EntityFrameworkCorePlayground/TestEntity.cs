using FrameworkLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFrameworkCorePlayground
{
    [EntityTypeConfiguration(typeof(TestEntityConfiguration))]
    public class TestEntity : IdentifiableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public bool IsRequired { get; set; }
    }

    public class TestEntityConfiguration : IEntityTypeConfiguration<TestEntity>
    {
        public void Configure(EntityTypeBuilder<TestEntity> builder)
        {
            builder
                .HasKey(x => x.Id);
            builder
                .Property(p => p.Name)
                .HasMaxLength(200)
                .IsRequired();
            builder
                .Property(p => p.Description)
                .HasMaxLength(500);
            builder
                .Property(p => p.Amount)
                .HasPrecision(9, 6)
                .IsRequired();
            builder
                .Property(p => p.IsRequired);
        }
    }
}

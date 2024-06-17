using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pryaniky.CRUD.DataAccess.Entities;

namespace Pryaniky.CRUD.DataAccess.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.HasOne(x => x.Order)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.OrderId);
        }
    }
}

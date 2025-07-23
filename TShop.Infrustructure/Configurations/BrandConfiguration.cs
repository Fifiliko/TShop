using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TShop.Domain.Entities;
namespace TShop.Infrustructure.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("Brands");
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name)
                   .IsRequired()
                   .HasMaxLength(30)
                   .HasColumnType("nvarchar(30)");
            builder.ToTable("Brands", t =>
            {
                t.HasCheckConstraint("CK_Brands_Name_ValidChars",
                    "Name NOT LIKE '%[^a-zA-Z0-9 _-]%'");
            });
        }
    }
}

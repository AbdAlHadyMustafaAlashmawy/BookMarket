using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BookMarket.Models.ModelSpecialConfigurations
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Cost).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(x => x.Kind).IsRequired();
            builder.Property(x => x.PictureUrl);
            builder.Property(x => x.Description);
            builder.Property(x => x.SellingTimes).IsRequired();

            // Configure Writer relationship
            builder.HasOne(b => b.Writer)
                   .WithMany(w => w.My_Books)
                   .HasForeignKey(b => b.WriterId)
                   .OnDelete(DeleteBehavior.Cascade); // Cascade delete behavior

            // Configure Producer relationship
            builder.HasOne(b => b.Producer)
                   .WithMany(p => p.Books)
                   .HasForeignKey(b => b.ProducerId)
                   .OnDelete(DeleteBehavior.Cascade); // Cascade delete behavior

            builder.ToTable("Books");
        }
    }
}
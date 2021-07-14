namespace BoostUp.Data
{
    using BoostUp.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class BoostUpDbContext : IdentityDbContext
    {
        public BoostUpDbContext(DbContextOptions<BoostUpDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Company>()
                .HasOne(c => c.Industry)
                .WithMany(i => i.Companies)
                .HasForeignKey(c => c.IndustryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Company>()
                .HasOne(c => c.Category)
                .WithMany(c => c.Companies)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }

        public DbSet<Company> Companies { get; init; }

        public DbSet<Industry> Industries { get; init; }

        public DbSet<Category> Categories { get; init; }
    }
}

namespace BoostUp.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    using BoostUp.Data.Models;

    public class BoostUpDbContext : IdentityDbContext<User>
    {
        public BoostUpDbContext(DbContextOptions<BoostUpDbContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Companies { get; init; }

        public DbSet<Industry> Industries { get; init; }

        public DbSet<Category> Categories { get; init; }

        public DbSet<Job> Jobs { get; init; }

        public DbSet<Address> Addresses { get; init; }

        public DbSet<Recruiter> Recruiters { get; init; }

        public DbSet<EmploymentType> EmploymentTypes { get; init; }

        public DbSet<Image> Images { get; init; }

        public DbSet<Friendship> Friendships { get; init; }

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

            builder
                .Entity<Company>()
                .HasOne(c => c.Address)
                .WithMany(a => a.Companies)
                .HasForeignKey(c => c.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Job>()
                .HasOne(j => j.Company)
                .WithMany(c => c.Jobs)
                .HasForeignKey(j => j.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                 .Entity<Job>()
                 .HasOne(j => j.Recruiter)
                 .WithMany(r => r.Jobs)
                 .HasForeignKey(j => j.RecruiterId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Job>()
                .HasOne(j => j.Address)
                .WithMany(a => a.Jobs)
                .HasForeignKey(j => j.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Job>()
                .HasOne(j => j.EmploymentType)
                .WithMany(et => et.Jobs)
                .HasForeignKey(j => j.EmploymentTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Recruiter>()
                .HasOne(r => r.Company)
                .WithMany(c => c.Recruiters)
                .HasForeignKey(r => r.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Recruiter>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Recruiter>(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<User>()
                .HasOne(u => u.Address)
                .WithMany(a => a.Users)
                .HasForeignKey(u => u.AddressId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
               .Entity<User>()
               .HasOne(u => u.Company)
               .WithMany(c => c.Employees)
               .HasForeignKey(u => u.CompanyId)
               .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}

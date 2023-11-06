using AfvalScheiden.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AfvalScheiden.Data
{
    public class AfvalDbContext : IdentityDbContext
    {

        public AfvalDbContext() { }

        
        public AfvalDbContext(DbContextOptions<AfvalDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure a primary key for IdentityUserLogin
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(p => new { p.LoginProvider, p.ProviderKey });

            modelBuilder.Entity<Person>()
                .HasOne(p => p.Logbook)
                .WithOne(l => l.Person)
                .HasForeignKey<Logbook>(l => l.PersonId);
        }


        public DbSet<Garbage> Garbages { get; set; }

        public DbSet<GarbageCategory> GarbageCategories { get; set; }
        public DbSet<Government> Governments { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Logbook> Logbooks { get; set; }

    }
}
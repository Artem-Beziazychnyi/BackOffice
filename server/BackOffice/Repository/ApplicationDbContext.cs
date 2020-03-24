
using BackOffice.Models;
using Microsoft.EntityFrameworkCore;

namespace BackOffice.Repository
{
    /// <summary>
    ///    Represents a db context for handle all operations with database
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Constructor that initializes a application db context
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        /// <summary>
        /// Property represents db set which works with Brands table
        /// </summary>
        public DbSet<Brand> Brands { set; get; }
        
        /// <summary>
        /// Property represents db set which works with BrandQuantitiesTimeReceived table
        /// </summary>
        public DbSet<BrandQuantityTimeReceived> BrandQuantitiesTimeReceived { get; set; }
        
        /// <summary>
        /// Override OnModelCreating by adding custom configuration to ModelBuilder
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Brand>()
                .HasIndex(u => u.Name)
                .IsUnique();
            
            builder.Entity<Brand>()
                .HasMany<BrandQuantityTimeReceived>()
                .WithOne(x=>x.Brand)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
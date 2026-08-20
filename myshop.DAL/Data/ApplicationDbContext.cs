using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using myshop.DAL.Data.Configuration;
using myshop.DAL.Models;
using myshop.DAL.Models.Interfaces;
using myshop.Entities.Models;

namespace myshop.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(CategoryConfiguration).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(OrderConfiguration).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(ReviewConfiguration).Assembly);

            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var deleteEntities = ChangeTracker.Entries<ISoftDelete>()
                .Where(E => E.State == EntityState.Deleted)
                .ToList();
            foreach(var entry in deleteEntities)
            {
                entry.State = EntityState.Modified;
                var entity = (ISoftDelete)entry.Entity;
                entity.IsDeleted = true;
                entity.DeletedAt = DateOnly.FromDateTime(DateTime.Now);

            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}

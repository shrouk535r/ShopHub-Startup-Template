using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using myshop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DAL.Data.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.FName)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.LName).IsRequired().HasMaxLength(50);
            builder.Property(u => u.City).HasMaxLength(200);
            builder.Property(u => u.Address).HasMaxLength(2000);

            builder.HasQueryFilter(p => !p.IsDeleted);
            builder.HasOne(u => u.Cart)
                .WithOne(c => c.ApplicationUser);

            builder.HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.Orders)
                .WithOne(o => o.ApplicationUser)
                .HasForeignKey(o => o.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(u => u.Cart)
                .WithOne(c => c.ApplicationUser)
                .HasForeignKey<ShoppingCart>(c => c.ApplicationUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

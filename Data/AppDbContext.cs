﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserSubscriptionWebApi.Models;
using UserSubscriptionWebApi.Models.DTOs;

namespace UserSubscriptionWebApi.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<SubscriptionType> SubscriptionTypes { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SubscriptionType>()
               .HasIndex(p => p.Name)
               .IsUnique();

            modelBuilder.Entity<Product>()
               .Property(b => b.IsDeleted)
               .HasDefaultValue(false);

            modelBuilder.Entity<Subscription>()
                     .HasOne<ApplicationUser>(r => r.ApplicationUser)
                    .WithMany(b => b.Subscriptions)
                    .HasForeignKey(b => b.ApplicationUserId)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Subscription>()
                      .HasOne<Product>(r => r.Product)
                     .WithMany(b => b.Subscriptions)
                     .HasForeignKey(b => b.ProductId)
                     .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Subscription>()
                     .HasOne<SubscriptionType>(r => r.SubscriptionType)
                    .WithMany(b => b.Subscriptions)
                    .HasForeignKey(b => b.SubscriptionTypeId)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Subscription>()
                    .Property(s => s.IsActive)
                    .HasComputedColumnSql( "CASE WHEN [StartDate] <= CURRENT_DATE AND [EndDate] >= CURRENT_DATE THEN true ELSE false END",stored:true);

        }
    }
}

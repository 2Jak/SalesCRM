using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SalesCRM.Data.Models;

namespace SalesCRM.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Lead>()
                .HasMany<FreeText>(lead => lead.FreeTexts)
                .WithOne(ft => ft.Lead)
                .HasForeignKey(ft => ft.LeadID);
            base.OnModelCreating(builder);
        }

        public DbSet<Lead> Leads { get; set; }
        public DbSet<FreeText> FreeTexts { get; set; }
        public DbSet<Course> Courses { get; set; }
    }
}

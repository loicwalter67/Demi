using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Demi.Domain.Entities;

namespace Demi.Infrastructure.Context
{
	public class DemiDbContext : IdentityDbContext<User>
	{
		public DemiDbContext(DbContextOptions<DemiDbContext> options) : base(options)
		{
			Database.Migrate();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//define the relationship between User and Form
			modelBuilder.Entity<User>().HasMany(f => f.RequestedForms).WithOne(u => u.RequesterUser).HasForeignKey(f => f.RequesterId).IsRequired(true);
			modelBuilder.Entity<User>().HasMany(f => f.ApprovedForms).WithOne(u => u.ApprovalUser).HasForeignKey(f => f.ApprovalUserId).IsRequired(false);
			
			modelBuilder.Entity<Form>().HasMany(f => f.Sources).WithOne(s => s.Form).HasForeignKey(s => s.FormId).IsRequired(true);

			base.OnModelCreating(modelBuilder);
		}

		public DbSet<Form> Forms { get; set; }
		public DbSet<Source> Sources { get; set; }
	}
}

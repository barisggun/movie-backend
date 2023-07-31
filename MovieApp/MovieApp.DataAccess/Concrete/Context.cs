
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieApp.EntityLayer.Entities;
using MovieApp.EntityLayer.Entities.ConnectionClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.DataAccess.Concrete
{
    public class Context : IdentityDbContext<AppUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //optionsBuilder.UseSqlServer("Server=104.247.162.242\\MSSQLSERVER2017;Database=akadem58_movie01;User Id=akadem58_movie01;Password=&7al1Y20h;TrustServerCertificate=True");

            optionsBuilder.UseSqlServer("Server=104.247.162.242\\MSSQLSERVER2019;Database=sinesozl_database;User Id=sinesozl_admin;Password=843261Sb*!;TrustServerCertificate=true");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>()
                        .HasMany(m => m.Movies)
                        .WithMany(a => a.Actors)
                        .UsingEntity<ActorMovie>(
                j => j.HasOne(ma => ma.Movie).WithMany().HasForeignKey(ma => ma.MovieId),
                j => j.HasOne(ma => ma.Actor).WithMany().HasForeignKey(ma => ma.ActorId),
                j =>
                {
                    j.HasKey(ma => new { ma.MovieId, ma.ActorId });
                    j.ToTable("ActorMovie");
                }
                );
            modelBuilder.Entity<Director>()
                        .HasMany(m => m.Movies)
                        .WithMany(d => d.Directors)
                        .UsingEntity<DirectorMovie>(
                j => j.HasOne(md => md.Movie).WithMany().HasForeignKey(of => of.MovieId),
                j => j.HasOne(md => md.Director).WithMany().HasForeignKey(of => of.DirectorId),
                j =>
                {
                    j.HasKey(md => new { md.MovieId, md.DirectorId });
                    j.ToTable("DirectorMovie");
                }
                );
            modelBuilder.Entity<Category>()
                        .HasMany(m => m.Movies)
                        .WithMany(c => c.Categories)
                        .UsingEntity<CategoryMovie>(
                j => j.HasOne(mc => mc.Movie).WithMany().HasForeignKey(mc => mc.MovieId),
                j => j.HasOne(mc => mc.Category).WithMany().HasForeignKey(mc => mc.CategoryId),
                j =>
                {
                    j.HasKey(mc => new { mc.MovieId, mc.CategoryId });
                    j.ToTable("CategoryMovie");
                }
                );


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<HomepageCover> HomepageCovers { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<WatchList> WatchLists { get; set; }
        public DbSet<WatchedList> WatchedLists { get; set; }
        public DbSet<AppSetting> AppSettings { get; set; }

    }
}

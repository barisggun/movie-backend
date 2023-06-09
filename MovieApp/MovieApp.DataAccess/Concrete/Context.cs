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
    public class Context : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("server =(localdb)\\MSSQLLocalDB;database=MovieBackend;Trusted_Connection=true;TrustServerCertificate=true");

        }
        //OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Actor>()
        //                .HasMany(m => m.Movies)
        //                .WithMany(a => a.Actors)
        //                .UsingEntity<ActorMovie>(
        //        j => j.HasOne(ma => ma.Movie).WithMany().HasForeignKey(ma => ma.MovieId),
        //        j => j.HasOne(ma => ma.Actor).WithMany().HasForeignKey(ma => ma.ActorId),
        //        j =>
        //        {
        //            j.HasKey(ma => new { ma.MovieId, ma.ActorId });
        //            j.ToTable("ActorMovie");
        //        }
        //        );
        //    modelBuilder.Entity<Director>()
        //                .HasMany(m => m.Movies)
        //                .WithMany(d => d.Directors)
        //                .UsingEntity<DirectorMovie>(
        //        j => j.HasOne(md => md.Movie).WithMany().HasForeignKey(of => of.MovieId),
        //        j => j.HasOne(md => md.Director).WithMany().HasForeignKey(of => of.DirectorId),
        //        j =>
        //        {
        //            j.HasKey(md => new { md.MovieId, md.DirectorId });
        //            j.ToTable("DirectorMovie");
        //        }
        //        );
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Actor>()
        //                .HasMany(m => m.Movies)
        //                .WithMany(a => a.Actors)
        //                .UsingEntity<ActorMovie>(
        //        j => j.HasOne(ma => ma.Movie).WithMany().HasForeignKey(ma => ma.MovieId),
        //        j => j.HasOne(ma => ma.Actor).WithMany().HasForeignKey(ma => ma.ActorId),
        //        j =>
        //        {
        //            j.HasKey(ma => new { ma.MovieId, ma.ActorId });
        //            j.ToTable("ActorMovie");
        //        }
        //        );
        //    modelBuilder.Entity<Director>()
        //                .HasMany(m => m.Movies)
        //                .WithMany(d => d.Directors)
        //                .UsingEntity<DirectorMovie>(
        //        j => j.HasOne(md => md.Movie).WithMany().HasForeignKey(of => of.MovieId),
        //        j => j.HasOne(md => md.Director).WithMany().HasForeignKey(of => of.DirectorId),
        //        j =>
        //        {
        //            j.HasKey(md => new { md.MovieId, md.DirectorId });
        //            j.ToTable("DirectorMovie");
        //        }
        //        );
        //}

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
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Director> Directors { get; set; }

    }
}

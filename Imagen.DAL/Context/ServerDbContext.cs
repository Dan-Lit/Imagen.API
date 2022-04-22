using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Imagen.DAL.Models;

namespace Imagen.DAL.Context
{
    public partial class ServerDbContext : DbContext
    {
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Imagetagconfig> Imagetagconfig { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        public ServerDbContext(DbContextOptions<ServerDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-KRAF49Q7;Initial Catalog=ImageSoftware;Integrated Security=True;Trust Server Certificate=True;Command Timeout=300");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("image");

                entity.Property(e => e.ImageId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("imageID");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("imageURL");

                entity.Property(e => e.Processed)
                    .HasColumnName("processed")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Tagged)
                    .HasColumnName("tagged")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Image)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__image__userID__403A8C7D");
            });

            modelBuilder.Entity<Imagetagconfig>(entity =>
            {
                entity.HasKey(e => new { e.ImageId, e.TagId })
                    .HasName("PK__imagetag__56615B620752481E");

                entity.ToTable("imagetagconfig");

                entity.Property(e => e.ImageId)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("imageID");

                entity.Property(e => e.TagId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tagID");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Imagetagconfig)
                    .HasForeignKey(d => d.ImageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__imagetagc__image__44FF419A");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.Imagetagconfig)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__imagetagc__tagID__45F365D3");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("tag");

                entity.Property(e => e.TagId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tagID");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__usuario__CB9A1CDF84302488");

                entity.ToTable("usuario");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

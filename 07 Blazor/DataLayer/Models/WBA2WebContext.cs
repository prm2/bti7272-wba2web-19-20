using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataLayer.Models
{
    public partial class WBA2WebContext : DbContext
    {
        public WBA2WebContext()
        {
        }

        public WBA2WebContext(DbContextOptions<WBA2WebContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<AccountGroup> AccountGroup { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=corpus.bfh.ch,55783;Database=WBA2Web;user=scott;password=tiger");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => new { e.Type, e.FunctionId, e.SubjectId, e.Number });

                entity.Property(e => e.Type).HasMaxLength(2);

                entity.Property(e => e.FunctionId).HasMaxLength(8);

                entity.Property(e => e.SubjectId).HasMaxLength(8);

                entity.Property(e => e.FunctionType)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.Function)
                    .WithMany(p => p.FunctionAccounts)
                    .HasForeignKey(d => new { d.FunctionType, d.FunctionId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Function");

                entity.HasOne(d => d.AccountGroup)
                    .WithMany(p => p.SubjectAccounts)
                    .HasForeignKey(d => new { d.Type, d.SubjectId })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Subject");
            });

            modelBuilder.Entity<AccountGroup>(entity =>
            {
                entity.HasKey(e => new { e.Type, e.Id });

                entity.Property(e => e.Type).HasMaxLength(2);

                entity.Property(e => e.Id).HasMaxLength(8);

                entity.Property(e => e.Description).HasMaxLength(2000);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.ParentId).HasMaxLength(8);

                entity.HasOne(d => d.ParentGroup)
                    .WithMany(p => p.SubGroups)
                    .HasForeignKey(d => new { d.Type, d.ParentId })
                    .HasConstraintName("FK_AccountGroup_Parent");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

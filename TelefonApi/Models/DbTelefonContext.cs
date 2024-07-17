using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TelefonApi.Models;

public partial class DbTelefonContext : DbContext
{
    public DbTelefonContext()
    {
    }

    public DbTelefonContext(DbContextOptions<DbTelefonContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TelefonTbl> TelefonTbls { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=MyDatabase");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TelefonTbl>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TelefonT__3213E83F905E60C6");

            entity.ToTable("TelefonTbl");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Marka)
                .HasMaxLength(50)
                .HasColumnName("marka");
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .HasColumnName("model");
            entity.Property(e => e.SatisAdet).HasColumnName("satis_adet");
            entity.Property(e => e.Ucret)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("ucret");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

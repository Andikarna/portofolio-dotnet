using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PTP.Models.MySql.PTPDev;

namespace AdidataDbContext.Models.Mysql.PTPDev
{
  public partial class PTPDevContext : DbContext
  {
    public PTPDevContext()
    {
    }

    public PTPDevContext(DbContextOptions<PTPDevContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; } = null!;
    public virtual DbSet<UsersToken> UsersTokens { get; set; } = null!;
    public virtual DbSet<MasterRole> MasterRoles { get; set; } = null!;
    public virtual DbSet<Experience> Experiences { get; set; } = null!;
    public virtual DbSet<Skill> Skills { get; set; } = null!;
    public virtual DbSet<Project> Projects { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var connectionString = configuration.GetConnectionString("PTP");

        var serverVersion = new MySqlServerVersion(new Version(10, 4, 28));

        optionsBuilder.UseMySql(connectionString, serverVersion);
      }

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      //modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
      //    .HasCharSet("utf8mb4");

      modelBuilder.UseCollation("utf8mb4_general_ci");
      modelBuilder.HasCharSet("utf8mb4");

      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<User>(entity =>
      {
        entity.ToTable("users");

        entity.Property(e => e.Id).HasColumnName("id");

        entity.Property(e => e.CreatedDate)
                  .HasColumnType("datetime")
                  .HasColumnName("created_date");

        entity.Property(e => e.Email)
                  .HasMaxLength(100)
                  .HasColumnName("email");

        entity.Property(e => e.Name)
                  .HasMaxLength(100)
                  .HasColumnName("name");

        entity.Property(e => e.Password)
                  .HasMaxLength(100)
                  .HasColumnName("password");

        entity.Property(e => e.Roleid).HasColumnName("roleid");

        entity.Property(e => e.UpdateDate)
                  .HasColumnType("datetime")
                  .HasColumnName("update_date");
      });

      modelBuilder.Entity<UsersToken>(entity =>
      {
        entity.ToTable("users_token");

        entity.Property(e => e.Id).HasColumnName("id");

        entity.Property(e => e.CreatedTime)
                  .HasColumnType("datetime")
                  .HasColumnName("created_time");

        entity.Property(e => e.ExpiredTime)
                  .HasColumnType("datetime")
                  .HasColumnName("expired_time");

        entity.Property(e => e.Hostname)
                  .HasMaxLength(100)
                  .HasColumnName("hostname");

        entity.Property(e => e.IpAddress)
                  .HasMaxLength(100)
                  .HasColumnName("ip_address");

        entity.Property(e => e.Nama)
                  .HasMaxLength(100)
                  .HasColumnName("nama");

        entity.Property(e => e.Token)
                  .HasColumnType("text")
                  .HasColumnName("token");

        entity.Property(e => e.UserId).HasColumnName("userId");
      });

      modelBuilder.Entity<Skill>(entity =>
      {
        entity.ToTable("skills");

        entity.Property(e => e.Id).HasColumnName("id");

        entity.Property(e => e.Name)
                  .HasMaxLength(100)
                  .HasColumnName("name");

        entity.Property(e => e.Category)
                  .HasMaxLength(100)
                  .HasColumnName("category");

        entity.Property(e => e.Level)
                  .HasMaxLength(100)
                  .HasColumnName("level");

        entity.Property(e => e.IconUrl)
                  .HasColumnType("text")
                  .HasColumnName("icon_url");

        entity.Property(e => e.IsFeatured)
                  .HasColumnName("is_featured");

        entity.Property(e => e.CreatedDate)
                  .HasColumnType("datetime")
                  .HasColumnName("created_date");

        entity.Property(e => e.UpdateDate)
                  .HasColumnType("datetime")
                  .HasColumnName("update_date");
      });

      modelBuilder.Entity<Project>(entity =>
      {
        entity.ToTable("projects");

        entity.Property(e => e.Id).HasColumnName("id");

        entity.Property(e => e.Title)
                  .HasMaxLength(255)
                  .HasColumnName("title");

        entity.Property(e => e.CoverImageUrl)
                  .HasColumnType("longtext")
                  .HasColumnName("cover_image_url");

        entity.Property(e => e.RepositoryUrl)
                  .HasColumnType("text")
                  .HasColumnName("repository_url");

        entity.Property(e => e.DemoUrl)
                  .HasColumnType("text")
                  .HasColumnName("demo_url");

        entity.Property(e => e.Status)
                  .HasMaxLength(100)
                  .HasColumnName("status");

        entity.Property(e => e.StartDate)
                  .HasColumnType("datetime")
                  .HasColumnName("start_date");

        entity.Property(e => e.Technologies)
                  .HasColumnType("text")
                  .HasColumnName("technologies");

        entity.Property(e => e.Summary)
                  .HasColumnType("text")
                  .HasColumnName("summary");

        entity.Property(e => e.Description)
                  .HasColumnType("longtext")
                  .HasColumnName("description");

        entity.Property(e => e.IsFeatured)
                  .HasColumnName("is_featured");

        entity.Property(e => e.CreatedDate)
                  .HasColumnType("datetime")
                  .HasColumnName("created_date");

        entity.Property(e => e.UpdateDate)
                  .HasColumnType("datetime")
                  .HasColumnName("update_date");
      });

      OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
  }
}

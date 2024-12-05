using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using PortfolioService.Models;

namespace PortfolioService.Data;

public partial class BlogDbContext : DbContext
{
    public BlogDbContext()
    {
    }

    public BlogDbContext(DbContextOptions<BlogDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FeaturedItem> FeaturedItems { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            throw new InvalidOperationException("DbContext must be configured through dependency injection.");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<FeaturedItem>(entity =>
        {
            entity.HasKey(e => e.FeaturedId).HasName("PRIMARY");

            entity.ToTable("featured_items");

            entity.Property(e => e.FeaturedId)
                .HasMaxLength(36)
                .HasColumnName("featured_id");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.Priority)
                .HasDefaultValueSql("'10'")
                .HasColumnName("priority");
            entity.Property(e => e.TargetUrl)
                .HasMaxLength(255)
                .HasColumnName("target_url");
            entity.Property(e => e.ThumbnailUrl)
                .HasMaxLength(255)
                .HasColumnName("thumbnail_url");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PRIMARY");

            entity.ToTable("posts");

            entity.Property(e => e.PostId)
                .HasMaxLength(36)
                .HasColumnName("post_id");
            entity.Property(e => e.Content)
                .HasColumnType("mediumtext")
                .HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at");
            entity.Property(e => e.IsDraft)
                .HasDefaultValueSql("'1'")
                .HasColumnName("is_draft");
            entity.Property(e => e.IsHidden)
                .HasDefaultValueSql("'0'")
                .HasColumnName("is_hidden");
            entity.Property(e => e.IsPinned)
                .HasDefaultValueSql("'0'")
                .HasColumnName("is_pinned");
            entity.Property(e => e.ThumbnailUrl)
                .HasMaxLength(255)
                .HasColumnName("thumbnail_url");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
            entity.Property(e => e.UrlString)
                .HasMaxLength(255)
                .IsFixedLength()
                .HasColumnName("url_string");
            entity.Property(e => e.Views)
                .HasDefaultValueSql("'0'")
                .HasColumnName("views");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

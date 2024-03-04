using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SearchRankingDL.EFmodels
{
    public partial class SearchRankingContext : DbContext
    {
        public virtual DbSet<SearchEngine_EF> SearchEngines { get; set; } = null!;
        public virtual DbSet<SearchEngineCss_EF> SearchEngineCsses { get; set; } = null!;
        public virtual DbSet<SearchResult_EF> SearchResults { get; set; } = null!;
        public virtual DbSet<SearchTerm_EF> SearchTerms { get; set; } = null!;
        public virtual DbSet<Url_EF> Urls { get; set; } = null!;

        public SearchRankingContext()
        {
        }

        public SearchRankingContext(DbContextOptions<SearchRankingContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Search-Ranking;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SearchEngine_EF>(entity =>
            {
                entity.ToTable("SearchEngine");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BaseUrl)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("BaseURL");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SearchEngineCss_EF>(entity =>
            {
                entity.ToTable("SearchEngineCSS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Cssselector)
                    .HasMaxLength(512)
                    .IsUnicode(false)
                    .HasColumnName("CSSselector");

                entity.Property(e => e.SearchEngineId).HasColumnName("SearchEngineID");

                entity.HasOne(d => d.SearchEngine_EF)
                    .WithMany(p => p.SearchEngineCsses_EF)
                    .HasForeignKey(d => d.SearchEngineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SearchEngineCSS_SearchEngine");
            });

            modelBuilder.Entity<SearchResult_EF>(entity =>
            {
                entity.ToTable("SearchResults");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SearchEngineId).HasColumnName("SearchEngineID");

                entity.Property(e => e.SearchTermId).HasColumnName("SearchTermID");

                entity.HasOne(d => d.SearchEngine_EF)
                    .WithMany(p => p.SearchResults_EF)
                    .HasForeignKey(d => d.SearchEngineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SearchResults_SearchEngine");

                entity.HasOne(d => d.SearchTerm_EF)
                    .WithMany(p => p.SearchResults_EF)
                    .HasForeignKey(d => d.SearchTermId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SearchResults_SearchTerms");
            });

            modelBuilder.Entity<SearchTerm_EF>(entity =>
            {
                entity.ToTable("SearchTerms");

                entity.HasIndex(e => e.SearchTerm1, "IX_SearchTerms")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Active).HasDefaultValue(true);

                entity.Property(e => e.SearchTerm1)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("SearchTerm");
            });

            modelBuilder.Entity<Url_EF>(entity =>
            {
                entity.ToTable("URLs");

                entity.HasIndex(e => e.Url1, "IX_URLs")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Url1)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("URL");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

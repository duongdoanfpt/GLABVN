using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PS11905_BAODUONG_ASMFULL_NET104.Models
{
    public partial class webglab_dbsContext : DbContext
    {
        //public webglab_dbsContext()
        //{
        //}

        public webglab_dbsContext(DbContextOptions<webglab_dbsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<NhomSp> NhomSps { get; set; }
        public virtual DbSet<Sanpham> Sanphams { get; set; }

        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<ChiTietHD> ChiTietHDs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<NhomSp>(entity =>
            {
                entity.HasKey(e => e.MaNhom)
                    .HasName("PK__NhomSP__234F91CD18CF969F");

                entity.ToTable("NhomSP");

                entity.Property(e => e.MaNhom).ValueGeneratedNever();

                entity.Property(e => e.TenNhom)
                    .IsRequired();
            });

            modelBuilder.Entity<Sanpham>(entity =>
            {
                entity.HasKey(e => e.MaSp)
                    .HasName("PK__SANPHAM__2725081CA68D9066");

                entity.ToTable("SANPHAM");

                entity.Property(e => e.MaSp).HasColumnName("MaSP");

                entity.Property(e => e.DonGia).HasColumnType("money");

                entity.Property(e => e.HinhAnh)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.MoTaSp)
                    .HasColumnName("MoTaSP");

                entity.Property(e => e.NhomSp).HasColumnName("NhomSP");

                entity.Property(e => e.TenSp)
                    .IsRequired()
                    .HasColumnName("TenSP");

                entity.HasOne(d => d.NhomSpNavigation)
                    .WithMany(p => p.Sanphams)
                    .HasForeignKey(d => d.NhomSp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SANPHAM__NhomSP__267ABA7A");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

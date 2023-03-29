using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SETI.WebApi
{
    public partial class SetiDbContext : DbContext
    {
        public SetiDbContext()
        {
        }

        public SetiDbContext(DbContextOptions<SetiDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Broker> Broker { get; set; }
        public virtual DbSet<DataGeneral> DataGeneral { get; set; }
        public virtual DbSet<DiscountRate> DiscountRate { get; set; }
        public virtual DbSet<InvestmentProject> InvestmentProject { get; set; }
        public virtual DbSet<InvestmentSector> InvestmentSector { get; set; }
        public virtual DbSet<InvestmentSubsector> InvestmentSubsector { get; set; }
        public virtual DbSet<LIST_Proyec_Mov_Porcentaje> LIST_Proyec_Mov_Porcentaje { get; set; }
        public virtual DbSet<Period> Period { get; set; }
        public virtual DbSet<ProjectMovement> ProjectMovement { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<SELECT_InvestmentProject> SELECT_InvestmentProject { get; set; }
        public virtual DbSet<SELECT_MaxiPeriodo> SELECT_MaxiPeriodo { get; set; }
        public virtual DbSet<SELECT_Project_Sum_Cant_Mov> SELECT_Project_Sum_Cant_Mov { get; set; }
        public virtual DbSet<ValorMovUltimo> ValorMovUltimo { get; set; }
        public virtual DbSet<beneficio_generado_por_la_inversion> beneficio_generado_por_la_inversion { get; set; }
        public virtual DbSet<tiempo_recuperaion_Inversion> tiempo_recuperaion_Inversion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("devtest");

            modelBuilder.Entity<Broker>(entity =>
            {
                entity.ToTable("Broker", "dbo");

                entity.Property(e => e.BrokerCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.BrokerName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.LocationRegion)
                    .WithMany(p => p.Broker)
                    .HasForeignKey(d => d.LocationRegionId)
                    .HasConstraintName("FK__Broker__Location__778AC167");
            });

            modelBuilder.Entity<DataGeneral>(entity =>
            {
                entity.HasKey(e => e.IdData)
                    .HasName("PK__DataGene__F298CC8D9B743180");

                entity.Property(e => e.BrokerName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.InvestmentAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ValuePayback).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ValueVan).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<DiscountRate>(entity =>
            {
                entity.ToTable("DiscountRate", "dbo");

                entity.HasOne(d => d.Period)
                    .WithMany(p => p.DiscountRate)
                    .HasForeignKey(d => d.PeriodId)
                    .HasConstraintName("FK__DiscountR__Perio__787EE5A0");
            });

            modelBuilder.Entity<InvestmentProject>(entity =>
            {
                entity.HasKey(e => e.ProjectId);

                entity.ToTable("InvestmentProject", "dbo");

                entity.Property(e => e.InvestmentAmount).HasColumnType("numeric(30, 4)");

                entity.Property(e => e.ProjectCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ProjectDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Broker)
                    .WithMany(p => p.InvestmentProject)
                    .HasForeignKey(d => d.BrokerId)
                    .HasConstraintName("FK__Investmen__Broke__797309D9");

                entity.HasOne(d => d.InvestmentRegion)
                    .WithMany(p => p.InvestmentProject)
                    .HasForeignKey(d => d.InvestmentRegionId)
                    .HasConstraintName("FK__Investmen__Inves__7A672E12");

                entity.HasOne(d => d.Subsector)
                    .WithMany(p => p.InvestmentProject)
                    .HasForeignKey(d => d.SubsectorId)
                    .HasConstraintName("FK__Investmen__Subse__7B5B524B");
            });

            modelBuilder.Entity<InvestmentSector>(entity =>
            {
                entity.HasKey(e => e.SectorId)
                    .HasName("PK_Sector");

                entity.ToTable("InvestmentSector", "dbo");

                entity.Property(e => e.SectorName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<InvestmentSubsector>(entity =>
            {
                entity.HasKey(e => e.SubsectorId)
                    .HasName("PK_Subsector");

                entity.ToTable("InvestmentSubsector", "dbo");

                entity.Property(e => e.SubsectorName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Sector)
                    .WithMany(p => p.InvestmentSubsector)
                    .HasForeignKey(d => d.SectorId)
                    .HasConstraintName("FK__Investmen__Secto__7C4F7684");
            });

            modelBuilder.Entity<LIST_Proyec_Mov_Porcentaje>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("LIST_Proyec_Mov_Porcentaje");

                entity.Property(e => e.DiscountRatePercentage).HasColumnType("numeric(18, 6)");

                entity.Property(e => e.SumMovimiento).HasColumnType("numeric(20, 4)");
            });

            modelBuilder.Entity<Period>(entity =>
            {
                entity.ToTable("Period", "dbo");
            });

            modelBuilder.Entity<ProjectMovement>(entity =>
            {
                entity.HasKey(e => e.MovementId);

                entity.ToTable("ProjectMovement", "dbo");

                entity.Property(e => e.MovementAmount).HasColumnType("numeric(20, 4)");

                entity.HasOne(d => d.Period)
                    .WithMany(p => p.ProjectMovement)
                    .HasForeignKey(d => d.PeriodId)
                    .HasConstraintName("FK__ProjectMo__Perio__7D439ABD");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.ProjectMovement)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK__ProjectMo__Proje__7E37BEF6");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("Region", "dbo");

                entity.Property(e => e.RegionCode)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.RegionName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SELECT_InvestmentProject>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("SELECT_InvestmentProject");

                entity.Property(e => e.BrokerCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.BrokerName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.InvestmentAmount).HasColumnType("numeric(30, 4)");

                entity.Property(e => e.ProjectCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ProjectDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.RegionName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SectorName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.SubsectorName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SELECT_MaxiPeriodo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("SELECT_MaxiPeriodo");
            });

            modelBuilder.Entity<SELECT_Project_Sum_Cant_Mov>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("SELECT_Project_Sum_Cant_Mov");

                entity.Property(e => e.SumMovimiento).HasColumnType("numeric(38, 4)");
            });

            modelBuilder.Entity<ValorMovUltimo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ValorMovUltimo");

                entity.Property(e => e.valorultimoPeriodo).HasColumnType("numeric(38, 4)");
            });

            modelBuilder.Entity<beneficio_generado_por_la_inversion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("beneficio_generado_por_la_inversion");

                entity.Property(e => e.BrokerName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.InversionInicial).HasColumnType("numeric(30, 4)");

                entity.Property(e => e.VAN).HasColumnType("numeric(38, 16)");
            });

            modelBuilder.Entity<tiempo_recuperaion_Inversion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("tiempo_recuperaion_Inversion");

                entity.Property(e => e.BrokerName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FlujoCajaAñosAntesRecuperar).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.InversionInicial).HasColumnType("numeric(30, 4)");

                entity.Property(e => e.SumaFlujos).HasColumnType("numeric(38, 4)");

                entity.Property(e => e.payback).HasColumnType("numeric(38, 6)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

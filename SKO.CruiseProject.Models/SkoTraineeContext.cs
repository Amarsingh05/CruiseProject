using System;
using System.Collections.Generic;
using CruiseProject.Models;
using Microsoft.EntityFrameworkCore;

namespace SKO.CruiseProject.Models;

public partial class SkoTraineeContext : DbContext
{
    public SkoTraineeContext()
    {
    }

    public SkoTraineeContext(DbContextOptions<SkoTraineeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CrewInfo> CrewInfos { get; set; }

    public virtual DbSet<DepartmentInfo> DepartmentInfos { get; set; }

    public virtual DbSet<GuestCrewInfo> GuestCrewInfos { get; set; }

    public virtual DbSet<GuestInfo> GuestInfos { get; set; }

    public virtual DbSet<PositionInfo> PositionInfos { get; set; }

    public virtual DbSet<ShipDepartmentConnection> ShipDepartmentConnections { get; set; }

    public virtual DbSet<ShipInfo> ShipInfos { get; set; }

    public virtual DbSet<VoyageInfo> VoyageInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CrewInfo>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PRIMARY");

            entity.ToTable("crew_info");

            entity.HasIndex(e => e.DeptId, "DEPT_ID_idx");

            entity.HasIndex(e => e.PositionId, "POSITION_ID_idx");

            entity.Property(e => e.PersonId)
                .HasMaxLength(45)
                .HasColumnName("PERSON_ID");
            entity.Property(e => e.DeptId).HasColumnName("DEPT_ID");
            entity.Property(e => e.PositionId).HasColumnName("POSITION_ID");
            entity.Property(e => e.SafetyNo).HasColumnName("SAFETY_NO");
            entity.Property(e => e.SignOffDate)
                .HasColumnType("date")
                .HasColumnName("SIGN_OFF_DATE");
            entity.Property(e => e.SignOnDate)
                .HasColumnType("date")
                .HasColumnName("SIGN_ON_DATE");

            entity.HasOne(d => d.Dept).WithMany(p => p.CrewInfos)
                .HasForeignKey(d => d.DeptId)
                .HasConstraintName("DEPTID");

            entity.HasOne(d => d.Person).WithOne(p => p.CrewInfo)
                .HasForeignKey<CrewInfo>(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PERSONID");

            entity.HasOne(d => d.Position).WithMany(p => p.CrewInfos)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("POSITIONID");
        });

        modelBuilder.Entity<DepartmentInfo>(entity =>
        {
            entity.HasKey(e => e.DeptId).HasName("PRIMARY");

            entity.ToTable("department_info");

            entity.Property(e => e.DeptId).HasColumnName("DEPT_ID");
            entity.Property(e => e.Department)
                .HasMaxLength(45)
                .HasColumnName("DEPARTMENT");
        });

        modelBuilder.Entity<GuestCrewInfo>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PRIMARY");

            entity.ToTable("guest_crew_info");

            entity.HasIndex(e => e.PersonId, "idx_crew_person__id");

            entity.Property(e => e.PersonId)
                .HasMaxLength(45)
                .HasColumnName("PERSON_ID");
            entity.Property(e => e.CabinNo).HasColumnName("CABIN_NO");
            entity.Property(e => e.CheckedinTerminal)
                .HasColumnType("enum('A','B','C','D')")
                .HasColumnName("CHECKEDIN_TERMINAL");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.FirstName)
                .HasMaxLength(45)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.Gender)
                .HasColumnType("enum('M','F','T')")
                .HasColumnName("GENDER");
            entity.Property(e => e.GuestOrCrew)
                .HasColumnType("enum('GUEST','CREW')")
                .HasColumnName("GUEST_OR_CREW");
            entity.Property(e => e.IsCheckedIn)
                .HasColumnType("enum('YES','NO')")
                .HasColumnName("IS_CHECKED_IN");
            entity.Property(e => e.IsOnboard)
                .HasColumnType("enum('YES','NO')")
                .HasColumnName("IS_ONBOARD");
            entity.Property(e => e.LastName)
                .HasMaxLength(45)
                .HasColumnName("last_name");
            entity.Property(e => e.Nationality)
                .HasMaxLength(45)
                .HasColumnName("NATIONALITY");
            entity.Property(e => e.ProfileImage)
                .HasColumnType("mediumtext")
                .HasColumnName("PROFILE_IMAGE");
            entity.Property(e => e.Title)
                .HasColumnType("enum('MS','MR','MRS')")
                .HasColumnName("TITLE");
        });

        modelBuilder.Entity<GuestInfo>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PRIMARY");

            entity.ToTable("guest_info");

            entity.HasIndex(e => e.PersonId, "PERSON_ID_idx");

            entity.HasIndex(e => e.VoyageNumber, "VOYAGE_NUMBER_idx");

            entity.Property(e => e.PersonId)
                .HasMaxLength(45)
                .HasColumnName("PERSON_ID");
            entity.Property(e => e.DbarkDate)
                .HasColumnType("date")
                .HasColumnName("DBARK_DATE");
            entity.Property(e => e.EmbarkDate)
                .HasColumnType("date")
                .HasColumnName("EMBARK_DATE");
            entity.Property(e => e.IsActive)
                .HasMaxLength(45)
                .HasColumnName("IS_ACTIVE");
            entity.Property(e => e.ReservationNumber)
                .HasMaxLength(45)
                .HasColumnName("RESERVATION_NUMBER");
            entity.Property(e => e.SequenceNo).HasColumnName("SEQUENCE_NO");
            entity.Property(e => e.VoyageNumber)
                .HasMaxLength(45)
                .HasColumnName("VOYAGE_NUMBER");

            entity.HasOne(d => d.Person).WithOne(p => p.GuestInfo)
                .HasForeignKey<GuestInfo>(d => d.PersonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PERSON_ID");

            entity.HasOne(d => d.VoyageNumberNavigation).WithMany(p => p.GuestInfos)
                .HasForeignKey(d => d.VoyageNumber)
                .HasConstraintName("VOYAGE_NUMBER");
        });

        modelBuilder.Entity<PositionInfo>(entity =>
        {
            entity.HasKey(e => e.PositionId).HasName("PRIMARY");

            entity.ToTable("position_info");

            entity.HasIndex(e => e.DepartmentId, "DEPARTMENT_ID_idx");

            entity.Property(e => e.PositionId).HasColumnName("POSITION_ID");
            entity.Property(e => e.DepartmentId).HasColumnName("DEPARTMENT_ID");
            entity.Property(e => e.Position)
                .HasMaxLength(45)
                .HasColumnName("POSITION");

            entity.HasOne(d => d.Department).WithMany(p => p.PositionInfos)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("DEPARTMENT_ID");
        });

        modelBuilder.Entity<ShipDepartmentConnection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ship_department_connection");

            entity.HasIndex(e => e.DeptId, "DEPTID_idx");

            entity.HasIndex(e => e.ShipId, "SHIP_ID_idx");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DeptId).HasColumnName("DEPT_ID");
            entity.Property(e => e.ShipId)
                .HasMaxLength(45)
                .HasColumnName("SHIP_ID");

            entity.HasOne(d => d.Dept).WithMany(p => p.ShipDepartmentConnections)
                .HasForeignKey(d => d.DeptId)
                .HasConstraintName("DEPTID1");

            entity.HasOne(d => d.Ship).WithMany(p => p.ShipDepartmentConnections)
                .HasForeignKey(d => d.ShipId)
                .HasConstraintName("SHIPID1");
        });

        modelBuilder.Entity<ShipInfo>(entity =>
        {
            entity.HasKey(e => e.ShipId).HasName("PRIMARY");

            entity.ToTable("ship_info");

            entity.Property(e => e.ShipId)
                .HasMaxLength(45)
                .HasColumnName("SHIP_ID");
            entity.Property(e => e.ShipName)
                .HasMaxLength(45)
                .HasColumnName("SHIP_NAME");
        });

        modelBuilder.Entity<VoyageInfo>(entity =>
        {
            entity.HasKey(e => e.VoyageNumber).HasName("PRIMARY");

            entity.ToTable("voyage_info");

            entity.HasIndex(e => e.ShipId, "SHIP_ID_idx");

            entity.HasIndex(e => e.VoyageNumber, "voyage_idx");

            entity.Property(e => e.VoyageNumber)
                .HasMaxLength(45)
                .HasColumnName("VOYAGE_NUMBER");
            entity.Property(e => e.IsVoyageActive)
                .HasMaxLength(5)
                .HasColumnName("is_voyage_active");
            entity.Property(e => e.PortName)
                .HasMaxLength(20)
                .HasColumnName("port_name");
            entity.Property(e => e.ShipId)
                .HasMaxLength(45)
                .HasColumnName("SHIP_ID");
            entity.Property(e => e.VoyageEndDate)
                .HasColumnType("date")
                .HasColumnName("voyage_end_date");
            entity.Property(e => e.VoyageStartDate)
                .HasColumnType("date")
                .HasColumnName("voyage_start_date");

            entity.HasOne(d => d.Ship).WithMany(p => p.VoyageInfos)
                .HasForeignKey(d => d.ShipId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SHIP_ID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QL_NhanVien.DataAccess.Entities;

public partial class QLNhanVienContext : DbContext
{
    public QLNhanVienContext()
    {
    }

    public QLNhanVienContext(DbContextOptions<QLNhanVienContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActualSalary> ActualSalaries { get; set; }

    public virtual DbSet<AttachedFile> AttachedFiles { get; set; }

    public virtual DbSet<Claim> Claims { get; set; }

    public virtual DbSet<EmailConfirmation> EmailConfirmations { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Submission> Submissions { get; set; }

    public virtual DbSet<SubmissionType> SubmissionTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-30D4OI46\\DUY;Initial Catalog=QL_NHANVIEN;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActualSalary>(entity =>
        {
            entity.HasKey(e => e.ActualSalaryId).HasName("PK__ActualSa__C60C25303867113D");

            entity.Property(e => e.ContractSalary).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SalaryAfterDeductions).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.User).WithMany(p => p.ActualSalaries)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__ActualSal__UserI__44FF419A");
        });

        modelBuilder.Entity<AttachedFile>(entity =>
        {
            entity.HasKey(e => e.AttachedFileId).HasName("PK__Attached__4A7D88DD5D8AFA98");

            entity.Property(e => e.FileName).HasMaxLength(255);

            entity.HasOne(d => d.Submission).WithMany(p => p.AttachedFiles)
                .HasForeignKey(d => d.SubmissionId)
                .HasConstraintName("FK__AttachedF__Submi__4D94879B");
        });

        modelBuilder.Entity<Claim>(entity =>
        {
            entity.HasKey(e => e.ClaimId).HasName("PK__Claims__EF2E139B35D88EDE");

            entity.Property(e => e.ClaimName).HasMaxLength(255);
        });

        modelBuilder.Entity<EmailConfirmation>(entity =>
        {
            entity.HasKey(e => e.EmailConfirmationId).HasName("PK__EmailCon__A04DCE811CB28412");

            entity.Property(e => e.ConfirmationCode).HasMaxLength(255);
            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.EmailConfirmations)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__EmailConf__UserI__5DCAEF64");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.RefreshTokenId).HasName("PK__RefreshT__F5845E393A8DF4A2");

            entity.Property(e => e.Created).HasColumnType("datetime");
            entity.Property(e => e.ExpierTime).HasColumnType("datetime");
            entity.Property(e => e.RefToken).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__RefreshTo__UserI__4222D4EF");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1AA9F669A2");

            entity.Property(e => e.RoleName).HasMaxLength(255);

            entity.HasMany(d => d.Claims).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RoleClaim",
                    r => r.HasOne<Claim>().WithMany()
                        .HasForeignKey("ClaimId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__RoleClaim__Claim__3F466844"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__RoleClaim__RoleI__3E52440B"),
                    j =>
                    {
                        j.HasKey("RoleId", "ClaimId").HasName("PK__RoleClai__24082F232868E089");
                    });
        });

        modelBuilder.Entity<Submission>(entity =>
        {
            entity.HasKey(e => e.SubmissionId).HasName("PK__Submissi__449EE12580271F89");

            entity.Property(e => e.Heading).HasMaxLength(255);
            entity.Property(e => e.SendDate).HasColumnType("datetime");

            entity.HasOne(d => d.SubmissionType).WithMany(p => p.Submissions)
                .HasForeignKey(d => d.SubmissionTypeId)
                .HasConstraintName("FK__Submissio__Submi__4AB81AF0");

            entity.HasOne(d => d.User).WithMany(p => p.Submissions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Submissio__UserI__49C3F6B7");
        });

        modelBuilder.Entity<SubmissionType>(entity =>
        {
            entity.HasKey(e => e.SubmissionTypeId).HasName("PK__Submissi__F2662978906E3BFF");

            entity.Property(e => e.SubmissionName).HasMaxLength(255);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C3F0EC57D");

            entity.Property(e => e.ContractSalary).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.EmailConfirmed).HasDefaultValueSql("((0))");
            entity.Property(e => e.GoogleId).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.RefreshToken)
                .HasMaxLength(255)
                .HasDefaultValueSql("('')");
            entity.Property(e => e.TokenCreated).HasColumnType("datetime");
            entity.Property(e => e.TokenExpires).HasColumnType("datetime");
            entity.Property(e => e.UserName).HasMaxLength(255);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__RoleId__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

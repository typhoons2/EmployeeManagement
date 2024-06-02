using System;
using System.Collections.Generic;

namespace QL_NhanVien.DataAccess.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string? Name { get; set; }

    public string? UserName { get; set; }

    public byte[]? PasswordHash { get; set; }

    public byte[]? PasswordSalt { get; set; }

    public decimal? ContractSalary { get; set; }

    public int? DaysOff { get; set; }

    public bool? Status { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? TokenCreated { get; set; }

    public DateTime? TokenExpires { get; set; }

    public int? RoleId { get; set; }

    public string? Email { get; set; }

    public string? GoogleId { get; set; }

    public bool? EmailConfirmed { get; set; }

    public string? ConfirmationCode { get; set; }

    public string? EmailConfirmationCode { get; set; }

    public virtual ICollection<ActualSalary> ActualSalaries { get; } = new List<ActualSalary>();

    public virtual ICollection<EmailConfirmation> EmailConfirmations { get; } = new List<EmailConfirmation>();

    public virtual ICollection<RefreshToken> RefreshTokens { get; } = new List<RefreshToken>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<Submission> Submissions { get; } = new List<Submission>();
}

using System;
using System.Collections.Generic;

namespace QL_NhanVien.DataAccess.Entities;

public partial class EmailConfirmation
{
    public int EmailConfirmationId { get; set; }

    public int? UserId { get; set; }

    public string? ConfirmationCode { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public virtual User? User { get; set; }
}

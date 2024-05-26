using System;
using System.Collections.Generic;

namespace QL_NhanVien.DataAccess.Entities;

public partial class RefreshToken
{
    public int RefreshTokenId { get; set; }

    public string? RefToken { get; set; }

    public DateTime? ExpierTime { get; set; }

    public DateTime? Created { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}

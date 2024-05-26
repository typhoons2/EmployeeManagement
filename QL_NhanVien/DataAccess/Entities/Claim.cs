using System;
using System.Collections.Generic;

namespace QL_NhanVien.DataAccess.Entities;

public partial class Claim
{
    public int ClaimId { get; set; }

    public string? ClaimName { get; set; }

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}

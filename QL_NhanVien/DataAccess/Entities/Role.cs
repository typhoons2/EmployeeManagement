﻿using System;
using System.Collections.Generic;

namespace QL_NhanVien.DataAccess.Entities;

public partial class Role
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<User> Users { get; } = new List<User>();

    public virtual ICollection<Claim> Claims { get; } = new List<Claim>();
}

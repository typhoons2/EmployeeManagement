using System;
using System.Collections.Generic;

namespace QL_NhanVien.DataAccess.Entities;

public partial class SubmissionType
{
    public int SubmissionTypeId { get; set; }

    public string? SubmissionName { get; set; }

    public virtual ICollection<Submission> Submissions { get; } = new List<Submission>();
}

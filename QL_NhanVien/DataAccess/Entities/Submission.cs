using System;
using System.Collections.Generic;

namespace QL_NhanVien.DataAccess.Entities;

public partial class Submission
{
    public int SubmissionId { get; set; }

    public string? Heading { get; set; }

    public string? SubmissionBody { get; set; }

    public DateTime? SendDate { get; set; }

    public bool? Status { get; set; }

    public int? UserId { get; set; }

    public int? SubmissionTypeId { get; set; }

    public virtual ICollection<AttachedFile> AttachedFiles { get; } = new List<AttachedFile>();

    public virtual SubmissionType? SubmissionType { get; set; }

    public virtual User? User { get; set; }
}

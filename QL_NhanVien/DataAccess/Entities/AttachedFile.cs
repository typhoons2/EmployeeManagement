using System;
using System.Collections.Generic;

namespace QL_NhanVien.DataAccess.Entities;

public partial class AttachedFile
{
    public int AttachedFileId { get; set; }

    public string? FileName { get; set; }

    public byte[]? FileData { get; set; }

    public int? SubmissionId { get; set; }

    public virtual Submission? Submission { get; set; }
}

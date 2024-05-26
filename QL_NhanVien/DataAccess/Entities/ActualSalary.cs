using System;
using System.Collections.Generic;

namespace QL_NhanVien.DataAccess.Entities;

public partial class ActualSalary
{
    public int ActualSalaryId { get; set; }

    public decimal? ContractSalary { get; set; }

    public decimal? SalaryAfterDeductions { get; set; }

    public int? Month { get; set; }

    public int? Year { get; set; }

    public int? DaysOff { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}

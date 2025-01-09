using System;
using System.Collections.Generic;

namespace DuAn_QLCY.Models;

public partial class Salary
{
    public int EmployeeId { get; set; }

    public decimal? Salary1 { get; set; }

    public decimal? Bonus { get; set; }

    public DateOnly PaymentDate { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace DuAn_QLCY.Models;

public partial class Insurance
{
    public int InsuranceCode { get; set; }

    public int EmployeeId { get; set; }

    public string Type { get; set; } = null!;

    public int Status { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public decimal CoverageAmount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}

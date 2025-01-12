using System;
using System.Collections.Generic;

namespace DuAn_QLCY.Models;

public partial class PerformanceKpi
{
    public int Kpiid { get; set; }

    public int? EmployeeId { get; set; }

    public decimal? Kpivalue { get; set; }

    public DateOnly? Kpimonth { get; set; }

    public virtual Employee? Employee { get; set; }
}

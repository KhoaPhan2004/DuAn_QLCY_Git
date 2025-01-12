using System;
using System.Collections.Generic;

namespace DuAn_QLCY.Models;

public partial class TimeTracking
{
    public int EntryId { get; set; }

    public int? EmployeeId { get; set; }

    public int? TaskId { get; set; }

    public DateOnly? Date { get; set; }

    public decimal? HoursWorked { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Task? Task { get; set; }
}

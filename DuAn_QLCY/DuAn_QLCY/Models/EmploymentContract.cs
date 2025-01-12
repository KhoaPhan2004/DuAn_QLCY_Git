using System;
using System.Collections.Generic;

namespace DuAn_QLCY.Models;

public partial class EmploymentContract
{
    public int ContractId { get; set; }

    public int? EmployeeId { get; set; }

    public string? ContractType { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual Employee? Employee { get; set; }
}

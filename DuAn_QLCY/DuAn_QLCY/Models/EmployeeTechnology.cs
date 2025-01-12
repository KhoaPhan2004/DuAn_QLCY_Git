using System;
using System.Collections.Generic;

namespace DuAn_QLCY.Models;

public partial class EmployeeTechnology
{
    public int EmployeeId { get; set; }

    public int TechId { get; set; }

    public int ProficiencyLevel { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Technology Tech { get; set; } = null!;
}

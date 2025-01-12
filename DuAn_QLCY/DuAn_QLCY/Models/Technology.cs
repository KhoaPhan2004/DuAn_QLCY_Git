using System;
using System.Collections.Generic;

namespace DuAn_QLCY.Models;

public partial class Technology
{
    public int TechId { get; set; }

    public string TechName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<EmployeeTechnology> EmployeeTechnologies { get; set; } = new List<EmployeeTechnology>();
}

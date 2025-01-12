using System;
using System.Collections.Generic;

namespace DuAn_QLCY.Models;

public partial class EmployeeType
{
    public int TypeId { get; set; }

    public string? TypeName { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}

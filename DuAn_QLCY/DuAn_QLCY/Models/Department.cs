using System;
using System.Collections.Generic;

namespace DuAn_QLCY.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string? DepartmentName { get; set; }

    public string? Description { get; set; }

    public DateOnly? ActiveFrom { get; set; }

    public DateOnly? ActiveTo { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}

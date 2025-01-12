using System;
using System.Collections.Generic;

namespace DuAn_QLCY.Models;

public partial class TrainingProgram
{
    public int ProgramId { get; set; }

    public string ProgramName { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string Trainer { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<EmployeeTraining> EmployeeTrainings { get; set; } = new List<EmployeeTraining>();
}

using System;
using System.Collections.Generic;

namespace DuAn_QLCY.Models;

public partial class EmployeeTraining
{
    public int EmployeeId { get; set; }

    public int ProgramId { get; set; }

    public string CompletionStatus { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;

    public virtual TrainingProgram Program { get; set; } = null!;
}

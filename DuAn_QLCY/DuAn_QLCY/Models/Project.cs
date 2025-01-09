using System;
using System.Collections.Generic;

namespace DuAn_QLCY.Models;

public partial class Project
{
    public int ProjectId { get; set; }

    public string? ProjectName { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Status { get; set; }

    public int? ManagerId { get; set; }

    public int? ClientId { get; set; }

    public virtual Client? Client { get; set; }

    public virtual Employee? Manager { get; set; }

    public virtual ICollection<ProjectDetail> ProjectDetails { get; set; } = new List<ProjectDetail>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}

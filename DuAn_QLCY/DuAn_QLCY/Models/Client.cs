using System;
using System.Collections.Generic;

namespace DuAn_QLCY.Models;

public partial class Client
{
    public int ClientId { get; set; }

    public string? CompanyName { get; set; }

    public string? ContactName { get; set; }

    public string? ContactEmail { get; set; }

    public string? PhoneNumber { get; set; }

    public virtual ICollection<ProjectDetail> ProjectDetails { get; set; } = new List<ProjectDetail>();

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}

using System;
using System.Collections.Generic;

namespace DuAn_QLCY.Models;

public partial class ProjectDetail
{
    public int DetailsId { get; set; }

    public int? ClientId { get; set; }

    public int ProjectId { get; set; }

    public string? DetailedDescription { get; set; }

    public decimal? EstimatedBudget { get; set; }

    public decimal? ActualBudget { get; set; }

    public string Srs { get; set; } = null!;

    public virtual Client? Client { get; set; }

    public virtual Project Project { get; set; } = null!;
}

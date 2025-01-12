using System;
using System.Collections.Generic;

namespace DuAn_QLCY.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public int? DepartmentId { get; set; }

    public string? Position { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public int? TypeId { get; set; }

    public DateOnly DayOfBirth { get; set; }

    public string? Gender { get; set; }

    public DateOnly JoiningDate { get; set; }

    public string? Nationality { get; set; }

    public string Address { get; set; } = null!;

    public virtual Department? Department { get; set; }

    public virtual ICollection<EmployeeTechnology> EmployeeTechnologies { get; set; } = new List<EmployeeTechnology>();

    public virtual ICollection<EmployeeTraining> EmployeeTrainings { get; set; } = new List<EmployeeTraining>();

    public virtual ICollection<EmploymentContract> EmploymentContracts { get; set; } = new List<EmploymentContract>();

    public virtual ICollection<Insurance> Insurances { get; set; } = new List<Insurance>();

    public virtual ICollection<Leaf> Leaves { get; set; } = new List<Leaf>();

    public virtual ICollection<PerformanceKpi> PerformanceKpis { get; set; } = new List<PerformanceKpi>();

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual Salary? Salary { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual ICollection<TimeTracking> TimeTrackings { get; set; } = new List<TimeTracking>();

    public virtual EmployeeType? Type { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DuAn_QLCY.Models;

public partial class QlCtPmContext : DbContext
{
    public QlCtPmContext()
    {
    }

    public QlCtPmContext(DbContextOptions<QlCtPmContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeTechnology> EmployeeTechnologies { get; set; }

    public virtual DbSet<EmployeeTraining> EmployeeTrainings { get; set; }

    public virtual DbSet<EmployeeType> EmployeeTypes { get; set; }

    public virtual DbSet<EmploymentContract> EmploymentContracts { get; set; }

    public virtual DbSet<Insurance> Insurances { get; set; }

    public virtual DbSet<Leaf> Leaves { get; set; }

    public virtual DbSet<PerformanceKpi> PerformanceKpis { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectDetail> ProjectDetails { get; set; }

    public virtual DbSet<Salary> Salaries { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<Technology> Technologies { get; set; }

    public virtual DbSet<TimeTracking> TimeTrackings { get; set; }

    public virtual DbSet<TrainingProgram> TrainingPrograms { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-HOAVLCC\\NHAN; Database=QL_CT_PM; Trusted_Connection = True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__Clients__E67E1A04648AC4DE");

            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.CompanyName).HasMaxLength(255);
            entity.Property(e => e.ContactEmail).HasMaxLength(100);
            entity.Property(e => e.ContactName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BCD7D1C0F6A");

            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.DepartmentName).HasMaxLength(100);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__7AD04FF1057D2176");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Nationality).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.Position).HasMaxLength(100);
            entity.Property(e => e.TypeId).HasColumnName("TypeID");

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_Employees_Departments");

            entity.HasOne(d => d.Type).WithMany(p => p.Employees)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK_Employees_EmployeeTypes");
        });

        modelBuilder.Entity<EmployeeTechnology>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.TechId }).HasName("PK__Employee__927FB478AD94A770");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.TechId).HasColumnName("TechID");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeTechnologies)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeTechnologies_Employees");

            entity.HasOne(d => d.Tech).WithMany(p => p.EmployeeTechnologies)
                .HasForeignKey(d => d.TechId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeTechnologies_Technologies");
        });

        modelBuilder.Entity<EmployeeTraining>(entity =>
        {
            entity.HasKey(e => new { e.EmployeeId, e.ProgramId }).HasName("PK__Employee__ED8219F22F7215EA");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.ProgramId).HasColumnName("ProgramID");
            entity.Property(e => e.CompletionStatus).HasMaxLength(50);

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeTrainings)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeTrainings_Employees");

            entity.HasOne(d => d.Program).WithMany(p => p.EmployeeTrainings)
                .HasForeignKey(d => d.ProgramId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeeTrainings_TrainingPrograms");
        });

        modelBuilder.Entity<EmployeeType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__Employee__516F0395ECF5E7C0");

            entity.Property(e => e.TypeId).HasColumnName("TypeID");
            entity.Property(e => e.TypeName).HasMaxLength(100);
        });

        modelBuilder.Entity<EmploymentContract>(entity =>
        {
            entity.HasKey(e => e.ContractId).HasName("PK__Employme__C90D3409A0829C2A");

            entity.Property(e => e.ContractId).HasColumnName("ContractID");
            entity.Property(e => e.ContractType).HasMaxLength(50);
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmploymentContracts)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_EmploymentContracts_Employees");
        });

        modelBuilder.Entity<Insurance>(entity =>
        {
            entity.HasKey(e => e.InsuranceCode).HasName("PK__Insuranc__E916199DA208E23F");

            entity.Property(e => e.CoverageAmount).HasColumnType("decimal(8, 2)");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Type).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Updated_at");

            entity.HasOne(d => d.Employee).WithMany(p => p.Insurances)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Insurances_Employees");
        });

        modelBuilder.Entity<Leaf>(entity =>
        {
            entity.HasKey(e => e.LeaveId).HasName("PK__Leaves__796DB979A27F7C06");

            entity.Property(e => e.LeaveId).HasColumnName("LeaveID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Reason).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Updated_at");

            entity.HasOne(d => d.Employee).WithMany(p => p.Leaves)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Leaves_Employees");
        });

        modelBuilder.Entity<PerformanceKpi>(entity =>
        {
            entity.HasKey(e => e.Kpiid).HasName("PK__Performa__72E69281752F342F");

            entity.ToTable("PerformanceKPIs");

            entity.Property(e => e.Kpiid).HasColumnName("KPIID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Kpimonth).HasColumnName("KPIMonth");
            entity.Property(e => e.Kpivalue)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("KPIValue");

            entity.HasOne(d => d.Employee).WithMany(p => p.PerformanceKpis)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_PerformanceKPIs_Employees");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PK__Projects__761ABED0130A0DAD");

            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.ManagerId).HasColumnName("ManagerID");
            entity.Property(e => e.ProjectName).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Client).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK_Projects_Clients");

            entity.HasOne(d => d.Manager).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK_Projects_Manager");
        });

        modelBuilder.Entity<ProjectDetail>(entity =>
        {
            entity.HasKey(e => e.DetailsId).HasName("PK__ProjectD__BAC862ACF221BA54");

            entity.Property(e => e.DetailsId).HasColumnName("DetailsID");
            entity.Property(e => e.ActualBudget).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.EstimatedBudget).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
            entity.Property(e => e.Srs).HasColumnName("SRS");

            entity.HasOne(d => d.Client).WithMany(p => p.ProjectDetails)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK_ProjectDetails_Clients");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectDetails)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProjectDetails_Projects");
        });

        modelBuilder.Entity<Salary>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Salaries__7AD04FF10D23D601");

            entity.Property(e => e.EmployeeId)
                .ValueGeneratedNever()
                .HasColumnName("EmployeeID");
            entity.Property(e => e.Bonus).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Salary1)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("Salary");

            entity.HasOne(d => d.Employee).WithOne(p => p.Salary)
                .HasForeignKey<Salary>(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Salaries_Employees");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__Tasks__7C6949D1E72066CD");

            entity.Property(e => e.TaskId).HasColumnName("TaskID");
            entity.Property(e => e.ProjectId).HasColumnName("ProjectID");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TaskName).HasMaxLength(255);

            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.AssignedTo)
                .HasConstraintName("FK_Tasks_Employees");

            entity.HasOne(d => d.Project).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tasks_Projects");
        });

        modelBuilder.Entity<Technology>(entity =>
        {
            entity.HasKey(e => e.TechId).HasName("PK__Technolo__8AFFB89F227EEC50");

            entity.Property(e => e.TechId).HasColumnName("TechID");
            entity.Property(e => e.TechName).HasMaxLength(100);
        });

        modelBuilder.Entity<TimeTracking>(entity =>
        {
            entity.HasKey(e => e.EntryId).HasName("PK__TimeTrac__F57BD2D7844CD744");

            entity.ToTable("TimeTracking");

            entity.Property(e => e.EntryId).HasColumnName("EntryID");
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.HoursWorked).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.TaskId).HasColumnName("TaskID");

            entity.HasOne(d => d.Employee).WithMany(p => p.TimeTrackings)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_TimeTracking_Employees");

            entity.HasOne(d => d.Task).WithMany(p => p.TimeTrackings)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK_TimeTracking_Tasks");
        });

        modelBuilder.Entity<TrainingProgram>(entity =>
        {
            entity.HasKey(e => e.ProgramId).HasName("PK__Training__7525603817B9B8FB");

            entity.Property(e => e.ProgramId).HasColumnName("ProgramID");
            entity.Property(e => e.ProgramName).HasMaxLength(255);
            entity.Property(e => e.Trainer).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC27A03F76D4");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_at");
            entity.Property(e => e.Email).HasMaxLength(191);
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Updated_at");

            entity.HasOne(d => d.Employee).WithMany(p => p.Users)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Employees");
        });

        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

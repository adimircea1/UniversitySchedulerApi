using Microsoft.EntityFrameworkCore;
using UniversityScheduler.Api.Core.Models;
using UniversityScheduler.Api.Core.Models.University_Entities;
using UniversityScheduler.Api.Infrastructure.DateOnlyAndTimeOnlySupport;

namespace UniversityScheduler.Api.Infrastructure.Repositories;

//here i will define the model mappings to the project's database
public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Student>? Students { get; set; }
    public DbSet<Professor>? Professors { get; set; }
    public DbSet<Catalogue>? Catalogues { get; set; }
    public DbSet<Grade>? Grades { get; set; }
    public DbSet<UniversityGroup>? UniversityGroups { get; set; }
    public DbSet<StudentAttendance>? StudentAttendances { get; set; }
    public DbSet<Course>? Courses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Add primary keys
        modelBuilder.Entity<Student>()
            .HasKey(student => student.Id);

        modelBuilder.Entity<Professor>()
            .HasKey(professor => professor.Id);

        modelBuilder.Entity<Catalogue>()
            .HasKey(catalogue => catalogue.Id);

        modelBuilder.Entity<Grade>()
            .HasKey(grade => grade.Id);

        modelBuilder.Entity<Course>()
            .HasKey(course => course.Id);

        modelBuilder.Entity<UniversityGroup>()
            .HasKey(group => group.Id);

        modelBuilder.Entity<StudentAttendance>()
            .HasKey(attendance => attendance.Id);

        modelBuilder.Entity<Student>()
            .HasIndex(student => student.Cnp)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasKey(user => user.Id);

        modelBuilder.Entity<User>()
            .HasIndex(user => user.Email)
            .IsUnique();

        //Fix DateOnly and TimeOnly mapping problems
        modelBuilder.Entity<Student>()
            .Property(student => student.BirthdayDate)
            .HasConversion(new DateOnlyConverter());

        modelBuilder.Entity<Professor>()
            .Property(professor => professor.BirthdayDate)
            .HasConversion(new DateOnlyConverter());

        modelBuilder.Entity<StudentAttendance>()
            .Property(attendance => attendance.DateOfTheCourse)
            .HasConversion(new DateOnlyConverter());

        modelBuilder.Entity<StudentAttendance>()
            .Property(attendance => attendance.TimeOfTheCourse)
            .HasConversion(new TimeOnlyConverter());


        //Create relations and add foreign keys
        modelBuilder.Entity<Catalogue>()
            .HasMany(catalogue => catalogue.Grades)
            .WithOne(grade => grade.Catalogue)
            .HasForeignKey(grade => grade.CatalogueId);

        modelBuilder.Entity<Course>()
            .HasOne(course => course.Professor)
            .WithMany(professor => professor.Courses)
            .HasForeignKey(course => course.ProfessorId);

        modelBuilder.Entity<Course>()
            .HasMany(course => course.StudentAttendances)
            .WithOne(attendance => attendance.Course)
            .HasForeignKey(attendance => attendance.CourseId);

        modelBuilder.Entity<UniversityGroup>()
            .HasMany(group => group.Students)
            .WithOne(student => student.UniversityGroup)
            .HasForeignKey(student => student.UniversityGroupId);

        modelBuilder.Entity<Student>()
            .HasMany(student => student.StudentAttendances)
            .WithOne(attendance => attendance.Student)
            .HasForeignKey(attendance => attendance.StudentId);

        modelBuilder.Entity<UniversityGroup>()
            .HasOne(group => group.Catalogue)
            .WithOne(catalogue => catalogue.UniversityGroup)
            .HasForeignKey<Catalogue>(catalogue => catalogue.UniversityGroupId);
    }
}
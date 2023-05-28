﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using UniversityScheduler.Api.Infrastructure.Repositories;

#nullable disable

namespace UniversityScheduler.Api.Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230528153005_Rebuild Tables")]
    partial class RebuildTables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("UniversityScheduler.Api.Core.Models.University_Entities.Catalogue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("UniversityGroupId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UniversityGroupId")
                        .IsUnique();

                    b.ToTable("Catalogues");
                });

            modelBuilder.Entity("UniversityScheduler.Api.Core.Models.University_Entities.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<int>("CourseType")
                        .HasColumnType("integer");

                    b.Property<int>("NumberOfCredits")
                        .HasColumnType("integer");

                    b.Property<int>("ProfessorId")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProfessorId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("UniversityScheduler.Api.Core.Models.University_Entities.Grade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CatalogueId")
                        .HasColumnType("integer");

                    b.Property<int>("CourseId")
                        .HasColumnType("integer");

                    b.Property<int>("StudentId")
                        .HasColumnType("integer");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CatalogueId");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("UniversityScheduler.Api.Core.Models.University_Entities.Professor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("BirthdayDate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CourseSpeciality")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.ToTable("Professors");
                });

            modelBuilder.Entity("UniversityScheduler.Api.Core.Models.University_Entities.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("BirthdayDate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Cnp")
                        .IsRequired()
                        .HasColumnType("varchar(13)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<int>("StudyYear")
                        .HasColumnType("integer");

                    b.Property<int?>("UniversityGroupId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Cnp")
                        .IsUnique();

                    b.HasIndex("UniversityGroupId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("UniversityScheduler.Api.Core.Models.University_Entities.StudentAttendance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("integer");

                    b.Property<string>("DateOfTheCourse")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("StudentId")
                        .HasColumnType("integer");

                    b.Property<string>("TimeOfTheCourse")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentAttendances");
                });

            modelBuilder.Entity("UniversityScheduler.Api.Core.Models.University_Entities.UniversityGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CatalogueId")
                        .HasColumnType("integer");

                    b.Property<string>("DiscordLink")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<int>("MaxSize")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(256)");

                    b.Property<int>("NumberOfMembers")
                        .HasColumnType("integer");

                    b.Property<int>("Speciality")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("UniversityGroups");
                });

            modelBuilder.Entity("UniversityScheduler.Api.Core.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("UniversityScheduler.Api.Core.Models.University_Entities.Catalogue", b =>
                {
                    b.HasOne("UniversityScheduler.Api.Core.Models.University_Entities.UniversityGroup", "UniversityGroup")
                        .WithOne("Catalogue")
                        .HasForeignKey("UniversityScheduler.Api.Core.Models.University_Entities.Catalogue", "UniversityGroupId");

                    b.Navigation("UniversityGroup");
                });

            modelBuilder.Entity("UniversityScheduler.Api.Core.Models.University_Entities.Course", b =>
                {
                    b.HasOne("UniversityScheduler.Api.Core.Models.University_Entities.Professor", "Professor")
                        .WithMany("Courses")
                        .HasForeignKey("ProfessorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Professor");
                });

            modelBuilder.Entity("UniversityScheduler.Api.Core.Models.University_Entities.Grade", b =>
                {
                    b.HasOne("UniversityScheduler.Api.Core.Models.University_Entities.Catalogue", "Catalogue")
                        .WithMany("Grades")
                        .HasForeignKey("CatalogueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Catalogue");
                });

            modelBuilder.Entity("UniversityScheduler.Api.Core.Models.University_Entities.Student", b =>
                {
                    b.HasOne("UniversityScheduler.Api.Core.Models.University_Entities.UniversityGroup", "UniversityGroup")
                        .WithMany("Students")
                        .HasForeignKey("UniversityGroupId");

                    b.Navigation("UniversityGroup");
                });

            modelBuilder.Entity("UniversityScheduler.Api.Core.Models.University_Entities.StudentAttendance", b =>
                {
                    b.HasOne("UniversityScheduler.Api.Core.Models.University_Entities.Course", "Course")
                        .WithMany("StudentAttendances")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniversityScheduler.Api.Core.Models.University_Entities.Student", "Student")
                        .WithMany("StudentAttendances")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("UniversityScheduler.Api.Core.Models.University_Entities.Catalogue", b =>
                {
                    b.Navigation("Grades");
                });

            modelBuilder.Entity("UniversityScheduler.Api.Core.Models.University_Entities.Course", b =>
                {
                    b.Navigation("StudentAttendances");
                });

            modelBuilder.Entity("UniversityScheduler.Api.Core.Models.University_Entities.Professor", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("UniversityScheduler.Api.Core.Models.University_Entities.Student", b =>
                {
                    b.Navigation("StudentAttendances");
                });

            modelBuilder.Entity("UniversityScheduler.Api.Core.Models.University_Entities.UniversityGroup", b =>
                {
                    b.Navigation("Catalogue");

                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}

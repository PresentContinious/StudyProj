﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231005083745_CompletedLessons")]
    partial class CompletedLessons
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataAccess.Models.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsCorrect")
                        .HasColumnType("bit");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("Answer");
                });

            modelBuilder.Entity("DataAccess.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Category");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "ІТ"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Аналіз даних"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Бізнес"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Гуманітарні науки"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Графічний дизайн"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Журналістика"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Іноземні мови"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Особистий розвиток"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Охорона здоров’я"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Підготовка до ЗНО"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Право"
                        },
                        new
                        {
                            Id = 12,
                            Name = "Психологія"
                        },
                        new
                        {
                            Id = 13,
                            Name = "Культура"
                        },
                        new
                        {
                            Id = 14,
                            Name = "Мистецтво"
                        },
                        new
                        {
                            Id = 15,
                            Name = "Суспільні науки"
                        });
                });

            modelBuilder.Entity("DataAccess.Models.CompletedLesson", b =>
                {
                    b.Property<int>("LessonId")
                        .HasColumnType("int");

                    b.Property<int>("StudentCourseCourseId")
                        .HasColumnType("int");

                    b.Property<string>("StudentCourseStudentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("TestPercentage")
                        .HasColumnType("int");

                    b.HasKey("LessonId", "StudentCourseCourseId", "StudentCourseStudentId");

                    b.HasIndex("StudentId");

                    b.HasIndex("StudentCourseCourseId", "StudentCourseStudentId");

                    b.ToTable("CompletedLesson");
                });

            modelBuilder.Entity("DataAccess.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<bool>("Certification")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Price")
                        .HasColumnType("int");

                    b.Property<string>("TeacherId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TotalMark")
                        .HasColumnType("int");

                    b.Property<int>("TotalReviews")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("TeacherId");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("DataAccess.Models.Data", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Data");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Soft Skills",
                            Type = 0
                        },
                        new
                        {
                            Id = 2,
                            Name = "Hard Skills",
                            Type = 0
                        },
                        new
                        {
                            Id = 3,
                            Name = "Іноземні мови",
                            Type = 0
                        },
                        new
                        {
                            Id = 4,
                            Name = "Особистий розвиток",
                            Type = 0
                        },
                        new
                        {
                            Id = 5,
                            Name = "Підготовка до ЗНО",
                            Type = 0
                        },
                        new
                        {
                            Id = 6,
                            Name = "Підготовка до вступу",
                            Type = 0
                        },
                        new
                        {
                            Id = 7,
                            Name = "Підготовка до іспитів",
                            Type = 0
                        },
                        new
                        {
                            Id = 8,
                            Name = "Підвищення кваліфікації",
                            Type = 1
                        },
                        new
                        {
                            Id = 9,
                            Name = "Підвищення рівня знань",
                            Type = 1
                        },
                        new
                        {
                            Id = 10,
                            Name = "Підвищення рівня володіння іноземною мовою",
                            Type = 1
                        },
                        new
                        {
                            Id = 11,
                            Name = "Адміністрація",
                            Type = 2
                        },
                        new
                        {
                            Id = 12,
                            Name = "Бухгалтерія",
                            Type = 2
                        },
                        new
                        {
                            Id = 13,
                            Name = "Веб-розробка",
                            Type = 2
                        },
                        new
                        {
                            Id = 14,
                            Name = "Видавництво",
                            Type = 2
                        },
                        new
                        {
                            Id = 15,
                            Name = "Видавнича справа",
                            Type = 2
                        },
                        new
                        {
                            Id = 16,
                            Name = "Початковий",
                            Type = 3
                        },
                        new
                        {
                            Id = 17,
                            Name = "Середній",
                            Type = 3
                        },
                        new
                        {
                            Id = 18,
                            Name = "Високий",
                            Type = 3
                        },
                        new
                        {
                            Id = 19,
                            Name = "Дуже високий",
                            Type = 3
                        });
                });

            modelBuilder.Entity("DataAccess.Models.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FileId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LessonId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LessonId");

                    b.ToTable("Document");
                });

            modelBuilder.Entity("DataAccess.Models.Lesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SessionId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SessionId");

                    b.ToTable("Lesson");
                });

            modelBuilder.Entity("DataAccess.Models.PaymentMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cvv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExpiryDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PaymentMethod");
                });

            modelBuilder.Entity("DataAccess.Models.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("LessonId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LessonId");

                    b.ToTable("Question");
                });

            modelBuilder.Entity("DataAccess.Models.Session", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Session");
                });

            modelBuilder.Entity("DataAccess.Models.StudentCourse", b =>
                {
                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("CurrentLessonId")
                        .HasColumnType("int");

                    b.Property<int>("CurrentSessionId")
                        .HasColumnType("int");

                    b.Property<bool>("Paid")
                        .HasColumnType("bit");

                    b.Property<int?>("PaymentMethodId")
                        .HasColumnType("int");

                    b.Property<int>("Progress")
                        .HasColumnType("int");

                    b.Property<string>("Review")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Stars")
                        .HasColumnType("int");

                    b.HasKey("CourseId", "StudentId");

                    b.HasIndex("PaymentMethodId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentCourse");
                });

            modelBuilder.Entity("DataAccess.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("About")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Aim")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("GetDiploma")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("NewOpportunities")
                        .HasColumnType("bit");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Occupation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("PhotoId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sex")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Skills")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("WantedLevel")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("DataAccess.Models.Video", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<long?>("DurationInMinutes")
                        .HasColumnType("bigint");

                    b.Property<string>("FileId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LessonId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LessonId")
                        .IsUnique();

                    b.ToTable("Video");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("DataAccess.Models.Student", b =>
                {
                    b.HasBaseType("DataAccess.Models.User");

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("DataAccess.Models.Teacher", b =>
                {
                    b.HasBaseType("DataAccess.Models.User");

                    b.HasDiscriminator().HasValue("Teacher");
                });

            modelBuilder.Entity("DataAccess.Models.Answer", b =>
                {
                    b.HasOne("DataAccess.Models.Question", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("DataAccess.Models.CompletedLesson", b =>
                {
                    b.HasOne("DataAccess.Models.Lesson", "Lesson")
                        .WithMany("CompletedLessons")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DataAccess.Models.Student", null)
                        .WithMany("CompletedLessons")
                        .HasForeignKey("StudentId");

                    b.HasOne("DataAccess.Models.StudentCourse", "StudentCourse")
                        .WithMany("CompletedLessons")
                        .HasForeignKey("StudentCourseCourseId", "StudentCourseStudentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Lesson");

                    b.Navigation("StudentCourse");
                });

            modelBuilder.Entity("DataAccess.Models.Course", b =>
                {
                    b.HasOne("DataAccess.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Models.Teacher", "Teacher")
                        .WithMany("Courses")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("DataAccess.Models.Document", b =>
                {
                    b.HasOne("DataAccess.Models.Lesson", "Lesson")
                        .WithMany("Documents")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("DataAccess.Models.Lesson", b =>
                {
                    b.HasOne("DataAccess.Models.Session", "Session")
                        .WithMany("Lessons")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Session");
                });

            modelBuilder.Entity("DataAccess.Models.Question", b =>
                {
                    b.HasOne("DataAccess.Models.Lesson", "Lesson")
                        .WithMany("Questions")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("DataAccess.Models.Session", b =>
                {
                    b.HasOne("DataAccess.Models.Course", "Course")
                        .WithMany("Sessions")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("DataAccess.Models.StudentCourse", b =>
                {
                    b.HasOne("DataAccess.Models.Course", "Course")
                        .WithMany("StudentCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DataAccess.Models.PaymentMethod", "PaymentMethod")
                        .WithMany()
                        .HasForeignKey("PaymentMethodId");

                    b.HasOne("DataAccess.Models.Student", "Student")
                        .WithMany("StudentCourses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("PaymentMethod");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("DataAccess.Models.Video", b =>
                {
                    b.HasOne("DataAccess.Models.Lesson", "Lesson")
                        .WithOne("Video")
                        .HasForeignKey("DataAccess.Models.Video", "LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DataAccess.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DataAccess.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAccess.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("DataAccess.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAccess.Models.Course", b =>
                {
                    b.Navigation("Sessions");

                    b.Navigation("StudentCourses");
                });

            modelBuilder.Entity("DataAccess.Models.Lesson", b =>
                {
                    b.Navigation("CompletedLessons");

                    b.Navigation("Documents");

                    b.Navigation("Questions");

                    b.Navigation("Video");
                });

            modelBuilder.Entity("DataAccess.Models.Question", b =>
                {
                    b.Navigation("Answers");
                });

            modelBuilder.Entity("DataAccess.Models.Session", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("DataAccess.Models.StudentCourse", b =>
                {
                    b.Navigation("CompletedLessons");
                });

            modelBuilder.Entity("DataAccess.Models.Student", b =>
                {
                    b.Navigation("CompletedLessons");

                    b.Navigation("StudentCourses");
                });

            modelBuilder.Entity("DataAccess.Models.Teacher", b =>
                {
                    b.Navigation("Courses");
                });
#pragma warning restore 612, 618
        }
    }
}
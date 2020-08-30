using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tuteexy.DataAccess.Migrations
{
    public partial class dd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    FullName = table.Column<string>(maxLength: 150, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HubTutorJob",
                columns: table => new
                {
                    TutorJobID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    JobTitle = table.Column<string>(maxLength: 100, nullable: false),
                    Course = table.Column<string>(maxLength: 100, nullable: false),
                    Subject = table.Column<string>(maxLength: 100, nullable: false),
                    Salary = table.Column<string>(maxLength: 100, nullable: false),
                    NumberofStudents = table.Column<string>(maxLength: 100, nullable: false),
                    Genderpreference = table.Column<string>(maxLength: 100, nullable: false),
                    Requirements = table.Column<string>(maxLength: 250, nullable: false),
                    StreetAddress = table.Column<string>(maxLength: 100, nullable: false),
                    City = table.Column<string>(maxLength: 100, nullable: false),
                    State = table.Column<string>(maxLength: 100, nullable: false),
                    PostalCode = table.Column<string>(maxLength: 100, nullable: false),
                    Country = table.Column<string>(maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HubTutorJob", x => x.TutorJobID);
                });

            migrationBuilder.CreateTable(
                name: "IMPage",
                columns: table => new
                {
                    PageID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    PageName = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IMPage", x => x.PageID);
                });

            migrationBuilder.CreateTable(
                name: "userlist",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 50, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 50, nullable: true),
                    SchoolName = table.Column<string>(maxLength: 50, nullable: true),
                    ClassName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userlist", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HubCourse",
                columns: table => new
                {
                    CourseID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(nullable: true),
                    Title = table.Column<string>(maxLength: 150, nullable: false),
                    Description = table.Column<string>(maxLength: 4000, nullable: false),
                    ImageUrl = table.Column<string>(maxLength: 150, nullable: true),
                    IsReplyClose = table.Column<bool>(nullable: false),
                    IsOffensive = table.Column<bool>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    SubmittedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HubCourse", x => x.CourseID);
                    table.ForeignKey(
                        name: "FK_HubCourse_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HubQuestion",
                columns: table => new
                {
                    QuestionID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 1024, nullable: false),
                    IsReplyClose = table.Column<bool>(nullable: false),
                    IsOffensive = table.Column<bool>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    SubmittedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HubQuestion", x => x.QuestionID);
                    table.ForeignKey(
                        name: "FK_HubQuestion_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HubShortStory",
                columns: table => new
                {
                    ShortStoryID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(nullable: true),
                    Title = table.Column<string>(maxLength: 150, nullable: false),
                    Description = table.Column<string>(maxLength: 4000, nullable: false),
                    ImageUrl = table.Column<string>(maxLength: 150, nullable: true),
                    IsReplyClose = table.Column<bool>(nullable: false),
                    IsOffensive = table.Column<bool>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    SubmittedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HubShortStory", x => x.ShortStoryID);
                    table.ForeignKey(
                        name: "FK_HubShortStory_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HubUserProfile",
                columns: table => new
                {
                    UserProfileID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(nullable: true),
                    FatherName = table.Column<string>(maxLength: 150, nullable: false),
                    MotherName = table.Column<string>(maxLength: 150, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime", nullable: false),
                    BloodGroup = table.Column<string>(maxLength: 20, nullable: false),
                    Gender = table.Column<string>(maxLength: 20, nullable: false),
                    Religion = table.Column<string>(maxLength: 20, nullable: false),
                    StreetAddress = table.Column<string>(maxLength: 100, nullable: true),
                    City = table.Column<string>(maxLength: 100, nullable: true),
                    State = table.Column<string>(maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(maxLength: 100, nullable: true),
                    Country = table.Column<string>(maxLength: 100, nullable: true),
                    ECPersonName = table.Column<string>(maxLength: 150, nullable: false),
                    ECPersonEmail = table.Column<string>(maxLength: 150, nullable: false),
                    ECPersonRelation = table.Column<string>(maxLength: 150, nullable: false),
                    ECPersonPhoneNumber = table.Column<string>(maxLength: 150, nullable: false),
                    ImageUrl = table.Column<string>(maxLength: 150, nullable: true),
                    FullName = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HubUserProfile", x => x.UserProfileID);
                    table.ForeignKey(
                        name: "FK_HubUserProfile_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LmsSchool",
                columns: table => new
                {
                    SchoolID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    SchoolName = table.Column<string>(maxLength: 100, nullable: false),
                    StreetAddress = table.Column<string>(maxLength: 100, nullable: true),
                    City = table.Column<string>(maxLength: 100, nullable: true),
                    State = table.Column<string>(maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(maxLength: 100, nullable: true),
                    Country = table.Column<string>(maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 100, nullable: true),
                    IsAuthorizedSchool = table.Column<bool>(nullable: false),
                    OwnerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsSchool", x => x.SchoolID);
                    table.ForeignKey(
                        name: "FK_LmsSchool_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HubCourseThread",
                columns: table => new
                {
                    CourseThreadID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseID = table.Column<long>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 1024, nullable: false),
                    IsReplyClose = table.Column<bool>(nullable: false),
                    IsOffensive = table.Column<bool>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    SubmittedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HubCourseThread", x => x.CourseThreadID);
                    table.ForeignKey(
                        name: "FK_HubCourseThread_HubCourse_CourseID",
                        column: x => x.CourseID,
                        principalTable: "HubCourse",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HubCourseThread_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HubQuestionThread",
                columns: table => new
                {
                    QuestionThreadID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionID = table.Column<long>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 1024, nullable: false),
                    IsReplyClose = table.Column<bool>(nullable: false),
                    IsOffensive = table.Column<bool>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    SubmittedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HubQuestionThread", x => x.QuestionThreadID);
                    table.ForeignKey(
                        name: "FK_HubQuestionThread_HubQuestion_QuestionID",
                        column: x => x.QuestionID,
                        principalTable: "HubQuestion",
                        principalColumn: "QuestionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HubQuestionThread_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HubShortStoryThread",
                columns: table => new
                {
                    ShortStoryThreadID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShortStoryID = table.Column<long>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 1024, nullable: false),
                    IsReplyClose = table.Column<bool>(nullable: false),
                    IsOffensive = table.Column<bool>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    SubmittedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HubShortStoryThread", x => x.ShortStoryThreadID);
                    table.ForeignKey(
                        name: "FK_HubShortStoryThread_HubShortStory_ShortStoryID",
                        column: x => x.ShortStoryID,
                        principalTable: "HubShortStory",
                        principalColumn: "ShortStoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HubShortStoryThread_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LmsClassRoom",
                columns: table => new
                {
                    ClassRoomID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    SchoolID = table.Column<long>(nullable: false),
                    ClassRoomName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsClassRoom", x => x.ClassRoomID);
                    table.ForeignKey(
                        name: "FK_LmsClassRoom_LmsSchool_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "LmsSchool",
                        principalColumn: "SchoolID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LmsHoliday",
                columns: table => new
                {
                    HolidayID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    SchoolID = table.Column<long>(nullable: false),
                    DateStart = table.Column<DateTime>(type: "datetime", nullable: false),
                    DateEnd = table.Column<DateTime>(type: "datetime", nullable: false),
                    HolidayName = table.Column<string>(maxLength: 128, nullable: false),
                    Duration = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsHoliday", x => x.HolidayID);
                    table.ForeignKey(
                        name: "FK_LmsHoliday_LmsSchool_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "LmsSchool",
                        principalColumn: "SchoolID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LmsSchoolNotice",
                columns: table => new
                {
                    SchoolNoticeID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    SchoolID = table.Column<long>(nullable: false),
                    Title = table.Column<string>(maxLength: 128, nullable: false),
                    Description = table.Column<string>(maxLength: 4000, nullable: false),
                    ScheduleDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    isPined = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsSchoolNotice", x => x.SchoolNoticeID);
                    table.ForeignKey(
                        name: "FK_LmsSchoolNotice_LmsSchool_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "LmsSchool",
                        principalColumn: "SchoolID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LmsSchoolTeacher",
                columns: table => new
                {
                    SchoolTeacherID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolID = table.Column<long>(nullable: false),
                    TeacherID = table.Column<string>(nullable: true),
                    ApprovedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsApproved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsSchoolTeacher", x => x.SchoolTeacherID);
                    table.ForeignKey(
                        name: "FK_LmsSchoolTeacher_LmsSchool_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "LmsSchool",
                        principalColumn: "SchoolID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LmsSchoolTeacher_AspNetUsers_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LmsSubject",
                columns: table => new
                {
                    SubjectID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    SchoolID = table.Column<long>(nullable: false),
                    SubjectName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsSubject", x => x.SubjectID);
                    table.ForeignKey(
                        name: "FK_LmsSubject_LmsSchool_SchoolID",
                        column: x => x.SchoolID,
                        principalTable: "LmsSchool",
                        principalColumn: "SchoolID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LmsClassRoomNotice",
                columns: table => new
                {
                    ClassRoomNoticeID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ClassRoomID = table.Column<long>(nullable: false),
                    Title = table.Column<string>(maxLength: 128, nullable: false),
                    Description = table.Column<string>(maxLength: 4000, nullable: false),
                    ScheduleDateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsClassRoomNotice", x => x.ClassRoomNoticeID);
                    table.ForeignKey(
                        name: "FK_LmsClassRoomNotice_LmsClassRoom_ClassRoomID",
                        column: x => x.ClassRoomID,
                        principalTable: "LmsClassRoom",
                        principalColumn: "ClassRoomID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LmsClassRoomStudent",
                columns: table => new
                {
                    ClassRoomStudentID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassRoomID = table.Column<long>(nullable: false),
                    StudentID = table.Column<string>(nullable: true),
                    ApprovedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsApproved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsClassRoomStudent", x => x.ClassRoomStudentID);
                    table.ForeignKey(
                        name: "FK_LmsClassRoomStudent_LmsClassRoom_ClassRoomID",
                        column: x => x.ClassRoomID,
                        principalTable: "LmsClassRoom",
                        principalColumn: "ClassRoomID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LmsClassRoomStudent_AspNetUsers_StudentID",
                        column: x => x.StudentID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LmsClassRoutine",
                columns: table => new
                {
                    ClassRoutineID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ClassRoomID = table.Column<long>(nullable: false),
                    DayName = table.Column<string>(maxLength: 50, nullable: false),
                    Period1 = table.Column<string>(maxLength: 50, nullable: false),
                    Period2 = table.Column<string>(maxLength: 50, nullable: false),
                    Period3 = table.Column<string>(maxLength: 50, nullable: false),
                    Period4 = table.Column<string>(maxLength: 50, nullable: true),
                    Period5 = table.Column<string>(maxLength: 50, nullable: true),
                    Period6 = table.Column<string>(maxLength: 50, nullable: true),
                    Period7 = table.Column<string>(maxLength: 50, nullable: true),
                    Period8 = table.Column<string>(maxLength: 50, nullable: true),
                    Period9 = table.Column<string>(maxLength: 50, nullable: true),
                    Period10 = table.Column<string>(maxLength: 50, nullable: true),
                    PeriodTime1 = table.Column<DateTime>(type: "datetime", nullable: false),
                    PeriodTime2 = table.Column<DateTime>(type: "datetime", nullable: false),
                    PeriodTime3 = table.Column<DateTime>(type: "datetime", nullable: false),
                    PeriodTime4 = table.Column<DateTime>(type: "datetime", nullable: false),
                    PeriodTime5 = table.Column<DateTime>(type: "datetime", nullable: false),
                    PeriodTime6 = table.Column<DateTime>(type: "datetime", nullable: false),
                    PeriodTime7 = table.Column<DateTime>(type: "datetime", nullable: false),
                    PeriodTime8 = table.Column<DateTime>(type: "datetime", nullable: false),
                    PeriodTime9 = table.Column<DateTime>(type: "datetime", nullable: false),
                    PeriodTime10 = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsClassRoutine", x => x.ClassRoutineID);
                    table.ForeignKey(
                        name: "FK_LmsClassRoutine_LmsClassRoom_ClassRoomID",
                        column: x => x.ClassRoomID,
                        principalTable: "LmsClassRoom",
                        principalColumn: "ClassRoomID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LmsClasswork",
                columns: table => new
                {
                    ClassworkID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassRoomID = table.Column<long>(nullable: false),
                    TeacherID = table.Column<string>(nullable: true),
                    Subject = table.Column<string>(maxLength: 64, nullable: false),
                    Title = table.Column<string>(maxLength: 128, nullable: false),
                    Description = table.Column<string>(maxLength: 4000, nullable: true),
                    DateAssigned = table.Column<DateTime>(type: "datetime", nullable: false),
                    TimeStart = table.Column<DateTime>(type: "datetime", nullable: false),
                    TimeEnd = table.Column<DateTime>(type: "datetime", nullable: false),
                    RefLink1 = table.Column<string>(maxLength: 150, nullable: true),
                    RefLink2 = table.Column<string>(maxLength: 150, nullable: true),
                    RefLink3 = table.Column<string>(maxLength: 150, nullable: true),
                    RefLink4 = table.Column<string>(maxLength: 150, nullable: true),
                    RefLink5 = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsClasswork", x => x.ClassworkID);
                    table.ForeignKey(
                        name: "FK_LmsClasswork_LmsClassRoom_ClassRoomID",
                        column: x => x.ClassRoomID,
                        principalTable: "LmsClassRoom",
                        principalColumn: "ClassRoomID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LmsClasswork_AspNetUsers_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LmsHomework",
                columns: table => new
                {
                    HomeworkID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassRoomID = table.Column<long>(nullable: false),
                    TeacherID = table.Column<string>(nullable: true),
                    TeacherName = table.Column<string>(maxLength: 150, nullable: true),
                    Subject = table.Column<string>(maxLength: 64, nullable: false),
                    Title = table.Column<string>(maxLength: 128, nullable: false),
                    Description = table.Column<string>(maxLength: 4000, nullable: true),
                    DateAssigned = table.Column<DateTime>(type: "datetime", nullable: false),
                    ScheduleDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    DateDue = table.Column<DateTime>(type: "datetime", nullable: false),
                    RefLink1 = table.Column<string>(maxLength: 150, nullable: true),
                    RefLink2 = table.Column<string>(maxLength: 150, nullable: true),
                    RefLink3 = table.Column<string>(maxLength: 150, nullable: true),
                    RefLink4 = table.Column<string>(maxLength: 150, nullable: true),
                    RefLink5 = table.Column<string>(maxLength: 150, nullable: true),
                    HwMarks = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsHomework", x => x.HomeworkID);
                    table.ForeignKey(
                        name: "FK_LmsHomework_LmsClassRoom_ClassRoomID",
                        column: x => x.ClassRoomID,
                        principalTable: "LmsClassRoom",
                        principalColumn: "ClassRoomID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LmsHomework_AspNetUsers_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LmsClassworkAttendance",
                columns: table => new
                {
                    ClassworkAttendanceID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassworkID = table.Column<long>(nullable: false),
                    StudentID = table.Column<string>(nullable: true),
                    AttenStatus = table.Column<string>(maxLength: 20, nullable: true),
                    WorkDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsClassworkAttendance", x => x.ClassworkAttendanceID);
                    table.ForeignKey(
                        name: "FK_LmsClassworkAttendance_LmsClasswork_ClassworkID",
                        column: x => x.ClassworkID,
                        principalTable: "LmsClasswork",
                        principalColumn: "ClassworkID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LmsClassworkAttendance_AspNetUsers_StudentID",
                        column: x => x.StudentID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LmsClassworkSheet",
                columns: table => new
                {
                    ClassworkSheetID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassworkID = table.Column<long>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 1024, nullable: false),
                    SubmittedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    AttnStatus = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsClassworkSheet", x => x.ClassworkSheetID);
                    table.ForeignKey(
                        name: "FK_LmsClassworkSheet_LmsClasswork_ClassworkID",
                        column: x => x.ClassworkID,
                        principalTable: "LmsClasswork",
                        principalColumn: "ClassworkID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LmsClassworkSheet_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LmsHomeworkSheet",
                columns: table => new
                {
                    HomeworkSheetID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomeworkID = table.Column<long>(nullable: false),
                    StudentID = table.Column<string>(nullable: true),
                    DateSubmitted = table.Column<DateTime>(type: "datetime", nullable: false),
                    Description = table.Column<string>(maxLength: 1024, nullable: true),
                    AttachLink1 = table.Column<string>(maxLength: 150, nullable: true),
                    AttachLink2 = table.Column<string>(maxLength: 150, nullable: true),
                    AttachLink3 = table.Column<string>(maxLength: 150, nullable: true),
                    AttachLink4 = table.Column<string>(maxLength: 150, nullable: true),
                    AttachLink5 = table.Column<string>(maxLength: 150, nullable: true),
                    HwMarks = table.Column<double>(nullable: false),
                    HWStatus = table.Column<string>(maxLength: 20, nullable: true),
                    HWComments = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LmsHomeworkSheet", x => x.HomeworkSheetID);
                    table.ForeignKey(
                        name: "FK_LmsHomeworkSheet_LmsHomework_HomeworkID",
                        column: x => x.HomeworkID,
                        principalTable: "LmsHomework",
                        principalColumn: "HomeworkID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LmsHomeworkSheet_AspNetUsers_StudentID",
                        column: x => x.StudentID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_HubCourse_UserID",
                table: "HubCourse",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_HubCourseThread_CourseID",
                table: "HubCourseThread",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_HubCourseThread_UserID",
                table: "HubCourseThread",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_HubQuestion_UserID",
                table: "HubQuestion",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_HubQuestionThread_QuestionID",
                table: "HubQuestionThread",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_HubQuestionThread_UserID",
                table: "HubQuestionThread",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_HubShortStory_UserID",
                table: "HubShortStory",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_HubShortStoryThread_ShortStoryID",
                table: "HubShortStoryThread",
                column: "ShortStoryID");

            migrationBuilder.CreateIndex(
                name: "IX_HubShortStoryThread_UserID",
                table: "HubShortStoryThread",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_HubUserProfile_UserID",
                table: "HubUserProfile",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_LmsClassRoom_SchoolID",
                table: "LmsClassRoom",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_LmsClassRoomNotice_ClassRoomID",
                table: "LmsClassRoomNotice",
                column: "ClassRoomID");

            migrationBuilder.CreateIndex(
                name: "IX_LmsClassRoomStudent_ClassRoomID",
                table: "LmsClassRoomStudent",
                column: "ClassRoomID");

            migrationBuilder.CreateIndex(
                name: "IX_LmsClassRoomStudent_StudentID",
                table: "LmsClassRoomStudent",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_LmsClassRoutine_ClassRoomID",
                table: "LmsClassRoutine",
                column: "ClassRoomID");

            migrationBuilder.CreateIndex(
                name: "IX_LmsClasswork_ClassRoomID",
                table: "LmsClasswork",
                column: "ClassRoomID");

            migrationBuilder.CreateIndex(
                name: "IX_LmsClasswork_TeacherID",
                table: "LmsClasswork",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_LmsClassworkAttendance_ClassworkID",
                table: "LmsClassworkAttendance",
                column: "ClassworkID");

            migrationBuilder.CreateIndex(
                name: "IX_LmsClassworkAttendance_StudentID",
                table: "LmsClassworkAttendance",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_LmsClassworkSheet_ClassworkID",
                table: "LmsClassworkSheet",
                column: "ClassworkID");

            migrationBuilder.CreateIndex(
                name: "IX_LmsClassworkSheet_UserID",
                table: "LmsClassworkSheet",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_LmsHoliday_SchoolID",
                table: "LmsHoliday",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_LmsHomework_ClassRoomID",
                table: "LmsHomework",
                column: "ClassRoomID");

            migrationBuilder.CreateIndex(
                name: "IX_LmsHomework_TeacherID",
                table: "LmsHomework",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_LmsHomeworkSheet_HomeworkID",
                table: "LmsHomeworkSheet",
                column: "HomeworkID");

            migrationBuilder.CreateIndex(
                name: "IX_LmsHomeworkSheet_StudentID",
                table: "LmsHomeworkSheet",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_LmsSchool_OwnerId",
                table: "LmsSchool",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_LmsSchoolNotice_SchoolID",
                table: "LmsSchoolNotice",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_LmsSchoolTeacher_SchoolID",
                table: "LmsSchoolTeacher",
                column: "SchoolID");

            migrationBuilder.CreateIndex(
                name: "IX_LmsSchoolTeacher_TeacherID",
                table: "LmsSchoolTeacher",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_LmsSubject_SchoolID",
                table: "LmsSubject",
                column: "SchoolID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "HubCourseThread");

            migrationBuilder.DropTable(
                name: "HubQuestionThread");

            migrationBuilder.DropTable(
                name: "HubShortStoryThread");

            migrationBuilder.DropTable(
                name: "HubTutorJob");

            migrationBuilder.DropTable(
                name: "HubUserProfile");

            migrationBuilder.DropTable(
                name: "IMPage");

            migrationBuilder.DropTable(
                name: "LmsClassRoomNotice");

            migrationBuilder.DropTable(
                name: "LmsClassRoomStudent");

            migrationBuilder.DropTable(
                name: "LmsClassRoutine");

            migrationBuilder.DropTable(
                name: "LmsClassworkAttendance");

            migrationBuilder.DropTable(
                name: "LmsClassworkSheet");

            migrationBuilder.DropTable(
                name: "LmsHoliday");

            migrationBuilder.DropTable(
                name: "LmsHomeworkSheet");

            migrationBuilder.DropTable(
                name: "LmsSchoolNotice");

            migrationBuilder.DropTable(
                name: "LmsSchoolTeacher");

            migrationBuilder.DropTable(
                name: "LmsSubject");

            migrationBuilder.DropTable(
                name: "userlist");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "HubCourse");

            migrationBuilder.DropTable(
                name: "HubQuestion");

            migrationBuilder.DropTable(
                name: "HubShortStory");

            migrationBuilder.DropTable(
                name: "LmsClasswork");

            migrationBuilder.DropTable(
                name: "LmsHomework");

            migrationBuilder.DropTable(
                name: "LmsClassRoom");

            migrationBuilder.DropTable(
                name: "LmsSchool");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}

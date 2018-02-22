using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebAPIApplication.Migrations
{
    public partial class Initializer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Credentials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credentials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducatorDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducatorDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LearnerDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Subscribed = table.Column<bool>(type: "bit", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearnerDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classroom",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EducatorDetailId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classroom", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classroom_EducatorDetails_EducatorDetailId",
                        column: x => x.EducatorDetailId,
                        principalTable: "EducatorDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EducatorDetailsId = table.Column<int>(type: "int", nullable: true),
                    LearnerDetailsId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_EducatorDetails_EducatorDetailsId",
                        column: x => x.EducatorDetailsId,
                        principalTable: "EducatorDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_LearnerDetails_LearnerDetailsId",
                        column: x => x.LearnerDetailsId,
                        principalTable: "LearnerDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Enrollment",
                columns: table => new
                {
                    EnrollmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClassroomID = table.Column<int>(type: "int", nullable: false),
                    LearnerDetailsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollment", x => x.EnrollmentID);
                    table.ForeignKey(
                        name: "FK_Enrollment_Classroom_ClassroomID",
                        column: x => x.ClassroomID,
                        principalTable: "Classroom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollment_LearnerDetails_LearnerDetailsId",
                        column: x => x.LearnerDetailsId,
                        principalTable: "LearnerDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classroom_EducatorDetailId",
                table: "Classroom",
                column: "EducatorDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_ClassroomID",
                table: "Enrollment",
                column: "ClassroomID");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_LearnerDetailsId",
                table: "Enrollment",
                column: "LearnerDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EducatorDetailsId",
                table: "Users",
                column: "EducatorDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_LearnerDetailsId",
                table: "Users",
                column: "LearnerDetailsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Credentials");

            migrationBuilder.DropTable(
                name: "Enrollment");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Classroom");

            migrationBuilder.DropTable(
                name: "LearnerDetails");

            migrationBuilder.DropTable(
                name: "EducatorDetails");
        }
    }
}

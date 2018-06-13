using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebAPIApplication.Migrations
{
    public partial class newinvitetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentInvitation_LearnerDetails_LearnerDetailsId",
                table: "StudentInvitation");

            migrationBuilder.DropIndex(
                name: "IX_StudentInvitation_LearnerDetailsId",
                table: "StudentInvitation");

            migrationBuilder.DropColumn(
                name: "LearnerDetailsId",
                table: "StudentInvitation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LearnerDetailsId",
                table: "StudentInvitation",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StudentInvitation_LearnerDetailsId",
                table: "StudentInvitation",
                column: "LearnerDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentInvitation_LearnerDetails_LearnerDetailsId",
                table: "StudentInvitation",
                column: "LearnerDetailsId",
                principalTable: "LearnerDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Vrektproject.Migrations
{
    public partial class _12312312 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "Profiles");

            migrationBuilder.AddColumn<string>(
                name: "CloudinaryImage",
                table: "Profiles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloudinaryImage",
                table: "Profiles");

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfileImage",
                table: "Profiles",
                nullable: true);
        }
    }
}

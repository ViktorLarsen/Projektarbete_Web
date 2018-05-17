using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Vrektproject.Migrations
{
    public partial class uploadpicturetest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CloudinaryImage",
                table: "Profiles");

            migrationBuilder.AddColumn<byte[]>(
                name: "AvatarImage",
                table: "Profiles",
                maxLength: 2147483647,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarImage",
                table: "Profiles");

            migrationBuilder.AddColumn<string>(
                name: "CloudinaryImage",
                table: "Profiles",
                nullable: true);
        }
    }
}

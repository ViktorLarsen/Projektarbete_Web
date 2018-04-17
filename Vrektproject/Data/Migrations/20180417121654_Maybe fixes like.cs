using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Vrektproject.Data.Migrations
{
    public partial class Maybefixeslike : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_AspNetUsers_MemberId1",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_AspNetUsers_RecruiterId1",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_MemberId1",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_RecruiterId1",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "MemberId1",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "RecruiterId1",
                table: "Likes");

            migrationBuilder.AlterColumn<string>(
                name: "RecruiterId",
                table: "Likes",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "MemberId",
                table: "Likes",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Likes_MemberId",
                table: "Likes",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_RecruiterId",
                table: "Likes",
                column: "RecruiterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_AspNetUsers_MemberId",
                table: "Likes",
                column: "MemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_AspNetUsers_RecruiterId",
                table: "Likes",
                column: "RecruiterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_AspNetUsers_MemberId",
                table: "Likes");

            migrationBuilder.DropForeignKey(
                name: "FK_Likes_AspNetUsers_RecruiterId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_MemberId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_RecruiterId",
                table: "Likes");

            migrationBuilder.AlterColumn<int>(
                name: "RecruiterId",
                table: "Likes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "Likes",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MemberId1",
                table: "Likes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecruiterId1",
                table: "Likes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Likes_MemberId1",
                table: "Likes",
                column: "MemberId1");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_RecruiterId1",
                table: "Likes",
                column: "RecruiterId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_AspNetUsers_MemberId1",
                table: "Likes",
                column: "MemberId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_AspNetUsers_RecruiterId1",
                table: "Likes",
                column: "RecruiterId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

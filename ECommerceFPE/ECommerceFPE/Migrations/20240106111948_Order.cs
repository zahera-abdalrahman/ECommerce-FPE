﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceFPE.Migrations
{
    /// <inheritdoc />
    public partial class Order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "ReviewAll");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "ReviewAll",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
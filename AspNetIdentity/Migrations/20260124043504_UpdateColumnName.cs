using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AspNetIdentity.Migrations
{
    /// <inheritdoc />
    public partial class UpdateColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "Users",
                newName: "LastName");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b92f0a3e-573b-4b12-8db1-2ccf6d58a34a"),
                column: "ConcurrencyStamp",
                value: "3f9025f1-8855-479b-8b5e-5017bc27c791");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c8d89a25-4b96-4f20-9d79-7f8a54c5213d"),
                column: "ConcurrencyStamp",
                value: "2fe9dae1-4475-4ea4-97e0-ee7c76ec34e6");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d7f44a42-1c1b-4c9f-8a50-55f6b234e8e2"),
                column: "ConcurrencyStamp",
                value: "2dac6261-6139-4c0b-b93e-fdcd18516812");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f2e6b8a1-9d43-4a7c-9f32-71d7c5dbe9f0"),
                column: "ConcurrencyStamp",
                value: "73464957-4fbe-47a0-9e33-c77a8e2ad35a");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Users",
                newName: "Lastname");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b92f0a3e-573b-4b12-8db1-2ccf6d58a34a"),
                column: "ConcurrencyStamp",
                value: "37ddcf05-8f88-4f72-b29b-6776e750617a");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c8d89a25-4b96-4f20-9d79-7f8a54c5213d"),
                column: "ConcurrencyStamp",
                value: "70bf5c0c-94d9-415c-9ac7-091bf69a1ff5");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d7f44a42-1c1b-4c9f-8a50-55f6b234e8e2"),
                column: "ConcurrencyStamp",
                value: "da8e1b24-2c2c-4e69-9b8f-e2bfacdaac41");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f2e6b8a1-9d43-4a7c-9f32-71d7c5dbe9f0"),
                column: "ConcurrencyStamp",
                value: "96cde0e6-9958-4313-b412-c7c7a7e1f179");
        }
    }
}

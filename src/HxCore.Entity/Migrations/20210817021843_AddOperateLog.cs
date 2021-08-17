using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HxCore.Entity.Migrations
{
    public partial class AddOperateLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "T_OperateLog");

            migrationBuilder.DropColumn(
                name: "Creater",
                table: "T_OperateLog");

            migrationBuilder.DropColumn(
                name: "CreaterId",
                table: "T_OperateLog");

            migrationBuilder.DropColumn(
                name: "Route",
                table: "T_OperateLog");

            migrationBuilder.DropColumn(
               name: "LastModifyTime",
               table: "T_OperateLog");

            migrationBuilder.DropColumn(
               name: "LastModifierId",
               table: "T_OperateLog");

            migrationBuilder.DropColumn(
               name: "LastModifier",
               table: "T_OperateLog");

            migrationBuilder.DropColumn(
               name: "Description",
               table: "T_OperateLog");

            //migrationBuilder.RenameColumn(
            //    name: "LastModifyTime",
            //    table: "T_OperateLog",
            //    newName: "OperateTime");

            //migrationBuilder.RenameColumn(
            //    name: "LastModifierId",
            //    table: "T_OperateLog",
            //    newName: "OperaterId");

            //migrationBuilder.RenameColumn(
            //    name: "LastModifier",
            //    table: "T_OperateLog",
            //    newName: "Operater");

            //migrationBuilder.RenameColumn(
            //    name: "Description",
            //    table: "T_OperateLog",
            //    newName: "Param");

            migrationBuilder.AddColumn<DateTime>(
               name: "OperateTime",
               table: "T_OperateLog",
               type: "datetime",
               nullable: true);

            migrationBuilder.AddColumn<string>(
               name: "OperaterId",
               table: "T_OperateLog",
               type: "varchar(36)",
               maxLength: 36,
               nullable: true);

            migrationBuilder.AddColumn<string>(
               name: "Operater",
               table: "T_OperateLog",
               type: "varchar(36)",
               maxLength: 36,
               nullable: true);

            migrationBuilder.AddColumn<string>(
               name: "Param",
               table: "T_OperateLog",
               type: "varchar(2000)",
               maxLength: 2000,
               nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ActionName",
                table: "T_OperateLog",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Agent",
                table: "T_OperateLog",
                type: "varchar(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ControllerName",
                table: "T_OperateLog",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ElapsedTime",
                table: "T_OperateLog",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "HttpMethod",
                table: "T_OperateLog",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "T_OperateLog",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Result",
                table: "T_OperateLog",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Success",
                table: "T_OperateLog",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "T_OperateLog",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionName",
                table: "T_OperateLog");

            migrationBuilder.DropColumn(
                name: "Agent",
                table: "T_OperateLog");

            migrationBuilder.DropColumn(
                name: "ControllerName",
                table: "T_OperateLog");

            migrationBuilder.DropColumn(
                name: "ElapsedTime",
                table: "T_OperateLog");

            migrationBuilder.DropColumn(
                name: "HttpMethod",
                table: "T_OperateLog");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "T_OperateLog");

            migrationBuilder.DropColumn(
                name: "Result",
                table: "T_OperateLog");

            migrationBuilder.DropColumn(
                name: "Success",
                table: "T_OperateLog");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "T_OperateLog");
            
            migrationBuilder.DropColumn(
                name: "OperateTime",
                table: "T_OperateLog");

            migrationBuilder.DropColumn(
                name: "OperaterId",
                table: "T_OperateLog");

            migrationBuilder.DropColumn(
                name: "Operater",
                table: "T_OperateLog");

            migrationBuilder.DropColumn(
                name: "Param",
                table: "T_OperateLog");
            //migrationBuilder.RenameColumn(
            //    name: "Param",
            //    table: "T_OperateLog",
            //    newName: "Description");

            //migrationBuilder.RenameColumn(
            //    name: "OperaterId",
            //    table: "T_OperateLog",
            //    newName: "LastModifierId");

            //migrationBuilder.RenameColumn(
            //    name: "Operater",
            //    table: "T_OperateLog",
            //    newName: "LastModifier");

            //migrationBuilder.RenameColumn(
            //    name: "OperateTime",
            //    table: "T_OperateLog",
            //    newName: "LastModifyTime");
            migrationBuilder.AddColumn<DateTime>(
               name: "LastModifyTime",
               table: "T_OperateLog",
               type: "datetime",
               nullable: true);

            migrationBuilder.AddColumn<string>(
               name: "LastModifierId",
               table: "T_OperateLog",
               type: "varchar(36)",
               maxLength: 36,
               nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifier",
                table: "T_OperateLog",
                type: "varchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
               name: "Description",
               table: "T_OperateLog",
               type: "varchar(2000)",
               maxLength: 2000,
               nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "T_OperateLog",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Creater",
                table: "T_OperateLog",
                type: "varchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreaterId",
                table: "T_OperateLog",
                type: "varchar(36)",
                maxLength: 36,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Route",
                table: "T_OperateLog",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true);
        }
    }
}

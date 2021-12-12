using Microsoft.EntityFrameworkCore.Migrations;

namespace HxCore.Extras.EntityFrameworkCore.Migrations
{
    public partial class UpdateBlogTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlogTags",
                table: "T_Blog");

            migrationBuilder.CreateTable(
                name: "T_BlogTag",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    BlogId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    TagId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_BlogTag", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_BlogTag");

            migrationBuilder.AddColumn<string>(
                name: "BlogTags",
                table: "T_Blog",
                type: "varchar(40)",
                maxLength: 40,
                nullable: true);
        }
    }
}

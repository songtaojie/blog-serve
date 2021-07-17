using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HxCore.Entity.Migrations
{
    public partial class InitTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_BasicInfo",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreaterId = table.Column<string>(maxLength: 36, nullable: true),
                    Creater = table.Column<string>(maxLength: 36, nullable: true),
                    LastModifyTime = table.Column<DateTime>(nullable: true),
                    LastModifier = table.Column<string>(maxLength: 36, nullable: true),
                    LastModifierId = table.Column<string>(maxLength: 36, nullable: true),
                    RealName = table.Column<string>(maxLength: 36, nullable: true),
                    CardId = table.Column<string>(maxLength: 40, nullable: true),
                    Birthday = table.Column<DateTime>(nullable: true),
                    Gender = table.Column<string>(maxLength: 8, nullable: true),
                    QQ = table.Column<string>(maxLength: 40, nullable: true),
                    WeChat = table.Column<string>(maxLength: 40, nullable: true),
                    Telephone = table.Column<string>(maxLength: 40, nullable: true),
                    Mobile = table.Column<string>(maxLength: 40, nullable: true),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    Address = table.Column<string>(maxLength: 200, nullable: true),
                    School = table.Column<string>(maxLength: 200, nullable: true),
                    UserId = table.Column<string>(maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_BasicInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Blog",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreaterId = table.Column<string>(maxLength: 36, nullable: true),
                    Creater = table.Column<string>(maxLength: 36, nullable: true),
                    LastModifyTime = table.Column<DateTime>(nullable: true),
                    LastModifier = table.Column<string>(maxLength: 36, nullable: true),
                    LastModifierId = table.Column<string>(maxLength: 36, nullable: true),
                    Deleted = table.Column<string>(type: "char(1)", nullable: true),
                    Disabled = table.Column<string>(type: "char(1)", nullable: true),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    PureContent = table.Column<string>(maxLength: 1000, nullable: true),
                    MarkDown = table.Column<string>(type: "char(1)", nullable: true),
                    Private = table.Column<string>(type: "char(1)", nullable: true),
                    Forward = table.Column<string>(type: "char(1)", nullable: true),
                    Publish = table.Column<string>(type: "char(1)", nullable: true),
                    PublishDate = table.Column<DateTime>(nullable: true),
                    Top = table.Column<string>(type: "char(1)", nullable: true),
                    Essence = table.Column<string>(type: "char(1)", nullable: true),
                    BlogTags = table.Column<string>(maxLength: 40, nullable: true),
                    CanCmt = table.Column<string>(type: "char(1)", nullable: true),
                    ReadCount = table.Column<long>(nullable: false),
                    LikeCount = table.Column<long>(nullable: false),
                    FavCount = table.Column<long>(nullable: false),
                    CmtCount = table.Column<long>(nullable: false),
                    PersonTop = table.Column<string>(type: "char(1)", nullable: true),
                    CoverImgUrl = table.Column<string>(maxLength: 255, nullable: true),
                    OrderFactor = table.Column<decimal>(nullable: false),
                    CategoryId = table.Column<string>(maxLength: 36, nullable: true),
                    BlogTypeId = table.Column<string>(maxLength: 36, nullable: true),
                    Carousel = table.Column<string>(type: "char(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Blog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_BlogExtend",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    BlogId = table.Column<string>(maxLength: 36, nullable: true),
                    Content = table.Column<string>(nullable: true),
                    ContentHtml = table.Column<string>(nullable: true),
                    ForwardUrl = table.Column<string>(maxLength: 255, nullable: true),
                    Location = table.Column<string>(maxLength: 255, nullable: true),
                    City = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_BlogExtend", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_BlogTag",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreaterId = table.Column<string>(maxLength: 36, nullable: true),
                    Creater = table.Column<string>(maxLength: 36, nullable: true),
                    LastModifyTime = table.Column<DateTime>(nullable: true),
                    LastModifier = table.Column<string>(maxLength: 36, nullable: true),
                    LastModifierId = table.Column<string>(maxLength: 36, nullable: true),
                    Deleted = table.Column<string>(type: "char(1)", nullable: true),
                    Disabled = table.Column<string>(type: "char(1)", nullable: true),
                    Name = table.Column<string>(maxLength: 36, nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    OrderIndex = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_BlogTag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_BlogType",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreaterId = table.Column<string>(maxLength: 36, nullable: true),
                    Creater = table.Column<string>(maxLength: 36, nullable: true),
                    LastModifyTime = table.Column<DateTime>(nullable: true),
                    LastModifier = table.Column<string>(maxLength: 36, nullable: true),
                    LastModifierId = table.Column<string>(maxLength: 36, nullable: true),
                    Deleted = table.Column<string>(type: "char(1)", nullable: true),
                    Disabled = table.Column<string>(type: "char(1)", nullable: true),
                    Name = table.Column<string>(maxLength: 40, nullable: true),
                    OrderIndex = table.Column<int>(nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_BlogType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Category",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreaterId = table.Column<string>(maxLength: 36, nullable: true),
                    Creater = table.Column<string>(maxLength: 36, nullable: true),
                    LastModifyTime = table.Column<DateTime>(nullable: true),
                    LastModifier = table.Column<string>(maxLength: 36, nullable: true),
                    LastModifierId = table.Column<string>(maxLength: 36, nullable: true),
                    Deleted = table.Column<string>(type: "char(1)", nullable: true),
                    Disabled = table.Column<string>(type: "char(1)", nullable: true),
                    Name = table.Column<string>(maxLength: 40, nullable: true),
                    OrderIndex = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_JobInfo",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreaterId = table.Column<string>(maxLength: 36, nullable: true),
                    Creater = table.Column<string>(maxLength: 36, nullable: true),
                    LastModifyTime = table.Column<DateTime>(nullable: true),
                    LastModifier = table.Column<string>(maxLength: 36, nullable: true),
                    LastModifierId = table.Column<string>(maxLength: 36, nullable: true),
                    Position = table.Column<string>(maxLength: 100, nullable: true),
                    Industry = table.Column<string>(maxLength: 100, nullable: true),
                    WorkUnit = table.Column<string>(maxLength: 100, nullable: true),
                    WorkYear = table.Column<int>(nullable: true),
                    Status = table.Column<string>(maxLength: 20, nullable: true),
                    Skills = table.Column<string>(maxLength: 1000, nullable: true),
                    GoodAreas = table.Column<string>(maxLength: 1000, nullable: true),
                    UserId = table.Column<string>(maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_JobInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Menu",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreaterId = table.Column<string>(maxLength: 36, nullable: true),
                    Creater = table.Column<string>(maxLength: 36, nullable: true),
                    LastModifyTime = table.Column<DateTime>(nullable: true),
                    LastModifier = table.Column<string>(maxLength: 36, nullable: true),
                    LastModifierId = table.Column<string>(maxLength: 36, nullable: true),
                    Deleted = table.Column<string>(type: "char(1)", nullable: true),
                    Disabled = table.Column<string>(type: "char(1)", nullable: true),
                    ParentId = table.Column<string>(maxLength: 36, nullable: true),
                    Path = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Component = table.Column<string>(maxLength: 50, nullable: true),
                    MenuType = table.Column<int>(nullable: false),
                    IsHide = table.Column<bool>(nullable: false),
                    IskeepAlive = table.Column<bool>(nullable: false),
                    Icon = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    OrderSort = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Module",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreaterId = table.Column<string>(maxLength: 36, nullable: true),
                    Creater = table.Column<string>(maxLength: 36, nullable: true),
                    LastModifyTime = table.Column<DateTime>(nullable: true),
                    LastModifier = table.Column<string>(maxLength: 36, nullable: true),
                    LastModifierId = table.Column<string>(maxLength: 36, nullable: true),
                    Deleted = table.Column<string>(type: "char(1)", nullable: true),
                    Disabled = table.Column<string>(type: "char(1)", nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    LinkUrl = table.Column<string>(maxLength: 100, nullable: true),
                    Area = table.Column<string>(maxLength: 100, nullable: true),
                    Controller = table.Column<string>(maxLength: 100, nullable: true),
                    Action = table.Column<string>(maxLength: 100, nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    OrderSort = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Module", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Role",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreaterId = table.Column<string>(maxLength: 36, nullable: true),
                    Creater = table.Column<string>(maxLength: 36, nullable: true),
                    LastModifyTime = table.Column<DateTime>(nullable: true),
                    LastModifier = table.Column<string>(maxLength: 36, nullable: true),
                    LastModifierId = table.Column<string>(maxLength: 36, nullable: true),
                    Deleted = table.Column<string>(type: "char(1)", nullable: true),
                    Disabled = table.Column<string>(type: "char(1)", nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    OrderSort = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_RoleMenu",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    RoleId = table.Column<string>(maxLength: 36, nullable: false),
                    PermissionId = table.Column<string>(maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_RoleMenu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_User",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    CreaterId = table.Column<string>(maxLength: 36, nullable: true),
                    Creater = table.Column<string>(maxLength: 36, nullable: true),
                    LastModifyTime = table.Column<DateTime>(nullable: true),
                    LastModifier = table.Column<string>(maxLength: 36, nullable: true),
                    LastModifierId = table.Column<string>(maxLength: 36, nullable: true),
                    Deleted = table.Column<string>(type: "char(1)", nullable: true),
                    Disabled = table.Column<string>(type: "char(1)", nullable: true),
                    UserName = table.Column<string>(maxLength: 36, nullable: false),
                    PassWord = table.Column<string>(maxLength: 36, nullable: false),
                    NickName = table.Column<string>(maxLength: 36, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    OpenId = table.Column<string>(maxLength: 80, nullable: true),
                    Lock = table.Column<string>(type: "char(1)", nullable: true),
                    AvatarUrl = table.Column<string>(maxLength: 500, nullable: true),
                    SuperAdmin = table.Column<string>(type: "char(1)", nullable: true),
                    Activate = table.Column<string>(type: "char(1)", nullable: true),
                    RegisterTime = table.Column<DateTime>(nullable: false),
                    UseMdEdit = table.Column<string>(type: "char(1)", nullable: true),
                    LoginIp = table.Column<string>(maxLength: 100, nullable: true),
                    LastLoginTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_UserRole",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    UserId = table.Column<string>(maxLength: 36, nullable: false),
                    RoleId = table.Column<string>(maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_UserRole", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_BasicInfo");

            migrationBuilder.DropTable(
                name: "T_Blog");

            migrationBuilder.DropTable(
                name: "T_BlogExtend");

            migrationBuilder.DropTable(
                name: "T_BlogTag");

            migrationBuilder.DropTable(
                name: "T_BlogType");

            migrationBuilder.DropTable(
                name: "T_Category");

            migrationBuilder.DropTable(
                name: "T_JobInfo");

            migrationBuilder.DropTable(
                name: "T_Menu");

            migrationBuilder.DropTable(
                name: "T_Module");

            migrationBuilder.DropTable(
                name: "T_Role");

            migrationBuilder.DropTable(
                name: "T_RoleMenu");

            migrationBuilder.DropTable(
                name: "T_User");

            migrationBuilder.DropTable(
                name: "T_UserRole");
        }
    }
}

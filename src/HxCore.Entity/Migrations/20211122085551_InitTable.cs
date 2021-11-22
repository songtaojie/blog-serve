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
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    RealName = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    CardId = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime", nullable: true),
                    Gender = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: true),
                    QQ = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true),
                    WeChat = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true),
                    Telephone = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true),
                    Mobile = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true),
                    Description = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    Address = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    School = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    UserId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreaterId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    Creater = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    LastModifyTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifier = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    LastModifierId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_BasicInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Blog",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    Title = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    PureContent = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                    MarkDown = table.Column<string>(type: "char(1)", nullable: true),
                    Private = table.Column<string>(type: "char(1)", nullable: true),
                    Forward = table.Column<string>(type: "char(1)", nullable: true),
                    Publish = table.Column<string>(type: "char(1)", nullable: true),
                    PublishDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Top = table.Column<string>(type: "char(1)", nullable: true),
                    Essence = table.Column<string>(type: "char(1)", nullable: true),
                    BlogTags = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true),
                    CanCmt = table.Column<string>(type: "char(1)", nullable: true),
                    ReadCount = table.Column<long>(type: "bigint", nullable: false),
                    LikeCount = table.Column<long>(type: "bigint", nullable: false),
                    FavCount = table.Column<long>(type: "bigint", nullable: false),
                    CmtCount = table.Column<long>(type: "bigint", nullable: false),
                    PersonTop = table.Column<string>(type: "char(1)", nullable: true),
                    CoverImgUrl = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    OrderFactor = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    CategoryId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    BlogTypeId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    Carousel = table.Column<string>(type: "char(1)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreaterId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    Creater = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    LastModifyTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifier = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    LastModifierId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    Deleted = table.Column<string>(type: "char(1)", nullable: true),
                    Disabled = table.Column<string>(type: "char(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Blog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_BlogExtend",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    BlogId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true),
                    ContentHtml = table.Column<string>(type: "text", nullable: true),
                    ForwardUrl = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    Location = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_BlogExtend", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_BlogTag",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    OrderIndex = table.Column<int>(type: "int", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreaterId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    Creater = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    LastModifyTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifier = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    LastModifierId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    Deleted = table.Column<string>(type: "char(1)", nullable: true),
                    Disabled = table.Column<string>(type: "char(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_BlogTag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_BlogType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true),
                    OrderIndex = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreaterId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    Creater = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    LastModifyTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifier = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    LastModifierId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    Deleted = table.Column<string>(type: "char(1)", nullable: true),
                    Disabled = table.Column<string>(type: "char(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_BlogType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Category",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: true),
                    OrderIndex = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreaterId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    Creater = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    LastModifyTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifier = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    LastModifierId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    Deleted = table.Column<string>(type: "char(1)", nullable: true),
                    Disabled = table.Column<string>(type: "char(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_JobInfo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    Position = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Industry = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    WorkUnit = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    WorkYear = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    Skills = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                    GoodAreas = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                    UserId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreaterId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    Creater = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    LastModifyTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifier = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    LastModifierId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_JobInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Menu",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    ParentId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    Path = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Component = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    MenuType = table.Column<int>(type: "int", nullable: false),
                    IsHide = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IskeepAlive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Icon = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    OrderSort = table.Column<int>(type: "int", nullable: false),
                    IsSystem = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreaterId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    Creater = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    LastModifyTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifier = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    LastModifierId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    Deleted = table.Column<string>(type: "char(1)", nullable: true),
                    Disabled = table.Column<string>(type: "char(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_MenuModule",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    ModuleId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    PermissionId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_MenuModule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Module",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    RouteUrl = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Area = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Controller = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Action = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    HttpMethod = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    OrderSort = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreaterId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    Creater = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    LastModifyTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifier = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    LastModifierId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    Deleted = table.Column<string>(type: "char(1)", nullable: true),
                    Disabled = table.Column<string>(type: "char(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Module", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_OperateLog",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    IPAddress = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    Location = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    Success = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Agent = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: true),
                    Url = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    ControllerName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    ActionName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    HttpMethod = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    Param = table.Column<string>(type: "text", nullable: true),
                    Result = table.Column<string>(type: "text", nullable: true),
                    ElapsedTime = table.Column<long>(type: "bigint", nullable: false),
                    OperateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    OperaterId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    Operater = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_OperateLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Role",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    OrderSort = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreaterId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    Creater = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    LastModifyTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifier = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    LastModifierId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    Deleted = table.Column<string>(type: "char(1)", nullable: true),
                    Disabled = table.Column<string>(type: "char(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_RoleMenu",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    RoleId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    PermissionId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_RoleMenu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    UserName = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    PassWord = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    NickName = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    OpenId = table.Column<string>(type: "varchar(80)", maxLength: 80, nullable: true),
                    Lock = table.Column<string>(type: "char(1)", nullable: true),
                    AvatarUrl = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    SuperAdmin = table.Column<string>(type: "char(1)", nullable: true),
                    Activate = table.Column<string>(type: "char(1)", nullable: true),
                    RegisterTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    UseMdEdit = table.Column<string>(type: "char(1)", nullable: true),
                    LoginIp = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    LastLoginTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreaterId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    Creater = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    LastModifyTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    LastModifier = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    LastModifierId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: true),
                    Deleted = table.Column<string>(type: "char(1)", nullable: true),
                    Disabled = table.Column<string>(type: "char(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_UserRole",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    UserId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    RoleId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
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
                name: "T_MenuModule");

            migrationBuilder.DropTable(
                name: "T_Module");

            migrationBuilder.DropTable(
                name: "T_OperateLog");

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

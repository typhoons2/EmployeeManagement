using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QL_NhanVien.Migrations
{
    /// <inheritdoc />
    public partial class AddClaimsAndRoleClaims : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            var existClaims = migrationBuilder.Sql("SELECT OBJECT_ID('dbo.Claims', 'U')").ToString();
            if (string.IsNullOrEmpty(existClaims))
            {
                migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    ClaimId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.ClaimId);
                });
            }
            var tableExists = migrationBuilder.Sql("SELECT OBJECT_ID('Roles', 'U')", suppressTransaction: true).ToString();

            if (string.IsNullOrEmpty(tableExists))
            {
                migrationBuilder.CreateTable(
                    name: "Roles",
                    columns: table => new
                    {
                        RoleId = table.Column<int>(nullable: false)
                            .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                        RoleName = table.Column<string>(nullable: true)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Roles", x => x.RoleId);
                    });
            }


            var exists = migrationBuilder.Sql("SELECT OBJECT_ID('dbo.SubmissionTypes', 'U')").ToString();
            if (string.IsNullOrEmpty(tableExists))
            {
                migrationBuilder.CreateTable(
                name: "SubmissionTypes",
                columns: table => new
                {
                    SubmissionTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubmissionName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Submissi__F2662978906E3BFF", x => x.SubmissionTypeId);
                });
            }

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => new { x.RoleId, x.ClaimId });
                    table.ForeignKey(
                        name: "FK_RoleClaims_Claims_ClaimId",
                        column: x => x.ClaimId,
                        principalTable: "Claims",
                        principalColumn: "ClaimId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            var existUser = migrationBuilder.Sql("SELECT OBJECT_ID('dbo.Users', 'U')").ToString();
            if (string.IsNullOrEmpty(existUser))
            {
                migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ContractSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DaysOff = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "('')"),
                    TokenCreated = table.Column<DateTime>(type: "datetime", nullable: true),
                    TokenExpires = table.Column<DateTime>(type: "datetime", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    GoogleId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    ConfirmationCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EmailConfirmationCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CC4C3F0EC57D", x => x.UserId);
                    table.ForeignKey(
                        name: "FK__Users__RoleId__398D8EEE",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId");
                });
            }
            var existActualSalaries = migrationBuilder.Sql("SELECT OBJECT_ID('dbo.ActualSalaries', 'U')").ToString();
            if (string.IsNullOrEmpty(existActualSalaries))
            {
                migrationBuilder.CreateTable(
                name: "ActualSalaries",
                columns: table => new
                {
                    ActualSalaryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SalaryAfterDeductions = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Month = table.Column<int>(type: "int", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: true),
                    DaysOff = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ActualSa__C60C25303867113D", x => x.ActualSalaryId);
                    table.ForeignKey(
                        name: "FK__ActualSal__UserI__44FF419A",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });
            }

            var existEmailConfirmations = migrationBuilder.Sql("SELECT OBJECT_ID('dbo.EmailConfirmations', 'U')").ToString();
            if (string.IsNullOrEmpty(existEmailConfirmations))
            {
                migrationBuilder.CreateTable(
                name: "EmailConfirmations",
                columns: table => new
                {
                    EmailConfirmationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    ConfirmationCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EmailCon__A04DCE811CB28412", x => x.EmailConfirmationId);
                    table.ForeignKey(
                        name: "FK__EmailConf__UserI__5DCAEF64",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });
            }
            var existRefreshTokens = migrationBuilder.Sql("SELECT OBJECT_ID('dbo.RefreshTokens', 'U')").ToString();
            if (string.IsNullOrEmpty(existRefreshTokens))
            {
                migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    RefreshTokenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RefToken = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ExpierTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RefreshT__F5845E393A8DF4A2", x => x.RefreshTokenId);
                    table.ForeignKey(
                        name: "FK__RefreshTo__UserI__4222D4EF",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });
            }

            

            migrationBuilder.CreateIndex(
                name: "IX_ActualSalaries_UserId",
                table: "ActualSalaries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AttachedFiles_SubmissionId",
                table: "AttachedFiles",
                column: "SubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailConfirmations_UserId",
                table: "EmailConfirmations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_ClaimId",
                table: "RoleClaims",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_SubmissionTypeId",
                table: "Submissions",
                column: "SubmissionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_UserId",
                table: "Submissions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActualSalaries");

            migrationBuilder.DropTable(
                name: "AttachedFiles");

            migrationBuilder.DropTable(
                name: "EmailConfirmations");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "Submissions");

            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DropTable(
                name: "SubmissionTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}

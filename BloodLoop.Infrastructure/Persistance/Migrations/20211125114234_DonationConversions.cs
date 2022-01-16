using Microsoft.EntityFrameworkCore.Migrations;

namespace BloodLoop.Infrastructure.Migrations
{
    public partial class DonationConversions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Donations_DonationTypes_DonationTypeLabel",
                table: "Donations"
                );

            migrationBuilder.DropPrimaryKey(
                name: "PK_DonationTypes",
                table: "DonationTypes"
                );

            migrationBuilder.AlterColumn<string>(
                name: "Label",
                table: "DonationTypes",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "DonationTypeLabel",
                table: "Donations",
                type: "nvarchar(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DonationTypes",
                table: "DonationTypes",
                column: "Label"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_DonationTypes_DonationTypeLabel",
                table: "Donations",
                column: "DonationTypeLabel",
                principalTable: "DonationTypes",
                principalColumn: "Label",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.CreateTable(
                name: "DonationConverters",
                columns: table => new
                {
                    DonationFromLabel = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    DonationToLabel = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Ratio = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationConverters", x => new { x.DonationFromLabel, x.DonationToLabel });
                    table.ForeignKey(
                        name: "FK_DonationConverters_DonationTypes_DonationFromLabel",
                        column: x => x.DonationFromLabel,
                        principalTable: "DonationTypes",
                        principalColumn: "Label",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonationConverters_DonationTypes_DonationToLabel",
                        column: x => x.DonationToLabel,
                        principalTable: "DonationTypes",
                        principalColumn: "Label");
                });

            migrationBuilder.CreateTable(
                name: "DonationIntervals",
                columns: table => new
                {
                    DonationFromLabel = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    DonationToLabel = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Interval = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationIntervals", x => new { x.DonationFromLabel, x.DonationToLabel });
                    table.ForeignKey(
                        name: "FK_DonationIntervals_DonationTypes_DonationFromLabel",
                        column: x => x.DonationFromLabel,
                        principalTable: "DonationTypes",
                        principalColumn: "Label",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DonationIntervals_DonationTypes_DonationToLabel",
                        column: x => x.DonationToLabel,
                        principalTable: "DonationTypes",
                        principalColumn: "Label");
                });

            migrationBuilder.InsertData(
                table: "DonationConverters",
                columns: new[] { "DonationFromLabel", "DonationToLabel", "Ratio" },
                values: new object[,]
                {
                    { "whole", "whole", 1.0 },
                    { "plasma", "whole", 0.33333333333333331 },
                    { "platelets", "whole", 1.0 },
                    { "redcells", "whole", 1.6666666666666667 },
                    { "disqualified", "whole", 0.0 }
                });

            migrationBuilder.InsertData(
                table: "DonationIntervals",
                columns: new[] { "DonationFromLabel", "DonationToLabel", "Interval" },
                values: new object[,]
                {
                    { "redcells", "plasma", 28.0 },
                    { "redcells", "whole", 56.0 },
                    { "platelets", "redcells", 28.0 },
                    { "platelets", "platelets", 28.0 },
                    { "platelets", "plasma", 28.0 },
                    { "platelets", "whole", 28.0 },
                    { "plasma", "redcells", 28.0 },
                    { "plasma", "plasma", 14.0 },
                    { "redcells", "platelets", 28.0 },
                    { "plasma", "whole", 14.0 },
                    { "whole", "redcells", 56.0 },
                    { "whole", "platelets", 28.0 },
                    { "whole", "plasma", 14.0 },
                    { "whole", "whole", 56.0 },
                    { "plasma", "platelets", 28.0 },
                    { "redcells", "redcells", 56.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonationConverters_DonationToLabel",
                table: "DonationConverters",
                column: "DonationToLabel");

            migrationBuilder.CreateIndex(
                name: "IX_DonationIntervals_DonationToLabel",
                table: "DonationIntervals",
                column: "DonationToLabel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonationConverters");

            migrationBuilder.DropTable(
                name: "DonationIntervals");

            migrationBuilder.AlterColumn<string>(
                name: "Label",
                table: "DonationTypes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "DonationTypeLabel",
                table: "Donations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)");
        }
    }
}

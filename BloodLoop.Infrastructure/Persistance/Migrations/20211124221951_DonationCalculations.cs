using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BloodLoop.Infrastructure.Migrations
{
    public partial class DonationCalculations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DonationConverters",
                columns: table => new
                {
                    DonationFromLabel = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DonationToLabel = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                        principalColumn: "Label",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DonationIntervals",
                columns: table => new
                {
                    DonationFromLabel = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DonationToLabel = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Interval = table.Column<TimeSpan>(type: "time", nullable: false)
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
                        principalColumn: "Label",
                        onDelete: ReferentialAction.Cascade);
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
                    { "redcells", "plasma", new TimeSpan(28, 0, 0, 0, 0) },
                    { "redcells", "whole", new TimeSpan(56, 0, 0, 0, 0) },
                    { "platelets", "redcells", new TimeSpan(28, 0, 0, 0, 0) },
                    { "platelets", "platelets", new TimeSpan(28, 0, 0, 0, 0) },
                    { "platelets", "plasma", new TimeSpan(28, 0, 0, 0, 0) },
                    { "platelets", "whole", new TimeSpan(28, 0, 0, 0, 0) },
                    { "plasma", "redcells", new TimeSpan(28, 0, 0, 0, 0) },
                    { "plasma", "plasma", new TimeSpan(14, 0, 0, 0, 0) },
                    { "redcells", "platelets", new TimeSpan(28, 0, 0, 0, 0) },
                    { "plasma", "whole", new TimeSpan(14, 0, 0, 0, 0) },
                    { "whole", "redcells", new TimeSpan(56, 0, 0, 0, 0) },
                    { "whole", "platelets", new TimeSpan(28, 0, 0, 0, 0) },
                    { "whole", "plasma", new TimeSpan(14, 0, 0, 0, 0) },
                    { "whole", "whole", new TimeSpan(56, 0, 0, 0, 0) },
                    { "plasma", "platelets", new TimeSpan(28, 0, 0, 0, 0) },
                    { "redcells", "redcells", new TimeSpan(56, 0, 0, 0, 0) }
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
        }
    }
}

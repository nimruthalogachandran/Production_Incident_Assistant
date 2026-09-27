using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Production_Incident_Assistant.Api.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIncidentStatusToEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.Sql(
        "UPDATE Incidents SET Status = CASE Status " +
        "WHEN 'Open' THEN '0' " +
        "WHEN 'Investigating' THEN '1' " +
        "WHEN 'Mitigated' THEN '2' " +
        "WHEN 'Resolved' THEN '3' " +
        "ELSE '0' END");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Incidents",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Incidents",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}

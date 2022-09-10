using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
namespace ISPH.Data.Migrations;
public partial class AddWorkTimeEmploymentTypeCompanyAddress : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(name: "Address", table: "Companies", type: "nvarchar(max)", nullable: false, defaultValue: "");
        migrationBuilder.AlterColumn<string>(name: "Description", table: "Advertisements",type: "nvarchar(max)", nullable: false, 
            defaultValue: "", oldClrType: typeof(string), oldType: "nvarchar(max)", oldNullable: true);
        migrationBuilder.AddColumn<int>(name: "EmploymentType", table: "Advertisements", type: "int", nullable: false, defaultValue: 0);
        migrationBuilder.AddColumn<int>(name: "WorkTimeType", table: "Advertisements", type: "int", nullable: false, defaultValue: 0);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(name: "Address", table: "Companies");
        migrationBuilder.DropColumn(name: "EmploymentType", table: "Advertisements");
        migrationBuilder.DropColumn(name: "WorkTimeType", table: "Advertisements");
        migrationBuilder.AlterColumn<string>(name: "Description", table: "Advertisements", type: "nvarchar(max)", nullable: true,
            oldClrType: typeof(string), oldType: "nvarchar(max)");
    }
}
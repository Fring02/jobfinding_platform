using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISPH.Data.Migrations
{
    public partial class AddStudentUniversityFacultyCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(name: "Course", table: "Students", type: "int", nullable: false, defaultValue: 0);
            migrationBuilder.AddColumn<string>(name: "Faculty", table: "Students", type: "nvarchar(max)", nullable: false, defaultValue: "");
            migrationBuilder.AddColumn<string>(name: "University", table: "Students", type: "nvarchar(max)", nullable: false, defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Course", table: "Students");
            migrationBuilder.DropColumn(name: "Faculty", table: "Students");
            migrationBuilder.DropColumn(name: "University", table: "Students");
        }
    }
}

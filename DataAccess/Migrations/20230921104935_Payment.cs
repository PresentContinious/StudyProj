using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Payment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourse_PaymentMethod_PaymentMethodId",
                table: "StudentCourse");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentMethodId",
                table: "StudentCourse",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourse_PaymentMethod_PaymentMethodId",
                table: "StudentCourse",
                column: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentCourse_PaymentMethod_PaymentMethodId",
                table: "StudentCourse");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentMethodId",
                table: "StudentCourse",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentCourse_PaymentMethod_PaymentMethodId",
                table: "StudentCourse",
                column: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

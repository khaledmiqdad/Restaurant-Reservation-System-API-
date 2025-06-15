using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantReservation.Db.Migrations
{
    /// <inheritdoc />
    public partial class EnhanceDatabaseWithNewViewsAndTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "RevenueResults",
                newName: "RevenueResult");

            migrationBuilder.CreateTable(
                name: "CustomerView",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartySize = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.Sql("DROP VIEW IF EXISTS EmployeeRestaurantDetails");

            migrationBuilder.Sql(@"
                CREATE VIEW EmployeeRestaurantDetails AS
                SELECT 
                    e.EmployeeId,
                    e.FirstName AS EmployeeFirstName,
                    e.LastName AS EmployeeLastName,
                    e.Position,
                    r.RestaurantId,
                    r.Name AS Restaurant_Name,
                    r.Address,
                    r.PhoneNumber,
                    r.OpeningHours
                FROM Employees e
                JOIN Restaurants r ON e.RestaurantId = r.RestaurantId;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerView");

            migrationBuilder.RenameTable(
                name: "RevenueResult",
                newName: "RevenueResults");

            migrationBuilder.Sql("DROP VIEW IF EXISTS EmployeeRestaurantDetails");

            migrationBuilder.Sql(@"
                CREATE VIEW EmployeeRestaurantDetails AS
                SELECT 
                    e.EmployeeId,
                    e.FirstName AS Employee_First_Name,
                    e.LastName AS Employee_Last_Name,
                    e.Position,
                    r.RestaurantId,
                    r.Name AS Restaurant_Name,
                    r.Address,
                    r.PhoneNumber,
                    r.OpeningHours
                FROM Employees e
                JOIN Restaurants r ON e.RestaurantId = r.RestaurantId;
            ");
        }
    }
}

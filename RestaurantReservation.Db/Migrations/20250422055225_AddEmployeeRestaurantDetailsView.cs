using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantReservation.Db.Migrations
{
    public partial class AddEmployeeRestaurantDetailsView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS EmployeeRestaurantDetails");
        }
    }
}

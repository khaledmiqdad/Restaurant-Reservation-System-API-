using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantReservation.Db.Migrations
{
    public partial class AddReservationDetailsView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW View_ReservationDetails AS
                SELECT 
                    r.ReservationId,
                    r.ReservationDate,
                    r.PartySize,
                    c.CustomerId,
                    c.FirstName AS CustomerFirstName,
                    c.LastName AS CustomerLastName,
                    c.Email AS CustomerEmail,
                    res.RestaurantId,
                    res.Name AS RestaurantName,
                    res.Address AS RestaurantAddress
                FROM Reservations r
                INNER JOIN Customers c ON r.CustomerId = c.CustomerId
                INNER JOIN Restaurants res ON r.RestaurantId = res.RestaurantId;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW IF EXISTS View_ReservationDetails");
        }
    }
}
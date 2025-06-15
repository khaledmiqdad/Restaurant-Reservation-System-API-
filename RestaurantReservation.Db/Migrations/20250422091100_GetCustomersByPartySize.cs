using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantReservation.Db.Migrations
{
    public partial class GetCustomersByPartySize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetCustomersByPartySize
                    @minPartySize INT
                AS
                BEGIN
                    SELECT DISTINCT c.CustomerId, c.FirstName, c.LastName, c.Email, c.PhoneNumber ,r.PartySize
                    FROM Customers c
                    INNER JOIN Reservations r ON c.CustomerId = r.CustomerId
                    WHERE r.PartySize > @minPartySize
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetCustomersByPartySize");
        }
    }
}

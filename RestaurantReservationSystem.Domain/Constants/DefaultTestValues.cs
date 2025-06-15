namespace RestaurantReservationSystem.Constants
{
    public static class DefaultTestValues
    {
        // Names & Emails
        public const string DefaultName = "Test";
        public const string UpdatedName = "Updated Test";

        public const string DefaultEmail = "Test@gmail.com";
        public const string UpdatedEmail = "UpdatedTest@gmail.com";

        public const string DefaultPhoneNumber = "0590000000";
        public const string UpdatedPhoneNumber = "0599999999";

        // Employee
        public const string DefaultPosition = "TestPosition";
        public const string UpdatedPosition = "Updatedposition";

        // Restaurant
        public const string DefaultRestaurantName = "TestRestaurant";
        public const string UpdatedRestaurantName = "UpdatedRestaurant";

        public const string DefaultRestaurantAddress = "TestAddress";
        public const string UpdatedRestaurantAddress = "UpdatedAddress";

        public const string DefaultOpeningHours = "8:00 - 16:00";
        public const string UpdatedOpeningHours = "9:00 - 17:00";

        // Menu Item
        public const string DefaultMenuItemName = "Grilled Chicken";
        public const string UpdatedMenuItemName = "Grilled Chicken (Updated)";

        public const string DefaultMenuItemDescription = "Delicious grilled chicken served with vegetables.";
        public const string UpdatedMenuItemDescription = "Updated description for grilled chicken.";

        public const decimal DefaultMenuItemPrice = 12.99m;
        public const decimal UpdatedMenuItemPrice = 14.99m;

        // Common IDs
        public const int Id1 = 1;
        public const int Id2 = 2;
        public const int Id4 = 4;

        // Order & Reservation
        public static readonly DateTime CurrentDate = DateTime.Now;
        public static readonly DateTime ReservationDateTomorrow = DateTime.Now.AddDays(1);
        public static readonly DateTime ReservationDateAfterTwoDays = DateTime.Now.AddDays(2);

        // Quantities
        public const int DefaultQuantity = 20;
        public const int UpdatedQuantity = 30;

        public const int DefaultPartySize = 4;
        public const int UpdatedPartySize = 10;

        public const int DefaultCapacity = 4;
        public const int UpdatedCapacity = 6;

        public const decimal DefaultTotalAmount = 50.00m;
        public const decimal UpdatedTotalAmount = 65.00m;
    }
}
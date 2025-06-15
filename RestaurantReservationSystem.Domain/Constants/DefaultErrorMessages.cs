namespace RestaurantReservationSystem.Constants
{
    public static class DefaultErrorMessages
    {
        public const string AddFailed = "Failed to add the data. Ensure all required fields are valid.";
        public const string RetrieveFailed = "Failed to retrieve data from the database.";
        public const string UpdateFailed = "Failed to update the data. It may have been modified or deleted by another process.";
        public const string DeleteWithRelations = "Cannot delete the data because it has related data.";
        public const string DeleteUnexpected = "Unexpected error occurred while deleting the data.";
    }
}
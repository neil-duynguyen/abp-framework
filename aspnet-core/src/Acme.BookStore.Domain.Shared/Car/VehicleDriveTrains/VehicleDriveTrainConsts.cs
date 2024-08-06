namespace ImportSample.VehicleDriveTrains
{
    public static class VehicleDriveTrainConsts
    {
        private const string DefaultSorting = "{0}EnName asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "VehicleDriveTrain." : string.Empty);
        }

    }
}
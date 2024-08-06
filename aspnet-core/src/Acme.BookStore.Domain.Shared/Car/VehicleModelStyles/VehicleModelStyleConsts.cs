namespace ImportSample.VehicleModelStyles
{
    public static class VehicleModelStyleConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "VehicleModelStyle." : string.Empty);
        }

    }
}
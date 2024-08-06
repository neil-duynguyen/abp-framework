namespace ImportSample.VehicleBodyStyles
{
    public static class VehicleBodyStyleConsts
    {
        private const string DefaultSorting = "{0}EnName asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "VehicleBodyStyle." : string.Empty);
        }

    }
}
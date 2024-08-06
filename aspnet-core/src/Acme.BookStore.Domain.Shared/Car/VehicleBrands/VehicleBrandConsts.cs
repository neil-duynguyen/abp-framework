namespace ImportSample.VehicleBrands
{
    public static class VehicleBrandConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "VehicleBrand." : string.Empty);
        }

    }
}
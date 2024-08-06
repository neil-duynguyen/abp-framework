namespace ImportSample.VehicleCategories
{
    public static class VehicleCategoryConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "VehicleCategory." : string.Empty);
        }

    }
}
namespace ImportSample.VehicleYearModels
{
    public static class VehicleYearModelConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "VehicleYearModel." : string.Empty);
        }

    }
}
namespace ImportSample.VehicleModels
{
    public static class VehicleModelConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "VehicleModel." : string.Empty);
        }

    }
}
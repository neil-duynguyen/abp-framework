namespace ImportSample.VehicleEngines
{
    public static class VehicleEngineConsts
    {
        private const string DefaultSorting = "{0}Label asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "VehicleEngine." : string.Empty);
        }

    }
}
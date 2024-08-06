namespace ImportSample.ProvinceCities
{
    public static class ProvinceCityConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "ProvinceCity." : string.Empty);
        }

    }
}
namespace ImportSample.Districts
{
    public static class DistrictConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "District." : string.Empty);
        }

    }
}
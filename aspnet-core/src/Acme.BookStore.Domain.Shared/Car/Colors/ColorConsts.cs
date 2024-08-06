namespace ImportSample.Colors
{
    public static class ColorConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Color." : string.Empty);
        }

    }
}
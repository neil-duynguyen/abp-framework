namespace ImportSample.ColorTranslations
{
    public static class ColorTranslationConsts
    {
        private const string DefaultSorting = "{0}Language asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "ColorTranslation." : string.Empty);
        }

    }
}
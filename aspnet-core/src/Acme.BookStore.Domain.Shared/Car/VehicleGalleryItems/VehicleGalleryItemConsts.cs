namespace ImportSample.VehicleGalleryItems
{
    public static class VehicleGalleryItemConsts
    {
        private const string DefaultSorting = "{0}Order asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "VehicleGalleryItem." : string.Empty);
        }

    }
}
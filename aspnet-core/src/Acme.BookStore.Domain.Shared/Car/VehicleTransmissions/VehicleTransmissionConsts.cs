namespace ImportSample.VehicleTransmissions
{
    public static class VehicleTransmissionConsts
    {
        private const string DefaultSorting = "{0}Label asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "VehicleTransmission." : string.Empty);
        }

    }
}
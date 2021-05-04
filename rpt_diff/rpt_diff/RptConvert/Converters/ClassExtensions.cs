namespace rpt_diff.RptConvert
{
    internal static class ClassExtensions
    {
        /*
         *  ToStringSafe
         *  - converts object to text representation
         *  - if object is null then returns empty string else returns converted object representation in string
         */
        public static string ToStringSafe(this object obj)
        {
            return obj?.ToString() ?? "";
        }
    }
}

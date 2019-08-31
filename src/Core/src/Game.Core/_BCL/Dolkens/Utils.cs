namespace Dolkens.Framework.Extensions
{
    public enum StringSizeEnum
    {
        Int8 = 1,
        Int16 = 2,
        Int32 = 4,
    }
   
    public static class Utils
    {
        /// <summary>
        /// Custom DateTime formats supported by the parser.
        /// </summary>
        public static string[] DateTimeFormats { get; set; } = new string[] {
            @"yyyy-MM-dd\THHmm", // 2010-12-31T2359
        };
    }
}
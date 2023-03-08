namespace XC.XSC.Utilities.UnitExtension
{
    /// <summary>
    /// Extension method Size Units
    /// </summary>
    public static class UnitExtension
    {
        public enum SizeUnits
        {
            Byte, KB, MB, GB, TB, PB, EB, ZB, YB
        }

        /// <summary>
        /// Get Size based on the corresponding unit
        /// </summary>
        /// <param name="value"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static Int32 GetSize(this Int32 value, SizeUnits unit)
        {
            return (value / (Int32)Math.Pow(1024, (Int32)unit));
        }
    }
}

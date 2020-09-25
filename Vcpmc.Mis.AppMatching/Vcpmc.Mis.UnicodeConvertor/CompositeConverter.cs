
namespace Vcpmc.Mis.UnicodeConverter
{
    public class CompositeConverter : Converter
    {
        /**
		 * Converts Unicode Composite to Unicode Precomposed (NFD -> NFC)
		 */
        public override string Convert(System.IO.FileInfo file)
        {
            string str = ReadChar(file, System.Text.Encoding.UTF8);
            return ConvertText(str);
        }

        /// <summary>
        /// Converts a string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public override string ConvertText(string str)
        {
            return CompositeToPrecomposed(str);
        }
    }
}


namespace Vcpmc.Mis.UnicodeConverter
{
	public class NcrConverter : Converter
	{
		/**
		 * Converts NCR to Unicode
		 */
		public override string Convert(System.IO.FileInfo file)
		{
			string str = ReadChar(file);

			if (IsHTML(file))
			{
				str = ConvertHTML(str);
			}
			else if (IsRTF(file))
			{
				str = ConvertRTF(ConvertNCR(str));
			}

			str = CompositeToPrecomposed(str);

			if (IsRTF(file))
			{
				str = ConvertToRTF(str);
			}

			return str;
		}

		/// <summary>
		/// Converts a string.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public override string ConvertText(string str)
		{
			return ConvertNCR(str);
		}
	}
}

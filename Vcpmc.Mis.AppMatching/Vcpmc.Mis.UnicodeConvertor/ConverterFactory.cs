using Vcpmc.Mis.UnicodeConverter.enums;

namespace Vcpmc.Mis.UnicodeConverter
{
	public class ConverterFactory
	{
		/// <summary>
		/// Creates text converter.
		/// </summary>
		/// <param name="sourceEncoding">encoding</param>
		/// <returns>a text converter</returns>
		public static Converter CreateConverter(VietEncodings sourceEncoding)
		{
			Converter converter;

			switch (sourceEncoding)
			{
				case VietEncodings.VNI:
					converter = new VniConverter(); break;
				case VietEncodings.TCVN3:
					converter = new Tcvn3Converter(); break;
				case VietEncodings.VIQR:
					converter = new ViqrConverter(); break;
				case VietEncodings.VISCII:
					converter = new VisciiConverter(); break;
				case VietEncodings.VPS:
					converter = new VpsConverter(); break;
				case VietEncodings.NCR:
					converter = new NcrConverter(); break;
				case VietEncodings.Unicode_Composite:
					converter = new CompositeConverter(); break;
				default:
					throw new System.Exception("Unsupported encoding: " + sourceEncoding);
			}

			return converter;
		}
	}
}

using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Vcpmc.Mis.UnicodeConverter
{
	public abstract class Converter
	{
		// Fonts for HTML font tags
		protected string SERIF = "Times New Roman";
		protected string SANS_SERIF = "Arial";

		/// <summary>
		/// Converts a file; to be implemented by concrete converters.
		/// </summary>
		/// <param name="file"></param>
		/// <returns></returns>
		public abstract string Convert(FileInfo file);

		/// <summary>
		/// Converts a string; to be implemented by concrete converters.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public abstract string ConvertText(string str);

		/**
		* Determines if the file is a HTML file 
		*
		* @param file 
		* @return 
		*/
		protected bool IsHTML(FileInfo file)
		{
			return file.Extension.ToLower().StartsWith(".htm");
		}

		/**
		* Determines if the file is a RTF file 
		*
		* @param file 
		* @return 
		*/
		protected bool IsRTF(FileInfo file)
		{
			return file.Extension.ToLower().StartsWith(".rtf");
		}

		/**
		*  Read a text file into a string
		*
		*/
		protected string ReadChar(FileInfo file)
		{
			StreamReader inReader = new StreamReader(file.FullName, Encoding.Default);
			string str = inReader.ReadToEnd();
			inReader.Close();

			return str;
		}

		/**
		*  Read a text file with encoding into a string
		*
		*/
		protected string ReadChar(FileInfo file, Encoding encoding)
		{
			StreamReader inReader = new StreamReader(file.FullName, encoding, true);
			string str = inReader.ReadToEnd();
			inReader.Close();

			return str;
		}

		/**
		*  Read a binary file into a string
		*
		*/
		protected string ReadByte(FileInfo file)
		{
			BinaryReader inReader = new BinaryReader(new BufferedStream(new FileStream(file.FullName, FileMode.Open, FileAccess.Read)));
			int size = (int)file.Length;
			byte[] b = new byte[size];
			inReader.Read(b, 0, size);
			inReader.Close();

			StringBuilder strB = new StringBuilder();

			for (int i = 0; i < size; i++)
			{
				strB.Append((char)b[i]);
			}

			return strB.ToString();
		}

		/**
		*  Changes HTML meta tag for charset to UTF-8.
		*
		*/
		protected string PrepareMetaTag(string str)
		{
			return Regex.Replace(
				Regex.Replace(
				// delete existing charset attribute in meta tag
				Regex.Replace(str, "(?i)charset=(?:iso-8859-1|windows-1252|windows-1258|us-ascii|x-user-defined)", ""),
				// delete the rest of the meta tag
				"(?i)<meta http-equiv=\"?Content-Type\"? content=\"text/html;\\s*\">\\n?", ""),
				// insert new meta tag with UTF-8 charset
				"(?i)<head>", "<head>\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">");
		}


		/**
		*  Translate Character entity references to corresponding Cp1252 characters.
		*
		*/
		protected string HtmlToAnsi(string str)
		{
			string[] extended_ansi_html = {"&trade;", "&#8209;", "&nbsp;",
					"&iexcl;", "&cent;", "&pound;", "&curren;", "&yen;", "&brvbar;", "&sect;", "&uml;", "&copy;", "&ordf;",
					"&laquo;", "&not;", "&shy;", "&reg;", "&macr;", "&deg;", "&plusmn;", "&sup2;", "&sup3;",
					"&acute;", "&micro;", "&para;", "&middot;", "&cedil;", "&sup1;", "&ordm;", "&raquo;",
					"&frac14;", "&frac12;", "&frac34;", "&iquest;", "&Agrave;", "&Aacute;", "&Acirc;",
					"&Atilde;", "&Auml;", "&Aring;", "&AElig;", "&Ccedil;", "&Egrave;", "&Eacute;", "&Ecirc;",
					"&Euml;", "&Igrave;", "&Iacute;", "&Icirc;", "&Iuml;", "&ETH;", "&Ntilde;", "&Ograve;",
					"&Oacute;", "&Ocirc;", "&Otilde;", "&Ouml;", "&times;", "&Oslash;", "&Ugrave;", "&Uacute;",
					"&Ucirc;", "&Uuml;", "&Yacute;", "&THORN;", "&szlig;", "&agrave;", "&aacute;", "&acirc;",
					"&atilde;", "&auml;", "&aring;", "&aelig;", "&ccedil;", "&egrave;", "&eacute;", "&ecirc;",
					"&euml;", "&igrave;", "&iacute;", "&icirc;", "&iuml;", "&eth;", "&ntilde;", "&ograve;",
					"&oacute;", "&ocirc;", "&otilde;", "&ouml;", "&divide;", "&oslash;", "&ugrave;", "&uacute;",
					"&ucirc;", "&uuml;", "&yacute;", "&thorn;", "&yuml;"};
			string[] extended_ansi = {"\u0099", "\u2011", "\u00A0",
					"\u00A1", "\u00A2", "\u00A3", "\u00A4", "\u00A5", "\u00A6", "\u00A7", "\u00A8", "\u00A9",
					"\u00AA", "\u00AB", "\u00AC", "\u00AD", "\u00AE", "\u00AF", "\u00B0", "\u00B1", "\u00B2",
					"\u00B3", "\u00B4", "\u00B5", "\u00B6", "\u00B7", "\u00B8", "\u00B9", "\u00BA", "\u00BB",
					"\u00BC", "\u00BD", "\u00BE", "\u00BF", "\u00C0", "\u00C1", "\u00C2", "\u00C3", "\u00C4",
					"\u00C5", "\u00C6", "\u00C7", "\u00C8", "\u00C9", "\u00CA", "\u00CB", "\u00CC", "\u00CD",
					"\u00CE", "\u00CF", "\u00D0", "\u00D1", "\u00D2", "\u00D3", "\u00D4", "\u00D5", "\u00D6",
					"\u00D7", "\u00D8", "\u00D9", "\u00DA", "\u00DB", "\u00DC", "\u00DD", "\u00DE", "\u00DF",
					"\u00E0", "\u00E1", "\u00E2", "\u00E3", "\u00E4", "\u00E5", "\u00E6", "\u00E7", "\u00E8",
					"\u00E9", "\u00EA", "\u00EB", "\u00EC", "\u00ED", "\u00EE", "\u00EF", "\u00F0", "\u00F1",
					"\u00F2", "\u00F3", "\u00F4", "\u00F5", "\u00F6", "\u00F7", "\u00F8", "\u00F9", "\u00FA",
					"\u00FB", "\u00FC", "\u00FD", "\u00FE", "\u00FF"};

			StringBuilder strB = new StringBuilder(str);

			for (int i = 0; i < extended_ansi_html.Length; i++)
			{
				strB.Replace(extended_ansi_html[i], extended_ansi[i]);
			}

			return strB.ToString();
		}

		/**
		*  Rich Text Format-to-ANSI conversion
		*
		*/
		protected string RtfToAnsi(string str)
		{
			StringBuilder strB = new StringBuilder(str);

			// Fix RTF's bullet '\u00b7' and square bullet '\u00a7' characters 
			// that may be used in VPS, VISCII, and TCVN encodings.

			strB.Replace("\\\'B7", "\\\'b7");
			strB.Replace("\\\'b7\\tab", "&middot;\\tab"); // temp change bullet characters 

			strB.Replace("\\\'A7", "\\\'a7");
			strB.Replace("\\\'a7\\tab", "&sect;\\tab"); // temp change square bullet characters 

			str = strB.ToString();
			strB.Length = 0; // release memory

			// This part uses Regex package to catch more bullet characters
			Regex regex1 = new Regex("\\\\fs?(\\d)+(\\s)*\\\\\'b7", RegexOptions.IgnoreCase);
			MatchCollection mc;
			mc = regex1.Matches(str);

			StringBuilder result = new StringBuilder();
			int startIndex = 0;

			for (int i = 0; i < mc.Count; i++)
			{
				result.Append(str.Substring(startIndex, mc[i].Index - startIndex + mc[i].Value.Length - 4));
				result.Append("&middot;"); // temp change bullet characters
				startIndex = mc[i].Index + mc[i].Value.Length;
			}
			result.Append(str.Substring(startIndex));
			str = result.ToString();
			result.Length = 0;
			startIndex = 0;

			// Fix RTF's square bullet characters '\u00a7' that may be used in VPS, VISCII, and TCVN encoding
			Regex regex2 = new Regex("\\\\fs?(\\d)+(\\s)*\\\\\'a7", RegexOptions.IgnoreCase);
			mc = regex2.Matches(str); // get a MatchCollection object

			for (int i = 0; i < mc.Count; i++)
			{
				result.Append(str.Substring(startIndex, mc[i].Index - startIndex + mc[i].Value.Length - 4));
				result.Append("&sect;"); // temp change square bullet characters
				startIndex = mc[i].Index + mc[i].Value.Length;
			}
			result.Append(str.Substring(startIndex));
			str = result.ToString();
			result.Length = 0;
			startIndex = 0;

			String pattern = "\\\'";
			int d;
			int foundIndex;

			// Look for a \'xx pattern to replace
			while ((foundIndex = str.IndexOf(pattern, startIndex)) >= 0)
			{
				result.Append(str.Substring(startIndex, foundIndex - startIndex));
				try
				{
					string temp = str.Substring(foundIndex + 2, 2);
					d = System.Convert.ToInt32(str.Substring(foundIndex + 2, 2), 16);
					if (d <= 0x7f)
					{
						// if ASCII, append it unprocessed
						result.Append(str.Substring(foundIndex, 4));
					}
					else
					{
						// if extended ASCII (or ANSI), cast to char, then append
						result.Append((char)d);
					}
				}
				catch (ArgumentException)
				{
					// if invalid hex sequence, append it unprocessed
					result.Append(str.Substring(foundIndex, 4));
				}
				startIndex = foundIndex + 4; // 4 = length of \'xx
			}
			result.Append(str.Substring(startIndex));
			str = result.ToString();
			result.Length = 0;
			startIndex = 0;

			// Fix a MS Word2000 RTF bug that erroneously introduces extra interjecting \r\n characters.
			// The disruption results in incorrect conversion in VNI encoding b/c it sometimes takes
			// 2 contiguous vowels to make 1 vowel.       
			// Look for a "\r\nx"; if x is a char > '\u007f', strip "\r\n"
			pattern = "\r\n";
			while ((foundIndex = str.IndexOf(pattern, startIndex)) >= 0)
			{
				result.Append(str.Substring(startIndex, foundIndex - startIndex));
				if (str[foundIndex + 2] > '\u007f')
				{
					result.Append(str[foundIndex + 2]);
					startIndex = foundIndex + 3;
				}
				else
				{
					// if ASCII, append unchanged
					result.Append(pattern);
					startIndex = foundIndex + 2; // 2 = pattern.length();
				}
			}
			result.Append(str.Substring(startIndex));

			return result.ToString();
		}

		/**
		 *  Converts Numeric Character References to Unicode
		 *
		 */
		protected string ConvertNCR(string str)
		{
			const string NCR = "&#";
			int NCR_LENGTH = NCR.Length;
			int STR_LENGTH = str.Length;

			StringBuilder result = new StringBuilder();
			int foundIndex;
			int startIndex = 0;

			while (startIndex < STR_LENGTH)
			{
				foundIndex = str.IndexOf(NCR, startIndex);

				if (foundIndex == -1)
				{
					result.Append(str.Substring(startIndex));
					break;
				}

				result.Append(str.Substring(startIndex, foundIndex - startIndex));
				startIndex = str.IndexOf(";", foundIndex);

				if (startIndex == -1)
				{
					result.Append(str.Substring(foundIndex));
					break;
				}

				string tok = str.Substring(foundIndex + NCR_LENGTH, startIndex - (foundIndex + NCR_LENGTH));

				try
				{
					int radix = 10;

					if (tok.Trim()[0] == 'x')
					{
						radix = 16;
						tok = tok.Substring(1, tok.Length - 1);
					}

					result.Append((char)System.Convert.ToInt32(tok, radix)); // convert number to Unicode
				}
				catch (FormatException)
				{
					result.Append('?');
				}

				startIndex++;
			}

			return result.ToString();
		}

		/**
		 *  Converts Unicode to RTF
		 *
		 */
		protected string ConvertToRTF(string str)
		{
			char ch;
			StringBuilder result = new StringBuilder();

			// Look for a non-ASCII character pattern to replace
			for (int i = 0; i < str.Length; i++)
			{
				ch = str[i];

				if (ch <= '\u007f') // for ASCII, normal char
				{
					result.Append(ch);
				}
				//				else if (ch <= '\u00ff') // for ANSI, \'xx, x is hexadecimal
				//				{
				//					result.Append("\\\'" + System.Convert.ToString((int) ch, 16));
				//				}
				else // for non-ANSI, \\uDec?
				{
					result.Append("\\u" + (int)ch + '?');
				}
			}

			result.Replace("&middot;", "\\\'B7"); // restore bullet characters
			result.Replace("&sect;", "\\\'A7"); // restore square bullet characters

			return result.ToString();
		}

		/**
		 *  Unicode Composite-to-Unicode Precomposed conversion
		 *  (NFD -> NFC)
		 */
		protected string CompositeToPrecomposed(string str)
		{
			// Perform Unicode NFC on NFD string
			return str.Normalize(NormalizationForm.FormC);
		}

		/**
		 * Converts HTML
		 */
		protected string ConvertHTML(string str)
		{
			return ReplaceFont(PrepareMetaTag(ConvertNCR(HtmlToAnsi(str))));
		}
		/**
		 * Converts RTF
		 */
		protected string ConvertRTF(string str)
		{
			return ReplaceFont(RtfToAnsi(str));
		}
		/**
		 * Replaces fonts, to be overridden by subclass when necessary
		 */
		protected virtual string ReplaceFont(string str)
		{
			return str;
		}
	}
}

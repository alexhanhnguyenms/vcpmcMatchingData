﻿using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Vcpmc.Mis.UnicodeConverter
{
	public class ViqrConverter : Converter
	{
		static readonly string[] VIQR_char = {"y~", "Y~", "y?", "Y?", "y.", "Y.", "y`", "Y`", "u+.", "U+.", "u+~",
							"U+~", "u+?", "U+?", "u+`", "U+`", "u+'", "U+'", "u?", "U?", "u.", "U.", "o+.", "O+.",
							"o+~", "O+~", "o+?", "O+?", "o+`", "O+`", "o+'", "O+'", "o^.", "O^.", "o^~", "O^~", "o^?",
							"O^?", "o^`", "O^`", "o^'", "O^'", "o?", "O?", "o.", "O.", "i.", "I.", "i?", "I?", "e^.",
							"E^.", "e^~", "E^~", "e^?", "E^?", "e^`", "E^`", "e^'", "E^'", "e~", "E~", "e?", "E?", "e.",
							"E.", "a(.", "A(.", "a(~", "A(~", "a(?", "A(?", "a(`", "A(`", "a('", "A('", "a^.", "A^.",
							"a^~", "A^~", "a^?", "A^?", "a^`", "A^`", "a^'", "A^'", "a?", "A?", "a.", "A.", "u+", "U+",
							"o+", "O+", "u~", "U~", "i~", "I~", "dd", "a(", "A(", "y'", "u'", "u`", "o~", "o^", "o'",
							"o`", "i'", "i`", "e^", "e'", "e`", "a~", "a^", "a'", "a`", "Y'", "U'", "U`", "O~", "O^",
							"O'", "O`", "DD", "I'", "I`", "E^", "E'", "E`", "A~", "A^", "A'", "A`"};
		static readonly string[] Unicode_char = {"\u1EF9", "\u1EF8", "\u1EF7", "\u1EF6", "\u1EF5", "\u1EF4",
							"\u1EF3", "\u1EF2", "\u1EF1", "\u1EF0", "\u1EEF", "\u1EEE", "\u1EED", "\u1EEC", "\u1EEB",
							"\u1EEA", "\u1EE9", "\u1EE8", "\u1EE7", "\u1EE6", "\u1EE5", "\u1EE4", "\u1EE3", "\u1EE2",
							"\u1EE1", "\u1EE0", "\u1EDF", "\u1EDE", "\u1EDD", "\u1EDC", "\u1EDB", "\u1EDA", "\u1ED9",
							"\u1ED8", "\u1ED7", "\u1ED6", "\u1ED5", "\u1ED4", "\u1ED3", "\u1ED2", "\u1ED1", "\u1ED0",
							"\u1ECF", "\u1ECE", "\u1ECD", "\u1ECC", "\u1ECB", "\u1ECA", "\u1EC9", "\u1EC8", "\u1EC7",
							"\u1EC6", "\u1EC5", "\u1EC4", "\u1EC3", "\u1EC2", "\u1EC1", "\u1EC0", "\u1EBF", "\u1EBE",
							"\u1EBD", "\u1EBC", "\u1EBB", "\u1EBA", "\u1EB9", "\u1EB8", "\u1EB7", "\u1EB6", "\u1EB5",
							"\u1EB4", "\u1EB3", "\u1EB2", "\u1EB1", "\u1EB0", "\u1EAF", "\u1EAE", "\u1EAD", "\u1EAC",
							"\u1EAB", "\u1EAA", "\u1EA9", "\u1EA8", "\u1EA7", "\u1EA6", "\u1EA5", "\u1EA4", "\u1EA3",
							"\u1EA2", "\u1EA1", "\u1EA0", "\u01B0", "\u01AF", "\u01A1", "\u01A0", "\u0169", "\u0168",
							"\u0129", "\u0128", "\u0111", "\u0103", "\u0102", "\u00FD", "\u00FA", "\u00F9", "\u00F5",
							"\u00F4", "\u00F3", "\u00F2", "\u00ED", "\u00EC", "\u00EA", "\u00E9", "\u00E8", "\u00E3",
							"\u00E2", "\u00E1", "\u00E0", "\u00DD", "\u00DA", "\u00D9", "\u00D5", "\u00D4", "\u00D3",
							"\u00D2", "\u0110", "\u00CD", "\u00CC", "\u00CA", "\u00C9", "\u00C8", "\u00C3", "\u00C2",
							"\u00C1", "\u00C0"};

		/**
		 *  Converts VIQR to Unicode
		 */
		public override string Convert(System.IO.FileInfo file)
		{
			string str = ReadChar(file);

			if (IsHTML(file))
			{
				str = ConvertHTML(str);
			}

			str = ConvertText(str);

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
			// adjust irregular characters to VIQR standard
			str = Regex.Replace(
				Regex.Replace(str,
				"(?i)(?<=[uo])\\*", "+"),
				"(?i)(d)([-d])", "$1$1");

			str.Replace('\u0092', '\''); // replace right single quotation mark (\u0092, or \u2019) with apostrophe

			// change tone marks to punctuation marks if ' or . or ? is before a whitespace
			// and after a vowel which in turn is after a vowel and any one or two marks `?~'.^(+ , or ae        
			StringBuilder strB = new StringBuilder(Regex.Replace(str, "(?i)(?<=(?:(?:[aeiouy][`?~'.^(+]{1,2})|[ae])[aeiouy])(?=[?'.](?:\\s|$|\\p{P}))", @"\"));

			for (int i = 0; i < VIQR_char.Length; i++)
			{
				strB.Replace(VIQR_char[i], Unicode_char[i]);
			}

			// delete redundant '\' characters
			str = Regex.Replace(strB.ToString(), "(?i)\\\\(?=[-.?'\\\\])", "");

			str = cleanupURL(str);

			return str;
		}

		/**
		 *  Corrects invalid characters in URLs
		 *
		 */
		string cleanupURL(string str)
		{
			StringBuilder strB = new StringBuilder(str);
			string[] URL_notations = { "://", "mailto:" };

			// correct characters in URLs, they can't be non-ASCII
			for (int i = 0; i < URL_notations.Length; i++)
			{
				try
				{
					string URL_notation = URL_notations[i];
					int URL_notationsLength = URL_notations[i].Length;

					int startIndex = 0;
					int foundIndex;

					while ((foundIndex = strB.ToString().IndexOf(URL_notation, startIndex)) != -1)
					{
						startIndex = foundIndex + URL_notationsLength;
						// Look for a pattern to replace
						char ch;

						while ((ch = strB[startIndex]) != ' ' && ch != '\n')
						{
							if (ch >= '\u1EA0') // A.
							{
								char replace = '\0';
								switch (ch)
								{
									case '\u1EA1': replace = 'a'; break;
									case '\u1EB9': replace = 'e'; break;
									case '\u1ECB': replace = 'i'; break;
									case '\u1ECD': replace = 'o'; break;
									case '\u1EE5': replace = 'u'; break;
									case '\u1EF5': replace = 'y'; break;
									case '\u1EA0': replace = 'A'; break;
									case '\u1EB8': replace = 'E'; break;
									case '\u1ECA': replace = 'I'; break;
									case '\u1ECC': replace = 'O'; break;
									case '\u1EE4': replace = 'U'; break;
									case '\u1EF4': replace = 'Y'; break;
									default: break;
								}
								if (replace != '\0')
								{
									strB[startIndex++] = replace;
									strB.Insert(startIndex, '.');
								}
							}
							startIndex++;
						}
					}
				}
				catch (IndexOutOfRangeException exc)
				{
					// end of string
					Console.Error.WriteLine(exc.StackTrace);
				}
			}
			return strB.ToString();
		}
	}
}

﻿using Vcpmc.Mis.UnicodeConverter.enums;

namespace Vcpmc.Mis.UnicodeConverter
{
	public class LegacyCharSet
	{
		private string[] legacyChars, unicodeChars;

		/** Creates a new instance of LegacyCharSet */
		public LegacyCharSet(VietEncodings sourceEncoding)
		{
			switch (sourceEncoding)
			{
				case VietEncodings.VNI:
					{
						string[] VNI_charss = { "\u00D1", "\u00F1", "\u00D3", "\u00F3", "\u00D2", "\u00F2", "\u00C6", "\u00E6", "\u00CE", "\u00EE", "O\u00C2", "o\u00E2", "y\u00F5", "Y\u00D5", "y\u00FB", "Y\u00DB", "y\u00F8", "Y\u00D8", "\u00F6\u00EF", "\u00D6\u00CF", "\u00F6\u00F5", "\u00D6\u00D5", "\u00F6\u00FB", "\u00D6\u00DB", "\u00F6\u00F8", "\u00D6\u00D8", "\u00F6\u00F9", "\u00D6\u00D9", "u\u00FB", "U\u00DB", "u\u00EF", "U\u00CF", "\u00F4\u00EF", "\u00D4\u00CF", "\u00F4\u00F5", "\u00D4\u00D5", "\u00F4\u00FB", "\u00D4\u00DB", "\u00F4\u00F8", "\u00D4\u00D8", "\u00F4\u00F9", "\u00D4\u00D9", "o\u00E4", "O\u00C4", "o\u00E3", "O\u00C3", "o\u00E5", "O\u00C5", "o\u00E0", "O\u00C0", "o\u00E1", "O\u00C1", "o\u00FB", "O\u00DB", "o\u00EF", "O\u00CF", "e\u00E4", "E\u00C4", "e\u00E3", "E\u00C3", "e\u00E5", "E\u00C5", "e\u00E0", "E\u00C0", "e\u00E1", "E\u00C1", "e\u00F5", "E\u00D5", "e\u00FB", "E\u00DB", "e\u00EF", "E\u00CF", "a\u00EB", "A\u00CB", "a\u00FC", "A\u00DC", "a\u00FA", "A\u00DA", "a\u00E8", "A\u00C8", "a\u00E9", "A\u00C9", "a\u00E4", "A\u00C4", "a\u00E3", "A\u00C3", "a\u00E5", "A\u00C5", "a\u00E0", "A\u00C0", "a\u00E1", "A\u00C1", "a\u00FB", "A\u00DB", "a\u00EF", "A\u00CF", "u\u00F5", "U\u00D5", "a\u00EA", "A\u00CA", "y\u00F9", "u\u00F9", "u\u00F8", "o\u00F5", "o\u00F9", "o\u00F8", "e\u00E2", "e\u00F9", "e\u00F8", "a\u00F5", "a\u00E2", "a\u00F9", "a\u00F8", "Y\u00D9", "U\u00D9", "U\u00D8", "O\u00D5", "O\u00D9", "O\u00D8", "E\u00C2", "E\u00D9", "E\u00D8", "A\u00D5", "A\u00C2", "A\u00D9", "A\u00D8", "\u00D4", "\u00F4", "\u00D6", "\u00F6", "\u00C6", "\u00E6" };
						string[] Unicode_charss = { "\u0110", "\u0111", "\u0128", "\u0129", "\u1ECA", "\u1ECB", "\u1EC8", "\u1EC9", "\u1EF4", "\u1EF5", "\u00c6", "\u00e6", "\u1EF9", "\u1EF8", "\u1EF7", "\u1EF6", "\u1EF3", "\u1EF2", "\u1EF1", "\u1EF0", "\u1EEF", "\u1EEE", "\u1EED", "\u1EEC", "\u1EEB", "\u1EEA", "\u1EE9", "\u1EE8", "\u1EE7", "\u1EE6", "\u1EE5", "\u1EE4", "\u1EE3", "\u1EE2", "\u1EE1", "\u1EE0", "\u1EDF", "\u1EDE", "\u1EDD", "\u1EDC", "\u1EDB", "\u1EDA", "\u1ED9", "\u1ED8", "\u1ED7", "\u1ED6", "\u1ED5", "\u1ED4", "\u1ED3", "\u1ED2", "\u1ED1", "\u1ED0", "\u1ECF", "\u1ECE", "\u1ECD", "\u1ECC", "\u1EC7", "\u1EC6", "\u1EC5", "\u1EC4", "\u1EC3", "\u1EC2", "\u1EC1", "\u1EC0", "\u1EBF", "\u1EBE", "\u1EBD", "\u1EBC", "\u1EBB", "\u1EBA", "\u1EB9", "\u1EB8", "\u1EB7", "\u1EB6", "\u1EB5", "\u1EB4", "\u1EB3", "\u1EB2", "\u1EB1", "\u1EB0", "\u1EAF", "\u1EAE", "\u1EAD", "\u1EAC", "\u1EAB", "\u1EAA", "\u1EA9", "\u1EA8", "\u1EA7", "\u1EA6", "\u1EA5", "\u1EA4", "\u1EA3", "\u1EA2", "\u1EA1", "\u1EA0", "\u0169", "\u0168", "\u0103", "\u0102", "\u00FD", "\u00FA", "\u00F9", "\u00F5", "\u00F3", "\u00F2", "\u00EA", "\u00E9", "\u00E8", "\u00E3", "\u00E2", "\u00E1", "\u00E0", "\u00DD", "\u00DA", "\u00D9", "\u00D5", "\u00D3", "\u00D2", "\u00CA", "\u00C9", "\u00C8", "\u00C3", "\u00C2", "\u00C1", "\u00C0", "\u01A0", "\u01A1", "\u01AF", "\u01B0", "\u00d4", "\u00f4" };
						legacyChars = VNI_charss;
						unicodeChars = Unicode_charss;
					}
					break;
				case VietEncodings.VPS:
					{
						string[] VPS_chars = { "\u00CF", "\u00B3", "\u009B", "\u00FD", "\u009C", "\u0019", "\u00FF", "\u00B2", "\u00BF", "\u0015", "\u00BB", "\u001D", "\u00BA", "\u00B1", "\u00D8", "\u00AF", "\u00D9", "\u00AD", "\u00FB", "\u00D1", "\u00F8", "\u0014", "\u00AE", "\u0013", "\u00AB", "\u00A6", "\u00AA", "\u009F", "\u00A9", "\u009E", "\u00A7", "\u009D", "\u00B6", "\u0012", "\u0087", "\u0099", "\u00B0", "\u0098", "\u00D2", "\u0097", "\u00D3", "\u0096", "\u00D5", "\u00BD", "\u0086", "\u0011", "\u00CE", "\u0010", "\u00CC", "\u00B7", "\u008C", "\u0006", "\u00CD", "\u0095", "\u008B", "\u0094", "\u008A", "\u0093", "\u0089", "\u0090", "\u00EB", "\u00FE", "\u00C8", "\u00DE", "\u00CB", "\u0005", "\u00A5", "\u0004", "\u00A4", "\u00F0", "\u00A3", "\u008F", "\u00A2", "\u008E", "\u00A1", "\u008D", "\u00C6", "\u0003", "\u00C5", "\u001C", "\u00C4", "\u0085", "\u00C0", "\u0084", "\u00C3", "\u0083", "\u00E4", "\u0081", "\u00E5", "\u0002", "\u00DC", "\u00D0", "\u00D6", "\u00F7", "\u00DB", "\u00AC", "\u00EF", "\u00B8", "\u00C7", "\u00E6", "\u0088", "\u009A", "\u00A8", "\u00BE", "\u00B9", "\u00BC", "\u00F1", "\u00B4", "\u00B5", "\u00D7", "\u0082", "\u0080", "\u0080", "\u20AC", "\u0082", "\u201A", "\u0161", "\u02C6", "\u0192", "\u201E", "\u2026", "\u017D", "\u0089", "\u201C", "\u008A", "\u201D", "\u2039", "\u2022", "\u008C", "\u2020", "\u2013", "\u2014", "\u02DC", "\u2021", "\u017E", "\u0178", "\u0153", "\u203A", "\u2122", "\u2030", "\u0152", "\u0160" };
						string[] Unicode_chars = { "\u1EF9", "\u1EF8", "\u1EF7", "\u1EF6", "\u1EF5", "\u1EF4", "\u1EF3", "\u1EF2", "\u1EF1", "\u1EF0", "\u1EEF", "\u1EEE", "\u1EED", "\u1EEC", "\u1EEB", "\u1EEA", "\u1EE9", "\u1EE8", "\u1EE7", "\u1EE6", "\u1EE5", "\u1EE4", "\u1EE3", "\u1EE2", "\u1EE1", "\u1EE0", "\u1EDF", "\u1EDE", "\u1EDD", "\u1EDC", "\u1EDB", "\u1EDA", "\u1ED9", "\u1ED8", "\u1ED7", "\u1ED6", "\u1ED5", "\u1ED4", "\u1ED3", "\u1ED2", "\u1ED1", "\u1ED0", "\u1ECF", "\u1ECE", "\u1ECD", "\u1ECC", "\u1ECB", "\u1ECA", "\u1EC9", "\u1EC8", "\u1EC7", "\u1EC6", "\u1EC5", "\u1EC4", "\u1EC3", "\u1EC2", "\u1EC1", "\u1EC0", "\u1EBF", "\u1EBE", "\u1EBD", "\u1EBC", "\u1EBB", "\u1EBA", "\u1EB9", "\u1EB8", "\u1EB7", "\u1EB6", "\u1EB5", "\u1EB4", "\u1EB3", "\u1EB2", "\u1EB1", "\u1EB0", "\u1EAF", "\u1EAE", "\u1EAD", "\u1EAC", "\u1EAB", "\u1EAA", "\u1EA9", "\u1EA8", "\u1EA7", "\u1EA6", "\u1EA5", "\u1EA4", "\u1EA3", "\u1EA2", "\u1EA1", "\u1EA0", "\u01B0", "\u01AF", "\u01A1", "\u01A0", "\u0169", "\u0168", "\u0129", "\u0128", "\u0111", "\u0103", "\u0102", "\u00FD", "\u00D9", "\u00D5", "\u00D3", "\u00D2", "\u0110", "\u00CD", "\u00CC", "\u00C8", "\u00C3", "\u00C0", "\u00C0", "\u00C0", "\u00C2", "\u00C3", "\u00FD", "\u0102", "\u1EA4", "\u1EA6", "\u1EA8", "\u1EB0", "\u1EBF", "\u1EC0", "\u1EC1", "\u1EC2", "\u1EC3", "\u1EC4", "\u1EC7", "\u1ECD", "\u1ED0", "\u1ED2", "\u1ED4", "\u1ED7", "\u1EDC", "\u1EDE", "\u1EF5", "\u1EF7", "\u1ED6", "\u1EBF", "\u1EC7", "\u1EC1" };
						legacyChars = VPS_chars;
						unicodeChars = Unicode_chars;
					}
					break;
				case VietEncodings.VISCII:
					{
						string[] VISCII_chars = { "\u2011", "\u00C5", "\u00E5", "\u00F0", "\u00CE", "\u00EE", "\u009D", "\u00FB", "\u00B4", "\u00BD", "\u00BF", "\u00DF", "\u0080", "\u00D5", "\u00C4", "\u00E4", "\u0084", "\u00A4", "\u0085", "\u00A5", "\u0086", "\u00A6", "\u0006", "\u00E7", "\u0087", "\u00A7", "\u0081", "\u00A1", "\u0082", "\u00A2", "\u0002", "\u00C6", "\u0005", "\u00C7", "\u0083", "\u00A3", "\u0089", "\u00A9", "\u00CB", "\u00EB", "\u0088", "\u00A8", "\u008A", "\u00AA", "\u008B", "\u00AB", "\u008C", "\u00AC", "\u008D", "\u00AD", "\u008E", "\u00AE", "\u009B", "\u00EF", "\u0098", "\u00B8", "\u009A", "\u00F7", "\u0099", "\u00F6", "\u008F", "\u00AF", "\u0090", "\u00B0", "\u0091", "\u00B1", "\u0092", "\u00B2", "\u0093", "\u00B5", "\u0095", "\u00BE", "\u0096", "\u00B6", "\u0097", "\u00B7", "\u00B3", "\u00DE", "\u0094", "\u00FE", "\u009E", "\u00F8", "\u009C", "\u00FC", "\u00BA", "\u00D1", "\u00BB", "\u00D7", "\u00BC", "\u00D8", "\u00FF", "\u00E6", "\u00B9", "\u00F1", "\u009F", "\u00CF", "\u001E", "\u00DC", "\u0014", "\u00D6", "\u0019", "\u00DB", "\u00A0", "\u20AC", "\u201E", "\u2026", "\u2020", "\u2021", "\u201A", "\u0192", "\u2030", "\u02C6", "\u0160", "\u2039", "\u0152", "\u017D", "\u203A", "\u02DC", "\u0161", "\u2122", "\u2018", "\u2019", "\u201C", "\u2022", "\u2013", "\u2014", "\u201D", "\u017E", "\u0153", "\u0178" };
						string[] Unicode_chars = { "\u1ef4", "\u0102", "\u0103", "\u0111", "\u0128", "\u0129", "\u0168", "\u0169", "\u01A0", "\u01A1", "\u01AF", "\u01B0", "\u1EA0", "\u1EA1", "\u1EA2", "\u1EA3", "\u1EA4", "\u1EA5", "\u1EA6", "\u1EA7", "\u1EA8", "\u1EA9", "\u1EAA", "\u1EAB", "\u1EAC", "\u1EAD", "\u1EAE", "\u1EAF", "\u1EB0", "\u1EB1", "\u1EB2", "\u1EB3", "\u1EB4", "\u1EB5", "\u1EB6", "\u1EB7", "\u1EB8", "\u1EB9", "\u1EBA", "\u1EBB", "\u1EBC", "\u1EBD", "\u1EBE", "\u1EBF", "\u1EC0", "\u1EC1", "\u1EC2", "\u1EC3", "\u1EC4", "\u1EC5", "\u1EC6", "\u1EC7", "\u1EC8", "\u1EC9", "\u1ECA", "\u1ECB", "\u1ECC", "\u1ECD", "\u1ECE", "\u1ECF", "\u1ED0", "\u1ED1", "\u1ED2", "\u1ED3", "\u1ED4", "\u1ED5", "\u1ED6", "\u1ED7", "\u1ED8", "\u1ED9", "\u1EDA", "\u1EDB", "\u1EDC", "\u1EDD", "\u1EDE", "\u1EDF", "\u1EE0", "\u1EE1", "\u1EE2", "\u1EE3", "\u1EE4", "\u1EE5", "\u1EE6", "\u1EE7", "\u1EE8", "\u1EE9", "\u1EEA", "\u1EEB", "\u1EEC", "\u1EED", "\u1EEE", "\u1EEF", "\u1EF0", "\u1EF1", "\u1EF2", "\u1EF3", "\u1EF4", "\u1EF5", "\u1EF6", "\u1EF7", "\u1EF8", "\u1EF9", "\u00D5", "\u1EA0", "\u1EA4", "\u1EA6", "\u1EA8", "\u1EAC", "\u1EB0", "\u1EB6", "\u1EB8", "\u1EBC", "\u1EBE", "\u1EC0", "\u1EC2", "\u1EC6", "\u1EC8", "\u1ECA", "\u1ECC", "\u1ECE", "\u1ED4", "\u1ED6", "\u1ED8", "\u1EDA", "\u1EDC", "\u1EDE", "\u1EE2", "\u1EE4", "\u1EE6", "\u1EF2" };
						legacyChars = VISCII_chars;
						unicodeChars = Unicode_chars;
					}
					break;
				case VietEncodings.TCVN3:
					{
						string[] TCVN3_chars = { "\u00FC", "\u00FB", "\u00FE", "\u00FA", "\u00F9", "\u00F7", "\u00F6", "\u00F5", "\u00F8", "\u00F1", "\u00F4", "\u00EE", "\u00EC", "\u00EB", "\u00EA", "\u00ED", "\u00E9", "\u00E7", "\u00E6", "\u00E5", "\u00E8", "\u00E1", "\u00E4", "\u00DE", "\u00D8", "\u00D6", "\u00D4", "\u00D3", "\u00D2", "\u00D5", "\u00CF", "\u00CE", "\u00D1", "\u00C6", "\u00BD", "\u00BC", "\u00AB", "\u00BE", "\u00CB", "\u00C9", "\u00C8", "\u00C7", "\u00CA", "\u00B6", "\u00B9", "\u00AD", "\u00A6", "\u00AC", "\u00A5", "\u00F2", "\u00DC", "\u00AE", "\u00A8", "\u00A1", "\u00F3", "\u00EF", "\u00E2", "\u00BB", "\u00E3", "\u00DF", "\u00DD", "\u00D7", "\u00AA", "\u00D0", "\u00CC", "\u00B7", "\u00A9", "\u00B8", "\u00B5", "\u00A4", "\u00A7", "\u00A3", "\u00A2", "\u0041\u00E0", "\u0041\u1EA3", "\u0041\u00E3", "\u0041\u00E1", "\u0041\u1EA1", "\u0045\u00E8", "\u0045\u1EBB", "\u0045\u1EBD", "\u0045\u00E9", "\u0045\u1EB9", "\u0049\u00EC", "\u0049\u1EC9", "\u0049\u0129", "\u0049\u00ED", "\u0049\u1ECB", "\u004F\u00F2", "\u004F\u1ECF", "\u004F\u00F5", "\u004F\u00F3", "\u004F\u1ECD", "\u0055\u00F9", "\u0055\u1EE7", "\u0055\u0169", "\u0055\u00FA", "\u0055\u1EE5", "\u0059\u1EF3", "\u0059\u1EF7", "\u0059\u1EF9", "\u0059\u00FD", "\u0059\u1EF5", "\u0102\u1EB1", "\u0102\u1EB3", "\u0102\u1EB5", "\u0102\u1EAF", "\u0102\u1EB7", "\u00C2\u1EA7", "\u00C2\u1EA9", "\u00C2\u1EAB", "\u00C2\u1EA5", "\u00C2\u1EAD", "\u00CA\u1EC1", "\u00CA\u1EC3", "\u00CA\u1EC5", "\u00CA\u1EBF", "\u00CA\u1EC7", "\u00D4\u1ED3", "\u00D4\u1ED5", "\u00D4\u1ED7", "\u00D4\u1ED1", "\u00D4\u1ED9", "\u01A0\u1EDD", "\u01A0\u1EDF", "\u01A0\u1EE1", "\u01A0\u1EDB", "\u01A0\u1EE3", "\u01AF\u1EEB", "\u01AF\u1EED", "\u01AF\u1EEF", "\u01AF\u1EE9", "\u01AF\u1EF1" };
						string[] Unicode_chars = { "\u1EF9", "\u1EF7", "\u1EF5", "\u1EF3", "\u1EF1", "\u1EEF", "\u1EED", "\u1EEB", "\u1EE9", "\u1EE7", "\u1EE5", "\u1EE3", "\u1EE1", "\u1EDF", "\u1EDD", "\u1EDB", "\u1ED9", "\u1ED7", "\u1ED5", "\u1ED3", "\u1ED1", "\u1ECF", "\u1ECD", "\u1ECB", "\u1EC9", "\u1EC7", "\u1EC5", "\u1EC3", "\u1EC1", "\u1EBF", "\u1EBD", "\u1EBB", "\u1EB9", "\u1EB7", "\u1EB5", "\u1EB3", "\u00F4", "\u1EAF", "\u1EAD", "\u1EAB", "\u1EA9", "\u1EA7", "\u1EA5", "\u1EA3", "\u1EA1", "\u01B0", "\u01AF", "\u01A1", "\u01A0", "\u0169", "\u0129", "\u0111", "\u0103", "\u0102", "\u00FA", "\u00F9", "\u00F5", "\u1EB1", "\u00F3", "\u00F2", "\u00ED", "\u00EC", "\u00EA", "\u00E9", "\u00E8", "\u00E3", "\u00E2", "\u00E1", "\u00E0", "\u00D4", "\u0110", "\u00CA", "\u00C2", "\u00C0", "\u1EA2", "\u00C3", "\u00C1", "\u1EA0", "\u00C8", "\u1EBA", "\u1EBC", "\u00C9", "\u1EB8", "\u00CC", "\u1EC8", "\u0128", "\u00CD", "\u1ECA", "\u00D2", "\u1ECE", "\u00D5", "\u00D3", "\u1ECC", "\u00D9", "\u1EE6", "\u0168", "\u00DA", "\u1EE4", "\u1EF2", "\u1EF6", "\u1EF8", "\u00DD", "\u1EF4", "\u1EB0", "\u1EB2", "\u1EB4", "\u1EAE", "\u1EB6", "\u1EA6", "\u1EA8", "\u1EAA", "\u1EA4", "\u1EAC", "\u1EC0", "\u1EC2", "\u1EC4", "\u1EBE", "\u1EC6", "\u1ED2", "\u1ED4", "\u1ED6", "\u1ED0", "\u1ED8", "\u1EDC", "\u1EDE", "\u1EE0", "\u1EDA", "\u1EE2", "\u1EEA", "\u1EEC", "\u1EEE", "\u1EE8", "\u1EF0" };
						legacyChars = TCVN3_chars;
						unicodeChars = Unicode_chars;
					}
					break;
				case VietEncodings.Unicode_Composite:
					{
						string[] UnicodeComposite_chars = { "a\u0300", "A\u0300", "a\u0309", "A\u0309", "a\u0303", "A\u0303", "a\u0301", "A\u0301", "a\u0323", "A\u0323", "\u0103\u0300", "\u0102\u0300", "\u0103\u0309", "\u0102\u0309", "\u0103\u0303", "\u0102\u0303", "\u0103\u0301", "\u0102\u0301", "\u0103\u0323", "\u0102\u0323", "\u00e2\u0300", "\u00c2\u0300", "\u00e2\u0309", "\u00c2\u0309", "\u00e2\u0303", "\u00c2\u0303", "\u00e2\u0301", "\u00c2\u0301", "\u00e2\u0323", "\u00c2\u0323", "e\u0300", "E\u0300", "e\u0309", "E\u0309", "e\u0303", "E\u0303", "e\u0301", "E\u0301", "e\u0323", "E\u0323", "\u00ea\u0300", "\u00ca\u0300", "\u00ea\u0309", "\u00ca\u0309", "\u00ea\u0303", "\u00ca\u0303", "\u00ea\u0301", "\u00ca\u0301", "\u00ea\u0323", "\u00ca\u0323", "i\u0300", "I\u0300", "i\u0309", "I\u0309", "i\u0303", "I\u0303", "i\u0301", "I\u0301", "i\u0323", "I\u0323", "o\u0300", "O\u0300", "o\u0309", "O\u0309", "o\u0303", "O\u0303", "o\u0301", "O\u0301", "o\u0323", "O\u0323", "\u00f4\u0300", "\u00d4\u0300", "\u00f4\u0309", "\u00d4\u0309", "\u00f4\u0303", "\u00d4\u0303", "\u00f4\u0301", "\u00d4\u0301", "\u00f4\u0323", "\u00d4\u0323", "\u01a1\u0300", "\u01a0\u0300", "\u01a1\u0309", "\u01a0\u0309", "\u01a1\u0303", "\u01a0\u0303", "\u01a1\u0301", "\u01a0\u0301", "\u01a1\u0323", "\u01a0\u0323", "u\u0300", "U\u0300", "u\u0309", "U\u0309", "u\u0303", "U\u0303", "u\u0301", "U\u0301", "u\u0323", "U\u0323", "\u01b0\u0300", "\u01af\u0300", "\u01b0\u0309", "\u01af\u0309", "\u01b0\u0303", "\u01af\u0303", "\u01b0\u0301", "\u01af\u0301", "\u01b0\u0323", "\u01af\u0323", "y\u0300", "Y\u0300", "y\u0309", "Y\u0309", "y\u0303", "Y\u0303", "y\u0301", "Y\u0301", "y\u0323", "Y\u0323" };
						string[] UnicodePrecomposed_chars = { "\u00e0", "\u00c0", "\u1ea3", "\u1ea2", "\u00e3", "\u00c3", "\u00e1", "\u00c1", "\u1ea1", "\u1ea0", "\u1eb1", "\u1eb0", "\u1eb3", "\u1eb2", "\u1eb5", "\u1eb4", "\u1eaf", "\u1eae", "\u1eb7", "\u1eb6", "\u1ea7", "\u1ea6", "\u1ea9", "\u1ea8", "\u1eab", "\u1eaa", "\u1ea5", "\u1ea4", "\u1ead", "\u1eac", "\u00e8", "\u00c8", "\u1ebb", "\u1eba", "\u1ebd", "\u1ebc", "\u00e9", "\u00c9", "\u1eb9", "\u1eb8", "\u1ec1", "\u1ec0", "\u1ec3", "\u1ec2", "\u1ec5", "\u1ec4", "\u1ebf", "\u1ebe", "\u1ec7", "\u1ec6", "\u00ec", "\u00cc", "\u1ec9", "\u1ec8", "\u0129", "\u0128", "\u00ed", "\u00cd", "\u1ecb", "\u1eca", "\u00f2", "\u00d2", "\u1ecf", "\u1ece", "\u00f5", "\u00d5", "\u00f3", "\u00d3", "\u1ecd", "\u1ecc", "\u1ed3", "\u1ed2", "\u1ed5", "\u1ed4", "\u1ed7", "\u1ed6", "\u1ed1", "\u1ed0", "\u1ed9", "\u1ed8", "\u1edd", "\u1edc", "\u1edf", "\u1ede", "\u1ee1", "\u1ee0", "\u1edb", "\u1eda", "\u1ee3", "\u1ee2", "\u00f9", "\u00d9", "\u1ee7", "\u1ee6", "\u0169", "\u0168", "\u00fa", "\u00da", "\u1ee5", "\u1ee4", "\u1eeb", "\u1eea", "\u1eed", "\u1eec", "\u1eef", "\u1eee", "\u1ee9", "\u1ee8", "\u1ef1", "\u1ef0", "\u1ef3", "\u1ef2", "\u1ef7", "\u1ef6", "\u1ef9", "\u1ef8", "\u00fd", "\u00dd", "\u1ef5", "\u1ef4" };
						legacyChars = UnicodeComposite_chars;
						unicodeChars = UnicodePrecomposed_chars;
					}
					break;
				case VietEncodings.VIQR:
					{
						string[] VIQR_chars = { "u*", "U*", "o*", "O*", "Dd", "D-", "d-", "y~", "Y~", "y?", "Y?", "y.", "Y.", "y`", "Y`", "u+.", "U+.", "u+~", "U+~", "u+?", "U+?", "u+`", "U+`", "u+'", "U+'", "u?", "U?", "u.", "U.", "o+.", "O+.", "o+~", "O+~", "o+?", "O+?", "o+`", "O+`", "o+'", "O+'", "o^^.", "O^^.", "o^^~", "O^^~", "o^^?", "O^^?", "o^^`", "O^^`", "o^^'", "O^^'", "o?", "O?", "o.", "O.", "i.", "I.", "i?", "I?", "e^^.", "E^^.", "e^^~", "E^^~", "e^^?", "E^^?", "e^^`", "E^^`", "e^^'", "E^^'", "e~", "E~", "e?", "E?", "e.", "E.", "a(.", "A(.", "a(~", "A(~", "a(?", "A(?", "a(`", "A(`", "a('", "A('", "a^^.", "A^^.", "a^^~", "A^^~", "a^^?", "A^^?", "a^^`", "A^^`", "a^^'", "A^^'", "a?", "A?", "a.", "A.", "u+", "U+", "o+", "O+", "u~", "U~", "i~", "I~", "dd", "a(", "A(", "y'", "u'", "u`", "o~", "o^^", "o'", "o`", "i'", "i`", "e^^", "e'", "e`", "a~", "a^^", "a'", "a`", "Y'", "U'", "U`", "O~", "O^^", "O'", "O`", "DD", "Dd", "I'", "I`", "E^^", "E'", "E`", "A~", "A^^", "A'", "A`", "\\.", "\\?", "\\d", "\\D", "\\'" };
						string[] Unicode_chars = { "u+", "U+", "o+", "O+", "DD", "DD", "dd", "\u1EF9", "\u1EF8", "\u1EF7", "\u1EF6", "\u1EF5", "\u1EF4", "\u1EF3", "\u1EF2", "\u1EF1", "\u1EF0", "\u1EEF", "\u1EEE", "\u1EED", "\u1EEC", "\u1EEB", "\u1EEA", "\u1EE9", "\u1EE8", "\u1EE7", "\u1EE6", "\u1EE5", "\u1EE4", "\u1EE3", "\u1EE2", "\u1EE1", "\u1EE0", "\u1EDF", "\u1EDE", "\u1EDD", "\u1EDC", "\u1EDB", "\u1EDA", "\u1ED9", "\u1ED8", "\u1ED7", "\u1ED6", "\u1ED5", "\u1ED4", "\u1ED3", "\u1ED2", "\u1ED1", "\u1ED0", "\u1ECF", "\u1ECE", "\u1ECD", "\u1ECC", "\u1ECB", "\u1ECA", "\u1EC9", "\u1EC8", "\u1EC7", "\u1EC6", "\u1EC5", "\u1EC4", "\u1EC3", "\u1EC2", "\u1EC1", "\u1EC0", "\u1EBF", "\u1EBE", "\u1EBD", "\u1EBC", "\u1EBB", "\u1EBA", "\u1EB9", "\u1EB8", "\u1EB7", "\u1EB6", "\u1EB5", "\u1EB4", "\u1EB3", "\u1EB2", "\u1EB1", "\u1EB0", "\u1EAF", "\u1EAE", "\u1EAD", "\u1EAC", "\u1EAB", "\u1EAA", "\u1EA9", "\u1EA8", "\u1EA7", "\u1EA6", "\u1EA5", "\u1EA4", "\u1EA3", "\u1EA2", "\u1EA1", "\u1EA0", "\u01B0", "\u01AF", "\u01A1", "\u01A0", "\u0169", "\u0168", "\u0129", "\u0128", "\u0111", "\u0103", "\u0102", "\u00FD", "\u00FA", "\u00F9", "\u00F5", "\u00F4", "\u00F3", "\u00F2", "\u00ED", "\u00EC", "\u00EA", "\u00E9", "\u00E8", "\u00E3", "\u00E2", "\u00E1", "\u00E0", "\u00DD", "\u00DA", "\u00D9", "\u00D5", "\u00D4", "\u00D3", "\u00D2", "\u0110", "\u0110", "\u00CD", "\u00CC", "\u00CA", "\u00C9", "\u00C8", "\u00C3", "\u00C2", "\u00C1", "\u00C0", ".", "?", "d", "D", "\'" };
						legacyChars = VIQR_chars;
						unicodeChars = Unicode_chars;
					}
					break;
				default:
					{
						throw new System.Exception("Unsupported encoding: " + sourceEncoding);
					}
			}
		}
		/**
		 * Retrieves legacy charset.
		 *
		 * @return 
		 */
		public string[] LegacyChars
		{
			get { return legacyChars; }
		}
		/**
		 * Retrieves corresponding Unicode charset.
		 *
		 * @return 
		 */
		public string[] UnicodeChars
		{
			get { return unicodeChars; }
		}
	}
}

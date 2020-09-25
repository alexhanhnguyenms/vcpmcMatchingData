using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Vcpmc.Mis.Common.vi
{
    public static class VnHelper
    {
        private static readonly string[] VietnameseSigns = new string[]
        {

            "aAeEoOuUiIdDyY",

            "áàạảãâấầậẩẫăắằặẳẵ",

            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

            "éèẹẻẽêếềệểễ",

            "ÉÈẸẺẼÊẾỀỆỂỄ",

            "óòọỏõôốồộổỗơớờợởỡ",

            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

            "úùụủũưứừựửữ",

            "ÚÙỤỦŨƯỨỪỰỬỮ",

            "íìịỉĩ",

            "ÍÌỊỈĨ",

            "đ",

            "Đ",

            "ýỳỵỷỹ",

            "ÝỲỴỶỸ"
        };        
        private static char[] tcvnchars = {
            'µ', '¸', '¶', '·', '¹',
            '¨', '»', '¾', '¼', '½', 'Æ',
            '©', 'Ç', 'Ê', 'È', 'É', 'Ë',
            '®', 'Ì', 'Ð', 'Î', 'Ï', 'Ñ',
            'ª', 'Ò', 'Õ', 'Ó', 'Ô', 'Ö',
            '×', 'Ý', 'Ø', 'Ü', 'Þ',
            'ß', 'ã', 'á', 'â', 'ä',
            '«', 'å', 'è', 'æ', 'ç', 'é',
            '¬', 'ê', 'í', 'ë', 'ì', 'î',
            'ï', 'ó', 'ñ', 'ò', 'ô',
            '­', 'õ', 'ø', 'ö', '÷', 'ù',
            'ú', 'ý', 'û', 'ü', 'þ',
            '¡', '¢', '§', '£', '¤', '¥', '¦'
        };

        private static char[] unichars = {
            'à', 'á', 'ả', 'ã', 'ạ',
            'ă', 'ằ', 'ắ', 'ẳ', 'ẵ', 'ặ',
            'â', 'ầ', 'ấ', 'ẩ', 'ẫ', 'ậ',
            'đ', 'è', 'é', 'ẻ', 'ẽ', 'ẹ',
            'ê', 'ề', 'ế', 'ể', 'ễ', 'ệ',
            'ì', 'í', 'ỉ', 'ĩ', 'ị',
            'ò', 'ó', 'ỏ', 'õ', 'ọ',
            'ô', 'ồ', 'ố', 'ổ', 'ỗ', 'ộ',
            'ơ', 'ờ', 'ớ', 'ở', 'ỡ', 'ợ',
            'ù', 'ú', 'ủ', 'ũ', 'ụ',
            'ư', 'ừ', 'ứ', 'ử', 'ữ', 'ự',
            'ỳ', 'ý', 'ỷ', 'ỹ', 'ỵ',
            'Ă', 'Â', 'Đ', 'Ê', 'Ô', 'Ơ', 'Ư'
        };

        private static char[] convertTable;

        public static void Converter()
        {
            convertTable = new char[256];
            for (int i = 0; i < 256; i++)
                convertTable[i] = (char)i;
            for (int i = 0; i < tcvnchars.Length; i++)
                convertTable[tcvnchars[i]] = unichars[i];
        }

        public static string TCVN3ToUnicode(string value)
        {
            char[] chars = value.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
                if (chars[i] < (char)256)
                    chars[i] = convertTable[chars[i]];
            return new string(chars);
        }
        #region convert VNI to unicode
        public static string ConvertVNIToUnicode(string str)
        {
            #region original
            string[] original = new string[] {
"aù",
"aø",
"aû",
"aõ",
"aï",
"aâ",
"aá",
"aà",
"aå",
"aã",
"aä",
"aê",
"aé",
"aè",
"aú",
"aü",
"aë",
"eù",
"eø",
"eû",
"eõ",
"eï",
"eâ",
"eá",
"eà",
"eå",
"eã",
"eä",
"où",
"oø",
"oû",
"oõ",
"oï",
"oâ",
"oá",
"oà",
"oå",
"oã",
"oä",
"ôù",
"ôø",
"ôû",
"ôõ",
"ôï",
"uù",
"uø",
"uû",
"uõ",
"uï",
"öù",
"öø",
"öû",
"öõ",
"öï",
"yù",
"yø",
"yû",
"yõ",
"AÙ",
"Aù",
"AØ",
"AÛ",
"AÕ",
"AÏ",
"AÂ",
"AÁ",
"AÀ",
"AÅ",
"AÃ",
"AÄ",
"AÊ",
"AÉ",
"AÈ",
"AÚ",
"AÜ",
"AË",
"EÙ",
"EØ",
"EÛ",
"EÕ",
"EÏ",
"EÂ",
"EÁ",
"EÀ",
"EÅ",
"EÃ",
"EÄ",
"OÙ",
"OØ",
"OÛ",
"OÕ",
"OÏ",
"OÂ",
"OÁ",
"OÀ",
"OÅ",
"OÃ",
"OÄ",
"ÔÙ",
"ÔØ",
"ÔÛ",
"ÔÕ",
"ÔÏ",
"UÙ",
"UØ",
"UÛ",
"UÕ",
"UÏ",
"ÖÙ",
"ÖØ",
"ÖÛ",
"ÖÕ",
"ÖÏ",
"YÙ",
"YØ",
"YÛ",
"YÕ",
"í",
"ì",
"æ",
"ó",
"ò",
"ô",
"ö",
"î",
"ñ",
"Í",
"Ì",
"Æ",
"Ó",
"Ò",
"Ô",
"Ö",
"Î",
"Ñ",

            };
            #endregion

            #region transfer
            string[] transfer = new string[] {
"á",
"à",
"ả",
"ã",
"ạ",
"â",
"ấ",
"ầ",
"ẩ",
"ẫ",
"ậ",
"ă",
"ắ",
"ằ",
"ẳ",
"ẵ",
"ặ",
"é",
"è",
"ẻ",
"ẽ",
"ẹ",
"ê",
"ế",
"ề",
"ể",
"ễ",
"ệ",
"ó",
"ò",
"ỏ",
"õ",
"ọ",
"ô",
"ố",
"ồ",
"ổ",
"ỗ",
"ộ",
"ớ",
"ờ",
"ở",
"ỡ",
"ợ",
"ú",
"ù",
"ủ",
"ũ",
"ụ",
"ứ",
"ừ",
"ử",
"ữ",
"ự",
"ý",
"ỳ",
"ỷ",
"ỹ",
"Á",
"Á",
"À",
"Ả",
"Ã",
"Ạ",
"Â",
"Ấ",
"Ầ",
"Ẩ",
"Ẫ",
"Ậ",
"Ă",
"Ắ",
"Ằ",
"Ẳ",
"Ẵ",
"Ặ",
"É",
"È",
"Ẻ",
"Ẽ",
"Ẹ",
"Ê",
"Ế",
"Ề",
"Ể",
"Ễ",
"Ệ",
"Ó",
"Ò",
"Ỏ",
"Õ",
"Ọ",
"Ô",
"Ố",
"Ồ",
"Ổ",
"Ỗ",
"Ộ",
"Ớ",
"Ờ",
"Ở",
"Ỡ",
"Ợ",
"Ú",
"Ù",
"Ủ",
"Ũ",
"Ụ",
"Ứ",
"Ừ",
"Ử",
"Ữ",
"Ự",
"Ý",
"Ỳ",
"Ỷ",
"Ỹ",
"í",
"ì",
"ỉ",
"ĩ",
"ị",
"ơ",
"ư",
"ỵ",
"đ",
"Í",
"Ì",
"Ỉ",
"Ĩ",
"Ị",
"Ơ",
"Ư",
"Ỵ",
"Đ",

            };
            #endregion
            
            if (str.Trim() == string.Empty)
            {
                return str;
            }  
            for (int i = 1; i < original.Length; i++)
            {
                for (int j = 0; j < original[i].Length; j++)
                {
                    str = str.Replace(original[j], transfer[j]);
                }
            }
            return str;
        }
        #endregion
        #region convert TCVN3 to Unicode
        public static string ConvertTCVN3ToUnicode(string str)
        {
            #region original
            string[] original = new string[] {


            };
            #endregion

            #region transfer
            string[] transfer = new string[] {
"á",
"à",
"ả",
"ã",
"ạ",
"â",
"ấ",
"ầ",
"ẩ",
"ẫ",
"ậ",
"ă",
"ắ",
"ằ",
"ẳ",
"ẵ",
"ặ",
"é",
"è",
"ẻ",
"ẽ",
"ẹ",
"ê",
"ế",
"ề",
"ể",
"ễ",
"ệ",
"ó",
"ò",
"ỏ",
"õ",
"ọ",
"ô",
"ố",
"ồ",
"ổ",
"ỗ",
"ộ",
"ớ",
"ờ",
"ở",
"ỡ",
"ợ",
"ú",
"ù",
"ủ",
"ũ",
"ụ",
"ứ",
"ừ",
"ử",
"ữ",
"ự",
"ý",
"ỳ",
"ỷ",
"ỹ",
"Á",
"Á",
"À",
"Ả",
"Ã",
"Ạ",
"Â",
"Ấ",
"Ầ",
"Ẩ",
"Ẫ",
"Ậ",
"Ă",
"Ắ",
"Ằ",
"Ẳ",
"Ẵ",
"Ặ",
"É",
"È",
"Ẻ",
"Ẽ",
"Ẹ",
"Ê",
"Ế",
"Ề",
"Ể",
"Ễ",
"Ệ",
"Ó",
"Ò",
"Ỏ",
"Õ",
"Ọ",
"Ô",
"Ố",
"Ồ",
"Ổ",
"Ỗ",
"Ộ",
"Ớ",
"Ờ",
"Ở",
"Ỡ",
"Ợ",
"Ú",
"Ù",
"Ủ",
"Ũ",
"Ụ",
"Ứ",
"Ừ",
"Ử",
"Ữ",
"Ự",
"Ý",
"Ỳ",
"Ỷ",
"Ỹ",
"í",
"ì",
"ỉ",
"ĩ",
"ị",
"ơ",
"ư",
"ỵ",
"đ",
"Í",
"Ì",
"Ỉ",
"Ĩ",
"Ị",
"Ơ",
"Ư",
"Ỵ",
"Đ",

            };
            #endregion
            return str;
        }
        #endregion

        /// <summary>
        /// Bo  tieng viet khong dau 1
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        //public static string RemoveSign4VietnameseString(string str)
        //{
        //    str = str.ToUpper();
        //    if (str.Trim()==string.Empty)
        //    {
        //        return str;
        //    }            
        //    for (int i = 1; i < VietnameseSigns.Length; i++)
        //    {
        //        for (int j = 0; j < VietnameseSigns[i].Length; j++)
        //        {
        //            str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
        //        }
        //    }            
        //    return str;
        //}
        public static string ConvertToUnSign(string str)
        {
            str = str.ToUpper();
            if (str.Trim() == string.Empty)
            {
                return str;
            }
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                {
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
                }
            }
            return str;
        }
        /// <summary>
        /// Bo tieng viet khong dau 2, bach ngoc toan
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //public static string ConvertToUnSign(string input)
        //{
        //    input = input.Trim();
        //    for (int i = 0x20; i < 0x30; i++)
        //    {
        //        input = input.Replace(((char)i).ToString(), " ");
        //    }
        //    Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
        //    string str = input.Normalize(NormalizationForm.FormD);
        //    string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
        //    while (str2.IndexOf("?") >= 0)
        //    {
        //        str2 = str2.Remove(str2.IndexOf("?"), 1);
        //    }
        //    return str2;
        //}
        /// <summary>
        /// check vietnamese í basic
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool Detect(string text)
        {
            char[] arr1 = new char[] {
                'á', 'à', 'ả', 'ã', 'ạ',
                'Á', 'À', 'Ả', 'Ã', 'Ạ',
                'â', 'ấ', 'ầ', 'ẩ', 'ẫ', 'ậ',
                'Â', 'Ấ', 'Ầ', 'Ẩ', 'Ẫ', 'Ậ',
                'ă', 'ắ', 'ằ', 'ẳ', 'ẵ', 'ặ',
                'Ă', 'Ắ', 'Ằ', 'Ẳ', 'Ẵ', 'Ặ',
                'đ',
                'Đ',
                'é','è','ẻ','ẽ','ẹ',
                'É','È','Ẻ','Ẽ','Ẹ',
                'ê','ế','ề','ể','ễ','ệ',
                'Ê','Ế','Ề','Ể','Ễ','Ệ',
                'í','ì','ỉ','ĩ','ị',
                'Í','Ì','Ỉ','Ĩ','Ị',
                'ó','ò','ỏ','õ','ọ',
                'Ó','Ò','Ỏ','Õ','Ọ',
                'ô','ố','ồ','ổ','ỗ','ộ',
                'Ô','Ố','Ồ','Ổ','Ỗ','Ộ',
                'ơ','ớ','ờ','ở','ỡ','ợ',
                'Ơ','Ớ','Ờ','Ở','Ỡ','Ợ',
                'ú','ù','ủ','ũ','ụ',
                'Ú','Ù','Ủ','Ũ','Ụ',
                'ư','ứ','ừ','ử','ữ','ự',
                'Ư','Ứ','Ừ','Ử','Ữ','Ự',
                'ý','ỳ','ỷ','ỹ','ỵ',
                'Ý','Ỳ','Ỷ','Ỹ','Ỵ',
            };

            for (int i = 0; i < text.Length; i++)
            {
                //char s = text[i];
                for (int j = 0; j < arr1.Length; j++)
                {
                    //char s1 = arr1[j];
                    if (text[i].Equals(arr1[j]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static string RemoveSpecialCharactor(string title)
        {
            if(title.Length == 0)
            {
                return string.Empty;
            }
            while(title.Length>0)
            {
                if (title.Contains("("))
                {
                    int pos_1 = title.IndexOf("(");
                    int pos_2 = title.IndexOf(")");
                    string strremove = string.Empty;
                    if (pos_2 >0 && pos_2 > pos_1)
                    {
                        strremove = title.Substring(pos_1, pos_2 - pos_1 + 1);
                        title = title.Replace(strremove, "").Trim();
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }            

            StringBuilder sb = new StringBuilder();
            foreach (char c in title)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();           
        }
    }
}

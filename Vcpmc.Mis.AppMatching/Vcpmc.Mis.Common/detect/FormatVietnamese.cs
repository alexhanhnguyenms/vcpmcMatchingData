using NPOI.SS.Formula;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Vcpmc.Mis.Shared.masterlist;

namespace Vcpmc.Mis.Common.detect
{
    public static class FormatVietnamese
    {
        static int[] arrLevel = new int[] { 1, 2, 3 };        
        static char[] arrayVovel = new char[]{'A', 'E', 'I', 'O', 'U', 'Y'};
        static string[] arrayVovel2 = new string[] { 
            "AI", "AO", "AU", "AY", "EO", 
            "EU", "IA", "IE", "YE", "IU", 
            "OA", "OE", "OI", "OO", "UA", 
            "UE", "UI", "UO", "UU", "UY" 
        };
        static string[] arrayVovel3 = new string[] { 
            "IEU" ,  "YEU", "OAI", "OAO", "OAY", 
            "OEO", "UAO", "UOI", "UOU", "UYA", 
            "UYE", "UYU" };

        public static string RemoveSpecialCharacters(string str)
        {
            //return Regex.Replace(str, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
            return Regex.Replace(str, "[^a-zA-Z ]", "", RegexOptions.Compiled);
        }

        public static DetectLangReturn DetectVietnamese(string strTextUpperCaseAndTrim)
        {
            DetectLangReturn ir = new DetectLangReturn();
            strTextUpperCaseAndTrim = strTextUpperCaseAndTrim.Trim();
            //DANH SACH CAC NHOM TU
            List<LeterItem> leterItems = new List<LeterItem>();
            //NEU KHONG CO GI, GIA TRI LUON LA 0
            if (strTextUpperCaseAndTrim == "")
            {
                return ir;
            }
            strTextUpperCaseAndTrim = strTextUpperCaseAndTrim.Replace("  ", " ");
            string[] arrayStr = strTextUpperCaseAndTrim.Split(' ');           
            decimal total = arrayStr.Length;
            if (arrayStr.Length == 0)
            {
                return ir;
            }
            LeterItem item = null;            
            for (int i = 0; i < arrayStr.Length; i++)
            {     
                if(arrayStr[i].Trim()=="")
                {
                    //count += 1;
                    total -= 1;
                    continue;
                }
                ir.TotalWord ++;
                item = GetGroup(arrayStr[i]);
                leterItems.Add(item);
                //dem so tu binh thuong
                if(item.ErrorType == LeterErrorType.Normal)
                {
                    ir.CorrectWord++;
                    ir.Score += item.Score1 * item.Score2 * item.Score3 * item.CoutGroup;
                }
                else
                {
                    //int dsf = 1;
                }
            }
            if(ir.Score > 0)
            {
                //int a = 1;
            }
            if(total ==0)
            {
                //int a = 1;
            }
            return ir;
        }

        public static LeterItem GetGroup(string leterUppercase)
        {
            bool check = false;
            //test
            //leterUppercase = "Nhanh".ToUpper();
            //
            LeterItem leterItem = new LeterItem();
            int len = leterUppercase.Length;
            var array = leterUppercase.ToArray();           
            
            int IndexfindVovelFirst = -1;
            int IndexfindVovelEnd = -1;

            #region Kiem tra ban dau
            for (int i = 0; i < len; i++)
            {
                //1. khong phi ky tu tieng viet, bo qua
                if (!array[i].IsUperCaseCharVietnamese())
                {
                    leterItem.ErrorType = LeterErrorType.NotCharactorVN;
                    return leterItem;
                }
                if (array[i].IsSpecialConsonant())
                {
                    leterItem.ErrorType = LeterErrorType.NotCharactorVN;
                    return leterItem;
                }
                //tim vị trí nguyên âm đầu tiên trong từ
                //neu la nguyen am
                if (array[i].IsUperCaseVowel())
                {
                    IndexfindVovelFirst = i;
                    IndexfindVovelEnd = i;
                    break;
                }

            }
            #endregion

            if (IndexfindVovelFirst > -1)
            {
                //1.group1
                #region phu am dau
                int countGroupConsonantsF = IndexfindVovelFirst;
                if (IndexfindVovelFirst > 0)
                {
                    string textGroupF = leterUppercase.Substring(0, IndexfindVovelFirst);                    
                    check = true;
                    //xac dinh co nhom phu am dau
                    if(textGroupF.Length>0)
                    {
                        leterItem.CoutGroup++;
                    }
                    switch (textGroupF.Length)
                    {
                        case 1:
                            //luôn bao giờ cũng đi với âm đệm u để thành qu
                            if (textGroupF.Equals("Q"))
                            {
                                if (!leterUppercase.Substring(IndexfindVovelFirst, 1).Equals("U"))                                  
                                {
                                    check = false;
                                }
                            }
                            // k luôn đứng trước các nguyên âm: i, e, ê.                            
                            else if (textGroupF.Equals("K"))
                            {
                                if(IndexfindVovelFirst<len)
                                {
                                    string sfsdf = leterUppercase.Substring(IndexfindVovelFirst, 1);
                                    if (leterUppercase.Substring(IndexfindVovelFirst,1).Equals("U") ||
                                        leterUppercase.Substring(IndexfindVovelFirst, 1).Equals("O") ||
                                        leterUppercase.Substring(IndexfindVovelFirst, 1).Equals("A") 
                                        )
                                    {
                                        check = false;
                                    }
                                }                                
                            }
                            //c luôn đứng trước các nguyên âm: a, ă, â, o, ô, ơ, u, ư.
                            else if (textGroupF.Equals("C"))
                            {
                                if (IndexfindVovelFirst < len)
                                {                                   
                                    if (leterUppercase.Substring(IndexfindVovelFirst, 1).Equals("E") ||
                                        leterUppercase.Substring(IndexfindVovelFirst, 1).Equals("I"))                                       
                                    {
                                        check = false;
                                    }
                                }
                            }
                            else
                            {
                                check = true;
                                leterItem.Score1 = 1;
                            }
                            break;
                        case 2:
                            if (textGroupF.Equals("CH") || textGroupF.Equals("GH") ||
                            textGroupF.Equals("KH") || textGroupF.Equals("NG") ||
                            textGroupF.Equals("NH") || textGroupF.Equals("PH") ||
                            textGroupF.Equals("TH") || textGroupF.Equals("TR"))
                            {
                                check = true;
                                leterItem.Score1 = 2;
                            }
                            else
                            {
                                check = false;
                            }
                            break;
                        case 3:
                            if (!textGroupF.Equals("NGH"))
                            {
                                leterItem.ErrorType = LeterErrorType.ConsonantFirstNotCorrectformat;
                                return leterItem;
                            }
                            else
                            {
                                leterItem.Score1 = 3;
                            }    
                            break;
                        default:
                            check = false;
                            break;
                    }

                    if (!check)
                    {
                        leterItem.Score1 = 0;
                        leterItem.ErrorType = LeterErrorType.ConsonantFirstNotCorrectformat;
                        return leterItem;
                    }                    

                }
                #endregion
                //2.group2
                #region Nguyen am
                int countGroupVovel = 1;
                for (int i = IndexfindVovelFirst; i < len; i++)
                {                    
                    if (!array[i].IsUperCaseVowel())
                    {
                        IndexfindVovelEnd = i-1;                        
                        break;
                    }
                    IndexfindVovelEnd = i;
                }
                countGroupVovel = IndexfindVovelEnd - IndexfindVovelFirst + 1;
                string groupVovel = leterUppercase.Substring(IndexfindVovelFirst, countGroupVovel);                
                if (groupVovel.Length > 0)
                {
                    check = false;
                    //xac dinh co nhom nguyen am
                    leterItem.CoutGroup++;
                    switch (groupVovel.Length)
                    {
                        case 1:
                            check = true;
                            leterItem.Score2 = 1;
                            break;
                        case 2:
                            for (int v = 0; v < arrayVovel2.Length; v++)
                            {
                                if (groupVovel == arrayVovel2[v])
                                {
                                    check = true;
                                    leterItem.Score2 = 2;
                                    break;
                                }
                            }
                            break;
                        case 3:
                            for (int v = 0; v < arrayVovel3.Length; v++)
                            {
                                if (groupVovel == arrayVovel3[v])
                                {
                                    leterItem.Score2 = 3;
                                    check = true;
                                    break;
                                }
                            }
                            break;
                        default:
                            check = false;
                            break;
                    }
                    if (!check)
                    {
                        leterItem.Score2 = 0;
                        leterItem.ErrorType = LeterErrorType.VovelNotCorrectformat;
                        return leterItem;
                    }
                }
                
                #endregion

                //3.group3
                #region phu am 2
                if (IndexfindVovelEnd < len - 1)
                {
                    string groupConsonantsS = "";
                    groupConsonantsS = leterUppercase.Substring(IndexfindVovelEnd + 1, len - IndexfindVovelEnd - 1);
                    //neu co vovel trong nhom nay la sai
                    int counttemp = groupConsonantsS.Length;
                    if(counttemp > 0)
                    {                        
                        for (int i = 0; i < counttemp; i++)
                        {
                            var arrayTr = groupConsonantsS.ToArray();
                            if (arrayTr[i].IsUperCaseVowel())
                            {
                                leterItem.ErrorType = LeterErrorType.ConsonantSecondNotCorrectformat;
                                return leterItem;
                            }
                        }
                        //xac dinh co nhom phu am sau
                        if(groupConsonantsS.Length > 0)
                        {
                            leterItem.CoutGroup++;
                        }

                        check = false;
                        if (groupConsonantsS.Length > 2)
                        {
                            leterItem.Score3 = 0;
                            check = false;
                        }
                        else if (groupConsonantsS.Length == 2)
                        {
                            if (groupConsonantsS.Equals("NH") || groupConsonantsS.Equals("NG") || groupConsonantsS.Equals("CH"))
                            {
                                leterItem.Score3 = 2;
                                check = true;
                            }
                            else
                            {
                                check = false;
                            }
                        }
                        else
                        {
                            if (groupConsonantsS.Equals("C") || groupConsonantsS.Equals("M")||
                                groupConsonantsS.Equals("N")|| groupConsonantsS.Equals("T") ||
                                groupConsonantsS.Equals("P")
                                )
                            {
                                leterItem.Score3 = 1;
                                check = true;
                            }
                            else
                            {                                
                                check = false;
                            }
                        }

                        if (!check)
                        {
                            leterItem.Score3 = 0;
                            leterItem.ErrorType = LeterErrorType.ConsonantSecondNotCorrectformat;
                            return leterItem;
                        }                        
                    }                   
                }
                #endregion
            }
            else
            {
                leterItem.ErrorType = LeterErrorType.NotFindVovel;
                return leterItem;
            }

            return leterItem;
        }
    }
}

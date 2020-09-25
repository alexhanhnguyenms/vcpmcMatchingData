using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcpmc.Mis.Common
{
    public class StringHelper
    {
        public static List<string> GetUniqueStringArray(string[] array)
        {
            List<string> data = new List<string>();
            for (int i = 0; i < array.Length; i++)
            {
                if(array[i].Trim() != string.Empty )
                {
                    string[] temp = array[i].Split(',');
                    string stritem = "";
                    foreach (var item in temp)
                    {
                        stritem = item.Trim();
                        if (stritem != string.Empty)
                        {
                            if (data.IndexOf(stritem) < 00)
                            {
                                data.Add(stritem);
                            }
                            //else
                            //{
                            //    int a = 0;
                            //}
                        }
                    }
                }                
            }
            return data;
        }
        public static string ConvertListToString(List<string> list)
        {
            string str = "";
            if (list!=null&&list.Count>0)
            {
                foreach (var item in list)
                {
                    if(str.Length>0)
                    {
                        str += ",";
                    }
                    str += item;
                }
            }
            return str;
        }
        public static int findStringInArray(string[] list, string strfind)
        {          
            for (int i = 0; i < list.Length; i++)
            {
                if(strfind == list[i])
                {
                    return i;
                }
            }
            return -1;
        }

        public static string[] ConvertStageToAuthor(string[] arrAyyOuthor,string[] stageName)
        {
            List<string> vs = new List<string>();

            //string[] vinhsu = new string[] {"VINH SU", "CO PHUONG", "DUC VUONG", "HAN NI", "Y SA", "DIEM NHI", "THUY TIEN"};
            //string[] giaotien = new string[] {"HOANG HOA", "THAO TRANG" , "DUONG TRUNG" , "DIEM DAO" , "RANG DONG", "NGAN TRANG", "THU ANH", "KIM KHANH", "XUAN HOA", "XUAN HAU", "XUAN HUONG" };
            bool isAdd = false;
            string name = stageName[0];
            for (int i = 0; i < arrAyyOuthor.Length; i++)
            {
                isAdd = false;
                //vinh su
                foreach (var item in stageName)
                {
                    if(item == arrAyyOuthor[i])
                    {
                        if(vs.IndexOf(name) < 0)
                        {
                            vs.Add(name);
                        }
                        isAdd = true;
                        break;
                    }                    
                }                
                if(!isAdd)
                {
                    vs.Add(arrAyyOuthor[i]);
                }
            }
            return vs.ToArray();
        }
        public static bool CheckStringInArrayString(string author, string[] stageName)
        {
            foreach (var item in stageName)
            {
                if (author == item)
                {
                    return true;                   
                }
            }
            return false;
        }
        public static bool CheckStringInArrayString(string author,List<string> listStageName)
        {
            foreach (var item in listStageName)
            {
                if (author == item)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool CheckStringContainsArrayString(string author, List<string> listStageName)
        {
            foreach (var item in listStageName)
            {
                if (author.Contains(item))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vcpmc.Mis.Shared.Mis.Members;

namespace Vcpmc.Mis.Common.Member
{
    public class ConvertNonMembertoMemberHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="IpCode"></param>
        /// <param name="IpNameType"></param>
        /// <param name="Checkvalue">Gia tri can chuyen doi</param>
        /// <param name="ToValue">sang gia tri</param>
        /// <returns></returns>
        public static bool ConvertValueFixParameter(List<CheckMember> list, string IpCode,string IpNameType,string Checkvalue, ref string ToValue)        
        {
            ToValue = string.Empty;
            var data = list.Where(p => p.IpCode == IpCode && p.IpNameType == IpNameType).ToList();
            if (data.Count > 0)
            { 
                if(Checkvalue == data[0].CheckValue)
                {
                    ToValue = data[0].ToValue;
                    return true;
                }                
            }
            return false;
        }
        public static bool ConvertValueFixParameterMember(List<CheckMember> list, string IpCode, string IpNameType, ref string ToValue)
        {
            ToValue = string.Empty;
            var data = list.Where(p => p.IpCode == IpCode && p.IpNameType == IpNameType).ToList();
            if (data.Count > 0)
            {
                //if (Checkvalue == data[0].CheckValue)
                //{
                ToValue = data[0].ToValue;
                return true;
                //}
            }
            return false;
        }
    }
}

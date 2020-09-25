using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vcpmc.Mis.AppMatching.Models;
using Vcpmc.Mis.Common.vi;
using Vcpmc.Mis.UnicodeConverter;
using Vcpmc.Mis.Utilities.Common;
using Vcpmc.Mis.ViewModels.Media.Youtube;
using Vcpmc.Mis.ViewModels.Mis.Members.Tracking;
using Vcpmc.Mis.ViewModels.Mis.Works.Tracking;
using Vcpmc.Mis.ViewModels.Mis.Works.Tracking.LoadJson;

namespace Vcpmc.Mis.AppMatching.Helper
{
    public static class JsonHelper
    {
        /// <summary>
        /// Nạp file json preclaim
        /// </summary>
        /// <param name="path"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public static List<PreclaimViewModel> JsonToPreclaim(string path)
        {
            string line;
            List<string> subJsonList = new List<string>();
            StreamReader r = new StreamReader(path);
            bool isFirst = false;
            string subJson = "";
            while ((line = r.ReadLine()) != null)
            {
                subJson += line;
                if (line.Trim() == "{")
                {
                    isFirst = true;
                }
                else if (line.Trim() == "}")
                {
                    if (isFirst)
                    {
                        subJsonList.Add(subJson);
                    }
                    isFirst = false;
                    subJson = "";
                }
            }
            List<PreclaimViewModel> objects = new List<PreclaimViewModel>();
            BsonDocument doctem = null;
            PreclaimJson preJson = null;
            PreclaimViewModel preVM = null;
            int serial = 1;
            for (int i = 0; i < subJsonList.Count; i++)
            {
                doctem = BsonSerializer.Deserialize<BsonDocument>(subJsonList[i]);
                preJson = BsonSerializer.Deserialize<PreclaimJson>(doctem);
                preVM = new PreclaimViewModel
                {
                    SerialNo = serial,
                    Asset_ID = preJson.Asset_ID,
                    ISRC = preJson.ISRC == null ? string.Empty: VnHelper.ConvertToUnSign(ConvertAllToUnicode.ConvertFromComposite(preJson.ISRC.ToUpper())),
                    Comp_Asset_ID = preJson.Comp_Asset_ID,
                    C_Title = preJson.C_Title == null ? string.Empty : VnHelper.ConvertToUnSign(ConvertAllToUnicode.ConvertFromComposite( preJson.C_Title.ToUpper())),
                    C_ISWC = preJson.C_ISWC == null ? string.Empty : VnHelper.ConvertToUnSign(ConvertAllToUnicode.ConvertFromComposite(preJson.C_ISWC.ToUpper())),
                    C_Workcode = preJson.C_Workcode == null ? string.Empty : VnHelper.ConvertToUnSign(ConvertAllToUnicode.ConvertFromComposite(preJson.C_Workcode.ToUpper())),
                    C_Writers = preJson.C_Writers == null ? string.Empty : VnHelper.ConvertToUnSign(ConvertAllToUnicode.ConvertFromComposite(preJson.C_Writers.ToUpper())),
                    Combined_Claim = preJson.Combined_Claim == null ? "0": VnHelper.ConvertToUnSign(preJson.Combined_Claim),
                    Mechanical = preJson.Mechanical == null ? "0" : VnHelper.ConvertToUnSign(preJson.Mechanical),
                    Performance = preJson.Performance == null ? "0" : VnHelper.ConvertToUnSign(preJson.Performance),
                    //MONTH = month,// preJson.MONTH,
                    //Year = year
                };
                //if (preJson.CREATED_AT != null)
                //{
                preVM.DtCREATED_AT = TimeTicks.ConvertTicksMongoToC(preJson.CREATED_AT);
                //}
                if (preJson.UPDATED_AT != null)
                {
                    preVM.DtUPDATED_AT = TimeTicks.ConvertTicksMongoToC((long)preJson.UPDATED_AT);
                }
                objects.Add(preVM);
                serial++;
            }
            return objects;
        }
        /// <summary>
        /// trangj thai tai tracking work
        /// </summary>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static WorkRetrievalStatusViewModel JsonToWorkRetrievalStatus(string strJson)
        {
            strJson = strJson.Replace("object", "_object");
            var obj2 = JsonConvert.DeserializeObject<WorkRetrievalStatusViewModel>(strJson);
            return obj2;
        }
        /// <summary>
        /// danh sách tracking work
        /// </summary>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static WorkRetrievalViewModel JsonToWorkRetrieval(string strJson)
        {
            strJson = strJson.Replace("object", "_object");
            var obj2 = JsonConvert.DeserializeObject<WorkRetrievalViewModel>(strJson);
            return obj2;
        }
        /// <summary>
        /// Danh sách membership
        /// </summary>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static MembershipViewModel JsonToMemebership(string strJson)
        {
            strJson = strJson.Replace("object", "_object");
            var obj2 = JsonConvert.DeserializeObject<MembershipViewModel>(strJson);
            return obj2;
        }
        /// <summary>
        /// Nap j file json
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string LoadJson(string path)
        {
            string json = "";
            using (StreamReader r = new StreamReader(path))
            {
                json = r.ReadToEnd().Trim();
            }
            return json;
        }
    }
}

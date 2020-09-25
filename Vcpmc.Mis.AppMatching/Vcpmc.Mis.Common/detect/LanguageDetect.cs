using DetectLanguage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcpmc.Mis.Common.detect
{
    public class LanguageDetect
    {
        string keyApi = "fcb933cfa7d69ef6921f6c5fa6f44e35";
        DetectLanguageClient client = null;
        public LanguageDetect (string key)
        {
            this.keyApi = key;
            client = new DetectLanguageClient(keyApi);
        }
        
        public bool DetectLanguage(string language, int indexDetect = 0)
        {
            bool check = false;
            try
            {
                //language = language.Replace(" ", "");
                if (indexDetect == 0)
                {
                    Task<string> a = client.DetectCodeAsync(language);
                    string b = a.Result;
                    if (b == "vi")
                    {
                        check = true;
                    }
                }
            }
            catch (Exception )
            {
                //int a = 1;
            }
            return check;
        }
    }
}

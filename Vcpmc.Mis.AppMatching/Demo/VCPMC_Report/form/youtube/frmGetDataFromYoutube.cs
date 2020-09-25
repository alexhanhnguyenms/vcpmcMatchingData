using System;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;

namespace Vcpmc.Mis.AppMatching.form.youtube
{
    public partial class frmGetDataFromYoutube : Form
    {
        public frmGetDataFromYoutube()
        {
            InitializeComponent();
        }

        private void frmGetDataFromYoutube_Load(object sender, EventArgs e)
        {

        }

        private void btnGetDataFromeExcel_Click(object sender, EventArgs e)
        {
            try
            {
                CookieContainer cookieContainer = new CookieContainer();
                HttpClientHandler handler = new HttpClientHandler
                {
                    CookieContainer = cookieContainer,
                    ClientCertificateOptions = ClientCertificateOption.Automatic,
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                    AllowAutoRedirect = true,
                    UseDefaultCredentials = false
                };
                HttpClient httpClient = new HttpClient(handler);
                //http://203.169.117.254:8080
                httpClient.BaseAddress = new Uri("https://www.youtube.com");
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.97 Safari/537.36");
                var html = httpClient.GetAsync("channel/UC__Mf4U3476dWy5XATva3-g").Result.Content.ReadAsStringAsync().Result;               
                
                //var allTheLines = html.SplitToLines().ToArray();
                //string text = "";
                string[] array = new string[] { "", "" };
                //int len = 0;
                //for (int i = 0; i < allTheLines.Length; i++)
                //{
                //    allTheLines[i] = allTheLines[i].Trim();
                //    text = "window[\"ytInitialData\"]";
                //    len = text.Length;
                //    if (allTheLines[i].Length> len)
                //    {
                //        if(allTheLines[i].Substring(0, len) == text)
                //        {
                //            array[0] = allTheLines[i];
                //            continue;
                //        }
                //    }
                //    text = "window[\"ytInitialPlayerResponse\"]";
                //    len = text.Length;
                //    if (allTheLines[i].Length > len)
                //    {
                //        if (allTheLines[i].Substring(0, len) == text)
                //        {
                //            array[1] = allTheLines[i];
                //            break;
                //        }
                //    }
                //}

                //for (int i = 0; i < array.Length; i++)
                //{
                //    rbLog.Text += array[i] + Environment.NewLine;
                //    break;
                //    //string json = array[i].Split('=')[1].Trim();
                //    //json = json.Replace("\\", "");
                //    //var obj = JsonConvert.DeserializeObject(json);
                //}
            }
            catch (Exception)
            {
                //int a = 1;
                //throw;
            }
        }
    }
}

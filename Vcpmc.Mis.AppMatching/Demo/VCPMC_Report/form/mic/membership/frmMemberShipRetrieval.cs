using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;

namespace Vcpmc.Mis.AppMatching.form.mic.membership
{
    public partial class frmMemberShipRetrieval : Form
    {
        CookieContainer cookies = null;
        HttpClient httpClient;
        HttpClientHandler handler;
        
        string urlBase = "http://203.169.117.254:8080";
        //string urlLogin = "http://203.169.117.254:8080/login";
        string _csrf = "";
        string JSESSIONID = "";
        public frmMemberShipRetrieval()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                test();
                IniHttpClient();
                //Post();
                bool check = GetLogin("login");
                if (check)
                {
                    var x = handler;
                    Post(_csrf, JSESSIONID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void IniHttpClient()
        {
            cookies = new CookieContainer();
            handler = new HttpClientHandler();
            handler.CookieContainer = cookies;
            //handler = new HttpClientHandler
            //{
            //    CookieContainer = cookies,
            //    ClientCertificateOptions = ClientCertificateOption.Automatic,
            //    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
            //    AllowAutoRedirect = true,
            //    UseDefaultCredentials = false
            //};
            httpClient = new HttpClient(handler);

            ////httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) coc_coc_browser/63.4.154 Chrome/57.4.2987.154 Safari/537.36");
            ///*
            // * Header:
            // * - Origin
            // * - Host
            // * - Referer
            // * - :scheme
            // * - accept
            // * - Accept-Encoding
            // * - Accept-Language
            // * - User-Argent
            // */
         
            httpClient.BaseAddress = new Uri(urlBase);
        }

        private void test()
        {
            CookieContainer cookies = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = cookies;

            HttpClient client = new HttpClient(handler);
            HttpResponseMessage response = client.GetAsync("http://203.169.117.254:8080/login").Result;

            Uri uri = new Uri("http://203.169.117.254:8080/login");
            IEnumerable<Cookie> responseCookies = cookies.GetCookies(uri).Cast<Cookie>();
            foreach (Cookie cookie in responseCookies)
                Console.WriteLine(cookie.Name + ": " + cookie.Value);
        }
        private bool GetLogin(string url)
        {
            bool check = false;
            _csrf = "";
            try
            {
                string html = "";
                HttpResponseMessage response = httpClient.GetAsync("http://203.169.117.254:8080/login").Result;
                #region MyRegion
                Uri uri = new Uri(urlBase + "/" + "login");              
                IEnumerable<Cookie> responseCookies = cookies.GetCookies(new Uri("http://203.169.117.254:8080/login")).Cast<Cookie>();
                foreach (Cookie cookie in responseCookies)
                {
                    if(cookie.Name == "JSESSIONID")
                    {
                        JSESSIONID = cookie.Value;
                    }
                }                 
                #endregion


                #region parse
                html = response.Content.ReadAsStringAsync().Result;
                //var allTheLines = html.SplitToLines().ToArray();
                //string text = "<input type=\"hidden\" name=\"_csrf\"";
                //int len = 0;
                string data = "";

                //for (int i = 0; i < allTheLines.Length; i++)
                //{
                //    allTheLines[i] = allTheLines[i].Trim();
                //    len = text.Length;
                //    if (allTheLines[i].Length > len)
                //    {
                //        if (allTheLines[i].Substring(0, len) == text)
                //        {
                //            data = allTheLines[i];
                //            break;
                //        }
                //    }
                //}
                data = data.Substring(data.IndexOf(" value="), data.Length - data.IndexOf(" value="));
                data = data.Replace("\"", "");
                data = data.Replace("/", "");
                data = data.Replace(">", "");
                data = data.Split('=')[1].Trim();
                _csrf = data;
                check = true;
                #endregion
                
            }
            catch (Exception )
            {
                check = false;
                _csrf = "";
            }
            return check;
        }

        private void Post(string _csrf, string JSESSIONID)
        {
            try
            {
                /*
                 *1.
                 *http://203.169.117.254:8080/login
                 * 
                 * 2.form:
                username: tamnguyen
                password: tamvcpmc1990
                zoneId: GMT+0700
                _csrf: 3f0da891-e28d-4439-bb51-75bb9937ccc4
                 */
                /*
                 
                 */
                IEnumerable<KeyValuePair<string, string>> keyValuePairs = new List<KeyValuePair<string,string>>()
                {
                    new KeyValuePair<string, string>("username", "tamvcpmc1990"),
                    new KeyValuePair<string, string>("password", "tamvcpmc1990"),
                    new KeyValuePair<string, string>("zoneId", "GMT+0700"),
                    new KeyValuePair<string, string>("_csrf", _csrf),
                };
                var content = new FormUrlEncodedContent(keyValuePairs);

                var client = new HttpClient { BaseAddress = new Uri(urlBase) };
                //header
                client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9");
                client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
                client.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");
                client.DefaultRequestHeaders.Add("Cache-Control", "max-age=0");
                client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                //client.DefaultRequestHeaders.Add("Content-Length", "101");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
                client.DefaultRequestHeaders.Add("Cookie", $"JSESSIONID={JSESSIONID}");
                client.DefaultRequestHeaders.Add("Host", "203.169.117.254:8080");
                client.DefaultRequestHeaders.Add("Origin", "http://203.169.117.254:8080");
                client.DefaultRequestHeaders.Add("Referer", "http://203.169.117.254:8080/login?forcedLogin=true");
                client.DefaultRequestHeaders.Add("Upgrade-Insecure-Requests", "1");
                //client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.97 Safari/537.36 (C# App)");
                //client.DefaultRequestHeaders.Add("User-Agent", "C# App");
                //client.DefaultRequestHeaders.Add("User-Agent", "User-Agent-Here");
                // call sync
                //http://203.169.117.254:8080/login?forcedLogin=true
                var response = client.PostAsync("/login?forcedLogin=true", content).Result;
                if (response.IsSuccessStatusCode)
                {
                   // int a = 1;
                }
                else
                {
                   // int a = 2;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void frmMemberShipRetrieval_Load(object sender, EventArgs e)
        {

        }
    }
}

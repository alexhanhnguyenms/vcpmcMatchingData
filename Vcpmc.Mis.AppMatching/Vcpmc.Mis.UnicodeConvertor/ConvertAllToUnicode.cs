
namespace Vcpmc.Mis.UnicodeConverter
{
    public static class ConvertAllToUnicode
    {
        public static string ToUnicode(string text)
        {  
            text = ConvertFromComposite(text);
            //text = ConvertFromNcr(text);
            //text = ConvertTextFromTcvn3(text);
            //text = ConvertTextFromViqr(text);
            //text = ConvertTextFromViscii(text);            
            //text = ConvertTextFromVni(text);
            //text = ConvertTextFromVps(text);
            return text;
        }
        static CompositeConverter Compositetarget = new CompositeConverter();
        public static string ConvertFromComposite(string text)
        {
            return Compositetarget.ConvertText(text);
        }
        static NcrConverter Ncrtarget = new NcrConverter();//ConvertTextTestNcr
        public static string ConvertFromNcr(string text)
        {
            return Ncrtarget.ConvertText(text);
        }
        static Tcvn3Converter Tcvn3target = new Tcvn3Converter();//ConvertTextTestTcvn3
        public static string ConvertTextFromTcvn3(string text)
        {
            return Tcvn3target.ConvertText(text);
        }
        static ViqrConverter Viqrtarget = new ViqrConverter();//ConvertTextTestViqr
        public static string ConvertTextFromViqr(string text)
        {
            return Viqrtarget.ConvertText(text);
        }
        static VisciiConverter Visciitarget = new VisciiConverter();//ConvertTextTestViscii
        public static string ConvertTextFromViscii(string text)
        {
            return Visciitarget.ConvertText(text);
        }
        static VniConverter Vnitarget = new VniConverter();//ConvertTextTestVni
        public static string ConvertTextFromVni(string text)
        {
            return Vnitarget.ConvertText(text);
        }
        static VpsConverter Vpstarget = new VpsConverter();//ConvertTextTestVps
        public static string ConvertTextFromVps(string text)
        {
            return Vpstarget.ConvertText(text);
        }
    }
}

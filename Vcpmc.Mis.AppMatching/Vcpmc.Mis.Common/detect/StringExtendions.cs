
namespace Vcpmc.Mis.Common.detect
{
    public static class StringExtendions
    {
        public static void RepalceSpecial(this string character)
        {
            int post = character.IndexOf("VN");
            if(post > 0)
            {
                //string d = "VIET NAM";
            }
        }
    }
}

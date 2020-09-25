using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vcpmc.Mis.Common.detect
{
    public static class CharExtentions
    {
        /// <summary>
        /// Check is vovel
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public static bool IsUperCaseVowel(this char character)
        {
            return new[] { 'A', 'E', 'I', 'O', 'U', 'Y' }.Contains((character));
        }
        public static bool IsSpecialConsonant(this char character)
        {
            return new[] { 'F', 'J', 'W', 'Z' }.Contains((character));
        }
        /// <summary>
        /// Check is charactor of vietnameses
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public static bool IsUperCaseCharVietnamese(this char character)
        {
            if(character >= 'A' && character <= 'Y')
            {
                return true;
            }
            return false;
        }

        
    }
}

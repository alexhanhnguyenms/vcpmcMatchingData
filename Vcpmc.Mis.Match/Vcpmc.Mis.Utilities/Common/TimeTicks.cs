using System;

namespace Vcpmc.Mis.Utilities.Common
{
    public static class TimeTicks
    {
        public static DateTime ConvertTicksMongoToC(long timeTicks)
        {
            long beginTicks = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
            return new DateTime(beginTicks + timeTicks * 10000, DateTimeKind.Utc);
        }
        public static long ConvertCToTicksMongo(DateTime time)
        {
            long beginTicks = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
            //return new DateTime(beginTicks + time.Ticks * 10000, DateTimeKind.Utc);
            //x= a+10000b=>b= (x-a)/100000
            return (time.Ticks - beginTicks) / 10000;
        }
    }
}

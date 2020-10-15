using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.Utilities
{
    public static class LimitRequestBackend
    {       
        /// <summary>
        /// Gioi han request cua doc quyen
        /// </summary>
        public static int LimitRequestMonopoly = 5000;
        /// <summary>
        /// Gioi han request cua thanh vien
        /// </summary>
        public static int LimitRequestMemberList = 5000;
        /// <summary>
        /// Gioi han request cua mas
        /// </summary>
        public static int LimitRequestMasterlist = 5000;
        /// <summary>
        /// Gioi han request cua tk
        /// </summary>
        public static int LimitRequestTrackingWork = 5000;
        /// <summary>
        /// Gioi han request cua preclaim
        /// </summary>
        public static int LimitRequestPreclaim = 3000;
        /// <summary>
        /// Gioi han request cua bai hat
        /// </summary>
        public static int LimitRequestWork = 3000;
        public static int LimitMatchingPreclaimRequest = 3000;
        /// <summary>
        /// Gioi han request cua matching
        /// </summary>
        //public static int LimitMatchingWorkRequest = 3000;
        /// <summary>
        /// Gioi han request cua fix thong so
        /// </summary>
        public static int LimitRequestFixParameter = 5000;

        public static int LimitRequestWorkHistory { get; set; } = 5000;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Vcpmc.Mis.Utilities.Common
{
    public enum MonopolyType:int
    {
        /// <summary>
        /// Không dò
        /// </summary>
        Not = 0,
        /// <summary>
        /// Tất cả
        /// </summary>
        All = 1,
        /// <summary>
        /// Nhạc chuong
        /// </summary>
        Tone = 2,
        /// <summary>
        /// Web
        /// </summary>
        Web = 3,
        /// <summary>
        /// Bieu dien
        /// </summary>
        Performances = 4,
        /// <summary>
        /// Bieu dien HCM
        /// </summary>
        PerformancesHCM = 5,
        /// <summary>
        /// CD-DVD
        /// </summary>
        Cddvd = 6,
        Kok = 7,
        /// <summary>
        ///truyen hinh
        /// </summary>
        Broadcasting = 8,
        /// <summary>
        /// Giai tri
        /// </summary>
        Entertaiment = 9,
        /// <summary>
        /// Phim
        /// </summary>
        Film = 10,
        /// <summary>
        /// Quang cao
        /// </summary>
        Advertisement = 11,
        /// <summary>
        /// Xuat ban
        /// </summary>
        PubMusicBook = 12,
        Youtube = 13,
        Other = 14,
    }
}

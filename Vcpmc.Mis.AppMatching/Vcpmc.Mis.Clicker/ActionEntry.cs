using Clicker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vcpmc.Mis.Clicker
{
    /// <summary>
    /// Thanh phan nhan
    /// </summary>
    public class ActionEntry
    {
        int x;
        int y;
        string text;
        int interval;
        ClickType type;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="text"></param>
        /// <param name="interval"></param>
        /// <param name="type"></param>
        public ActionEntry(int x, int y, string text, int interval, ClickType type)
        {
            this.x = x;
            this.y = y;
            this.text = text;
            this.interval = interval;
            this.type = type;
        }
        /// <summary>
        /// Toa do x
        /// </summary>
        public int X
        {
            set { x = value; }
            get { return x; }
        }
        /// <summary>
        /// toa do y
        /// </summary>
        public int Y
        {
            set { y = value; }
            get { return y; }
        }
        /// <summary>
        /// Du lieu can nhan
        /// </summary>
        public string Text
        {
            set { text = value; }
            get { return text; }
        }
        /// <summary>
        /// Chu ky nhan
        /// </summary>
        public int Interval
        {
            set { interval = value; }
            get { return interval; }
        }
        /// <summary>
        /// Loai nhan
        /// </summary>
        public ClickType Type
        {
            set { type = value; }
            get { return type; }
        }
    }
}

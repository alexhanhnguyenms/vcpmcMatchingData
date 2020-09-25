using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Vcpmc.Mis.ViewModels.Mis.Distribution.Quarter
{
    [Serializable]
    public class Distribution: ICloneable
    {
        /// <summary>
        /// Ma tac gia
        /// </summary>
        public string IntNo { get; set; } = string.Empty;
        /// <summary>
        /// Ten tac gia
        /// </summary>
        public string Member { get; set; } = string.Empty;
        /// <summary>
        /// IPI (Interested party information) is a unique identifying number assigned by the CISAC database 
        /// to each Interested Party in collective rights management.
        /// </summary>
        public string IPINameNo { get; set; } = string.Empty;
        /// <summary>
        /// he IPI Base Number is the code for a natural person or a legal entity. It has the pattern H-NNNNNNNNN-C.        
        /// H: header(a single letter)
        /// N: identification number(nine numeric digits)
        /// C: check digit(a single number)
        /// For example Pablo Picasso has the IP Base Number I-001068130-6.
        /// </summary>
        public string IPIBaseNo { get; set; } = string.Empty;
        /// <summary>
        /// Tiền tác giả
        /// </summary>
        public decimal WriterShare  { get; set; } = 0;
        /// <summary>
        /// Tiền phát hành
        /// </summary>
        public decimal PublisherShare  { get; set; } = 0;
        /// <summary>
        /// Điều chỉnh
        /// </summary>
        public decimal Adjs { get; set; } = 0;
        /// <summary>
        /// Phân phối youtube
        /// </summary>
        public decimal Youtube { get; set; } = 0;
        /// <summary>
        /// Phân phối ex
        /// </summary>
        public decimal DisExcel { get; set; } = 0;
        /// <summary>
        /// Tiền bản Quyền
        /// </summary>
        public decimal NetRoyalties { get; set; } = 0;
        /// <summary>
        /// Số thứ tự
        /// </summary>
        public int SerialNo { get; set; } = 0;
        /// <summary>
        /// Detail
        /// </summary>
        public List<DistributionDetails> Items { get; set; } = new List<DistributionDetails>();
        public string Path { get; set; } = string.Empty;
        /// <summary>
        /// Load detail
        /// </summary>
        public bool IsLoadDetail { get; set; } = true;
        /// <summary>
        /// Tong phan phoi
        /// </summary>
        public decimal TotalRoyalty { get; set; } = 0;
        /// <summary>
        /// Tên file
        /// </summary>
        public string NameFile { get; set; } = string.Empty;

        //public virtual object Clone()
        //{
        //    return this.MemberwiseClone();
        //}
        public object Clone()
        {

            using (MemoryStream stream = new MemoryStream())
            {
                if (this.GetType().IsSerializable)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, this);
                    stream.Position = 0;
                    return formatter.Deserialize(stream);
                }
                return null;
            }
        }
    }
}

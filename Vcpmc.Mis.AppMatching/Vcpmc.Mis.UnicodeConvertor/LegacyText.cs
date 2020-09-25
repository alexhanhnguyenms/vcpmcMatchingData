using System;
using System.IO;
using Vcpmc.Mis.UnicodeConverter.enums;

namespace Vcpmc.Mis.UnicodeConverter
{
    public class LegacyText : IDocConverter
    {
        Converter converter;

        /// <summary>
        /// Constructor for the LegacyText object. (formerly UnicodeConversion)
        /// </summary>
        /// <param name="sourceEncoding">One of "VISCII", "VPS", "VNI", "VIQR", "TCVN3 (ABC)", or "Unicode Composite"</param>
        public LegacyText(string sourceEncoding)
            : this((VietEncodings)Enum.Parse(typeof(VietEncodings), sourceEncoding))
        {
        }

        /// <summary>
        /// Constructor for the UnicodeConversion object.
        /// </summary>
        /// <param name="sourceEncoding">One of "VISCII", "VPS", "VNI", "VIQR", "TCVN3 (ABC)", or "Unicode Composite"</param>
        public LegacyText(VietEncodings sourceEncoding)
        {
            converter = ConverterFactory.CreateConverter(sourceEncoding);
        }

        /// <summary>
        /// Converts a text-based file.
        /// </summary>
        /// <param name="filename">The name of the file to convert</param>
        /// <returns></returns>
        public string Convert(string filename)
        {
            return converter.Convert(new FileInfo(filename));
        }

        /// <summary>
        /// Converts a text-based file
        /// </summary>
        /// <param name="file">The file to convert</param>
        /// <returns></returns>
        public string Convert(FileInfo file)
        {
            return converter.Convert(file);
        }

        /// <summary>
        /// Converts a specified legacy text-based file and puts output file in a specified directory.
        /// </summary>
        /// <param name="outputDir"></param>
        /// <param name="file"></param>
        public void Convert(DirectoryInfo outputDir, FileInfo file)
        {
            using (StreamWriter outWriter = new StreamWriter(new FileStream(Path.Combine(outputDir.FullName, file.Name), FileMode.Create)))
            {
                outWriter.Write(Convert(file));
                outWriter.Close();
            }
        }

        /// <summary>
        /// Cleans up and releases resources.
        /// </summary>
        public void Quit()
        {
            converter = null;
        }
    }
}

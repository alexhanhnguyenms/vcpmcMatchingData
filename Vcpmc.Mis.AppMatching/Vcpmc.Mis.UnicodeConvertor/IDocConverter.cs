using System.IO;

namespace Vcpmc.Mis.UnicodeConverter
{
    /// <summary>
    /// Interface for legacy document converters.
    /// </summary>
    public interface IDocConverter
    {
        /// <summary>
        /// Converts a legacy document.
        /// </summary>
        /// <param name="outputDir">output directory</param>
        /// <param name="file">input file</param>
        void Convert(DirectoryInfo outputDir, FileInfo file);

        /// <summary>
        /// Quits the converter instance (Word/Excel or others), releasing resources.
        /// </summary>
        void Quit();
    }
}

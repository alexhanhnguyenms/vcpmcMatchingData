using System.Collections.Generic;
using Vcpmc.Mis.UnicodeConverter;

namespace Vcpmc.Mis.Legacy
{
    /// <summary>
    /// Factory for document converters.
    /// </summary>
    public class DocConverterFactory
    {
        const string Word = "Word";
        const string Excel = "Excel";
        const string PowerPoint = "PowerPoint";
        const string Text = "Text";

        /// <summary>
        /// Creates document converter.
        /// </summary>
        /// <param name="docConverters">table of converters</param>
        /// <param name="filename">input file name</param>
        /// <param name="sourceEncoding">encoding</param>
        /// <returns>a document converter</returns>
        public static IDocConverter CreateConverter(Dictionary<string, IDocConverter> docConverters, string fileExtension, string sourceEncoding)
        {
            if (docConverters == null)
            {
                docConverters = new Dictionary<string, IDocConverter>();
            }

            IDocConverter converter;

            if (fileExtension == ".doc" || fileExtension == ".docx")
            {
                if (!docConverters.ContainsKey(Word))
                {
                    docConverters.Add(Word, new LegacyDoc(sourceEncoding));
                }
                converter = docConverters[Word];
            }
            else if (fileExtension == ".xls" || fileExtension == ".xlsx")
            {
                if (!docConverters.ContainsKey(Excel))
                {
                    docConverters.Add(Excel, new LegacyXLS(sourceEncoding));
                }
                converter = docConverters[Excel];
            }
            else if (fileExtension == ".ppt" || fileExtension == ".pptx")
            {
                if (!docConverters.ContainsKey(PowerPoint))
                {
                    docConverters.Add(PowerPoint, new LegacyPPT(sourceEncoding));
                }
                converter = docConverters[PowerPoint];
            }
            else
            {
                if (!docConverters.ContainsKey(Text))
                {
                    docConverters.Add(Text, new LegacyText(sourceEncoding));
                }
                converter = docConverters[Text];
            }

            return converter;
        }
    }
}

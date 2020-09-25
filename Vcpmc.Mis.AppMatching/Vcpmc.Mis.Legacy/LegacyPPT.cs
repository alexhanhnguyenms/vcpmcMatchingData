using System;
using System.IO;
using System.Runtime.InteropServices;
using Vcpmc.Mis.UnicodeConverter;
using Vcpmc.Mis.UnicodeConverter.enums;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;

namespace Vcpmc.Mis.Legacy
{
    public class LegacyPPT : OfficeAppResources, IDocConverter
    {
        // instance variable
        VietEncodings sourceEncoding;

        PowerPoint._Application ppApp;
        PowerPoint.Presentations ppPresSet;
        PowerPoint._Presentation ppPres;

        string[] FindTexts;
        string[] ReplaceWiths;

        /**
		 * Instantiates PowerPoint object.
		 */
        public LegacyPPT(string sourceEncoding)
            : this((VietEncodings)Enum.Parse(typeof(VietEncodings), sourceEncoding))
        {
        }

        public LegacyPPT(VietEncodings sourceEncoding)
        {
            this.sourceEncoding = sourceEncoding;

            try
            {
                ppApp = new PowerPoint.Application();
                ppApp.DisplayAlerts = PowerPoint.PpAlertLevel.ppAlertsNone;
                ppPresSet = ppApp.Presentations;
                InitializeComponent();
            }
            catch (COMException exc)
            {
                throw exc;
            }
        }

        /**
		 * Initializes components.
		 */
        void InitializeComponent()
        {
            LegacyCharSet legacyCharSet = new LegacyCharSet(sourceEncoding);
            FindTexts = legacyCharSet.LegacyChars;
            ReplaceWiths = legacyCharSet.UnicodeChars;
        }

        /// <summary>
        /// Converts a PowerPoint presentation to Unicode and saves to an output directory.
        /// </summary>
        /// <param name="outputDir"></param>
        /// <param name="file"></param>
        public void Convert(DirectoryInfo outputDir, FileInfo file)
        {
            try
            {
                string inputFileFullName = file.FullName;

                ppPres = ppPresSet.Open(inputFileFullName,
                            Microsoft.Office.Core.MsoTriState.msoFalse,
                            Microsoft.Office.Core.MsoTriState.msoTrue,
                            Microsoft.Office.Core.MsoTriState.msoFalse);

                PowerPoint.Slides slides = ppPres.Slides;

                foreach (PowerPoint.Slide slide in slides)
                {
                    ReplaceTextInSlide(slide);
                }

                ppPres.SaveCopyAs(Path.Combine(outputDir.FullName, file.Name)); //, PowerPoint.PpSaveAsFileType.ppSaveAsPresentation, Microsoft.Office.Core.MsoTriState.msoFalse);
                NAR(slides);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                ppPres.Close();
                NAR(ppPres);
            }
        }

        /// <summary>
        /// Replaces text in Slide.
        /// </summary>
        /// <param name="slide"></param>
        void ReplaceTextInSlide(PowerPoint.Slide slide)
        {
            //Loop through all shapes in slide
            foreach (PowerPoint.Shape shape in slide.Shapes)
            {
                ReplaceTextInShape(shape);
            }
            foreach (PowerPoint.Shape shape in slide.NotesPage.Shapes)
            {
                ReplaceTextInShape(shape);
            }
        }

        /// <summary>
        /// Replaces text in Shape.
        /// </summary>
        /// <param name="shape"></param>
        void ReplaceTextInShape(PowerPoint.Shape shape)
        {
            if (shape.HasTextFrame == Microsoft.Office.Core.MsoTriState.msoTrue)
            {
                ReplaceTextInTextFrame(shape.TextFrame);
            }
            else if (shape.HasTable == Microsoft.Office.Core.MsoTriState.msoTrue)
            {
                ReplaceTextInTables(shape.Table);
            }
            else if (shape.Type == Microsoft.Office.Core.MsoShapeType.msoGroup || shape.Type.ToString() == "msoSmartArt")   // msoSmartArt type is new in OFFICE2007
            {
                foreach (PowerPoint.Shape gshape in shape.GroupItems)
                {
                    ReplaceTextInShape(gshape);
                }
            }
        }

        /// <summary>
        /// Replaces text in TextFrame.
        /// </summary>
        /// <param name="ppTextFrame"></param>
        void ReplaceTextInTextFrame(PowerPoint.TextFrame ppTextFrame)
        {
            if (ppTextFrame.HasText == Microsoft.Office.Core.MsoTriState.msoFalse)
            {
                return;
            }

            replaceTextInTextRange(ppTextFrame.TextRange);
        }

        /// <summary>
        /// Replaces text in Table.
        /// </summary>
        /// <param name="ppTable"></param>
        void ReplaceTextInTables(PowerPoint.Table ppTable)
        {
            int iRows = ppTable.Rows.Count;
            int iCols = ppTable.Columns.Count;

            for (int i = 1; i <= iRows; i++)
            {
                for (int j = 1; j <= iCols; j++)
                {
                    replaceTextInTextRange(ppTable.Cell(i, j).Shape.TextFrame.TextRange);
                }
            }
        }

        /// <summary>
        /// Replaces text in TextRange.
        /// </summary>
        /// <param name="textRange"></param>
        void replaceTextInTextRange(PowerPoint.TextRange textRange)
        {
            string text = textRange.Text;
            if (text.Trim().Length == 0)
            {
                return;
            }

            try
            {
                textRange.Font.Name = "Arial";
            }
            catch (Exception)
            {
                //ignore for SmartArt
            }

            for (int i = 0; i < FindTexts.Length; i++)
            {
                int newstart = 0;

                string strFindWhat = FindTexts[i];
                string strReplaceWhat = ReplaceWiths[i];

                PowerPoint.TextRange otemp = textRange.Replace(strFindWhat, strReplaceWhat, newstart, Microsoft.Office.Core.MsoTriState.msoTrue, Microsoft.Office.Core.MsoTriState.msoFalse);
                while (otemp != null)
                {
                    newstart = otemp.Start + otemp.Length;
                    otemp = textRange.Replace(strFindWhat, strReplaceWhat, newstart, Microsoft.Office.Core.MsoTriState.msoTrue, Microsoft.Office.Core.MsoTriState.msoFalse);
                }
            }
        }

        /// <summary>
        /// Quits PowerPoint application.
        /// </summary>
        public void Quit()
        {
            if (ppApp != null)
            {
                try
                {
                    NAR(ppPresSet);
                    ppApp.Quit();
                    NAR(ppApp);
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
                catch (System.InvalidCastException exc)
                {
                    Console.WriteLine(exc.Message);
                }
            }
        }
    }
}

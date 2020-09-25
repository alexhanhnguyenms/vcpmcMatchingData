using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Vcpmc.Mis.UnicodeConverter;
using Vcpmc.Mis.UnicodeConverter.enums;
using Word = Microsoft.Office.Interop.Word;

namespace Vcpmc.Mis.Legacy
{
    public class LegacyDoc : OfficeAppResources, IDocConverter
    {
        // instance variable
        VietEncodings sourceEncoding;

        Word._Application wordApp;
        Word.Documents documents;
        Word._Document aDoc;

        // parameters for Open method
        object ConfirmConversions = false;
        object ReadOnly = false;
        object AddToRecentFiles = false;
        object Revert = false;
        object Visible = true;
        object wFormat = Word.WdOpenFormat.wdOpenFormatAuto;
        object missing = System.Reflection.Missing.Value;

        // parameters for Close and Quit method
        object SaveChanges = Word.WdSaveOptions.wdDoNotSaveChanges;

        // parameters for Replace All method
        object Forward = true;
        object Wrap = Word.WdFindWrap.wdFindContinue;
        object Format = false;
        object MatchCase = true;
        object MatchWholeWord = false;
        object MatchWildcards = false;
        object MatchSoundsLike = false;
        object MatchAllWordForms = false;
        object Replace = Word.WdReplace.wdReplaceAll;

        object[] FindTexts;
        object[] ReplaceWiths;
        object[] Parameters;

        /**
         * Instantiates Word object.
         */
        public LegacyDoc(string sourceEncoding)
            : this((VietEncodings)Enum.Parse(typeof(VietEncodings), sourceEncoding))
        {
        }

        public LegacyDoc(VietEncodings sourceEncoding)
        {
            this.sourceEncoding = sourceEncoding;

            try
            {
                wordApp = new Word.Application(); // instantiate Word object
                wordApp.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone;
                documents = wordApp.Documents;
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

        //
        // Converts a Word document to Unicode and saves to an output directory.
        //
        public void Convert(DirectoryInfo outputDir, FileInfo file)
        {
            try
            {
                string inputFileFullName = file.FullName;

                /*		   
                Documents.Open FileName:="filename.doc", ConfirmConversions:=False, ReadOnly _
                        :=False, AddToRecentFiles:=False, PasswordDocument:="", PasswordTemplate _
                        :="", Revert:=False, WritePasswordDocument:="", WritePasswordTemplate:="" _
                        , Format:=wdOpenFormatAuto
                */
                /*
                Word.Document Open( System.Object FileName , System.Object ConfirmConversions , 
                                    System.Object ReadOnly , System.Object AddToRecentFiles , 
                                    System.Object PasswordDocument , System.Object PasswordTemplate , 
                                    System.Object Revert , System.Object WritePasswordDocument , 
                                    System.Object WritePasswordTemplate , System.Object Format , 
                                    System.Object Encoding , System.Object Visible )
                */

#if OFFICEXP
				aDoc = documents.Open(ref inputFileFullName, 
							ref ConfirmConversions,
							ref ReadOnly, 
							ref AddToRecentFiles, 
							ref missing, 
							ref missing, 
							ref Revert, 
							ref missing, 
							ref missing, 
							ref wFormat,
							ref missing,
							ref Visible,
							ref missing,
							ref missing,
							ref missing);
#elif OFFICE2000
				aDoc = documents.Open(ref inputFileFullName, 
							ref ConfirmConversions,
							ref ReadOnly, 
							ref AddToRecentFiles, 
							ref missing, 
							ref missing, 
							ref Revert, 
							ref missing, 
							ref missing, 
							ref wFormat,
							ref missing,
							ref Visible);
#else
                //				aDoc = documents.Open(ref inputFileFullName, 
                //							ref ConfirmConversions,
                //							ref ReadOnly, 
                //							ref AddToRecentFiles, 
                //							ref missing, 
                //							ref missing, 
                //							ref Revert, 
                //							ref missing, 
                //							ref missing, 
                //							ref wFormat);
                Parameters = new object[] {
                                inputFileFullName,
                                ConfirmConversions,
                                ReadOnly,
                                AddToRecentFiles,
                                missing,
                                missing,
                                Revert,
                                missing,
                                missing,
                                wFormat
                                          };
                aDoc = (Word.Document)documents.GetType().InvokeMember("Open",
                    BindingFlags.InvokeMethod, null, documents, Parameters);

#endif
                //				 aDoc.Activate();

                wordApp.Selection.Find.ClearFormatting();
                wordApp.Selection.Find.Replacement.ClearFormatting();

                object FindText;
                object ReplaceWith;

                ConvertBuiltInProperties(aDoc);

                // convert story types: main text, textbox, header/footer, footnote, etc.
                foreach (Word.Range storyRange in aDoc.StoryRanges)
                {
                    Word.Range currentStoryRange = storyRange;
                    string strTrim = currentStoryRange.Text.Trim();
                    // if no text, skip
                    if (strTrim.Length > 0 && (strTrim != "\u0003" && strTrim != "\u0004")) // 3 & 4 seems to be Word's control characters
                    {
                        for (int i = 0; i < FindTexts.Length; i++)
                        {
                            FindText = FindTexts[i];
                            ReplaceWith = ReplaceWiths[i];
#if OFFICEXP
						currentStoryRange.Find.Execute(ref FindText, ref MatchCase, ref MatchWholeWord, ref MatchWildcards, ref MatchSoundsLike, ref MatchAllWordForms, ref Forward, ref Wrap, ref Format, ref ReplaceWith, ref Replace, ref missing , ref missing, ref missing, ref missing);			
#elif OFFICE2000
						currentStoryRange.Find.Execute(ref FindText, ref MatchCase, ref MatchWholeWord, ref MatchWildcards, ref MatchSoundsLike, ref MatchAllWordForms, ref Forward, ref Wrap, ref Format, ref ReplaceWith, ref Replace, ref missing , ref missing, ref missing, ref missing);			
#else
                            //						currentStoryRange.Find.Execute(ref FindText, ref MatchCase, ref MatchWholeWord, ref MatchWildcards, ref MatchSoundsLike, ref MatchAllWordForms, ref Forward, ref Wrap, ref Format, ref ReplaceWith, ref Replace);			
                            Parameters = new object[] { FindText, MatchCase, MatchWholeWord, MatchWildcards, MatchSoundsLike, MatchAllWordForms, Forward, Wrap, Format, ReplaceWith, Replace };
                            currentStoryRange.Find.GetType().InvokeMember("Execute", BindingFlags.InvokeMethod, null, currentStoryRange.Find, Parameters);
#endif
                        }

                        currentStoryRange.Font.Name = "Arial"; // font for non-main text
                        if (currentStoryRange.StoryType == Word.WdStoryType.wdMainTextStory)
                            currentStoryRange.Font.Name = "Time New Roman"; // font for main text
                    }

                    // if there are more stories of the same type, continue converting
                    while (currentStoryRange.NextStoryRange != null)
                    {
                        currentStoryRange = currentStoryRange.NextStoryRange;

                        // if no text, skip
                        if (currentStoryRange.Text.Trim().Length > 0)
                        {
                            for (int i = 0; i < FindTexts.Length; i++)
                            {
                                FindText = FindTexts[i];
                                ReplaceWith = ReplaceWiths[i];
#if OFFICEXP
							currentStoryRange.Find.Execute(ref FindText, ref MatchCase, ref MatchWholeWord, ref MatchWildcards, ref MatchSoundsLike, ref MatchAllWordForms, ref Forward, ref Wrap, ref Format, ref ReplaceWith, ref Replace, ref missing , ref missing, ref missing, ref missing);			
#elif OFFICE2000
							currentStoryRange.Find.Execute(ref FindText, ref MatchCase, ref MatchWholeWord, ref MatchWildcards, ref MatchSoundsLike, ref MatchAllWordForms, ref Forward, ref Wrap, ref Format, ref ReplaceWith, ref Replace, ref missing , ref missing, ref missing, ref missing);			
#else
                                //							currentStoryRange.Find.Execute(ref FindText, ref MatchCase, ref MatchWholeWord, ref MatchWildcards, ref MatchSoundsLike, ref MatchAllWordForms, ref Forward, ref Wrap, ref Format, ref ReplaceWith, ref Replace);			
                                Parameters = new object[] { FindText, MatchCase, MatchWholeWord, MatchWildcards, MatchSoundsLike, MatchAllWordForms, Forward, Wrap, Format, ReplaceWith, Replace };
                                currentStoryRange.Find.GetType().InvokeMember("Execute", BindingFlags.InvokeMethod, null, currentStoryRange.Find, Parameters);
#endif
                            }
                            //						currentStoryRange.WholeStory();
                            currentStoryRange.Font.Name = "Arial";
                            if (currentStoryRange.StoryType == Word.WdStoryType.wdMainTextStory)
                                currentStoryRange.Font.Name = "Time New Roman"; // font for main text
                        }
                    }
                }

                //string outputDirName = outputDir.FullName + "_Unicode" + "\\" + file.DirectoryName.Substring(selectedDir.FullName.Length);
                wordApp.ChangeFileOpenDirectory(outputDir.FullName);
                object outputFileName = file.Name;

                /*
                ActiveDocument.SaveAs FileName:="filename.doc", FileFormat:=wdFormatDocument, _
                        LockComments:=False, Password:="", AddToRecentFiles:=True, WritePassword _
                    :="", ReadOnlyRecommended:=False, EmbedTrueTypeFonts:=False, _
                    SaveNativePictureFormat:=False, SaveFormsData:=False, SaveAsAOCELetter:=False
                */

                //		aDoc.SaveAs( System.Object FileName , System.Object FileFormat , System.Object LockComments , System.Object Password , System.Object AddToRecentFiles , System.Object WritePassword , System.Object ReadOnlyRecommended , System.Object EmbedTrueTypeFonts , System.Object SaveNativePictureFormat , System.Object SaveFormsData , System.Object SaveAsAOCELetter );
#if OFFICEXP
				aDoc.SaveAs(ref outputFileName, ref missing , ref missing , ref missing , ref missing , ref missing , ref missing , ref missing , ref missing , ref missing , ref missing, ref missing , ref missing , ref missing , ref missing , ref missing);
#else
                //				aDoc.SaveAs(ref outputFileName, ref missing , ref missing , ref missing , ref missing , ref missing , ref missing , ref missing , ref missing , ref missing , ref missing );
                Parameters = new object[] { outputFileName, missing, missing, missing, missing, missing, missing, missing, missing, missing, missing };
                aDoc.GetType().InvokeMember("SaveAs", BindingFlags.InvokeMethod, null, aDoc, Parameters);
#endif
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                aDoc.Close(ref SaveChanges, ref missing, ref missing);
                NAR(aDoc);
            }
        }

        /// <summary>
        /// Converts Word document's properties.
        /// </summary>
        /// <param name="aDoc"></param>
        void ConvertBuiltInProperties(Word._Document aDoc)
        {
            // For MS Word Built-in Properties, see:
            // http://msdn.microsoft.com/en-us/library/microsoft.office.tools.word.document.builtindocumentproperties.aspx
            // Note: Conversion of properties values does not work for VIQR b/c of Word special characters used in Find/Replace operations.
            // Get adoc's properties
            object oDocBuiltInProps = aDoc.BuiltInDocumentProperties;
            ConvertBuiltInProperty(Word.WdBuiltInProperty.wdPropertyTitle, oDocBuiltInProps);
            ConvertBuiltInProperty(Word.WdBuiltInProperty.wdPropertySubject, oDocBuiltInProps);
            ConvertBuiltInProperty(Word.WdBuiltInProperty.wdPropertyAuthor, oDocBuiltInProps);
            ConvertBuiltInProperty(Word.WdBuiltInProperty.wdPropertyComments, oDocBuiltInProps);
        }

        /// <summary>
        /// Converts Word document's property.
        /// </summary>
        /// <param name="propIndex"></param>
        /// <param name="oDocBuiltInProps"></param>
        void ConvertBuiltInProperty(Word.WdBuiltInProperty propIndex, object oDocBuiltInProps)
        {
            // Microsoft.Office.Interop.Word.WdBuiltInProperty
            // ref: http://support2.microsoft.com/default.aspx?scid=kb;EN-US;303296			
            Type typeDocBuiltInProps = oDocBuiltInProps.GetType();

            string strIndex = propIndex.ToString().Replace("wdProperty", string.Empty);
            object oDocProp = typeDocBuiltInProps.InvokeMember("Item",
                BindingFlags.Default | BindingFlags.GetProperty,
                null, oDocBuiltInProps,
                new object[] { strIndex });

            Type typeDocProp = oDocProp.GetType();

            string strValue = string.Empty;
            object oDocPropValue = typeDocProp.InvokeMember("Value",
                BindingFlags.Default | BindingFlags.GetProperty,
                null, oDocProp,
                new object[] { });

            if (oDocPropValue != null)
            {
                strValue = oDocPropValue.ToString();
            }

            if (strValue.Trim().Length > 0)
            {
                // convert Title to Unicode
                StringBuilder strB = new StringBuilder(strValue);
                for (int i = 0; i < FindTexts.Length; i++)
                {
                    strB = strB.Replace((string)FindTexts[i], (string)ReplaceWiths[i]);
                }
                strValue = strB.ToString();

                //Set the Title property
                typeDocProp.InvokeMember("Item",
                    BindingFlags.Default | BindingFlags.SetProperty,
                    null, oDocBuiltInProps,
                    new object[] { strIndex, strValue });
            }
        }

        ///
        ///Quits Word application.
        ///
        public void Quit()
        {
            if (wordApp != null)
            {
                try
                {
                    NAR(documents);
                    wordApp.Quit(ref SaveChanges, ref missing, ref missing);	// release Word resources
                    NAR(wordApp);
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

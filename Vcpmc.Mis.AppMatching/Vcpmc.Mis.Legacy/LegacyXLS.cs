using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Vcpmc.Mis.UnicodeConverter;
using Vcpmc.Mis.UnicodeConverter.enums;
using Excel = Microsoft.Office.Interop.Excel;

namespace Vcpmc.Mis.Legacy
{
	public class LegacyXLS : OfficeAppResources, IDocConverter
	{
		VietEncodings sourceEncoding;

		Excel.Application excelApp;
		Excel.Workbooks workbooks;
		Excel.Workbook workbook;

		object missing = Type.Missing;

		// parameters for Open method
		object ReadOnly = false;
		object IgnoreReadOnlyRecommended = true;
		object Editable = false;

		// parameters for Close method
		object SaveChanges = false;

		// parameters for Replace method
		object MatchCase = true;
		object SearchFormat = false;
		object ReplaceFormat = false;
		object LookAt = Excel.XlLookAt.xlPart;
		object SearchOrder = Excel.XlSearchOrder.xlByRows;

		object[] Whats;
		object[] Replacements;
		object[] Parameters;

		/**
		 * Instantiates Excel object.
		 */
		public LegacyXLS(string sourceEncoding) : this((VietEncodings)Enum.Parse(typeof(VietEncodings), sourceEncoding))
		{
		}

		public LegacyXLS(VietEncodings sourceEncoding)
		{
			this.sourceEncoding = sourceEncoding;

			try
			{
				excelApp = new Excel.Application(); // instantiate Excel object
				excelApp.DisplayAlerts = false;
				workbooks = excelApp.Workbooks;
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
			if (sourceEncoding == VietEncodings.VIQR)
			{
				// Problem with converting VIQR in Excel
				// Microsoft Knowledge Base Article - 124739
				// Can't Use Find/Replace on Text with Leading Apostrophe
				// http://support.microsoft.com/default.aspx?scid=kb;en-us;124739
				// The engine for the Find and Find Next commands does not recognize 
				// a leading apostrophe character because this character in this position is designed to be transparent.

				String message = "Conversion of Excel workbooks in VIQR format is not possible.\n"
					+ "Microsoft Knowledge Base Article - 124739\n"
					+ "Can't Use Find/Replace on Text with Leading Apostrophe\n"
					+ "http://support.microsoft.com/default.aspx?scid=kb;en-us;124739";

				throw new Exception(message);
			}

			LegacyCharSet legacyCharSet = new LegacyCharSet(sourceEncoding);
			Whats = legacyCharSet.LegacyChars;
			Replacements = legacyCharSet.UnicodeChars;
		}

		//
		// Converts an Excel workbook to Unicode and saves to an output directory.
		//
		public void Convert(DirectoryInfo outputDir, FileInfo file)
		{
			try
			{
				string inputFileFullName = file.FullName;

#if OFFICEXP
				/*
				Excel.Workbook Open ( System.String Filename , 
						System.Object UpdateLinks , System.Object ReadOnly , 
						System.Object Format , System.Object Password , 
						System.Object WriteResPassword , System.Object IgnoreReadOnlyRecommended , 
						System.Object Origin , System.Object Delimiter , 
						System.Object Editable , System.Object Notify , 
						System.Object Converter , System.Object AddToMru , 
						System.Object Local , System.Object CorruptLoad )
				*/
				workbook = workbooks.Open(inputFileFullName, 
											missing,
											ReadOnly,
											missing,
											missing,
											missing,
											IgnoreReadOnlyRecommended,
											missing,
											missing,
											Editable,
											missing,
											missing,
											missing,
											missing,
											missing
											);
#else
				/*
				Excel.Workbook Open ( System.String Filename , 
						System.Object UpdateLinks , System.Object ReadOnly , 
						System.Object Format , System.Object Password , 
						System.Object WriteResPassword , System.Object IgnoreReadOnlyRecommended , 
						System.Object Origin , System.Object Delimiter , 
						System.Object Editable , System.Object Notify , 
						System.Object Converter , System.Object AddToMru )
				*/
				//				workbook = workbooks.Open(inputFileFullName, 
				//											missing,
				//											ReadOnly,
				//											missing,
				//											missing,
				//											missing,
				//											IgnoreReadOnlyRecommended,
				//											missing,
				//											missing,
				//											Editable,
				//											missing,
				//											missing,
				//											missing
				//											);
				Parameters = new object[] {
											inputFileFullName,
											missing,
											ReadOnly,
											missing,
											missing,
											missing,
											IgnoreReadOnlyRecommended,
											missing,
											missing,
											Editable,
											missing,
											missing,
											missing
										  };
				workbook = (Excel.Workbook)workbooks.GetType().InvokeMember("Open",
					BindingFlags.InvokeMethod, null, workbooks, Parameters);

#endif

				Excel.Sheets worksheets = workbook.Worksheets;

				foreach (Excel.Worksheet worksheet in workbook.Sheets)
				{
					// Go thru each worksheet
					string worksheetName = worksheet.Name;
					Excel.Range cells = worksheet.Cells;

					for (int i = 0; i < Whats.Length; i++)
					{
						worksheetName = worksheetName.Replace((string)Whats[i], (string)Replacements[i]); // convert sheet name

#if OFFICE2000
//						Replace(What, Replacement, [LookAt], [SearchOrder], [MatchCase], [MatchByte])
						cells.Replace(Whats[i], Replacements[i], LookAt, SearchOrder, MatchCase, missing);
#elif OFFICEXP
//						Replace (What ,Replacement ,LookAt ,SearchOrder ,MatchCase ,MatchByte ,SearchFormat ,ReplaceFormat )
						cells.Replace(Whats[i], Replacements[i], LookAt, SearchOrder, MatchCase, missing, SearchFormat, ReplaceFormat);
#else
						//						Replace(What, Replacement, [LookAt], [SearchOrder], [MatchCase], [MatchByte], [MatchControlCharacters], [MatchDiacritics], [MatchKashida], [MatchAlefHamza]) 
						//						cells.Replace(Whats[i], Replacements[i], LookAt, SearchOrder, MatchCase, missing, missing, missing, missing, missing); // This one throws an exception for Excel 2000
						Parameters = new object[] { Whats[i], Replacements[i], LookAt, SearchOrder, MatchCase, missing };
						cells.GetType().InvokeMember("Replace", BindingFlags.InvokeMethod, null, cells, Parameters);
#endif
					}

					worksheet.Name = worksheetName;
					cells.Font.Name = "Arial";
				}

				workbook.SaveCopyAs(Path.Combine(outputDir.FullName, file.Name));
				NAR(worksheets);
			}
			catch (Exception e)
			{
				throw e;
			}
			finally
			{
				workbook.Close(SaveChanges, missing, missing);
				NAR(workbook);
			}
		}

		//
		// Quits Excel application.
		//
		public void Quit()
		{
			if (excelApp != null)
			{
				try
				{
					NAR(workbooks);
					excelApp.Quit();
					NAR(excelApp);
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

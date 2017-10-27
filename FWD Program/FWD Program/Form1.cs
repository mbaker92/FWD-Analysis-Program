/* Author: Matthew Baker
 * Program: FWD Program
 * Purpose: Used to create summary/analysis excel file for a selected FWD access database.
 * Date Created: 10/20/2017
 * Date Modified: 10/21/2017
 * Version : 1.1
 *      Version 1.0: Initial setup of the functions and basic functionality.
 *      Version 1.1: Some Try Catch blocks added to stop the program from failing due to Access or Excel problems.
 */

using System;
using System.Diagnostics;
using System.Windows.Forms;
using Access = Microsoft.Office.Interop.Access;
using Excel = Microsoft.Office.Interop.Excel;

namespace FWD_Program
{
    public partial class Form1 : Form
    {
        // Global Variables for the class
        private string DirectPath = "";
        private string ExecutingPath = "";
        private static string AccessFile = "FWD_DataAnalysis.accdb";
        private static string ExcelFile = "FWD_DataAnalysisExcelMacroSheet_Exe.xlsb";


        public Form1()
        {
            InitializeComponent();

            // Set Text on Form to blank 
            DirectoryPath.Text = "";

            // Get the path of where the program is being executed.
            ExecutingPath = Environment.CurrentDirectory;
        }


        /* Function Name: Browser_Click
         * Purpose: The user starts the processing by first selecting a folder. Once
         *          the folder is selected, the directory label is changed to the folder
         *          chosen and the path is copied to the global variable DirectPath. The
         *          RenameOldFile function is called to rename the old default Excel filename
         *          if it exists. DeleteFile is called to remove the Excel file created by
         *          a previous run of RunAccess if it exists. The Excel and Access files used
         *          for processing are copied to the directory. RunAccess and RunExcel are
         *          called. Once those functions are done, the Browser_Click function will then
         *          delete the files that are not needed in the directory anymore and notify
         *          the user that the Excel file is ready for viewing.
         */
        private void Browser_Click(object sender, EventArgs e)
        {
            if(FolderBrowser.ShowDialog() == DialogResult.OK)
            {
                // Get the User's Selected Path and Change DirectoryPath label to show the path
                DirectPath = FolderBrowser.SelectedPath;
                DirectoryPath.Text = DirectPath;

                // Rename Default File
                RenameOldFile();

                // Delete FWDCombinedtable.xlsx if it exists
                DeleteFile("FWD_CombinedTable.xlsx");

                // Copy Content Files
                CopyContentFiles();

                /* Try Catch block for Excel and Access Functions in case the directory is not a trusted location
                 * for the Access and Excel programs. Also used in case the user cancels something during the 
                 * Access or Excel code execution.
                */
                try
                {
                    // Run AnalyseData in accdb file
                    RunAccess();

                    // Run RunGrapherIdentifier in xlsb
                    RunExcel();
                }
                catch (System.Runtime.InteropServices.COMException)
                {
                    MessageBox.Show("Please choose the folder again to restart.", " An Error Occurred", MessageBoxButtons.OK);
                    return;
                }

                // Delete files at end of processing
                DeleteFile("FWD_DataAnalysisExcelMacroSheet_Exe.xlsb");
                DeleteFile("FWD_CombinedTable.xlsx");
                DeleteFile("FWD_DataAnalysis.accdb");

                // Notifiy user that Excel file is ready for viewing
                MessageBox.Show("Excel File is Ready For Viewing", "Finished", MessageBoxButtons.OK);
            }
        }


        /* Function Name: InstructionLabel_LinkClicked
         * Purpose: Start another process to open a .txt document with notepad that 
         *          has instructions on how to use the program and what to expect. 
         *          The text file is in the FWDFiles folder of the executing path.
         */
        private void InstructionLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
          Process.Start("notepad.exe", ExecutingPath + @"\FWDFiles\Instructions.txt");
        }


        /* Function Name: DeleteFile
         * Purpose: DeleteFile will delete any file with the same name as the string
         *          passed into the function that is in the same path as the user chose
         *          at the beginning of the program.
         */
        private void DeleteFile(string Filename)
        {
            if(System.IO.File.Exists(DirectPath + @"\"+ Filename ))
            {
                System.IO.File.Delete(DirectPath + @"\" + Filename);
            }
        }


        /* Function Name: CopyContentFiles
         * Purpose: CopyContentFiles will copy the access and excel files from the 
         *          FWDFiles folder of the executing path to the user selected path.
         *          If one of the files is already there, the file will be overwritten.
         */
        private void CopyContentFiles()
        {
            System.IO.File.Copy(ExecutingPath + @"\FWDFiles\" + AccessFile, DirectPath + @"\" + AccessFile, true);
            System.IO.File.Copy(ExecutingPath + @"\FWDFiles\" + ExcelFile, DirectPath + @"\" + ExcelFile, true);
        }


        /* Function Name: RenameOldFile
         * Purpose: The Default file created by the excel file is FWDOut.xlsm. If there if
         *          already a file in the folder, the function will append the time to the end
         *          of the filename so that it is not destroyed if the user chooses to make
         *          another excel with the default filename.
         */
        private void RenameOldFile()
        {
            try
            {
                if (System.IO.File.Exists(DirectPath + @"\FWDOut.xlsm"))
                {
                    System.IO.File.Move(DirectPath + @"\FWDOut.xlsm", DirectPath + @"\FWDOut" + DateTime.Now.ToString("HHmmss") + @".xlsm");
                }
            }
            catch(Exception)
            {
                if(MessageBox.Show("Close Excel Document FWDOut.xlsm to Continue", "Error", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    RenameOldFile();
                }   
            }
        }


        /* Function Name: RunAccess
         * Purpose: RunAccess will create a new instance of Access and open the Access database
         *          that was copied to the user selected folder. It will then run the macro used
         *          in analyzing the data from another database. When the macro is done, the function
         *          will save and quit the access database. It will then release the resources.
         */
        private void RunAccess()
        {
            Access.Application oAccess = new Access.Application();
            oAccess.Visible = true;
            oAccess.OpenCurrentDatabase(DirectPath + @"\" + AccessFile, false, "");
            try
            {
                oAccess.DoCmd.RunMacro("Run_FWD_Analysis");
                oAccess.DoCmd.Quit(Access.AcQuitOption.acQuitSaveAll);
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                throw;
            }
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oAccess);
            oAccess = null;
        }


        /* Function Name: RunExcel
         * Purpose: RunExcel will create a new instance of Excel and open the Excel file copied to
         *          the user selected directory. The function will then run the macro that modifies
         *          the Excel file that came from the code of the RunAccess function. Once the macro
         *          is done, the function will quit Excel and release the resources.
         */
        private void RunExcel()
        {
            object oMissing = System.Reflection.Missing.Value;
            Excel.Application oExcel = new Excel.Application();
            oExcel.Visible = true;
            Excel.Workbooks oBooks = oExcel.Workbooks;
            Excel._Workbook oBook = null;
            oBook = oBooks.Open(DirectPath + @"\" + ExcelFile, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing, oMissing);

            try
            {
                // Run the Excel Macro
                oExcel.Run("RunGrapherIdentifier");
                oBook.Close(false, oMissing, oMissing);
                oExcel.Quit();
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                throw;
            }

            //Release Resources
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oBook);
            oBook = null;
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oBooks);
            oBooks = null;
            System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel);
            oExcel = null;
        }
    }
}


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Import_GDT
{
   /// <summary>
   /// 
   /// </summary>
   public partial class MainViewModel
   {
      private Cursor _previousCursor;
      
      BackgroundWorker bg;
      ObservableCollection<stFileNames> tempListOfFiles;
      double cur;
      List<string> allnames;
      /// <summary>
      /// 
      /// </summary>
      /// <param name="obj"></param>
      private void fnClose(object obj = null)
      {
         CloseAction();
      }

      //MVM.MakeAbout();
      /// <summary>
      /// MVM.MakeAbout();
      /// <Label x:Name="About" Content="{ Binding About, Mode = TwoWay, UpdateSourceTrigger = PropertyChanged}" Margin="0,-3.283,3,0" VerticalAlignment="Top" HorizontalAlignment="Right" Width="101.967" Height="25.96"/>
      /// </summary>
      public void fnMakeAbout()
      {
         Version v = Assembly.GetExecutingAssembly().GetName().Version;
         About = string.Format(CultureInfo.InvariantCulture, @"Version {0}.{1}.{2} (r{3})", v.Major, v.Minor, v.Build, v.Revision);
      }

      // <summary>
      /// 
      /// </summary>
      /// <param name="obj"></param>
      private void fnImportGDTs(object obj = null)
      {
         
         //read source files and gdt and ensure 
         if (rootFolder == null || !Directory.Exists(rootFolder))
         {
            IMessage("Please drag your root folder to the top text box", "Root Folder?");
            return;
         }
         _previousCursor = Mouse.OverrideCursor;
         Mouse.OverrideCursor = Cursors.Wait;
         ResetProgress();
         ProcessImport();

      }
      Stopwatch stopWatchProgram;
      Stopwatch stopWatchWrite;
      /// <summary>
      /// Create bg and run
      /// </summary>
      private void ProcessImport()
      {
         Overlay = Visibility.Visible;
         stopWatchProgram = new Stopwatch();
         stopWatchWrite = new Stopwatch();
         stopWatchProgram.Start();

         stopWatchWrite.Start();


         allnames = new List<string>();
         cur = 0;
         bg = new BackgroundWorker();
         bg.DoWork += Bg_DoWork;
         bg.WorkerReportsProgress = true;
         bg.ProgressChanged += Bg_ProgressChanged;
         bg.RunWorkerCompleted += Bg_RunWorkerCompleted;
         bg.RunWorkerAsync();         
      }
      private void Bg_ProgressChanged(object sender, ProgressChangedEventArgs e)
      {
         curProgressNum = e.ProgressPercentage;
      }      

      private void Bg_DoWork(object sender, DoWorkEventArgs e)
      {
         curProgressText = "Reading root gdts";
         curProgressNum = 2;
         GetAllExistingGDTS();
         curProgressText = "Importing gdt";
         curProgressNum = 2;
         EstablishGoodNamesInNewGDT();
      }      

      private void Bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
      {
        // ListOfFiles = tempListOfFiles;
         curProgressNum = 100;
         curProgressText = "Import complete";

         TimeSpan ts = stopWatchProgram.Elapsed;
         string elapsedTime = String.Format("Program: {0}:{1:00}", ts.Seconds, ts.Milliseconds);
        // Console.WriteLine(elapsedTime);
         ts = stopWatchWrite.Elapsed;
         elapsedTime = String.Format("Write file: {0}:{1:00}", ts.Seconds, ts.Milliseconds);
         
         //Console.WriteLine(elapsedTime);
         Overlay = Visibility.Collapsed;
         Mouse.OverrideCursor = _previousCursor;
      }

      private void EstablishGoodNamesInNewGDT()
      {

         foreach (stFileNames s in ListOfFiles)
         {
            var goodlines = GetNamesAndTypes(s.Text);
            WriteGoodFile(goodlines, s, 100.0/ListOfFiles.Count);
         }
      }
      /// <summary>
      /// make sure we have a good place and desire to write the file
      /// </summary>
      /// <param name="goodlines"></param>
      /// <param name="s"></param>
      private void WriteGoodFile(IEnumerable<string> goodlines, stFileNames s, double div)
      {
         string dest;
         string filename;
         string fullpath;
         dest = Path.Combine(rootFolder, "source_data");
         //check is source_data folder exists and make it if it doesn't
         if (!Directory.Exists(dest))
         {
            Directory.CreateDirectory(dest);
         }

         filename = Path.GetFileName(s.Text);
         fullpath = Path.Combine(dest, filename);
         //check if file exists and ask to replace if it does
         if (File.Exists(fullpath))
         {
            if (MessageBox.Show("File exists, would you like to replace it?\n\n" + filename,
               "File Exists", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
               return;
            }
         }
         curProgressText = "Importing " + filename;
         WriteGoodLines(fullpath, s.Text, goodlines, div);
      }
      /// <summary>
      /// Writes the file
      /// </summary>
      /// <param name="fullpath"></param>
      /// <param name="text"></param>
      /// <param name="goodlines"></param>
      private void WriteGoodLines(string fullpath, string text, IEnumerable<string> goodlines, double div)
      {
         
         StringBuilder sb;
         bool writeline;
         double inc;
         List<string> mgoodlines;

         mgoodlines = goodlines.ToList();


         string[] filelines = File.ReadAllLines(text);
         //only write goodlines gdt in source_data folder named after file
         //or write goodlines and rename bad lines and replace all incase of derive
         
         sb = new StringBuilder();         
         writeline = true;
         //Console.WriteLine("start " + goodlines.Count());
         inc = div / filelines.Length;
         cur = 0;


         foreach (string f in filelines)
         {
            //Console.WriteLine(f);
            cur += inc;
            //Thread.Sleep(50);
            //bg.ReportProgress((int)cur);
            curProgressNum = (int)cur;
            if (writeline)
            {
               if (f.Contains(".gdf"))
               {
                  //Console.WriteLine(goodlines.Count());
                  
                  if (!GoodGDF(f, mgoodlines))
                  {
                     writeline = false;
                     continue;
                  }
                  else
                  {
                     //Console.WriteLine("before add allnames " + goodlines.Count());
                     allnames.Add(f);
                     //Console.WriteLine("after add allnames " + goodlines.Count());
                  }
               }
               sb.AppendLine(f);               
            }
            else
            {               
               if (f.Contains("}"))
               {
                  writeline = true;
               }
            }
         }

         //Would write all lines be faster?
         //File.WriteAllLines(fullpath, sb.ToString().Split('\n'));
         //or writealltext faster?
         File.WriteAllText(fullpath, sb.ToString());

      }

      private bool GoodGDF(string f, List<string> goodlines)
      {
         
         string gdftype;
         string gdfname;
         string[] gdfsplit;
         gdfsplit = f.Trim().Split('\"');
         gdftype = gdfsplit[3];
         gdfname = gdfsplit[1];


         //Is this the hold up?
         //Console.WriteLine("before linq");         
         var lines = goodlines.Where(x => x.Contains("\""+gdfname+ "\"") && x.Contains("\"" + gdftype+ "\""));
         return lines.Count()>0;
         
      }

      private void GetAllExistingGDTS()
      {
         string[] allfiles;
         double inc;

         allfiles = Directory.GetFiles(rootFolder, "*.gdt", SearchOption.AllDirectories);

         inc = 100.0 / allfiles.Length;
         foreach (string i in allfiles)
         {
            curProgressText = "Reading: " + Path.GetFileNameWithoutExtension(i);
            //Thread.Sleep(500);
            AddNameToList(i, inc);
         }

      }

      private void AddNameToList(string i, double inc)
      {
         cur += inc;
         bg.ReportProgress((int)cur);
         var nlines = GetNamesAndTypes(i);
         allnames.AddRange(nlines);
      }

      private IEnumerable<string> GetNamesAndTypes(string i)
      {
         if (!File.Exists(i)) return null;
         string[] lines = File.ReadAllLines(i);
      
         //Console.WriteLine("before linq");
         var nlines = lines.Where(x => x.Contains(".gdf") && !allnames.Contains(x));
         //Console.WriteLine("after linq " + i);
         return nlines;

      }
      /// <summary>
      /// Handle messages for information
      /// </summary>
      /// <param name="v">Message</param>
      /// <param name="x">Title</param>
      public void IMessage(string v, string x)
      {
         MessageBox.Show(v, x, MessageBoxButton.OK, MessageBoxImage.Information);
      }

      internal void fnDropFiles(DragEventArgs e)
      {
         if (tempListOfFiles == null)
         {
            tempListOfFiles = new ObservableCollection<stFileNames>();
         }
         string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
         foreach (string file in files)
         {
            
            if (File.Exists(file))
            {
               if (file.EndsWith(".gdt"))
               {
                  if (ListOfFiles != null)
                  {
                     var alreadyincluded = ListOfFiles.Where(x => x.Text == file);
                     if (alreadyincluded.Count() > 0)
                     {
                        continue;
                     }
                  }
                  else
                  {
                     ListOfFiles = new ObservableCollection<stFileNames>();
                  }
                  stFileNames st = new stFileNames();
                  st.Text = file;
                  ListOfFiles.Add(st);

               }
            }
         }
         if(ListOfFiles.Count>0)
         {
            SeeClear = Visibility.Visible;
         }
         else
         {
            SeeClear = Visibility.Collapsed;
         }
         if (rootFolder != null)
         {
            ImportGDT = Directory.Exists(rootFolder) && ListOfFiles.Count > 0;
            
            return;
         }
         ImportGDT = false;
      }


      // <summary>
      /// 
      /// </summary>
      /// <param name="obj"></param>
      private void fnClear(object obj = null)
      {
         ListOfFiles = new ObservableCollection<stFileNames>();
         SeeClear = Visibility.Collapsed;
         ResetProgress();
      }

      private void ResetProgress()
      {
         curProgressNum = 0;
         curProgressText = "";
      }
   }/* End Class */
}/* End NameSpace */

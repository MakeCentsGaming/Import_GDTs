using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Timers;
using System.Windows;

namespace Import_GDT
{
   /// <summary>
   /// 
   /// </summary>
   public partial class MainViewModel
   {      
      private Timer Clock { get; set; }            
      private static MainViewModel _instance;
      /// <summary>
      /// 
      /// </summary>
      public static MainViewModel Instance
      {
         get { return _instance; }
         set
         {
            _instance = value;
            if (_instance != null)
               _instance.OnPropertyChanged("Instance");
         }
      }
      /// <summary>
      /// 
      /// </summary>
      public Window MyParentWindow { get; set; }
      
      /// <summary>
      /// 
      /// </summary>
      public Action CloseAction { get; set; }

      /// <summary>
      /// 
      /// </summary>
      public Action SetWaitCursor { get; set; }

      /// <summary>
      /// 
      /// </summary>
      public Action SetNormalCursor { get; set; }

      private string _WelcomeMessage;
      /// <summary>
      /// 
      /// </summary>
      public string WelcomeMessage
      {
         get { return _WelcomeMessage; }
         set
         {
            _WelcomeMessage = value;
            OnPropertyChanged("WelcomeMessage");
         }
      }

      private string _CurrentDisplayDate;
      /// <summary>
      /// 
      /// </summary>
      public string CurrentDisplayDate
      {
         get { return _CurrentDisplayDate; }
         set
         {
            _CurrentDisplayDate = value;
            OnPropertyChanged("CurrentDisplayDate");
         }
      }
      

      private Visibility _OverlayVisibility;
      /// <summary>
      /// 
      /// </summary>
      public Visibility OverlayVisibility
      {
         get { return _OverlayVisibility; }
         set
         {
            _OverlayVisibility = value;
            OnPropertyChanged("OverlayVisibility");
         }
      }
      

      private string _OverlayMessage;
      /// <summary>
      /// 
      /// </summary>
      public string OverlayMessage
      {
         get { return _OverlayMessage; }
         set
         {
            _OverlayMessage = value;
            OnPropertyChanged("OverlayMessage");
         }
      }
      

      private bool _LayoutRootEnabled;
      /// <summary>
      /// 
      /// </summary>
      public bool LayoutRootEnabled
      {
         get { return _LayoutRootEnabled; }
         set
         {
            _LayoutRootEnabled = value;
            OnPropertyChanged("LayoutRootEnabled");
         }
      }


      private ObservableCollection<string> _DirList;
      /// <summary>
      /// 
      /// </summary>
      public ObservableCollection<string> DirList
      {
         get { return _DirList; }
         set
         {
            _DirList = value;
            OnPropertyChanged("DirList");
         }
      }

      private string _About;
      /// <summary>
      /// 
      /// </summary>
      public string About
      {
         get { return _About; }
         set
         {
            _About = value;
            OnPropertyChanged("About");
         }
      }

      private bool _SingleFile;
      /// <summary>
      /// 
      /// </summary>
      public bool SingleFile
      {
         get { return _SingleFile; }
         set
         {
            _SingleFile = value;
            OnPropertyChanged("SingleFile");
         }
      }

      private Visibility _Dropping;
      /// <summary>
      /// 
      /// </summary>
      public Visibility Dropping
      {
         get { return _Dropping; }
         set
         {
            _Dropping = value;
            OnPropertyChanged("Dropping");
         }
      }

      private Visibility _Overlay;
      /// <summary>
      /// 
      /// </summary>
      public Visibility Overlay
      {
         get { return _Overlay; }
         set
         {
            _Overlay = value;
            OnPropertyChanged("Overlay");
         }
      }

      private ObservableCollection<stFileNames> _ListOfFiles;
      /// <summary>
      /// 
      /// </summary>
      public ObservableCollection<stFileNames> ListOfFiles
      {
         get { return _ListOfFiles; }
         set
         {
            _ListOfFiles = value;
            OnPropertyChanged("ListOfFiles");
            
         }
      }

      private bool _ImportGDT;
      /// <summary>
      /// 
      /// </summary>
      public bool ImportGDT
      {
         get { return _ImportGDT; }
         set
         {
            _ImportGDT = value;
            OnPropertyChanged("ImportGDT");
         }
      }

      private int _curProgressNum;
      /// <summary>
      /// 
      /// </summary>
      public int curProgressNum
      {
         get { return _curProgressNum; }
         set
         {
            _curProgressNum = value;
            OnPropertyChanged("curProgressNum");
         }
      }

      private string _curProgressText;
      /// <summary>
      /// 
      /// </summary>
      public string curProgressText
      {
         get { return _curProgressText; }
         set
         {
            _curProgressText = value;
            OnPropertyChanged("curProgressText");
         }
      }


      private string _rootFolder;
      /// <summary>
      /// 
      /// </summary>
      public string rootFolder
      {
         get { return _rootFolder; }
         set
         {
            _rootFolder = value;
            OnPropertyChanged("rootFolder");
            Properties.Settings.Default.rootFolder = rootFolder;
            Properties.Settings.Default.Save();
            if (ListOfFiles != null)
            {
               if (ListOfFiles.Count > 0)
               {
                  SeeClear = Visibility.Visible;
               }
               else
               {
                  SeeClear = Visibility.Collapsed;
               }
               ImportGDT = Directory.Exists(rootFolder) && ListOfFiles.Count > 0;
               return;
            }
            ImportGDT = false;

         }
      }

      private Visibility _SeeClear;
      /// <summary>
      /// 
      /// </summary>
      public Visibility SeeClear
      {
         get { return _SeeClear; }
         set
         {
            _SeeClear = value;
            OnPropertyChanged("SeeClear");
         }
      }

   }/* End Class */
}/* End NameSpace */
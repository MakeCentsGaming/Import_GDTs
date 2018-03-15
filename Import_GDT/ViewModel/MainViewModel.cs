using System;
using System.Timers;
using System.Windows;

namespace Import_GDT
{
   /// <summary>
   /// 
   /// </summary>
   public partial class MainViewModel : clsBaseViewModel
   {
      /// <summary>
      /// 
      /// </summary>
      public MainViewModel()
      {
         LayoutRootEnabled = true;
         OverlayVisibility = Visibility.Collapsed;
         OverlayMessage = string.Empty;

         Clock = new Timer();
         Clock.Elapsed += Clock_Elapsed;
         Clock.Interval = 1000;
         Clock.Start();

         Instance = this;

         fnMakeAbout();
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
      void Clock_Elapsed(object sender, ElapsedEventArgs e)
      {
         CurrentDisplayDate = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss tt");
      }

      /// <summary>
      /// 
      /// </summary>
      public void Init()
      {
      }

      /// <summary>
      /// 
      /// </summary>
      public void DeInit()
      {
         Clock.Stop();
         Clock.Dispose();
         Clock = null;
      }
   }/* End Class */
}/* End NameSpace */

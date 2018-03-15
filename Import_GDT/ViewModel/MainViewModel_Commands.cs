using System.Windows.Input;

namespace Import_GDT
{
   /// <summary>
   /// 
   /// </summary>
   public partial class MainViewModel
   {
      private ICommand pClose;
      /// <summary>
      /// 
      /// </summary>
      public ICommand CmdClose
      {
         get { return pClose = new DelegateCommand(fnClose); }
      }

      private ICommand pImportGDTs;
      /// <summary>
      /// 
      /// </summary>
      public ICommand CmdImportGDTs
      {
         get { return pImportGDTs = new DelegateCommand(fnImportGDTs); }
      }

      private ICommand pClear;
      /// <summary>
      /// 
      /// </summary>
      public ICommand CmdClear
      {
         get { return pClear = new DelegateCommand(fnClear); }
      }
   }/* End Class */
}/* End NameSpace */

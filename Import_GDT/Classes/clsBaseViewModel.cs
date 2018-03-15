/*******************************************************************************
File        : clsBaseViewModel.cs
Description : 
--------------------------------------------------------------------------------
Functions:
--------------------------------------------------------------------------------
History:
2014/10/21 TPR Created
*******************************************************************************/
using System.ComponentModel;

namespace Import_GDT
{
   /*******************************************************************************
   Class        : clsBaseViewModel
   Description  : 
   --------------------------------------------------------------------------------
   History:
   2014/10/21 TPR Created
   *******************************************************************************/
   public class clsBaseViewModel: INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;
      /*******************************************************************************
      Function    : OnPropertyChanged()
      Description : 
      Parameters  : string propertyName - 
      Returns     : void
      --------------------------------------------------------------------------------
      History:
      2014/10/21 TPR Created
      *******************************************************************************/
      protected void OnPropertyChanged(string propertyName)
      {
         if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
      }
   }/* End Class */
}/* End NameSpace */

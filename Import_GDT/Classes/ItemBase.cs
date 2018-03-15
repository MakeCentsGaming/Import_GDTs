/*******************************************************************************
File        : ItemBase.cs
Description : 
--------------------------------------------------------------------------------
Functions:
--------------------------------------------------------------------------------
History:
2014/10/17 TPR Created
*******************************************************************************/
using System;
using System.ComponentModel;

namespace Import_GDT
{
   /*******************************************************************************
   Class        : ItemBase
   Description  : 
   --------------------------------------------------------------------------------
   History:
   2014/10/17 TPR Created
   *******************************************************************************/
   [Serializable]
   public class ItemBase: INotifyPropertyChanged
   {
      [field:NonSerialized]
      public event PropertyChangedEventHandler PropertyChanged;
      /*******************************************************************************
      Function    : OnPropertyChanged()
      Description : 
      Parameters  : string propertyName - 
      Returns     : void
      --------------------------------------------------------------------------------
      History:
      2014/10/17 TPR Created
      *******************************************************************************/
      protected void OnPropertyChanged(string propertyName)
      {
         if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
      }
   }/* End Class */
}/* End NameSpace */

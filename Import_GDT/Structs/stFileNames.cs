using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Import_GDT
{
    public class stFileNames: ItemBase
    {
      private string _Text;
      public string Text
      {
         get { return _Text; }
         set
         {
            _Text = value;
            OnPropertyChanged("TheText");
         }
      }
   }
}

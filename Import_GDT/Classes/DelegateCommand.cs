/*******************************************************************************
File        : DelegateCommand.cs
Description : 
--------------------------------------------------------------------------------
Functions:
--------------------------------------------------------------------------------
History:
2014/10/15 TPR Created
*******************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Import_GDT
{
   /*******************************************************************************
   Class        : DelegateCommand
   Description  : 
   --------------------------------------------------------------------------------
   History:
   2014/10/15 TPR Created
   *******************************************************************************/
   [Serializable]
   class DelegateCommand : ICommand
   {
      private readonly Predicate<object> _canExecute;
      private readonly Action<object> _execute;

      [field:NonSerialized]
      public event EventHandler CanExecuteChanged;

      /*******************************************************************************
      Constructor : DelegateCommand()
      Description : 
      Parameters  : Action<object> execute - 
      --------------------------------------------------------------------------------
      History:
      2014/10/15 TPR Created
      *******************************************************************************/
      public DelegateCommand(Action<object> execute): this(execute, null)
      {
      }
      /*******************************************************************************
      Constructor : DelegateCommand()
      Description : 
      Parameters  : Action<object> execute - 
                  : Predicate<object> canExecute - 
      --------------------------------------------------------------------------------
      History:
      2014/10/15 TPR Created
      *******************************************************************************/
      public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
      {
         _execute = execute;
         _canExecute = canExecute;
      }
      /*******************************************************************************
      Function    : CanExecute()
      Description : 
      Parameters  : object parameter - 
      Returns     : bool
      --------------------------------------------------------------------------------
      History:
      2014/10/15 TPR Created
      *******************************************************************************/
      public bool CanExecute(object parameter)
      {
         if (_canExecute == null)
         return true;

         return _canExecute(parameter);
      }
      /*******************************************************************************
      Function    : Execute()
      Description : 
      Parameters  : object parameter - 
      Returns     : void
      --------------------------------------------------------------------------------
      History:
      2014/10/15 TPR Created
      *******************************************************************************/
      public void Execute(object parameter)
      {
         _execute(parameter);
      }
      /*******************************************************************************
      Function    : RaiseCanExecuteChanged()
      Description : 
      Parameters  : None
      Returns     : void
      --------------------------------------------------------------------------------
      History:
      2014/10/15 TPR Created
      *******************************************************************************/
      public void RaiseCanExecuteChanged()
      {
         if (CanExecuteChanged != null)
            CanExecuteChanged(this, EventArgs.Empty);
      }        
   }/* End Class */
}/* End NameSpace */

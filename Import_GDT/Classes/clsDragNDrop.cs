using System.Windows;
using System.Windows.Controls;

namespace Import_GDT
{
   
    class clsDragNDrop
    {
      //private bool firstfile;//removed when gone to stat
      private TextBox tb;
      private ListBox lb;
      private string type;
      private MainViewModel MVM;

      /// <summary>
      /// <para></para>
      /// clsDragNDrop.TextBoxDragNDrop(textbox);
      /// </summary>
      /// <param name="tb">The textbox</param>
      /// <param name="firstfile">Grab the first file in the array of args</param>
      public static void TextBoxDragNDrop(TextBox tb,bool firstfile = true)
      {
         //this.firstfile = firstfile;
         //this.tb = tb;
         tb.AllowDrop = true;
         tb.Drop += textBox_Drop;
         tb.PreviewDragOver += textBox_DragOver;
      }

      public void TextBoxTypeDragNDrop(TextBox tb, string type = "", bool firstfile = true)
      {
         this.type = type;
         //this.firstfile = firstfile;
         //this.tb = tb;
         tb.AllowDrop = true;
         tb.Drop += textBoxType_Drop;
         tb.PreviewDragOver += textBox_DragOver;
      }
      /// <summary>
      /// clsDragNDrop dnd = new clsDragNDrop;
      /// dnd.TextBoxDragNDrop(textbox, this);
      /// </summary>
      /// <param name="tb"></param>
      /// <param name="mainWindow"></param>
      /// <param name="firstfile"></param>
      public void TextBoxDragNDrop(TextBox tb, MainWindow mainWindow, bool firstfile = true)
      {
         this.tb = tb;
         mainWindow.AllowDrop = true;
         mainWindow.Drop += Window_Drop;
         mainWindow.PreviewDragOver += textBox_DragOver;
      }

      public void TextBoxDragNDrop(TextBox tb, MainWindow mainWindow,  string type="", bool firstfile = true)
      {
         
         this.type = type;
         this.tb = tb;
         mainWindow.AllowDrop = true;
         mainWindow.Drop += WindowType_Drop;
         mainWindow.PreviewDragOver += textBox_DragOver;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="lb"></param>
      /// <param name="firstfile"></param>
      public static void ListDragNDrop(ListBox lb, bool firstfile = true)
      {
         //this.firstfile = firstfile;
         //this.tb = tb;
         lb.AllowDrop = true;
         lb.Drop += listBox_Drop;
         lb.PreviewDragOver += listBox_DragOver;
      }

      public void ListDragNDrop(ListBox lb, MainWindow mainWindow, MainViewModel MVM, string type, bool firstfile = true)
      {
         this.MVM = MVM;
         this.type = type;
         this.lb = lb;
         //this.firstfile = firstfile;
         //this.tb = tb;
         mainWindow.AllowDrop = true;
         mainWindow.Drop += listBoxType_Drop;
         mainWindow.PreviewDragOver += listBox_DragOver;
      }
      private static void listBox_DragOver(object sender, DragEventArgs e)
      {
         if (e.Data.GetDataPresent(DataFormats.FileDrop))
            e.Effects = DragDropEffects.Copy;
         else
            e.Effects = DragDropEffects.None;

         e.Handled = true;
      }
      private void listBoxType_Drop(object sender, DragEventArgs e)
      {
         string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
         if (files != null && files.Length != 0)
         {
            foreach (string f in files)
            {
               if(f.EndsWith(this.type))
               {
                  if (!MainViewModel.Instance.DirList.Contains(f))
                     MainViewModel.Instance.DirList.Add(f);
               }
            }

         }

         if (MVM.DirList.Count > 1)
         {
            MVM.SingleFile = false;
         }
         else
         {
            MVM.SingleFile = true;
         }

      }
      private static void listBox_Drop(object sender, DragEventArgs e)
      {
         string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
         if (files != null && files.Length != 0)
         {
            foreach (string f in files)
            {
               if (!MainViewModel.Instance.DirList.Contains(f))
                  MainViewModel.Instance.DirList.Add(f);
            }

         }
      }
      private static void textBox_DragOver(object sender, DragEventArgs e)
      {
         if (e.Data.GetDataPresent(DataFormats.FileDrop))
         {
            e.Effects = DragDropEffects.Copy;
            MainViewModel.Instance.Dropping = Visibility.Visible;
         }
         else
         {
            e.Effects = DragDropEffects.None;
            MainViewModel.Instance.Dropping = Visibility.Hidden;
         }

         e.Handled = true;
      }
      private void textBoxType_Drop(object sender, DragEventArgs e)
      {
         string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
         if (files != null && files.Length != 0)
         {
            TextBox tb = (TextBox)sender;
            if(files[0].EndsWith(this.type))
            {
               tb.Text = files[0];
            }
            
         }
      }
      private static void textBox_Drop(object sender, DragEventArgs e)
      {
         string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
         if (files != null && files.Length != 0)
         {
            TextBox tb = (TextBox)sender;
            tb.Text = files[0];
         }
      }
      private void WindowType_Drop(object sender, DragEventArgs e)
      {
         string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
         if (files != null && files.Length != 0)
         {
            if(files[0].EndsWith(this.type))
            {
               tb.Text = files[0];
            }

         }
      }
      private void Window_Drop(object sender, DragEventArgs e)
      {
         string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
         MainViewModel.Instance.Dropping = Visibility.Hidden;
         if (files != null && files.Length != 0)
         {
            tb.Text = files[0];

         }
      }
   }
}

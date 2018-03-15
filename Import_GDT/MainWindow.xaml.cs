using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Import_GDT
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      public MainViewModel MVM { get { return this.DataContext as MainViewModel; } }
		public MainWindow()
      {
         InitializeComponent();
			MVM.Overlay=Visibility.Collapsed;
			BindOverlay();
         MVM.SeeClear = Visibility.Collapsed;
         clsDragNDrop.TextBoxDragNDrop(textBox);
         MVM.rootFolder = Properties.Settings.Default.rootFolder;
      }
      

      private void BindOverlay()
		{
			Binding OverlayBinding = new Binding();
			OverlayBinding.Source = MVM;
			OverlayBinding.Path = new PropertyPath("Overlay");
			OverlayBinding.Mode = BindingMode.TwoWay;
			OverlayBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
			Overlay.SetBinding(VisibilityProperty, OverlayBinding);

      }

      private void listBox1_Drop(object sender, DragEventArgs e)
      {
         MVM.fnDropFiles(e);
      }

      private void listBox1_PreviewDragOver(object sender, DragEventArgs e)
      {
         if (e.Data.GetDataPresent(DataFormats.FileDrop))
            e.Effects = DragDropEffects.Copy;
         else
            e.Effects = DragDropEffects.None;

         e.Handled = true;
      }
   }
}

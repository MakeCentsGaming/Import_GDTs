   M S B A M L   `   `   `  ����E  AImport_GDT, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Import_GDT
Import_GDT  5   	       Import_GDT.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Import_GDT     5   	     � Title$  Import GDT(s)��5   	   $��233.115��6   $	��525��60    � Icon$ ImportGDTS MainWindow.png��6<   .��5
      � .+�5   
   �� -   5         ��Name$ About��5      $��0,-3.283,3,0q�6p   $	��Top=�6�   $��Right=�6�   $��101.967��6�   $��25.96��6�   ��6   �� *About+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5   
   -   5         ��Name$ listBox1��5       J� 	AllowDrop . $��10,71.871,10,41.456q�6      ListBoxFileName#���   5        ItemsPanelTemplate1#���  5      ��5   c   �� *ListOfFiles+5      �� 5   
   -   5         ��Name$ textBox��5   a   $��23��6r   $��10,25.296,10,0q�6~    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   ��6   �� *
rootFolder+$ TwoWay=�$ PropertyChanged=�6�   � 5   
   $��10,0,98.667,10q�67   $��24.449��6O   $��Bottom=�6_   	 � Value	 6   �� *curProgressNum+6_   �� 5   
   -   5        
 ��Name$
 button��5   W   $��Import GDT(s)��6g   $��0,0,10.667,10q�6     SimpleButton#���  6�   $��Right=�6�   $��83��6�   $��24.449��6�   $��Bottom=�6�   ��6   �� *CmdImportGDTs+{�65   �� *	ImportGDT+6�   �� 5   
   -   5        $ label��5      $
��Left=�6v   $��41.333,0,0,10.496q�6�   $��25.96��6�   $��Bottom=�6�   ��6   �� *curProgressText+$ TwoWay=�$ PropertyChanged=�6�   �� 5   
   -   5        $ label1��5      $
��GDTS��6    $
��Left=�6/   $��10,48.961,0,0q�6J   $	��Top=�6a   �� 5   
   -   5        $ label1_Copy��5      $&�� Highest folder to analyze (root)��6%   $
��Left=�6P   $��10,0.725,0,0q�6k   $	��Top=�6�   �� 5   
   -   5        $
 button_Copy��5   N   $��Clear��6c   $��0,50.221,10.667,0q�6s   #���  6�   $��Right=�6�   $��83��6�   $��20.316��6�   $	��Top=�6�   y�6   �� *
SeeClear+��61   �� *
CmdClear+6�   �� 5    
   -	   5         ��Name$ Overlay��5       $y�	Collapsed=�6"   
���  ��$��3q�6K   ���	#FFB0AAAA$��2��6v    J� Opacity$	 0.2Q�6�   }�.  J� SnapsToDevicePixels . $��2��6�   5!       � DataContext 5"         Import_GDT.MainViewModel  5#   
   5%      
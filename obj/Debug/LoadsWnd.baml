   M S B A M L   `   `   `  ����L  HEtkilesimDiyagram, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:EtkilesimDiyagramEtkilesimDiyagram  5   	        EtkilesimDiyagram.LoadsWnd   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v16.2, Version=16.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aF 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:EtkilesimDiyagram     5   	     � Title$  Yükler��5   	   $	��420��6   $	��200��6%    � SizeToContent$ WidthAndHeight=�61    � 
ResizeMode$ NoResize=�6P    � WindowStartupLocation$ CenterOwner=�6f    � DataContext 5	      '   !EtkilesimDiyagram.AppFileTemplate  5
   
   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   $��30��6-    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��0��6'    �� Orientation$ 
Horizontal=�66   $��Right=�6Q    � 	Resources 5      	 �� 
TargetType
 �� Property �� Value��%[   '��       �� 5      "	 ��6   .�5      �� 
 � ��$ 10,10,10,10q�6/   5      5      .+�5      �� -   5         ��Name$ bPasteLoadsFromClipboard��5      $��Panodan Yapıştır��68   6S   5      �� 5   
   $��1��6   $��	0,0,-11,0q�6"   .+�5      &   System.Windows.Controls.DataGrid  -   5          Name$ 
reBarsGrid��5         AutoGenerateColumns .  $	��300��6�   $��	10,0,23,0q�6�      CanUserReorderColumns .     CanUserResizeColumns .     MinColumnWidth$ 50Q�6�   ��6G   �� *Loads+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�   Columns 5       0  *System.Windows.Controls.DataGridTextColumn  5!      ,  &System.Windows.Controls.DataGridColumn   Header$ M (tm)��6)   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *M+  5"      $ N (t)��6)    68   �� *N+5$      5%      �� 5&   
   $��0��6   $��2��6'   $ 
Horizontal=�66   $	��192��6Q   $��Right=�6]   $��0,0,-9,0q�6y    5'      ��%[   '��       �� 5(      "	 ��6   .�5)      �� 
 � ��$ 10,10,10,10q�6/   5*      5+      .+�5,      �� -   5        $ bTamam��5,       �� 	IsDefault . $��Tamam��66   $��70��6F   6k   �� 5-      -   5        $ bCikis��5-       �� IsCancel . $��İptal��65   $��70��6E   6j   5.      5/      50      
   M S B A M L   `   `   `  ����L  HEtkilesimDiyagram, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:EtkilesimDiyagramEtkilesimDiyagram  5   	   "    EtkilesimDiyagram.MalzemeWnd   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v16.2, Version=16.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aF 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TXceed.Wpf.Toolkit, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   +localclr-namespace:EtkilesimDiyagram  5   	     � Title$  Malzeme��5	   	   $	��150��6   $	��250��6&    � 
ResizeMode$ NoResize=�62    � WindowStartupLocation$ CenterOwner=�6H    � SizeToContent$ WidthAndHeight=�6l    � DataContext 5
      '   !EtkilesimDiyagram.AppFileTemplate  5   
   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   $��30��6,   �� 5      $
��Auto �6   $��30��6-   �� 5      $
��Auto �6   $��30��6-    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��0��6'    �� Orientation$ 
Horizontal=�66   $��Stretch=�6Q   $��0,0,5,0q�6o   .+�5      �� $��Beton dayanım, fc:��6   $��Center=�63   $	��120��6P   $��5,0,0,0q�6^   $  Xceed.Wpf.Toolkit.DoubleUpDown  5      -   5        $��txtFc��5   !   $��Center=�6R   $��50��6o   �  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   FormatString$ F0��6|   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]	   Minimum$	 0X�6�   
   ShowButtonSpinner
 .     Value 60   �� *fc+ �� Mode$ TwoWay=�6�   �� 5      $	��MPa��6   $��Center=�6$   5      �� 5   
   $��0��6   $��1��6'   $ 
Horizontal=�66   $
��Auto��6Q   $��Stretch=�6^   .+�5      �� $��Çelik dayanım, fy:��6   $��Center=�64   $	��120��6Q   $��5,0,0,0q�6_     5      -   5        $��txtFy��5   !   $��Center=�6R   $��50��6o   $ F0��6|   $	 0X�6�   
 .   60   �� *fy+$ TwoWay=�6�   �� 5       $	��MPa��6   $��Center=�6$   5!      �� 5"   
   $��0��6   $��2��6'   $ 
Horizontal=�66   $
��Auto��6Q   $��Center=�6^    � 	Resources 5#       �� 
TargetType �� Property �� Value��%[   '��       �� 5$      " ��6   .�5%      ��  � ��$ 10,10,10,10q�6/   5&      5'      .+�5(      �� -   5         ��Name$ bTamam��5(       �� 	IsDefault . $��Tamam��66   $��70��6F   6k   �� 5)      -   5        $ bCikis��5)       �� IsCancel . $��İptal��65   $��70��6E   6j   5*      5+      5,      
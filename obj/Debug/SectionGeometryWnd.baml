   M S B A M L   `   `   `  ����L  HEtkilesimDiyagram, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:EtkilesimDiyagramEtkilesimDiyagram  5   	   *    $EtkilesimDiyagram.SectionGeometryWnd   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v16.2, Version=16.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aF 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TXceed.Wpf.Toolkit, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   +localclr-namespace:EtkilesimDiyagram  5   	     � Title$  Kesit��5	   	   $	��140��6   $	��250��6$    � 
ResizeMode$ NoResize=�60    � WindowStartupLocation$ CenterOwner=�6F    � SizeToContent$ WidthAndHeight=�6j    � DataContext 5
      '   !EtkilesimDiyagram.AppFileTemplate  5   
   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   $��30��6,   �� 5      $
��Auto �6   $��30��6-   �� 5      $
��Auto �6   $��30��6-    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��0��6'    �� Orientation$ 
Horizontal=�66   $
��Auto��6Q   $��Stretch=�6^   .+�5      �� $��Genişlik (b):��6   $��Right=�6.   $��Center=�6J   $	��100��6g   $��5,0,0,0q�6u   $  Xceed.Wpf.Toolkit.DoubleUpDown  5      -   5        $
��txtB��5   !   $��Center=�6P   $��70��6m   �  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   FormatString$ F0��6z   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]	   Minimum$	 0X�6�   
   ShowButtonSpinner
 .     Value 6/   �� *b+ �� Mode$ TwoWay=�6�   �� 5      $��cm��6   $��Right=�6#   $��Center=�6?   $��5,0,0,0q�6\   5      �� 5   
   $��0��6   $��1��6'   $ 
Horizontal=�66   $
��Auto��6Q   $��Stretch=�6^   .+�5      �� $��Yükseklik (h):��6   $��Right=�60   $��Center=�6L   $	��100��6i   $��5,0,0,0q�6w     5      -   5        $
��txtH��5   !   $��Center=�6P   $��70��6m   $ F0��6z   $	 0X�6�   
 .   6/   �� *h+$ TwoWay=�6�   �� 5       $��cm��6   $��Right=�6#   $��Center=�6?   $��5,0,0,0q�6\   5!      �� 5"   
   $��0��6   $��2��6'   $ 
Horizontal=�66   $
��Auto��6Q   $��Center=�6^    � 	Resources 5#       �� 
TargetType �� Property �� Value��%[   '��       �� 5$      " ��6   .�5%      ��  � ��$ 10,10,10,10q�6/   5&      5'      .+�5(      �� -   5         ��Name$ bTamam��5(       �� 	IsDefault . $��Tamam��66   $��70��6F   6k   �� 5)      -   5        $ bCikis��5)       �� IsCancel . $��İptal��65   $��70��6E   6j   5*      5+      5,      
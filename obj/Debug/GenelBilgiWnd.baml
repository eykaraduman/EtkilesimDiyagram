   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5   	   L HEtkilesimDiyagram, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:EtkilesimDiyagramEtkilesimDiyagram 5   	   C ?RcDesign, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5(clr-namespace:RcDesign;assembly=RcDesignRcDesign 5   	   %   EtkilesimDiyagram.GenelBilgiWnd   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v16.2, Version=16.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aF 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	   1localclr-namespace:EtkilesimDiyagram     5   	   7rc(clr-namespace:RcDesign;assembly=RcDesign     5   	     � Title$  Genel Bilgi��5
   	   $	��220��6   $	��450��6*    � 
ResizeMode$ NoResize=�66    � SizeToContent$ WidthAndHeight=�6L    � WindowStartupLocation$ CenterOwner=�6k    � DataContext 5      '  !EtkilesimDiyagram.AppFileTemplate  5   
    � 	Resources 5         designCodeFromEnum S� 
MethodName S� 
ObjectType   System.Enum S� MethodParameters	 M� TypeName��%u   &	        S� 5   
   $ 	GetValues��68   "  6O    5      M� 5      $	 rc:DesignCodes��6   5      5      .��5      � 
 � RowDefinitions
 5   
   �� 5      $
��Auto �6   $��30��6,   �� 5      $
��Auto �6   $��30��6-   �� 5      $
��Auto �6   $��30��6-   �� 5      $
��Auto �6   $��30��6-   �� 5      $
��Auto �6   $��30��6-    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   .+�5!   
   �� $��0��6   $��0��6'    �� Orientation$ 
Horizontal=�66   $
��Auto��6Q   $��Stretch=�6^   .+�5"      �� $��Tasarım Kodu:��6   $��Right=�6.   $��Center=�6J   $	��120��6g   $��5,0,0,0q�6u   �� 5#      $��Center=�6   $��Center=�64   $	��150��6O   ��6^   ��  �� Source# [   �� SelectedItem 6�   �� *Code+ �� Mode$ TwoWay=�5$      �� 5&   
   $��0��6   $��1��6'   $ 
Horizontal=�66   $
��Auto��6Q   $��Stretch=�6^   .+�5'      �� $��Rapor Başlık:��6   $��Right=�6.   $��Center=�6J   $	��120��6g   $��5,0,0,0q�6u   �� 5(      -   5        $��txtRaporBaslik��5(      $��Center=�6Y   $	��300��6v   $��0,0,5,0q�6�   ��6/   �� *ReportTitle+$ TwoWay=�6�   5)      �� 5+   
   $��0��6   $��2��6'   $ 
Horizontal=�66   $
��Auto��6Q   $��Stretch=�6^   .+�5,      �� $��Diagram Başlık:��6   $��Right=�60   $��Center=�6L   $	��120��6i   $��5,0,0,0q�6w   �� 5-      -   5        $��txtDiagramBaslik��5-      $��Center=�6U   $	��300��6r   $��0,0,5,0q�6�   ��61   �� *Title+$ TwoWay=�6�   5.      �� 50   
   $��0��6   $��3��6'   $ 
Horizontal=�66   $
��Auto��6Q   $��Stretch=�6^   .+�51      �� $��Diagram Altbaşlık:��6   $��Right=�64   $��Center=�6P   $	��120��6m   $��5,0,0,0q�6{   �� 52      -   5        $��txtDiagramAltBaslik��52      $��Center=�6[   $	��300��6x   $��0,0,5,0q�6�   ��64   �� *
SubTitle+$ TwoWay=�6�   53      �� 54   
   $��0��6   $��4��6'   $ 
Horizontal=�66   $
��Auto��6Q   $��Center=�6^    55       �� 
TargetType �� Property �� Value��%[   '��       �� 56      " ��6   .�57      ��  � ��$ 10,10,10,10q�6/   58      59      .+�5:      �� -   5         ��Name$ bTamam��5:       �� 	IsDefault . $��Tamam��66   $��70��6F   6k   �� 5;      -   5        $ bCikis��5;       �� IsCancel . $��İptal��65   $��70��6E   6j   5<      5=      5>      
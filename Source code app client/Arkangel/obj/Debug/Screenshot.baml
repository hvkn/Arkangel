   M S B A M L   `   `   `  ����C  ?Arkangel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:ArkangelArkangel  5          Arkangel.Screenshot   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:Arkangel     5      V RMaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5	      E ADragablz, Version=0.0.3.203, Culture=neutral, PublicKeyToken=null6dragablz'http://dragablz.net/winfx/xaml/dragablz  5
         MaterialDesignBody#���   5      $��Regular"�5      $��13(�5      &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5         TextRenderingMode$
 Auto=�5        MaterialDesignPaper#���  5        MaterialDesignFont#���  5      .��5      � .+�5   
   �� $��0q�6   ���#F2F2F2.+�5      � $��53��6   .+�5      ��  �� Orientation$ 
Horizontal=�6   $	��Top=�66   $��0q�6N   ���#0489B1.+�5      �� 
 J� Uid$ 0��6   $	��150��6%   $��
Screenshot��61   $��53��6F   
�������$��20(�6�   ��6R   Z� ��6h   Z� 6�   5      � 5      -   5         �Name$ 
GridCursor��5      $	��150��6+   $��5��67   ���#DBA901$
��Left=�6W   $��0q�6r   $��Bottom=�6}   5      � 5      -   5        $ GridMain��5      $	��372��6%   $
��10,0q�62   .+�5      �� .+�5      �� $
��auto��6   .  (MaterialDesignThemes.Wpf.ColorZoneAssist   Mode$ Light=�6,   $��Enable/Disable��6X   $��4q�6p   $��15(�6|   $��
0,10,10,10q�6�   .��5      �� $ 
Horizontal=�6%   .+�5      �� -   5         ��Name$ 	cb_enable��5   '   $��50��6:   $��3��6F   $��25(�6Z   $"��Auto make a screenshot every��6h   6�   �� 5      $��5��6)   �� 5       -   5         ��Name$ tb_hours��5    &   $��50��68   $��35��6C   $	��Top=�6O   $��25(�6g   $��RightToLeft=�6u   6�   �� 5!      $��5��6)   �� 5"      $��hours��6(   $��Center=�65   $��25(�6P   �� 5#      $��5��6)   �� 5$      -   5        $ 
tb_minutes��5$   &   $��50��6:   $��35��6E   $��25(�6Q   $	��Top=�6_   $��RightToLeft=�6w   6�   �� 5%      $��5��6)   �� 5&      $��minutes��6(   $��Center=�67   $��25(�6R   5'      5(      �� 5)      $��Settings��6   $ Light=�61   $��15(�6]   $��4q�6k   $��0,0,10,0q�6w   .��5*      �� .+�5+      �� -   5        $ cb_timeNuser��5+   '   $��40��6=   $*��$Timestaps and username on screenshot��6I   $��25(�6x   6�   �� 5,      -   5        $ cb_doubleScr��5,   '   $��40��6=   $(��"Enable douple screen on screenshot��6I   $��25(�6v   6�   �� 5-      $ 
Horizontal=�6)   $��40��6B   .+�5.   "   �� -   5        $ cb_enDel��5.   +   $��Delete after��6=   $��25(�6T   6b   �� 5/   "   $��5��6-   �� 50   "   -	   5        $ 
cb_daysDel��50   *   $��50��6>   $��32��6I   $��20(�6U   $	��Top=�6c   $��RightToLeft=�6{   6�   �� 51   "   $��5��6-   �� 52   "   $
��days��6,   $��25(�68   $��Center=�6F   53      �� 54      $��Picture Quality��6(   $��25(�6?   $��Center=�6M   6l   �� 55      $ 
Horizontal=�6)   $��40��6B   $	��500��6N   .+�56   "   �� $	��Low��6,   $��20(�67   $��Center=�6E   6b   �� 57   "   $��10��6-   6:   �� 58   "   -
   5         ��Name$ 
sld_quaity��58   )   $	��400��6=   $��Center=�6I   	 � Value$	 50Q�6d   
 � Maximum$	
 100Q�6o   6�   �� 59   "   $��10��6-   6:   �� 5:   "   $
��High��6,   $��20(�68   $��Center=�6F   6c   5;      5<      5>      5?      5@      5B      �� 5C   
   -   5         ��Name$ bt_OK��5C      $��574,388,35,10q�6    $	��150��67   $��OK��6C   6P   5D      5E      
   M S B A M L   `   `   `  ���� C  ?Arkangel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:ArkangelArkangel      Arkangel.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     (localclr-namespace:Arkangel     V RMaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  -     � Title$  Arkangel��$	��525��$	��900��   MaterialDesignBody#���   $��Regular"�$��20(�&   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�   TextRenderingMode$
 Auto=� � WindowStartupLocation$ CenterScreen=����#0489B1  MaterialDesignFont#���   � Icon$! .\Sources\logo_LSC_icon.ico�� � 
ResizeMode$ NoResize=� � WindowStyle$
 None=�
�������$��2q� � 	Resources   OpenMenu �� TargetProperty	 �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame
 O� KeyTime O� Value p� KeyTime p� Value  	CloseMenu��%�  &	       &	 �     �� .��U� $ (FrameworkElement.Width)�$	 GridMenu��.\�  $
 0��$ 50Q�  $
 0:0:0.5��$	 150Q�r� $ Margin�$	 	mainPanel��.	��� $ 00:00:00��$ 68,64,78,27q��� $ 	00:00:0.5��$ 150,64,9,27q��� .��U� $ (FrameworkElement.Width)�$	 GridMenu��.\�  $
 0��$	 150Q�  $
 0:0:0.5��$ 50Q�r� $ Margin�$	 	mainPanel��.	��� $ 00:00:00��$ 150,64,9,27q��� $ 	00:00:0.5��$ 68,64,68,27q� � Triggers :�  :� RoutedEvent$ ButtonBase.Click�� :� 
SourceName$ OpenMenu��.[��� #��[ :� $ ButtonBase.Click��$ 	CloseMenu��.[��� #��[ .��� ���#F2F2F2.+�� $	��168��$	��Top=�$��	150,0,0,0q�.+��� -   $��50��$	��Top=����#0489B1$��
-100,0,0,0q�.+���  �� Orientation$ 
Horizontal=�$��Right=�$	��200��$��50��.+��� -    ��Name$ bt_home��$��50��$��50��$
��Left=�
�������$��4q���Z� ��Z� .��'  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=null]]   Kind$
 Home=�$��40��$��40���� -   $ bt_sync��$��50��$��50��$
��Left=�
�������$��4q���Z� ��Z� .��  $
 Sync=�$��40��$��40��'  !MaterialDesignThemes.Wpf.PopupBox  -     Name$ 	btn_Login��$��50��$��50��   PlacementMode$ BottomAndAlignRightEdges=�   	StaysOpen .  $��Center=�
�������$��20(���Z� ��Z�    PopupContent. �� .+��� .���� $ 
Horizontal=�$��Center=�.+�  $ AccountMultiple=�$��25��$��25���� $��10���� -    ��Name$ 	_username��$��Right=�$��Center=��� -   $ 
bt_Setting��.���� $ 
Horizontal=�$��Center=�.+�  $ Settings=�$��25��$��25���� $��10���� $��Setting��$��Right=�$��Center=��� -   $ 	bt_logout��$��Center=�.���� $ 
Horizontal=�$��Center=�.+�  $ Logout=�$��25��$��25���� $��10���� $��Log out��$��Right=�$��Center=��� -	   $ bt_Quit��$��50��$��50��$��Right=�
�������$��4q���Z� ��Z� .��  $ Power=�$��40��$��40���� $��69�����#0489B1$��-150,50,0,49q��� -
    ��Name$ GridMenu��$��50��$
��Left=�.+��� $��50��$	��Top=����#0489B1.+�� $��50��.+��� -   $ 	CloseMenu��$��Right=�$��50��$��50��
�������$��1q�$y�	Collapsed=���Z� ��Z� .��  $ 	ArrowLeft=�$��45��$��45���� -   $ OpenMenu��$��Right=�$��50��$��50��
�������$��1q���Z� ��Z� .��  $
 Menu=�$��45��$��45���� $��0q�.+��� -   $ StackPanelMenu��$	��150��$
��Left=�$��	0 -10 0 0q� J� RenderTransformOrigin$ 0.5,0.5&�$y�Hidden=�z�V� .~��� �� �� T� .+��� $ 
Horizontal=�$	��Top=�$	��100��$��Center=��� -    ��Name$ allIkon��$��Disabled=�$��Disabled=�$	��300��.@��� -    ��Name$ listViewItem��$��60��$ 0.5,0.5&�$��0q�z�V� .~��� �� �� T� .���� $ 
Horizontal=�$	��0 0q�$
��auto��$
��auto��.+�  $ Asterisk=�$��30��$��30��$��Center=�$��5q��� $��General��$��10q�$��Center=�$��15(��� -   $ listViewItem1��$��60��$ 0.5,0.5&�z�V� .~��� �� �� T� .���� $ 
Horizontal=�$	��0 0q�.+�  $ ContentCopy=�$��30��$��30��$��Center=�$��0q��� $��	Clipboard��$��10q�$��Center=�$��15(��� -   $ listViewItem2��$��60��$ 0.5,0.5&�z�V� .~��� �� �� T� .���� $ 
Horizontal=�$	��0 0q�.+�  $ Upload=�$��30��$��30��$��Center=�$��0q��� $	��FTP��$��10q�$��Center=�$��15(��� -   $ listViewItem3��$��60��$ 0.5,0.5&�z�V� .~��� �� �� T� .���� $ 
Horizontal=�$��0q�.+�  $ Webcam=�$��30��$��30��$��Center=�$��0q��� $��Webcam��$��10q�$��Center=�$��15(��� -   $ listViewItem4��$��60��$ 0.5,0.5&�z�V� .~��� �� �� T� .���� $ 
Horizontal=�$��0q�.+�  $ Target=�$��30��$��30��$��Center=�$��0q��� $��Target��$��10q�$��Center=�$��15(��� -   $��60��$ 0.5,0.5&�z�V� .~��� �� �� T� .���� $ 
Horizontal=�$��0q�.+�  $ 
AccountBox=�$��30��$��30��$��Center=�$��0q��� $
��User��$��10q�$��Center=�$��15(��� -   $��60��$ 0.5,0.5&�z�V� .~��� �� �� T� .���� $ 
Horizontal=�$��0q�.+�  $ GoogleChrome=�$��30��$��30��$��Center=�$��0q��� $��Website Usage��$��5q�$��Center=�$��15(��� -   $��60��$ 0.5,0.5&�z�V� .~��� �� �� T� .���� $ 
Horizontal=�$��0q�.+�  $ Camera=�$��30��$��30��$��Center=�$��0q��� $��
Screenshot��$��5q�$��Center=�$��15(��� -   $��60��$ 0.5,0.5&�z�V� .~��� �� �� T� .���� $ 
Horizontal=�$��0q�.+�  $ Gmail=�$��30��$��30��$��Center=�$��0q��� $
��Mail��$��5q�$��Center=�$��15(��� -   $��60��$ 0.5,0.5&�z�V� .~��� �� �� T� .���� $ 
Horizontal=�$��0q�.+�  $ Alert=�$��30��$��30��$��Center=�$��0q��� $��Alert��$��5q�$��Center=�$��15(�� -    �Name$ 	mainPanel��$��Center=�$��Center=�$��68,64,68,27q�$	��430��$	��760�����#F2F2F2
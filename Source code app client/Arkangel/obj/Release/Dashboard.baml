   M S B A M L   `   `   `  ���� C  ?Arkangel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:ArkangelArkangel      Arkangel.Dashboard   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     (localclr-namespace:Arkangel     V RMaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes     MaterialDesignBody#���   $��Regular"�$��13(�&   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�   TextRenderingMode$
 Auto=�  MaterialDesignPaper#���    MaterialDesignFont#���  .��� -    �Name$ 	_dasboard�����#F2F2F2.+�� $��0,0,0,0q����#0489B1$
��54.5��$	��Top=�.+��� -    ��Name$ tb_hello��$��Good Morning��$��40(�$	��Top=�$��Center=�
�������$��60��� $	��360��$
�� 700��$��Bottom=� � ColumnDefinitions �� $��* ��� $��* ��� $��* � � RowDefinitions �� $��* ��� $��* ��� $��* �.+��� -    ��Name$ btn_General��$
��auto��$��0,0,5,5q�$��0q����#3f51b5��Z� .����  �� Orientation$ 
Horizontal=�$	��180��.+�'  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=null]]   Kind$ Asterisk=�$��55��$��55���� $��10���� $��General��$��33(�$��Center=�$��Font Awesome)��� -   $ btn_Clipboard��$��1��$
��Auto��$��30(�$��Font Awesome)����#4caf50$��0,0,0,5q�$��8q���Z� .���� $ 
Horizontal=�$	��230��.+�  $ ContentCopy=�$��55��$��55���� $��10���� $��	Clipboard��$��33(�$��Center=�$��Font Awesome)��� -   $��2��$
��Auto��$��30(����#d84315$��Font Awesome)�$��5,0,0,5q���Z� .���� $ 
Horizontal=�$	��130��.+�  $ UploadMultiple=�$��55��$��55���� $��10���� $	��FTP��$��33(�$��Center=�$��Font Awesome)��� -   $ 	bt_Webcam��$��0,0,5,4q�$��1��$
��Auto��$��20(�$��Font Awesome)����#8bc34a$��4q���Z� .���� $ 
Horizontal=�$	��196��$��92��$��Center=�.+�  $ Webcam=�$��55��$��55��$��Center=�$��Center=��� $��10���� $��Webcam��$��33(�$��Center=�$��Font Awesome)�$��Center=��� -   $ 	bt_Target��$��1��$
��Auto�����#e53935$��30(�$��Font Awesome)�$��0,0,0,4q�$��1����Z� .���� $ 
Horizontal=�$	��180��.+�  $ Target=�$��55��$��55���� $��10���� $��Target��$��33(�$��Center=�$��Font Awesome)��� -   $ bt_User��$��2��$
��Auto�����#7b1fa2$��30(�$��Font Awesome)�$��5,0,0,4q�$��1����Z� .���� $ 
Horizontal=�$	��140��.+�  $ 
AccountBox=�$��55��$��55���� $��10���� $
��User��$��33(�$��Center=�$��Font Awesome)��� -	   $ bt_websiteusage�����#ff5722$
��Auto��$��30(�$��Font Awesome)�$��0,0,5,4q�$��2����Z� .���� $ 
Horizontal=�$	��210��.+�  $ GoogleChrome=�$��55��$��55��$��Center=��� $��10���� $ Vertical=�.+��� $��Website��$��33(�$��Font Awesome)�$��Center=��� $��Usage��$��33(�$��Font Awesome)�$��Center=��� -
   $ bt_screenshot�����#ffb300$
��Auto��$��30(�$��Font Awesome)�$��0,0,0,4q�$��2��	 J� RenderTransformOrigin$	 1.636,0.621&�$��1��$��4,4,2,4q���Z� .���� $ 
Horizontal=�$	��230��.+�  $ Camera=�$��55��$��55���� $��10���� $��
Screenshot��$��30(�$��Center=�$��Font Awesome)��� -   $ bt_Email��$��	5,0,115,4q�$��2��$
��Auto��$��20(�$��Font Awesome)�$��2��$��1q���Z� .���� $ Vertical=�$	��110��.+�  $ Email=�$��55��$��55��$��Center=��� $
��Mail��$��26(�$��Center=�$��Font Awesome)��� -   $��	123,0,0,4q����#303f9f$��2��$
��Auto��$��20(�$��Font Awesome)�$��Center=�
   TextHintingMode$
 Fixed=�$	��110��$��1q�$��2����Z� .���� $ Vertical=�$	��110��.+�  $ AlertBox=�$��55��$��55��$��Center=��� $��Alert��$��26(�$��Center=�$��Font Awesome)�
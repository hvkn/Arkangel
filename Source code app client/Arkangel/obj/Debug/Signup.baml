   M S B A M L   `   `   `  ����  Arkangel    Arkangel.Signup   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   V RMaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  Login��5	   	   $	��500��6   $	��350��6$    � 
ResizeMode$ NoResize=�5
   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	   $��Segoe UI Emoji)�5   	    � Icon$! .\Sources\logo_LSC_icon.ico��5   	   .��5      � .+�5   
   �� -   5        $	��280��5      $	��Top=�6!   ��5      �� 5       �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0.5,0&�67   .��5      �  � Color$ 	#FF2281D1��6#   � 5      $ 	#FF34268A��6#    � Offset$ 1Q�65   � 5      $ 	#FF33288B��6#   $ 0.546Q�65   5      5      �� 5   
   $	��280��6   $	��240��6    $��Bottom=�6-   $
��0,80q�6H   	 �� RadiusY$	 10��6V   
 �� RadiusX$
 10��6c   
������� J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 15Q�6#      	Direction$ 0Q�63      RenderingBias$ Quality=�6A      ShadowDepth$ 1Q�6Y      Color$ 	#FFBBBBBB��6i   5      � 5   
   $��Bottom=�6   $��35,80q�6*   $	��240��69   .+�5       �� $��30��6   $��30��6!   $��235,45,15,165q�6,   $��0q�6C   ��6O   Z� ��6e   Z� .��5!      � .+�5"      '  !MaterialDesignThemes.Wpf.PackIcon  -   5          Name$ 	true_mail��5"   .   $y�Hidden=�6A   $��30��6U   $��30��6a   �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=null]]   Kind$ Check=�6l   
��� � �6�     5#      -   5        $ 
false_mail��5#   .   $y�Hidden=�6B   $��30��6V   $��30��6b   $ Close=�6m   
��� � �6�   5$      5%      �� 5&      $��30��6   $��30��6!   $��235,169,15,41q�6,   $��0q�6C   ��6O   Z� ��6e   Z� .��5'      � .+�5(        -   5        $ true_password��5(   .   $y�Hidden=�6E   $��30��6Y   $��30��6e   $ Check=�6p   
��� � �6�     5)      -   5        $ false_password��5)   .   $y�Hidden=�6F   $��30��6Z   $��30��6f   $ Close=�6q   
��� � �6�   5*      5+      �� 5,      $��SIGN UP��6   $��Center=�6&   $	��Top=�6C   $��5q�6[   
�������$��18(�6x   �� 5-      $��Center=�6   $��15q�64   .+�5.      �� -   5         ��Name$ tb_username��5.      $
��0,10q�6/   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ EMAIL��6=    %  !MaterialDesignFloatingHintTextBox#��[  6d   $��Champagne & Limousines)�6�   $��18(�6�   6�   G� 5/      -   5         G�Name$ tb_password��5/      $
��0,10q�63   $ PASSWORD��6A    ) %MaterialDesignFloatingHintPasswordBox#��[ 6k   $��Champagne & Limousines)�6�   $��18(�6�   G� 50      -   5        $ tb_repassword��50      $
��0,10q�65   $ RETYPE PASSWORD��6C   #��[ 6t   $��Champagne & Limousines)�6�   $��18(�6�   6�   52      53      �� 54   
   -	   5         ��Name$ 
btn_Signup��54      $	��150��6%   $��Center=�61   $��Bottom=�6N   $��100,0,100,27q�6i   $��SIGN UP��6   6�   �� 55   
   -
   5        $ 	btn_Close��55      $��50��6$   $��50��6/   $��Right=�6;   $	��Top=�6W   $��0q�6�   ��6o   Z� ��6�   Z� .��56        $ Power=�6&   $��50��63   $��50��6?   6L   57      �� 58   
   $B��<.\Sources\34054291_1039096782920609_216488157267361792_n.png��6   $	��100��6V   $��70��6c   $��Center=�6n   $	��Top=�6�   $��140,77,140,0q�6�    J� IsManipulationEnabled . 6�   � 59   
   -   5         �Name$ 
processbar��59      $��
0,435,0,61q�6*   $y�Hidden=�6>   6T   5;      5<      
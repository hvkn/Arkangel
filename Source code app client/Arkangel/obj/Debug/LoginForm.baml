   M S B A M L   `   `   `  ����  Arkangel    Arkangel.LoginForm   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   V RMaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  Login��5	   	   $	��500��6   $	��350��6$    � 
ResizeMode$ NoResize=�5
   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	   $��Segoe UI Emoji)�5   	    � Icon$! .\Sources\logo_LSC_icon.ico��5   	   .��5      � .+�5      �� -   5        $	��280��5      $	��Top=�6%   ��5      �� 5       �� EndPoint$ 0.5,1&�6&    �� 
StartPoint$ 0.5,0&�67   .��5      �  � Color$ 	#FF2281D1��6#   � 5      $ 	#FF34268A��6#    � Offset$ 1Q�65   � 5      $ 	#FF33288B��6#   $ 0.546Q�65   5      5      �� 5   
   $	��280��6   $	��240��6    $��Bottom=�6-   $
��0,80q�6H   	 �� RadiusY$	 10��6V   
 �� RadiusX$
 10��6c   
������� J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 15Q�6#      	Direction$ 0Q�63      RenderingBias$ Quality=�6A      ShadowDepth$ 1Q�6Y      Color$ 	#FFBBBBBB��6i   5      � 5   
   $��Bottom=�6   $��35,80q�6*   $	��240��69   .+�5       �� $��LOGIN��6   $��Center=�6$   $	��Top=�6A   $��5q�6Y   
�������$��18(�6v   �� 5!      $��Center=�6   $��15q�64   .+�5"      �� -   5         ��Name$ tb_username��5"      $
��0,10q�6/   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ USERNAME��6=    %  !MaterialDesignFloatingHintTextBox#��[  6g   $��Champagne & Limousines)�6�   $��18(�6�   6�   G� 5#      -   5         G�Name$ tb_password��5#      $
��0,10q�63   $ PASSWORD��6A    ) %MaterialDesignFloatingHintPasswordBox#��[ 6k   $��Champagne & Limousines)�6�   $��18(�6�   6�   5$      5%      �� 5&   
   -   5         ��Name$ 	btn_Login��5&      $	��150��6$   $��Center=�60   $��Bottom=�6M   $
��0,65q�6h   $��LOGIN��6v   6�   �� 5'   
   $��Forget Password?��6   $��Center=�6,   $��Bottom=�6I   $��130,0,126,35q�6d   
������� � Cursor$
 Hand��6�   �� 5(   
   -   5        $ 	bt_signin��5(      $��Bottom=�6$   $��Sign Up��6?   $��10(�6Q   $��70��6_   $��20��6j   $��140,0,140,10q�6v   $��0q�6�   $��Center=�6�   6�   �� 5)   
   -   5        $ 	btn_Close��5)      $��50��6$   $��50��6/   $��Right=�6;   $	��Top=�6W   $��0q�6�   ��6o   Z� ��6�   Z� .��5*      '  !MaterialDesignThemes.Wpf.PackIcon  �  �ControlzEx.PackIconBase`1[[MaterialDesignThemes.Wpf.PackIconKind, MaterialDesignThemes.Wpf, Version=2.4.0.1044, Culture=neutral, PublicKeyToken=null]]   Kind$ Power=�6&   $��50��63   $��50��6?   6L   5+      �� 5,   
   $B��<.\Sources\34054291_1039096782920609_216488157267361792_n.png��6   $	��100��6V   $��70��6c   $��Center=�6n   $	��Top=�6�   $��140,77,140,0q�6�    J� IsManipulationEnabled . 6�   � 5-   
   -   5         �Name$ 
processbar��5-      $��
0,435,0,61q�6*   $y�Hidden=�6>   6T   5.      5/      
   M S B A M L   `   `   `  ����F  BBankManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:BankManagerBankManager  5          BankManager.App   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      +localclr-namespace:BankManager     5        �� 	Resources  5       �� 
TargetType   RoundButtonPageTemplate ��Name �� CornerRadius J� Effect3  -System.Windows.Media.Effects.DropShadowEffect   	Direction   Opacity   ShadowDepth �� Triggers	 P� Property
 P� Value �� 
TargetName �� Property �� Value �� EndPoint � MappingMode �� 
StartPoint � Color � Offset  RoundButtonTemplate  TextBoxBaseControlTemplate ��Name  PasswordBoxControlTemplate  ImageBorderTemplate  ImageShadowStyle �� 
TargetType��%�  &	        &	 4    &	     &	     &	     &	 �    �� 5   
   " ��6   .R�5      �� $ 
ThisBorder��6   $ 60��6)   $��0q�6;   #��z��6O    5	        5
      $	 135Q�6'   $	 0.3Q�67   $ 8Q�6E   ._�5      �� #��z��6$   $��Center=�6H   $��Center=�6c   5       5      P� 5      	 �@J�IsMouseOver
 . . �5      �� $ 
ThisBorder��6    � �� 5      �� 5      $	 1,0&�62   $ RelativeToBoundingBox=�6A   $	 0,0&�6e   .��5   "   � $ Green��6/   $ 4Q�6=   � 5   "   $ White��6/   $ 0Q�6=   5      5      5      5      �� 5   
   " ��6   .R�5      �� $ 60��6   $��0q�6'   #��z��6;   ._�5      �� #��z��6#   $��Center=�6G   $��Center=�6b    5        5      $	 135Q�6'   $	 0.3Q�67   $ 8Q�6E   5!      5"      �� 5#   
   " ��6=   .R�5$      �� $ Bd��5%      #��z��5$      
���   �#��z��5&      $ 60��6E    5'        5(      $	 135Q�6'   $	 0.3Q�67   $ 8Q�6E   ._�5*      �� $ PART_ContentHost��6   5+      5,      �� 5-   
   " G�6=   .R�5.      �� $ Bd��5/      #��z��5.      
���   �#��z��50      $ 60��6E    51        52      $	 135Q�6'   $	 0.3Q�67   $ 8Q�6E   ._�54      �� $ PART_ContentHost��6   55      56      �� 57   
   " ��6   .R�58      �� $ 60��6   $��0q�6'   #��z��6;    59        5:      $	 135Q�6'   $	 0.3Q�67   $ 8Q�6E   5<      5=      �� 5>   
   " ��6)   .�5?      ��  �@J�Effect 5@        5A      $	 135Q�6'   $	 0.3Q�67   $ 8Q�6E   5C      5D      5E      5F      
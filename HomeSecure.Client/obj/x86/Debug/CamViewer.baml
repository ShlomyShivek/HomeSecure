   M S B A M L   `   `   `  ����  HomeSecure.Client!    HomeSecure.Client.CamViewer   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      ��   �� Orientation$  Vertical=�6   $	��330��6(   .+�5	   
   �� -   5         ��Name$ 
CameraName��5	      $
��Auto��6$   $
��Auto��61   $��Camera Name��6?   $��Center=�6U   �� 5
   
   -   5         ��Name$ 
VideoImage��5
      $	��280��6$   $	��250��60   $6��0/HomeSecure.Client;component/Images/No_Image.gif��6=   �� 5   
   $  
Horizontal=�6   $��Center=�6.   $��5q�6K   .+�5      	� -   5         	�Name$ 
StopCamera��5      $��46��6.   $��38��69    d� 	IsChecked$
 TrueY�6^   ��5      �� 5       �� 
TargetType" 	�6&   .R�5      � .+�5      �� $��Normal��6$   $C��=/HomeSecure.Client;component/Images/Stop-Normal-Blue-icon.png��62   $y�Hidden=�6y   �� 5      $��Pressed��6$   $D��>/HomeSecure.Client;component/Images/Stop-Pressed-Blue-icon.png��63   5       �� Triggers 5      P� 5       P� Property �@d�	IsChecked P� Value$
 TrueY�6;   . �5   "   �� 	 �� 
TargetName$	 Normal��6)   
 �� Property
 � y� �� Value$ Hidden=�6S   �� 5   "   $	 Pressed��6)   
 � y�$ Visible=�6T   5      P� 5       �@d�	IsChecked$ FalseY�6;   . �5   "   �� $	 Normal��6)   
 � y�$ Visible=�6S   �� 5   "   $	 Pressed��6)   
 � y�$ Hidden=�6T   5      5      5      	� 5!      -   5        $ StartCamera��5!      $��46��6/   $��38��6:   $ FalseY�6`   ��5"      �� 5#      " 	�6&   .R�5$      � .+�5%      �� $��Normal��6$   $>��8/HomeSecure.Client;component/Images/Play-Normal-icon.png��62   �� 5&      $��Pressed��6$   $?��9/HomeSecure.Client;component/Images/Play-Pressed-icon.png��63   $y�Hidden=�6v   5'       5(      P� 5)       �@d�	IsChecked$
 TrueY�6;   . �5*   "   �� $	 Normal��6)   
 � y�$ Hidden=�6S   �� 5+   "   $	 Pressed��6)   
 � y�$ Visible=�6T   5,      P� 5-       �@d�	IsChecked$ FalseY�6;   . �5.   "   �� $	 Normal��6)   
 � y�$ Visible=�6S   �� 5/   "   $	 Pressed��6)   
 � y�$ Hidden=�6T   50      52      54      55      56      57      
   M S B A M L   `   `   `  ����E  AIMS_SPORTS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:IMS_SPORTS
IMS_SPORTS  5   	       IMS_SPORTS.AuditLog   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:IMS_SPORTS     5   	   -   5          �Name$  	AuditLog1��5   	    � Title$ AuditLog��5   	   $	��450��6   $	��800��6'   .��5	      � .+�5
   
   �� -   5         ��Name$ bg_rectangle��5
      $
��Left=�6*   $��40��6E   $��
222,98,0,0q�6Q   
���   �$	��Top=�6t   $	��550��6�   ���	#FFEDEDED6�   �� 5   
   -   5        $ rectangleAL_bg1��5      $
��Left=�6-   $��68��6H   $��	194,0,0,0q�6T   $	��Top=�6g   $	��606��6   ���	#FFC8814E6�   �� 5   
   -   5        $ rectangleAL_bg��5      $
��Left=�6,   $	��434��6G   $��Center=�6T   $	��194��6o   ���	#FFE0BBBB6{   �� 5   
   -   5         ��Name$ auditlog_lbl��5      $��	AUDIT LOG��6&   $
��Left=�6:   $��
216,18,0,0q�6U   $	��Top=�6i   ���	#FF483A24$��Arial Black)�6�   $��24(�6�   ��6�   Z� �� 5   
   -   5         ��Name$ inventoryAL_btn1��5      $�� ��6+   $
��Left=�66   $��
21,132,0,0q�6Q   $	��Top=�6e   $��0,0,0,0q�6}   $	��152��6�   $��49��6�   ��5      �� 5       �� ImageSource$ /inventory.png��6    j� Stretch$ UniformToFill=�6:   5      �� 5   
   -   5        $ prchaseAL_btn1��5      $�� ��6)   $
��Left=�64   $��21,0,0,0q�6O   $��Center=�6a    J� RenderTransformOrigin$ 0.496,-2.687&�6|   $	��152��6�   $��49��6�   $��0,0,0,0q�6�   ��5      �� 5      $ /purhase.png��6   $ UniformToFill=�68   5      �� 5   
   -   5        $ usermAL_btn1��5      $�� ��6'   $
��Left=�62   $��
21,252,0,0q�6M   $	��Top=�6a   $��0,0,0,0q�6y   $	��152��6�   $��49��6�   ��5      �� 5      $ UniformToFill=�6   $ /manageuser.png��65   5      �� 5   
   -	   5        $ auditAL_btn1��5      $�� ��6'   $
��Left=�62   $��
21,311,0,0q�6M   $	��Top=�6a   $	��152��6y   $��49��6�   $��0,0,0,0q�6�   ��5      �� 5      $ /auditlogs.png��6   $ UniformToFill=�6:   5!      �� 5"   
   -
   5        $ logoutAL_btn1��5"      $�� ��6(   $
��Left=�63   $��
21,371,0,0q�6N   $	��Top=�6b   $	��152��6z   $��49��6�   $��0,0,0,0q�6�   ��5#      �� 5$      $ UniformToFill=�6   $ /logouted (1).png��65   5&      �� 5'   
   -   5        $ dashboardAL_btn1��5'      $�� ��6+   $
��Left=�66   $��	21,73,0,0q�6Q   $	��Top=�6d   $	��152��6|   $��49��6�   $��0,0,0,0q�6�   ��5(      �� 5)      $ UniformToFill=�6   $ /dashboards.png��65   5+      �� 5,   
   -   5         ��Name$ 
dbccAL_lbl��5,      $
��Left=�6$   $��53��6?   $��	21,-2,0,0q�6K   $	��Top=�6^   $	��154��6v   $��/logout (1).png��6�   $��UniformToFill=�6�   �� 5-   
   -   5        $ lineUM_rectangle��5-      $
��Left=�6.   $��1��6I   $��	18,68,0,0q�6T   
���   �$	��Top=�6v   $	��158��6�   $ 0.5,0.5&�6�   z�5.      V� 5/      .~�50      �� 	 �� ScaleY$	 -1Q�6%   �� 51      �� 52      T� 53      54      56      &   System.Windows.Controls.DataGrid  57   
   -   5        
  Name$
 auditLogDataGrid��57      $
��Left=�58      $��222,138,0,0q�59      $	��Top=�5:      $	��258��5;      $	��550��6      AutoGenerateColumns .     HeadersVisibility$ Column=�5=         
IsReadOnly .    Columns 5?      0  *System.Windows.Controls.DataGridTextColumn  5@      ,  &System.Windows.Controls.DataGridColumn   Header$ UserID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 70 6P   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *userID+6P     5A      $ Username��6%   $	 120 6T    67   �� *
username+6T     5B      $ Status��6%   $	 100 6P    65   �� *status+6P     5C      $ Action��6%   $	 130 6P    65   �� *action+6P     5D      $ 	Timestamp��6%   $	 130 6U    68   �� *
datetime+6U   5F      5H      5I      
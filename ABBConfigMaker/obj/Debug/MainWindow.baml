   M S B A M L   `   `   `  ����I  EABBConfigMaker, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:ABBConfigMakerABBConfigMaker  5   	       ABBConfigMaker.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:ABBConfigMaker     5   	   -   5          � Title$  Config Maker��5   	   $��749.5��6   $
��1280��6-   .��5	      � $��0,0,2,0q�6   .+�5
   
   �� -   5         ��Name$ btn_loadFile��5
      $��Load xls file��6'   $
��Left=�6?   $��
726,21,0,0q�6Z   $	��Top=�6n   $	��168��6�   $��23��6�   6�   �� 5   
   -   5         ��Name$ txt_loadedFile��5      $
��Left=�6*   $��23��6E   $��	16,21,0,0q�6Q    �� TextWrapping$
 Wrap=�6d   $	��Top=�6x   $	��710��6�    �� 
IsReadOnly . 6�   �� 5   
   -   5        $ btn_MakeFile��5      $��Create Cfg File��6'   $
��Left=�6A   $��1146,682,0,0q�6\   $	��Top=�6r   $	��114��6�   6�   �� 5   
   -   5        $ txt_cfgFile��5      $
��Left=�6'   $��23��6B   $��	16,60,0,0q�6N   $
 Wrap=�6a   $	��Top=�6u   $	��710��6�    . 6�   �� 5   
   -   5        $ btn_loadFile_Copy��5      $��Load cfg File��6,   $
��Left=�6D   $��
726,60,0,0q�6_   $	��Top=�6s   $	��168��6�   $��23��6�   6�   �� 5   
   $��Options��6   $
��Left=�6$   $��83��6?   $��
917,10,0,0q�6K   $	��Top=�6_   $	��332��6w   .��5      � $
��Left=�6   $��63��6.   $��0,-4,0,0q�6:   $	��Top=�6L   $	��313��6d   .+�5      	� -   5         	�Name$ radWriteUpdateAll��5      $3��-Append new xls and update existng cfg signals��69   $
��Left=�6q   $��
7,10,-27,0q�6�   $	��Top=�6�   $��20��6�   $	��278��6�   	� 5      -   5        $ radDeleteAndAdd��5      $,��&Delete old cfg and add new xls signals��67   $
��Left=�6h   $��17��6�   $��7,25,0,0q�6�   $	��Top=�6�   $	��229��6�   6�   	� 5      -	   5        $ 	radSelect��5      $��Select individually��61   $
��Left=�6O   $��7,42,0,0q�6j   $	��Top=�6|   6�   5      5      �� 5   
   -
   5         ��Name$ lsvXls��5      $
��Left=�6#   $	��559��6>   $��
17,118,0,0q�6K   $	��Top=�6_   $	��414��6w    �� View 5      �� 5      .P�5      �� 5      5      �� 5   
   -   5        $ lsvCfgLoaded��5      $
��Left=�6)   $	��559��6D   $��436,118,0,0q�6Q   $	��Top=�6f   $	��358��6~    5      �� 5      .P�5       �� 5!      5#      �� 5$   
   -   5        $ lsvCfgFinal��5$      $
��Left=�6(   $	��559��6C   $��844,118,0,0q�6P   $	��Top=�6e   $	��417��6}    5%      �� 5&      .P�5'      �� 5(      5*      �� 5+   
   -   5        $ btnAddRecord��5+      $��>��6'   $
��Left=�66   $��799,351,0,0q�6Q   $	��Top=�6f   $��40��6~   $��40��6�   6�   �� 5,   
   -   5        $ btnRemoveRecord��5,      $��<��6*   $
��Left=�69   $��799,395,0,0q�6T   $	��Top=�6i   $��40��6�   $��40��6�   6�   �� 5-   
   $��XLS signals:��6   $
��Left=�6'   $��	17,92,0,0q�6B   $	��Top=�6U   �� 5.   
   $��Cfg signals in cfg file:��6   $
��Left=�63   $��
436,92,0,0q�6N   $	��Top=�6b   �� 5/   
   $��Signals to write:��6   $
��Left=�6,   $��
844,92,0,0q�6G   $	��Top=�6[   �� 50   
   -   5         ��Name$ chckboxSelectAllCfg��50      $��
Select all��60   $
��Left=�6E   $��436,682,0,0q�6`   $	��Top=�6u   	 J� RenderTransformOrigin$	 -3.754,0.333&�6�   �� 51   
   -   5        $ SelectAllXls��51      $��
Select All��6)   $
��Left=�6>   $��
16,682,0,0q�6Y   $	��Top=�6m   6�   53      54      
   M S B A M L   `   `   `  ????I  EVewModelSample, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:VewModelSample.ViewVewModelSample.View  5   	   C&clr-namespace:VewModelSample.ViewModelVewModelSample.ViewModel  5   	   $    VewModelSample.View.ChangeTime   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:VewModelSample.View     5   	   <	viewModel&clr-namespace:VewModelSample.ViewModel     5   	   4Model"clr-namepsace:VewModelSample.Model     5   	   Hi:http://schemas.microsoft.com/expression/2010/interactivity     5   	   Hei9http://schemas.microsoft.com/expression/2010/interactions     5   	     ? 
ResizeMode$  NoResize=?5
   	    ? Title$ 시간 변경??5   	   $	??480??6   $	??600??6$   ??5      ?? 5   
    ?? ImageSource 6   ?? *BackgroundFilepath+.??5      ?  ? ColumnDefinitions 5   
   ?? 5      $??5* ?6   ?? 5      $??5* ?6    ? RowDefinitions 5   
   ?? 5      $??2* ?6   ?? 5      $??2* ?6   ?? 5      $??2* ?6   ?? 5      $??2* ?6   ?? 5      $??2* ?6   ?? 5      $??1* ?6   .+?5   
   ? $??0??6   $??5??6   .+?5      &   System.Windows.Controls.Calendar     IsTodayHighlighted .     styleCalendar#???   62   $??Center=?6?   $??Center=?6?      SelectedDate 6Z   ??  ?? Path$ SelectedDate? ?? Mode$ OneWayToSource=?	 ?? UpdateSourceTrigger$	 PropertyChanged=?6?   5       ? 5!   
   $??1??6   $??4??6   .+?5"      ? $	??300??6   $	??300??6   .+?5$      ?? $??..\images\clockFrame.png??6   $??Center=?6:   $??Center=?6W   $	??262??6r   $	??280??6   6?   ?? 5'      $??..\images\sechand.png??6   $??
67,8,59,90q?67   
 J? RenderTransformOrigin$
 
0.48,0.704&?6K    K? ZIndex$ 2??6n   z?5(      V? 5)      .~?5*      ?? ?? 5+      ?? 5,       ?? Angle 6.   ?? *SetSecAngle+$ OneWay=?$	 PropertyChanged=?T? 5-      5.      50      ?? 51      $??..\images\minhand.png??6   $??81,23,84,116q?67   $
 	0.52,0.79&?6M   $ 1??6o   z?52      V? 53      .~?54      ?? ?? 55      ?? 56       6.   ?? *SetMinAngle+$ OneWay=?$	 PropertyChanged=?T? 57      58      5:      ?? 5;      $??..\images\hourhand.png??6   $??105,66,105,137q?68   $
 
0.515,0.86&?6P   z?5<      V? 5=      .~?5>      ?? ?? 5?      ?? 5@       6.   ?? *SetHourAngle+$ OneWay=?$	 PropertyChanged=?T? 5A      5B      5D      5E      5F      ? 5G   
   $??1??6   $??4??6   .+?5H      ??  ?? Orientation$ 
Horizontal=?6   $??Center=?62   $??Center=?6O   .+?5I      ?? $??Center=?6w   $??60??6?   $??22??6?    ?? SelectedIndex 6   ?? *TimeSelectIndex+$ TwoWay=?$	 PropertyChanged=?.@?5J      ?? $??오전??6#   ?? 5K      $??오후??6#   5L      ?? 5M        MyWaterMarkStyle#???  6   
 ? Tag$	 시??6E   $??Center=?6M    ?? TextWrapping$
 Wrap=?6k   $??Center=?6?   $??60??6?   $??22??6?   ??6   ?? $ SetHour?$ OneWayToSource=?$	 PropertyChanged=?6?   ?? 5N      #???  6   $	 분??6E   $??Center=?6M   $
 Wrap=?6j   $??Center=?6?   $??60??6?   $??22??6?   ??6~   ?? $ SetMin?$ OneWayToSource=?$	 PropertyChanged=?6?   ?? 5O      #???  6   $	 초??6E   $??Center=?6M   $
 Wrap=?6j   $??Center=?6?   $??60??6?   $??22??6?   ??6~   ?? $ SetSec?$ OneWayToSource=?$	 PropertyChanged=?6?   5P      5Q      ? 5R   
   $??1??6   $??5??6   $ 1??6,   .+?5S      ?? $ 
Horizontal=?6   $??Center=?62   .+?5T      ??   buttonHover#???  6   $??설정??6?   $??Center=?6?   $??Center=?6?   $	??113??6?   ??6?   ?? *ChangeTimeConfirm+$	 PropertyChanged=?6?   ?? 5U       ?? IsCancel . #???  6)   -   'VewModelSample.ViewModel.ClockViewModel   CloseCommand#??Z 6O   $??닫기??6?   $??Center=?6?   $??Center=?6  $	??113??67  $??20,0,0,0q?6C  ??6?   ??  ?? RelativeSource ?? *FindAncestor+ ?? AncestorType" ?6C  5V      5W      ?? 5X   
     
BorderThin#???  6   $??0??68   $??2??6H   $??5??6\   $??3q?6m   $??날짜 및 시간 설정??6?   
???   ?6?   ?? 5Y   
   
???????$??0??6'   $??2??67   $??6??6K   5Z      5\      
   M S B A M L   `   `   `  ????I  EVewModelSample, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:VewModelSample.ViewVewModelSample.View  5   	   C&clr-namespace:VewModelSample.ViewModelVewModelSample.ViewModel  5   	   '    !VewModelSample.View.StopwatchFunc   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:VewModelSample.View     5   	   <	viewModel&clr-namespace:VewModelSample.ViewModel     5   	     ? 
ResizeMode$  NoResize=?5	   	    ? Title$ 스톱워치??5
   	   $	??480??6   $	??600??6#   ??5      ?? 5   
    ?? ImageSource 6   ?? *BackgroundFilepath+.??5      ?  ? ColumnDefinitions 5   
   ?? 5      $??2* ?6   ?? 5      $??2* ?6   ?? 5      $??3* ?6   ?? 5      $??3* ?6   ?? 5      $??2* ?6   ?? 5      $??2* ?6    ? RowDefinitions 5   
   ?? 5      $??1* ?6   ?? 5      $
??1.5* ?6   ?? 5      $??2* ?6   ?? 5      $??2* ?6   ?? 5      $??* ?6   ?? 5      $??1* ?6   ?? 5      $??42.1854545454546 ?6   .+?5"   
   ??    
BorderThin#???   6   $??2??68   $??2??6H   $??1??6\   $??4??6i   $??3q?6z   $??스톱워치??6?   
???   ? K? ZIndex$ 1??6?   ? 5#   
   $??2??6   $??1??6   $??2??6,   .+?5$      ?? $??30(?6    ?? TextAlignment$ Center=?6&   $??Center=?6=   $
??Wrap=?6Z   $	??214??6n   $??Center=?6?   $??0,12,0,0q?6?   ??6z   ?? *	StopWatch+ ?? Mode$ TwoWay=? ?? UpdateSourceTrigger$ PropertyChanged=?6?   5%      ? 5&   
   $??2??6   $??2??6   $??2??6,   $??2??6=   .+?5'      &   System.Windows.Controls.DataGrid  	   AutoGenerateColumns	 .  
   CanUserAddRows
 .  ??6   ??  ?? Path$ swDatas?$ TwoWay=? ?? NotifyOnSourceUpdated .  ?? NotifyOnTargetUpdated .    Columns 5(      0  *System.Windows.Controls.DataGridTextColumn  5)      ,  &System.Windows.Controls.DataGridColumn   Header$ Sequence??6)      MinWidth$ 10Q?6;      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6I   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6V   ?? $ stopWatchSeq?  5*      $ SaveTime??6)   $ 10Q?6;   $
 Auto 6I    6V   ?? $ saveTime?5,      5-      ? 5.   
   $??2??6   $??4??6   $??2??6,   .+?5/      ??  ?? Orientation$ 
Horizontal=?6   $??Center=?62   $??Center=?6O   .+?50      ??   buttonHover#???  6b   $??Center=?6  $??Center=?62  $	??113??6M  {?6   ?? *SwLeftButtonTF+$ PropertyChanged=???6?   ?? *RecordStopwatch+$ PropertyChanged=???6?   ?? *
SwLeftText+$ PropertyChanged=?6M  ?? 51      #???  6   $??Center=?6?   $??Center=?6?   $	??113??6  $??10,0,0,0q?6  ??6?   ?? *StartStopWatch+$ PropertyChanged=???6?   ?? *SwRightText+$ PropertyChanged=?6  52      53      ?? 54   
   
???????$??2??6'   $??2??67   $??4??6K   $ -1??6X   ? 56   
   $??4??6   $??2??6   $??6??63   $ 1??6@   .+?57      ?? $ 
Horizontal=?6   $??Center=?62   .+?58      ?? #???  6   -   'VewModelSample.ViewModel.ClockViewModel   CloseCommand#??Z 6?   $??닫기??6?   $??Center=?6?   $??Center=?6  $	??113??6'  ??6z   ??  ?? RelativeSource ?? *FindAncestor+ ?? AncestorType" ?6'  59      5:      ?? 5;   
   
???????$??0??6'   $??6??67   $??6??6K   5<      5=      
   M S B A M L   `   `   `  ????I  EVewModelSample, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:VewModelSample.ViewVewModelSample.View  5   	   C&clr-namespace:VewModelSample.ViewModelVewModelSample.ViewModel  5   	   !    VewModelSample.View.ViewLog   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:VewModelSample.View     5   	   <	viewModel&clr-namespace:VewModelSample.ViewModel     5   	     ? 
ResizeMode$  NoResize=?5	   	    ? Title$ 로그 확인??5
   	   $	??480??6   $	??935??6$   ??5      ?? 5   
    ?? ImageSource 6   ?? *BackgroundFilepath+.??5      ?  ? ColumnDefinitions 5   
   ?? 5      $
??2.5* ?6   ?? 5      $
??2.5* ?6   ?? 5      $
??2.5* ?6   ?? 5      $
??2.5* ?6    ? RowDefinitions 5   
   ?? 5      $??1* ?6   ?? 5      $??42.1854545454546 ?6   .+?5   
   ? $??0??6   $??4??6   .+?5      &   System.Windows.Controls.DataGrid     AutoGenerateColumns .     CanUserAddRows .  ??6   ??  ?? Path$ logDatas? ?? Mode$ TwoWay=?	 ?? NotifyOnSourceUpdated	 . 
 ?? NotifyOnTargetUpdated
 .    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Sequence??6)      MinWidth$ 10Q?6;      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6I   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6V   ?? $ dataGridSequence?6?     5      $ Function??6)   $ 10Q?6;   $
 Auto 6I    6V   ?? $ dataGridFunction?6?     5      $ 	AddedTime??6)   $ 10Q?6<   $
 Auto 6J    6W   ?? $ dataGridAddedTime?6?     5       $ 
RecordText??6)   $ 10Q?6=   $
 Auto 6K    6X   ?? $ dataGridSimpleRecordText?6?   5"      5#      ? 5$   
   $??3??6   $??2??6   $??1??63    K? ZIndex$ 1??6@   .+?5%      ??  ?? Orientation$ 
Horizontal=?6   $??Center=?62   .+?5&      ??    buttonHover#???   6   -   'VewModelSample.ViewModel.ClockViewModel   CloseCommand#??Z 6?   $??닫기??6?   $??Center=?6?   $??Center=?6  $	??113??6'  ??6z   ??  ?? RelativeSource ?? *FindAncestor+ ?? AncestorType" ?6'  5'      5(      ?? 5)   
   
???????$??0??6'   $??4??67   $??1??6K   5*      5+      
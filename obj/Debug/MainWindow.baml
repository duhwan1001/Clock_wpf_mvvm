   M S B A M L   `   `   `  ????I  EVewModelSample, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:VewModelSample.ViewVewModelSample.View  5   	   C&clr-namespace:VewModelSample.ViewModelVewModelSample.ViewModel  5   	       VewModelSample.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:VewModelSample.View     5   	   <	viewModel&clr-namespace:VewModelSample.ViewModel     5   	     ? 
ResizeMode$  NoResize=?5	   	    ? Title$ 메인화면??5
   	   $	??450??6   $	??700??6#   ??5      ?? 5   
    ?? ImageSource 6   ?? *BackgroundFilepath+.??5      ?  ? ColumnDefinitions 5   
   ?? 5      $??5* ?6   ?? 5      $??5* ?6   .+?5   
   ? $??0??6   .+?5      #   VewModelSample.View.ClockFunc  $??Center=?6   ?? 5         buttonHover#???   6   $??배경변경??6`   $
??Left=?6o   $??	10,10,0,0q?6?   $	??Top=?6?   $??50??6?   $??20??6?   ??6;   ?? *ChangeBackground+6?   5      ? 5   
   $??1??6   .+?5      )   #VewModelSample.View.BaseUserControl  $??Center=?6$   5      ?? 5   
     
BorderThin#???  6   $??1??68   $??3q?6H   $??기능??6\   
???   ?6h   5      5      
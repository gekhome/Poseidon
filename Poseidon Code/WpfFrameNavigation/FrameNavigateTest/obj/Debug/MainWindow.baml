   M S B A M L   `   `   `  ����M  IFrameNavigationWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS<clr-namespace:FrameNavigationWPF;assembly=FrameNavigationWPFFrameNavigationWPF  5       FrameNavigateTest"   FrameNavigateTest.MainWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      UFrameNavigationWPF<clr-namespace:FrameNavigationWPF;assembly=FrameNavigationWPF  5      -   5          �Name$  Window��5       � Title$ 
MainWindow��5      $	��640��5	      $	��480��6   $��Segoe UI Light)�6   $��21.333(�67   .��5      � -   5         �Name$ 
LayoutRoot��5      .+�5      � $��	286,8,8,8q�6	   .+�5       � -   5          �Name$ frame��5      $��Frame��6     � Source$- '/FrameNavigateTest;component/Page1.xaml?�6*     � NavigationUIVisibility$ Hidden=�6[   5      �� 5      $
��Left=�6   $
��Wrap=�6)   $��Page 1��6=   $	��Top=�6K   $��	20,20,0,0q�6c    � Cursor$
 Hand��6v   .  (System.Windows.Interactivity.Interaction   	Behaviors 5      &    FrameNavigationWPF.FrameNavigate  5         
NavigateTo$ 
Page1.xaml?�6'   	   OutsideNavigation	 . 
   TargetFrame$
 frame��6X      TransitionTime$ 2Q�6l      
Transition$ Random=�6   5      �� 5      $
��Left=�6   $
��Wrap=�6)   $��Page 2��6=   $	��Top=�6K   $��	20,60,0,0q�6c   $
 Hand��6v    5        5      $ 
Page2.xaml?�6'   	 . $
 frame��6X   $ Random=�6l   $ 2Q�6�   5      5      5      
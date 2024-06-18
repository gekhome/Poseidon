using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Interactivity;
using System.Windows.Media.Effects;

namespace FrameNavigationWPF
{
	public class FrameNavigate : Behavior<FrameworkElement>
	{
		Panel panel;
		Frame frame;
		Frame newFrame;
		double spinDirection = 0;
		int transitionNumber = 0;
		
		[Description("Select a Page to Navigate to."),Category("Behavior Settings")]
		public Uri NavigateTo { get { return (Uri)GetValue(NavigateToProperty); } set { SetValue(NavigateToProperty, value); }}
		public static readonly DependencyProperty NavigateToProperty = DependencyProperty.Register("NavigateTo", typeof(Uri), typeof(FrameNavigate), null);
		
		public enum Transitions { Left = 0, Right = 1, Up = 2, Down = 3,
			LeftWithFade = 4, RightWithFade = 5, UpWithFade = 6, DownWithFade = 7, 
			Fade = 8, Zoom = 9, SpinLeft = 10, SpinRight = 11, 
			RotateLeft = 12, RotateRight = 13, SkewLeft = 14, SkewRight = 15,  
			SwitchLeft = 16, SwitchRight = 17, SwitchUp = 18, SwitchDown = 19,
			SwitchLeftFade = 20, SwitchRightFade = 21, SwitchUpFade = 22, SwitchDownFade = 23, Blur = 24, Random = 25}
		[Description("Select the direction you would like the slide transition to go in."),Category("Behavior Settings")]
		public Transitions Transition { get { return (Transitions)GetValue(TransitionProperty); } set { SetValue(TransitionProperty, value); }}
		public static readonly DependencyProperty TransitionProperty = DependencyProperty.Register("Transition", typeof(Transitions), typeof(FrameNavigate), null);
		
		[Description("Seconds for the transition.  Example: 0.5 for half a second, 1 for a second, etc..."),Category("Behavior Settings")]
		public double TransitionTime { get { return (double)GetValue(TransitionTimeProperty); } set { SetValue(TransitionTimeProperty, value); }}
		public static readonly DependencyProperty TransitionTimeProperty = DependencyProperty.Register("TransitionTime", typeof(double), typeof(FrameNavigate), null);
		
		[Description("Navigate From Outside Frame?"),Category("Behavior Settings")]
		public bool OutsideNavigation { get { return (bool)GetValue(OutsideNavigationProperty); } set { SetValue(OutsideNavigationProperty, value); }}
		public static readonly DependencyProperty OutsideNavigationProperty = DependencyProperty.Register("OutsideNavigation", typeof(bool), typeof(FrameNavigate), null);
		
		[Description("Frame"),Category("Behavior Settings")]
		[CustomPropertyValueEditor(CustomPropertyValueEditor.Element)]
		public string TargetFrame { get { return (string)GetValue(TargetFrameProperty); } set { SetValue(TargetFrameProperty, value); }}
		public static readonly DependencyProperty TargetFrameProperty = DependencyProperty.Register("TargetFrame", typeof(string), typeof(FrameNavigate), null);
		
		
		protected override void OnAttached()
		{
			base.OnAttached();
			Attached();
		}

		protected override void OnDetaching()
		{
			base.OnDetaching();
		}
		
		private void Attached()
		{
			FrameworkElement fe = this.AssociatedObject as FrameworkElement;
			if(fe.GetType().Name.ToString() == "Button")
			{
				Button btn = fe as Button;
				btn.Click += onClick;
			}
			else
				fe.MouseLeftButtonDown += onMouseDown;
		}
		
		private void onClick(object sender, RoutedEventArgs e)
		{
			Button btn = sender as Button;
            if (!(OutsideNavigation))
			{
				DependencyObject parent = VisualTreeHelper.GetParent(btn);
				while(!(parent is Frame))
				{
					parent = VisualTreeHelper.GetParent(parent);
				}
				frame = parent as Frame;
			}
            else
            {
                DependencyObject parent = VisualTreeHelper.GetParent(btn);
                while (!(parent is Window))
                {
                    parent = VisualTreeHelper.GetParent(parent);
                }
                Window win = parent as Window;
                frame = win.FindName(TargetFrame) as Frame;
            }
			panel = frame.Parent as Panel;			
            string a = NavigateTo.ToString();
            string b = frame.Source.ToString();
            string c = b.Substring(b.Length - a.Length);
            if (c != a)
            	navigateFrame();
		}
		
		private void onMouseDown(object sender, MouseButtonEventArgs e)
		{
            DependencyObject obj = sender as DependencyObject;
            if (!(OutsideNavigation))
            {
                DependencyObject parent = VisualTreeHelper.GetParent(obj);
                while (!(parent is Frame))
                {
                    parent = VisualTreeHelper.GetParent(parent);
                }
                frame = parent as Frame;
            }
            else
            {
                DependencyObject parent = VisualTreeHelper.GetParent(obj);
                while (!(parent is Window))
                {
                    parent = VisualTreeHelper.GetParent(parent);
                }
                Window win = parent as Window;
                frame = win.FindName(TargetFrame) as Frame;
            }
            panel = frame.Parent as Panel;
            string a = NavigateTo.ToString();
            string b = frame.Source.ToString();
            string c = b.Substring(b.Length - a.Length);
            if (c != a)
                navigateFrame();
		}
		
		private void navigateFrame()
		{
			if (Transition == Transitions.Random)
			{
				Random random = new Random();
				transitionNumber = random.Next(0, 25);
				navigateFrame2();
			}
			else
			{
				transitionNumber = Convert.ToInt16(Transition);
				navigateFrame2();
			}
		}
		
		private void navigateFrame2()
		{			
			newFrame = new Frame();
			newFrame.Content = frame.Content;
			newFrame.Width = frame.ActualWidth;
			newFrame.Height = frame.ActualHeight;
			newFrame.HorizontalAlignment = HorizontalAlignment.Left;
			newFrame.VerticalAlignment = VerticalAlignment.Top;
			newFrame.Margin = new Thickness(frame.Margin.Left, frame.Margin.Right, 0, 0);
			newFrame.Background = frame.Background;
			panel.Children.Add(newFrame);
			
			frame.Source = NavigateTo;
			newFrame.IsHitTestVisible = false;
			frame.IsHitTestVisible = false;
			
			double a = frame.ActualWidth;
			double b = frame.ActualHeight;
			
			TranslateTransform tt = new TranslateTransform();
			DoubleAnimation da = new DoubleAnimation();
			da.Completed += new EventHandler(daComplete);
			da.Duration = new Duration(TimeSpan.FromSeconds(TransitionTime));
			
			TranslateTransform tt2 = new TranslateTransform();
			DoubleAnimation da2 = new DoubleAnimation();
			da2.Completed += new EventHandler(daComplete);
			da2.Duration = new Duration(TimeSpan.FromSeconds(TransitionTime));
			
			DoubleAnimation daOpacity = new DoubleAnimation();
			daOpacity.Duration = da.Duration;
			
			DoubleAnimation daOpacity2 = new DoubleAnimation();
			daOpacity2.Duration = da.Duration;
			
			if (transitionNumber == 0)  //Left
			{
				da.From = 0;
				da.To = -a;
				da2.From = a;
				da2.To = 0;
				newFrame.RenderTransform = tt;
				frame.RenderTransform = tt2;
				tt.BeginAnimation(TranslateTransform.XProperty, da);
				tt2.BeginAnimation(TranslateTransform.XProperty, da2);
			}			
			if (transitionNumber == 1) //Right
			{
				da.From = 0;
				da.To = a;
				da2.From = -a;
				da2.To = 0;
				newFrame.RenderTransform = tt;
				frame.RenderTransform = tt2;
				tt.BeginAnimation(TranslateTransform.XProperty, da);
				tt2.BeginAnimation(TranslateTransform.XProperty, da2);
			}
			if (transitionNumber == 2) //Up
			{
				da.From = 0;
				da.To = -b;
				da2.From = b;
				da2.To = 0;
				newFrame.RenderTransform = tt;
				frame.RenderTransform = tt2;
				tt.BeginAnimation(TranslateTransform.YProperty, da);
				tt2.BeginAnimation(TranslateTransform.YProperty, da2);
			}
			if (transitionNumber == 3) //Down
			{
				da.From = 0;
				da.To = b;
				da2.From = -b;
				da2.To = 0;
				newFrame.RenderTransform = tt;
				frame.RenderTransform = tt2;
				tt.BeginAnimation(TranslateTransform.YProperty, da);
				tt2.BeginAnimation(TranslateTransform.YProperty, da2);
			}
			if (transitionNumber == 4) //LeftWithFade
			{
				da.From = 0;
				da.To = -a;
				da2.From = a;
				da2.To = 0;				
				daOpacity.From = 1;
				daOpacity.To = 0;
				daOpacity2.From = 0;
				daOpacity2.To = 1;				
				newFrame.RenderTransform = tt;
				frame.RenderTransform = tt2;				
				newFrame.BeginAnimation(Frame.OpacityProperty, daOpacity);
				tt.BeginAnimation(TranslateTransform.XProperty, da);				
				frame.BeginAnimation(Frame.OpacityProperty, daOpacity2);
				tt2.BeginAnimation(TranslateTransform.XProperty, da2);
			}			
			if (transitionNumber == 5) //RightWithFade
			{
				da.From = 0;
				da.To = a;
				da2.From = -a;
				da2.To = 0;				
				daOpacity.From = 1;
				daOpacity.To = 0;
				daOpacity2.From = 0;
				daOpacity2.To = 1;				
				newFrame.RenderTransform = tt;
				frame.RenderTransform = tt2;				
				newFrame.BeginAnimation(Frame.OpacityProperty, daOpacity);
				tt.BeginAnimation(TranslateTransform.XProperty, da);				
				frame.BeginAnimation(Frame.OpacityProperty, daOpacity2);
				tt2.BeginAnimation(TranslateTransform.XProperty, da2);
			}
			if (transitionNumber == 6) //UpWithFade
			{
				da.From = 0;
				da.To = -b;
				da2.From = b;
				da2.To = 0;
				daOpacity.From = 1;
				daOpacity.To = 0;
				daOpacity2.From = 0;
				daOpacity2.To = 1;
				newFrame.RenderTransform = tt;
				frame.RenderTransform = tt2;
				newFrame.BeginAnimation(Frame.OpacityProperty, daOpacity);
				tt.BeginAnimation(TranslateTransform.YProperty, da);
				frame.BeginAnimation(Frame.OpacityProperty, daOpacity2);
				tt2.BeginAnimation(TranslateTransform.YProperty, da2);
			}
			if (transitionNumber == 7) //DownWithFade
			{
				da.From = 0;
				da.To = b;
				da2.From = -b;
				da2.To = 0;
				daOpacity.From = 1;
				daOpacity.To = 0;
				daOpacity2.From = 0;
				daOpacity2.To = 1;
				newFrame.RenderTransform = tt;
				frame.RenderTransform = tt2;
				newFrame.BeginAnimation(Frame.OpacityProperty, daOpacity);
				tt.BeginAnimation(TranslateTransform.YProperty, da);
				frame.BeginAnimation(Frame.OpacityProperty, daOpacity2);
				tt2.BeginAnimation(TranslateTransform.YProperty, da2);
			}
			if (transitionNumber == 8) //Fade
			{
				da.Completed -= new EventHandler(daComplete);
				da.Completed += new EventHandler(daFade);
				da.Duration = new Duration(TimeSpan.FromSeconds(TransitionTime/2));
				da2.Completed -= new EventHandler(daComplete);
				da2.Duration = da.Duration;
				da.From = 1;
				da.To = 0;
				da2.From = 0;
				da2.To = 0;
				newFrame.BeginAnimation(Frame.OpacityProperty, da);
				frame.BeginAnimation(Frame.OpacityProperty, da2);
			}
			if (transitionNumber == 9) //Zoom
			{
				newFrame.RenderTransformOrigin = new Point(0.5, 0.5);
				frame.RenderTransformOrigin = new Point(0.5, 0.5);
				da.Completed -= new EventHandler(daComplete);
				da.Completed += new EventHandler(daZoom);
				da.Duration = new Duration(TimeSpan.FromSeconds(TransitionTime/2));
				da2.Duration = da.Duration;
				da.From = 1;
				da.To = 0;
				da2.From = 0;
				da2.To = 0;				
				ScaleTransform st = new ScaleTransform();
				newFrame.RenderTransform = st;
				ScaleTransform st2 = new ScaleTransform();
				frame.RenderTransform = st2;				
				st.BeginAnimation(ScaleTransform.ScaleXProperty, da);
				st.BeginAnimation(ScaleTransform.ScaleYProperty, da);
				st2.BeginAnimation(ScaleTransform.ScaleXProperty, da2);
				st2.BeginAnimation(ScaleTransform.ScaleYProperty, da2);				
			}
			if (transitionNumber == 10) //SpinLeft
			{
				newFrame.RenderTransformOrigin = new Point(0.5, 0.5);
				frame.RenderTransformOrigin = new Point(0.5, 0.5);
				da.Completed -= new EventHandler(daComplete);
				spinDirection = 360;
				da.Completed += new EventHandler(daSpin);
				da.Duration = new Duration(TimeSpan.FromSeconds(TransitionTime/2));
				da2.Duration = new Duration(TimeSpan.FromSeconds(TransitionTime/2));
				da.From = 0;
				da.To = -360;
				da2.From = 1;
				da2.To = 0;
				TransformGroup tg = new TransformGroup();
				RotateTransform rt = new RotateTransform();
				ScaleTransform st = new ScaleTransform();
				ScaleTransform st2 = new ScaleTransform();
				st2.ScaleX = st2.ScaleY = 0;
				frame.RenderTransform = st2;
				tg.Children.Add(rt);
				tg.Children.Add(st);
				newFrame.RenderTransform = tg;
				rt.BeginAnimation(RotateTransform.AngleProperty, da);
				st.BeginAnimation(ScaleTransform.ScaleXProperty, da2);
				st.BeginAnimation(ScaleTransform.ScaleYProperty, da2);
			}
			if (transitionNumber == 11) //SpinRight
			{
				newFrame.RenderTransformOrigin = new Point(0.5, 0.5);
				frame.RenderTransformOrigin = new Point(0.5, 0.5);
				da.Completed -= new EventHandler(daComplete);
				spinDirection = -360;
				da.Completed += new EventHandler(daSpin);
				da.Duration = new Duration(TimeSpan.FromSeconds(TransitionTime/2));
				da2.Duration = new Duration(TimeSpan.FromSeconds(TransitionTime/2));
				da.From = 0;
				da.To = 360;
				da2.From = 1;
				da2.To = 0;
				TransformGroup tg = new TransformGroup();
				RotateTransform rt = new RotateTransform();
				ScaleTransform st = new ScaleTransform();
				ScaleTransform st2 = new ScaleTransform();
				st2.ScaleX = st2.ScaleY = 0;
				frame.RenderTransform = st2;
				tg.Children.Add(rt);
				tg.Children.Add(st);
				newFrame.RenderTransform = tg;
				rt.BeginAnimation(RotateTransform.AngleProperty, da);
				st.BeginAnimation(ScaleTransform.ScaleXProperty, da2);
				st.BeginAnimation(ScaleTransform.ScaleYProperty, da2);
			}
			if (transitionNumber == 12) //RotateLeft
			{
				newFrame.RenderTransformOrigin = new Point(0.5, 1);
				frame.RenderTransformOrigin = new Point(0.5, 1);
				da.From = 0;
				da.To = -180;
				da2.From = 180;
				da2.To = 0;
				RotateTransform rt = new RotateTransform();
				RotateTransform rt2 = new RotateTransform();
				newFrame.RenderTransform = rt;
				frame.RenderTransform = rt2;
				rt.BeginAnimation(RotateTransform.AngleProperty, da);
				rt2.BeginAnimation(RotateTransform.AngleProperty, da2);
			}
			if (transitionNumber == 13) //RotateRight
			{
				newFrame.RenderTransformOrigin = new Point(0.5, 1);
				frame.RenderTransformOrigin = new Point(0.5, 1);
				da.From = 0;
				da.To = 180;
				da2.From = -180;
				da2.To = 0;
				RotateTransform rt = new RotateTransform();
				RotateTransform rt2 = new RotateTransform();
				newFrame.RenderTransform = rt;
				frame.RenderTransform = rt2;
				rt.BeginAnimation(RotateTransform.AngleProperty, da);
				rt2.BeginAnimation(RotateTransform.AngleProperty, da2);
			}
			if (transitionNumber == 14) //SkewLeft
			{
				newFrame.RenderTransformOrigin = new Point(0.5, 1);
				frame.RenderTransformOrigin = new Point(0.5, 1);
				da.From = 0;
				da.To = 89;
				da2.From = -89;
				da2.To = 0;
				SkewTransform st = new SkewTransform();
				SkewTransform st2 = new SkewTransform();
				newFrame.RenderTransform = st;
				frame.RenderTransform = st2;
				st.BeginAnimation(SkewTransform.AngleXProperty, da);
				st2.BeginAnimation(SkewTransform.AngleXProperty, da2);
			}
			if (transitionNumber == 15) //SkewRight
			{
				newFrame.RenderTransformOrigin = new Point(0.5, 1);
				frame.RenderTransformOrigin = new Point(0.5, 1);
				da.From = 0;
				da.To = -89;
				da2.From = 89;
				da2.To = 0;
				SkewTransform st = new SkewTransform();
				SkewTransform st2 = new SkewTransform();
				newFrame.RenderTransform = st;
				frame.RenderTransform = st2;
				st.BeginAnimation(SkewTransform.AngleXProperty, da);
				st2.BeginAnimation(SkewTransform.AngleXProperty, da2);
			}
			if (transitionNumber == 16)  //SwitchLeft
			{
				da.From = 0;
				da.To = -a;
				da2.From = -a;
				da2.To = 0;
				newFrame.RenderTransform = tt;
				frame.RenderTransform = tt2;
				tt.BeginAnimation(TranslateTransform.XProperty, da);
				tt2.BeginAnimation(TranslateTransform.XProperty, da2);
			}			
			if (transitionNumber == 17) //SwitchRight
			{
				da.From = 0;
				da.To = a;
				da2.From = a;
				da2.To = 0;
				newFrame.RenderTransform = tt;
				frame.RenderTransform = tt2;
				tt.BeginAnimation(TranslateTransform.XProperty, da);
				tt2.BeginAnimation(TranslateTransform.XProperty, da2);
			}
			if (transitionNumber == 18) //SwitchUp
			{
				da.From = 0;
				da.To = -b;
				da2.From = -b;
				da2.To = 0;
				newFrame.RenderTransform = tt;
				frame.RenderTransform = tt2;
				tt.BeginAnimation(TranslateTransform.YProperty, da);
				tt2.BeginAnimation(TranslateTransform.YProperty, da2);
			}
			if (transitionNumber == 19) //SwitchDown
			{
				da.From = 0;
				da.To = b;
				da2.From = b;
				da2.To = 0;
				newFrame.RenderTransform = tt;
				frame.RenderTransform = tt2;
				tt.BeginAnimation(TranslateTransform.YProperty, da);
				tt2.BeginAnimation(TranslateTransform.YProperty, da2);
			}
			if (transitionNumber == 20) //SwitchLeftWithFade
			{
				da.From = 0;
				da.To = -a;
				da2.From = -a;
				da2.To = 0;				
				daOpacity.From = 1;
				daOpacity.To = 0;
				daOpacity2.From = 0;
				daOpacity2.To = 1;				
				newFrame.RenderTransform = tt;
				frame.RenderTransform = tt2;				
				newFrame.BeginAnimation(Frame.OpacityProperty, daOpacity);
				tt.BeginAnimation(TranslateTransform.XProperty, da);				
				frame.BeginAnimation(Frame.OpacityProperty, daOpacity2);
				tt2.BeginAnimation(TranslateTransform.XProperty, da2);
			}			
			if (transitionNumber == 21) //SwitchRightWithFade
			{
				da.From = 0;
				da.To = a;
				da2.From = a;
				da2.To = 0;				
				daOpacity.From = 1;
				daOpacity.To = 0;
				daOpacity2.From = 0;
				daOpacity2.To = 1;				
				newFrame.RenderTransform = tt;
				frame.RenderTransform = tt2;				
				newFrame.BeginAnimation(Frame.OpacityProperty, daOpacity);
				tt.BeginAnimation(TranslateTransform.XProperty, da);				
				frame.BeginAnimation(Frame.OpacityProperty, daOpacity2);
				tt2.BeginAnimation(TranslateTransform.XProperty, da2);
			}
			if (transitionNumber == 22) //UpWithFade
			{
				da.From = 0;
				da.To = -b;
				da2.From = -b;
				da2.To = 0;
				daOpacity.From = 1;
				daOpacity.To = 0;
				daOpacity2.From = 0;
				daOpacity2.To = 1;
				newFrame.RenderTransform = tt;
				frame.RenderTransform = tt2;
				newFrame.BeginAnimation(Frame.OpacityProperty, daOpacity);
				tt.BeginAnimation(TranslateTransform.YProperty, da);
				frame.BeginAnimation(Frame.OpacityProperty, daOpacity2);
				tt2.BeginAnimation(TranslateTransform.YProperty, da2);
			}
			if (transitionNumber == 23) //DownWithFade
			{
				da.From = 0;
				da.To = b;
				da2.From = b;
				da2.To = 0;
				daOpacity.From = 1;
				daOpacity.To = 0;
				daOpacity2.From = 0;
				daOpacity2.To = 1;
				newFrame.RenderTransform = tt;
				frame.RenderTransform = tt2;
				newFrame.BeginAnimation(Frame.OpacityProperty, daOpacity);
				tt.BeginAnimation(TranslateTransform.YProperty, da);
				frame.BeginAnimation(Frame.OpacityProperty, daOpacity2);
				tt2.BeginAnimation(TranslateTransform.YProperty, da2);
			}
			if (transitionNumber == 24) //Blur
			{
				BlurEffect effect = new BlurEffect();
				effect.Radius = 100;
				frame.Effect = effect;

                BlurEffect effect2 = new BlurEffect();
                effect2.Radius = 0;
                newFrame.Effect = effect2;

                frame.Opacity = 0;
				
                da.Completed -= new EventHandler(daComplete);
                da.Completed += new EventHandler(daBlur);
                da.Duration = new Duration(TimeSpan.FromSeconds(TransitionTime / 2));
                da.From = 0;
                da.To = 100;
                effect2.BeginAnimation(BlurEffect.RadiusProperty, da);
			}
			
		}

		private void daComplete(object sender, System.EventArgs e)
		{
			panel.Children.Remove(newFrame);
			frame.IsHitTestVisible = true;
		}
		
		private void daFade(object sender, System.EventArgs e)
		{
			DoubleAnimation da = new DoubleAnimation();
			da.Completed += new EventHandler(daComplete);
			da.Duration = new Duration(TimeSpan.FromSeconds(TransitionTime/2));
			da.From = 0;
			da.To = 1;
			frame.BeginAnimation(Frame.OpacityProperty, da);
		}
		
		private void daZoom(object sender, System.EventArgs e)
		{
			DoubleAnimation da = new DoubleAnimation();
			da.Completed += new EventHandler(daComplete);
			da.Duration = new Duration(TimeSpan.FromSeconds(TransitionTime/2));
			da.From = 0;
			da.To = 1;
			ScaleTransform st = new ScaleTransform();
			frame.RenderTransform = st;
			st.BeginAnimation(ScaleTransform.ScaleXProperty, da);
			st.BeginAnimation(ScaleTransform.ScaleYProperty, da);
		}
		private void daSpin(object sender, System.EventArgs e)
		{
			DoubleAnimation da = new DoubleAnimation();
			da.Completed += new EventHandler(daComplete);
			da.Duration = new Duration(TimeSpan.FromSeconds(TransitionTime/2));
			DoubleAnimation da2 = new DoubleAnimation();
			da2.Duration = new Duration(TimeSpan.FromSeconds(TransitionTime/2));
			da.From = 0;
			da.To = spinDirection;
			da2.From = 0;
			da2.To = 1;
			TransformGroup tg = new TransformGroup();
			RotateTransform rt = new RotateTransform();
			ScaleTransform st = new ScaleTransform();
			tg.Children.Add(rt);
			tg.Children.Add(st);
			frame.RenderTransform = tg;
			rt.BeginAnimation(RotateTransform.AngleProperty, da);
			st.BeginAnimation(ScaleTransform.ScaleXProperty, da2);
			st.BeginAnimation(ScaleTransform.ScaleYProperty, da2);			
		}

        private void daBlur(object sender, System.EventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation();
            da.Completed += new EventHandler(daComplete);
            da.Duration = new Duration(TimeSpan.FromSeconds(TransitionTime / 2));
            BlurEffect effect = frame.Effect as BlurEffect;
            frame.Opacity = 1;
            da.From = 100;
            da.To = 0;
            effect.BeginAnimation(BlurEffect.RadiusProperty, da);
        }
	}	
}
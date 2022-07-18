using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DynamicTreeTestPrj
{
  public partial class MainWindow : Window
  {
    private DispatcherTimer _timer = new DispatcherTimer();

    private List<Rectangle> _playerAABB = new List<Rectangle>();

    private Brush _brush;

    public MainWindow()
    {
      InitializeComponent();

      _timer.Tick += GameLoop;
      _timer.Interval = TimeSpan.FromMilliseconds((20));
      _timer.Start();
    }

    private void GameLoop(object? sender, EventArgs e)
    {
      
    }

    private void ClickOnCanvas(object sender, MouseButtonEventArgs ev)
    {

      Rectangle rect = new Rectangle
      {
        Tag = "Player",
        Height = 100,
        Width = 100,
        Stroke = Brushes.Black,
        StrokeThickness = 1,
      };
      _brush = new SolidColorBrush(Colors.Black);

      Canvas.SetLeft(rect, ev.GetPosition((IInputElement)sender).X);
      Canvas.SetTop(rect, ev.GetPosition((IInputElement)sender).Y);
      MyCanvas.Children.Add(rect);

      string temp = "";
      temp += ev.GetPosition((IInputElement)sender).X;
      temp += " , ";
      temp += ev.GetPosition((IInputElement)sender).Y;

      //RenderTransform = new SkewTransform(0, 0);
      
      TextBox textBox = new TextBox
      {
        Text = temp,
        // AcceptsReturn = true,
        // AcceptsTab = true,
        //
        // TextAlignment = TextAlignment.Center,
        // TextWrapping  = TextWrapping.Wrap,
         //RenderTransform = new RotateTransform(0),
        
       // RenderTransformOrigin = new Point(0.5, 0.5),
        RenderTransform = new ScaleTransform(1, -1),
       

        
        
      };
      Canvas.SetLeft(textBox, ev.GetPosition((IInputElement)sender).X);
      Canvas.SetTop(textBox, ev.GetPosition((IInputElement)sender).Y);
      MyCanvas.Children.Add(textBox);

    }
  }
}
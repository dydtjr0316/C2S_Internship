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
using System.Runtime.Intrinsics;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
namespace DynamicTreeTestPrj
{
  public partial class MainWindow : Window
  {
    private DispatcherTimer _timer = new DispatcherTimer();

    private List<Rectangle> _playerAABB = new List<Rectangle>();

    private Brush _brush;

    private Tree _tree;

    private int _particleCnt = 0;

    public MainWindow()
    {
      InitializeComponent();
      
      Random rand = new Random();
      var nSweeps = 100000; // The number of Monte Carlo sweeps.
      var sampleInterval = 100; // The number of sweeps per sample.
      var nSmall = 1000; // The number of small particles.
      var nLarge = 100; // The number of large particles.
      var diameterSmall = 1.0f; // The diameter of the small particles.
      var diameterLarge = 10.0f; // The diameter of the large particles.
      var density = 0.1f; // The system density.
      var maxDisp = 0.1f; // Maximum trial displacement (in units of diameter).


      List<bool> periodicity = new List<bool>(2) { false, false };

// Work out base length of simulation box.
      var baseLength =
        Math.Pow((Math.PI * (nSmall * diameterSmall + nLarge * diameterLarge)) / (4.0f * density),
          1.0f / 2.0f);

      List<float> boxSize = new List<float>(2) { (float)baseLength, (float)baseLength };

      _tree = new Tree(2, 0.1f, ref periodicity, ref boxSize, 1000);


    }

    private void GameLoop(object? sender, EventArgs e)
    {
      
    }

    private void ClickOnCanvas(object sender, MouseButtonEventArgs ev)
    {
      List<float> tempPos = new List<float>();
      tempPos.Add((float)ev.GetPosition((IInputElement)sender).X);
      tempPos.Add((float)ev.GetPosition((IInputElement)sender).Y);

      if (_particleCnt == 2)
      {
        int i = 0;
      }
      _tree.InsertParticle(_particleCnt,ref tempPos, 40);
      ++_particleCnt;

      for (int i = 0; i < _tree.GetnParticles(); ++i)
      {
        if (!_tree.GetNodes()[i]._isAlloc) continue;
        MyCanvas.Children.Remove(_tree.GetNodes()[i]._rect);
        _tree.GetNodes()[i]._rect = new Rectangle()
        {
          Tag = "Player",
          Height = 
            _tree.GetNodes()[i].GetAABB().GetUppderBound()[1] -
            _tree.GetNodes()[i].GetAABB().GetLowerBound()[1],
          
          Width = _tree.GetNodes()[i].GetAABB().GetUppderBound()[0] -
                  _tree.GetNodes()[i].GetAABB().GetLowerBound()[0],
          Stroke = Brushes.Black,
          StrokeThickness = 1,
        };
        _tree.GetNodes()[i]._brush = new SolidColorBrush(Colors.Black);
        
        Console.Write("Node"+i+"\n");
        Console.Write("Min : "+_tree.GetNodes()[i].GetAABB().GetLowerBound()[0]+" - "+_tree.GetNodes()[i].GetAABB().GetLowerBound()[1]+"\n");
        Console.Write("Max : "+_tree.GetNodes()[i].GetAABB().GetUppderBound()[0]+" - "+_tree.GetNodes()[i].GetAABB().GetUppderBound()[1]+"\n");

        _tree.printTree();
        Canvas.SetLeft(_tree.GetNodes()[i]._rect,100);
        Canvas.SetTop(_tree.GetNodes()[i]._rect, 100);
        MyCanvas.Children.Add(_tree.GetNodes()[i]._rect);
      }

    }
  }
}
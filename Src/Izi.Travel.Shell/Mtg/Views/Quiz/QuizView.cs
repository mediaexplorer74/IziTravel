// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Views.Quiz.QuizView
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Core.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Views.Quiz
{
  public class QuizView : UserControl
  {
    private const double ExpandedMaxHeight = 770.0;
    internal Grid RootGrid;
    internal Border Background;
    internal Image ImageBackground;
    internal Grid GridQuestion;
    internal ViewboxTextBlock TextQuestion;
    internal Rectangle Shadow;
    internal ItemsControl ListAnswer;
    internal Image ImageResult;
    internal CompositeTransform ImageResultTransform;
    internal TextBlock TextResult;
    internal StackPanel PanelButtonResult;
    internal Grid GridExpandedAnswer;
    internal CompositeTransform TransformExpandedAnswer;
    internal ScrollViewer ScrollViewerExpandedAnswer;
    internal TextBlock TextExpandedAnswer;
    private bool _contentLoaded;

    public QuizView() => this.InitializeComponent();

    private void OnTextBlockExpandedAnswerSizeChanged(object sender, SizeChangedEventArgs e)
    {
      if (double.IsNaN(e.NewSize.Height) || e.NewSize.Height <= 0.0)
        return;
      bool flag = e.NewSize.Height > 770.0;
      this.ScrollViewerExpandedAnswer.IsHitTestVisible = flag;
      this.GridExpandedAnswer.Height = !flag ? e.NewSize.Height + 10.0 : 770.0;
    }

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Izi.Travel.Shell;component/Mtg/Views/Quiz/QuizView.xaml", UriKind.Relative));
      this.RootGrid = (Grid) this.FindName("RootGrid");
      this.Background = (Border) this.FindName("Background");
      this.ImageBackground = (Image) this.FindName("ImageBackground");
      this.GridQuestion = (Grid) this.FindName("GridQuestion");
      this.TextQuestion = (ViewboxTextBlock) this.FindName("TextQuestion");
      this.Shadow = (Rectangle) this.FindName("Shadow");
      this.ListAnswer = (ItemsControl) this.FindName("ListAnswer");
      this.ImageResult = (Image) this.FindName("ImageResult");
      this.ImageResultTransform = (CompositeTransform) this.FindName("ImageResultTransform");
      this.TextResult = (TextBlock) this.FindName("TextResult");
      this.PanelButtonResult = (StackPanel) this.FindName("PanelButtonResult");
      this.GridExpandedAnswer = (Grid) this.FindName("GridExpandedAnswer");
      this.TransformExpandedAnswer = (CompositeTransform) this.FindName("TransformExpandedAnswer");
      this.ScrollViewerExpandedAnswer = (ScrollViewer) this.FindName("ScrollViewerExpandedAnswer");
      this.TextExpandedAnswer = (TextBlock) this.FindName("TextExpandedAnswer");
    }
  }
}

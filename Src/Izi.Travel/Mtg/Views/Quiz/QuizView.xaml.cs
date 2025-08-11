// ********************************************************************
// Type: Izi.Travel.Shell.Mtg.Views.Quiz.QuizView
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Core.Controls;
using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Views.Quiz
{
  public partial class QuizView : UserControl
  {
    private const double ExpandedMaxHeight = 770.0;
   
    public QuizView() => this.InitializeComponent();

    private void OnTextBlockExpandedAnswerSizeChanged(object sender, SizeChangedEventArgs e)
    {
      if (double.IsNaN(e.NewSize.Height) || e.NewSize.Height <= 0.0)
        return;
      bool flag = e.NewSize.Height > 770.0;
      this.ScrollViewerExpandedAnswer.IsHitTestVisible = flag;
      this.GridExpandedAnswer.Height = !flag ? e.NewSize.Height + 10.0 : 770.0;
    }

   
  }
}


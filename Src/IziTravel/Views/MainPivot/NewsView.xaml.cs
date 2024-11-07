using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Caliburn.Micro;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace IziTravel.Views.MainPivot
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewsView : Page
    {
        public NewsView()
        {
            this.InitializeComponent();
        }
    }
}
/*
 // Type: IziTravel.Views.MainPivot.NewsView
// Assembly: IziTravel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

using System;
using System.Diagnostics;
using System.Windows;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace IziTravel.Views.MainPivot
{
  public class NewsView : UserControl
  {
    private bool _contentLoaded;

    public NewsView() => this.InitializeComponent();

    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/IziTravel;component/Views/MainPivot/NewsView.xaml", UriKind.Relative));
    }
  }
}

 */
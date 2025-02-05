// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Views.Common.Detail.DetailPartView
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Mtg.Helpers;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Detail;
using Izi.Travel.Utility;
using Microsoft.Phone.Controls;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Views.Common.Detail
{
  public class DetailPartView : PhoneApplicationPage
  {
    private bool _contentLoaded;

    public DetailPartView()
    {
      Counter.Construct("D:\\TeamCity\\buildAgent\\work\\961976af89b2d6d9\\src\\Izi.Travel.Shell\\Mtg\\Views\\Common\\Detail\\DetailPartView.xaml.cs");
      this.InitializeComponent();
    }

    ~DetailPartView()
    {
      Counter.Destruct("D:\\TeamCity\\buildAgent\\work\\961976af89b2d6d9\\src\\Izi.Travel.Shell\\Mtg\\Views\\Common\\Detail\\DetailPartView.xaml.cs");
    }

    protected override void OnBackKeyPress(CancelEventArgs e)
    {
      if (!(this.DataContext is DetailPartViewModel dataContext) || dataContext.MtgObject == null)
        return;
      NavigationHelper.TryGoBack(e, dataContext.MtgObject.ParentUid, dataContext.MtgObject.Language);
    }

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Izi.Travel.Shell;component/Mtg/Views/Common/Detail/DetailPartView.xaml", UriKind.Relative));
    }
  }
}

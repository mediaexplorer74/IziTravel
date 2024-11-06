// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Views.Common.Player.PlayerPartView
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Mtg.Helpers;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Player;
using Microsoft.Phone.Controls;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Views.Common.Player
{
  public class PlayerPartView : PhoneApplicationPage
  {
    internal ContentControl ActiveItem;
    private bool _contentLoaded;

    public PlayerPartView() => this.InitializeComponent();

    protected override void OnBackKeyPress(CancelEventArgs e)
    {
      if (!(this.DataContext is PlayerPartViewModel dataContext) || !(dataContext.ActiveItem is PlayerViewModel activeItem) || activeItem.MtgObject == null)
        return;
      NavigationHelper.TryGoBack(e, activeItem.MtgObject.ParentUid ?? activeItem.MtgObject.Uid, activeItem.MtgObject.Language);
    }

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Izi.Travel.Shell;component/Mtg/Views/Common/Player/PlayerPartView.xaml", UriKind.Relative));
      this.ActiveItem = (ContentControl) this.FindName("ActiveItem");
    }
  }
}

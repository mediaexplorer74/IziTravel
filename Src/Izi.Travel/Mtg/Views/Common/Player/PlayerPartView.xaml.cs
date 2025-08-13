// ********************************************************************
// Type: Izi.Travel.Mtg.Views.Common.Player.PlayerPartView
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Izi.Travel.Mtg.Helpers;
using Izi.Travel.Mtg.ViewModels.Common.Player;

using System;
using System.ComponentModel;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace Izi.Travel.Mtg.Views.Common.Player
{
  public partial class PlayerPartView : Page
  {
    
    public PlayerPartView() => this.InitializeComponent();

    //protected override void OnBackKeyPress(CancelEventArgs e)
    //{
    //  if (!(this.DataContext is PlayerPartViewModel dataContext) || !(dataContext.ActiveItem is PlayerViewModel activeItem) || activeItem.MtgObject == null)
    //    return;
    //  NavigationHelper.TryGoBack(e, activeItem.MtgObject.ParentUid ?? activeItem.MtgObject.Uid, activeItem.MtgObject.Language);
    //}

    
  }
}


// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Services.Contract.IDialogService
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Core.Controls.Flyout;
using Izi.Travel.Shell.Core.Services.Entities;
using System;
using System.Windows;

#nullable disable
namespace Izi.Travel.Shell.Core.Services.Contract
{
  public interface IDialogService
  {
    void Show(
      string title,
      string message,
      MessageBoxButtonContent button,
      Action<FlyoutDialog, MessageBoxResult> callback);

    void Show(
      string title,
      string message,
      MessageBoxButtonContent button,
      Action<FlyoutDialog> prepare,
      Action<FlyoutDialog, MessageBoxResult> callback);

    void ShowToast(
      string message,
      Uri backgroundNavigationUri,
      Action foregroundNavigationAction,
      bool showInBackground,
      string backgroundBrushName = "IziTravelBlueBrush",
      bool vibrateAndSound = false);
  }
}

// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Controls.FlipViewer.FlipViewerItemDisplayedEventArgs
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;

#nullable disable
namespace Izi.Travel.Shell.Core.Controls.FlipViewer
{
  public class FlipViewerItemDisplayedEventArgs : EventArgs
  {
    public int ItemIndex { get; private set; }

    public FlipViewerItemDisplayedEventArgs(int itemIndex) => this.ItemIndex = itemIndex;
  }
}

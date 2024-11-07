// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.ImageTileState
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using System.Windows.Media.Animation;

#nullable disable
namespace Coding4Fun.Toolkit.Controls
{
  public struct ImageTileState
  {
    public Storyboard Storyboard { get; set; }

    public int Row { get; set; }

    public int Column { get; set; }

    public bool ForceLargeImageCleanup { get; set; }
  }
}

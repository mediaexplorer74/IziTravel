// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Controls.FlipViewer.DragState
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;

#nullable disable
namespace Izi.Travel.Shell.Core.Controls.FlipViewer
{
  internal class DragState
  {
    public double MaxDraggingBoundary { get; set; }

    public double MinDraggingBoundary { get; set; }

    public bool GotDragDelta { get; set; }

    public bool IsDraggingFirstElement { get; set; }

    public bool IsDraggingLastElement { get; set; }

    public DateTime LastDragUpdateTime { get; set; }

    public double DragStartingMediaStripOffset { get; set; }

    public double NetDragDistanceSincleLastDragStagnation { get; set; }

    public double LastDragDistanceDelta { get; set; }

    public int NewDisplayedElementIndex { get; set; }

    public double UnsquishTranslationAnimationTarget { get; set; }

    public DragState(double maxDraggingBoundary) => this.MaxDraggingBoundary = maxDraggingBoundary;
  }
}

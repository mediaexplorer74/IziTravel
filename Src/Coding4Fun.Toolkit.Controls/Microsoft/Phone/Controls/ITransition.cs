// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.ITransition
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Windows.Media.Animation;

#nullable disable
namespace Microsoft.Phone.Controls
{
  internal interface ITransition
  {
    event EventHandler Completed;

    ClockState GetCurrentState();

    TimeSpan GetCurrentTime();

    void Pause();

    void Resume();

    void Seek(TimeSpan offset);

    void SeekAlignedToLastTick(TimeSpan offset);

    void SkipToFill();

    void Begin();

    void Stop();
  }
}

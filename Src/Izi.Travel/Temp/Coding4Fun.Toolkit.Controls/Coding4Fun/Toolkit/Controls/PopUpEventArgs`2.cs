﻿// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.PopUpEventArgs`2
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using System;

#nullable disable
namespace Coding4Fun.Toolkit.Controls
{
  public class PopUpEventArgs<T, TPopUpResult> : EventArgs
  {
    public TPopUpResult PopUpResult { get; set; }

    public Exception Error { get; set; }

    public T Result { get; set; }
  }
}

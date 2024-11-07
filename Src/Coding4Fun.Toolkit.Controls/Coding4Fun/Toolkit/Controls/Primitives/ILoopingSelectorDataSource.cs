﻿// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Primitives.ILoopingSelectorDataSource
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Windows.Controls;

#nullable disable
namespace Coding4Fun.Toolkit.Controls.Primitives
{
  public interface ILoopingSelectorDataSource
  {
    object GetNext(object relativeTo);

    object GetPrevious(object relativeTo);

    object SelectedItem { get; set; }

    bool IsEmpty { get; }

    event EventHandler<SelectionChangedEventArgs> SelectionChanged;
  }
}
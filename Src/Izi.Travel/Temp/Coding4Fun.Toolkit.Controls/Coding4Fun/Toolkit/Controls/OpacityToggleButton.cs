﻿// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.OpacityToggleButton
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Windows;

#nullable disable
namespace Coding4Fun.Toolkit.Controls
{
  public class OpacityToggleButton : ToggleButtonBase
  {
    public static readonly DependencyProperty AnimationDurationProperty = DependencyProperty.Register(nameof (AnimationDuration), typeof (Duration), typeof (OpacityToggleButton), new PropertyMetadata((object) new Duration(TimeSpan.FromMilliseconds(100.0))));
    public static readonly DependencyProperty UncheckedOpacityProperty = DependencyProperty.Register(nameof (UncheckedOpacity), typeof (double), typeof (OpacityToggleButton), new PropertyMetadata((object) 0.5));
    public static readonly DependencyProperty CheckedOpacityProperty = DependencyProperty.Register(nameof (CheckedOpacity), typeof (double), typeof (OpacityToggleButton), new PropertyMetadata((object) 1.0));

    public OpacityToggleButton() => this.DefaultStyleKey = (object) typeof (OpacityToggleButton);

    public Duration AnimationDuration
    {
      get => (Duration) this.GetValue(OpacityToggleButton.AnimationDurationProperty);
      set => this.SetValue(OpacityToggleButton.AnimationDurationProperty, (object) value);
    }

    public double UncheckedOpacity
    {
      get => (double) this.GetValue(OpacityToggleButton.UncheckedOpacityProperty);
      set => this.SetValue(OpacityToggleButton.UncheckedOpacityProperty, (object) value);
    }

    public double CheckedOpacity
    {
      get => (double) this.GetValue(OpacityToggleButton.CheckedOpacityProperty);
      set => this.SetValue(OpacityToggleButton.CheckedOpacityProperty, (object) value);
    }
  }
}

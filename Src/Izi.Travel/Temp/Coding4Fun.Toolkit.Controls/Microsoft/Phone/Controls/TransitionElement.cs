// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.TransitionElement
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using System.Windows;

#nullable disable
namespace Microsoft.Phone.Controls
{
  internal abstract class TransitionElement : DependencyObject
  {
    public abstract ITransition GetTransition(UIElement element);
  }
}

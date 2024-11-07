// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Common.IImageSource
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using System.Windows.Media;

#nullable disable
namespace Coding4Fun.Toolkit.Controls.Common
{
  public interface IImageSource
  {
    Stretch Stretch { get; set; }

    ImageSource ImageSource { get; set; }
  }
}

// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.DevelopmentHelpers
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using Coding4Fun.Toolkit.Controls.Common;
using System;

#nullable disable
namespace Coding4Fun.Toolkit.Controls
{
  public static class DevelopmentHelpers
  {
    [Obsolete("Moved to Coding4Fun.Toolkit.Controls.Common.ApplicationSpace")]
    public static bool IsDesignMode => ApplicationSpace.IsDesignMode;

    public static bool IsTypeOf(this object target, Type type) => type.IsInstanceOfType(target);

    public static bool IsTypeOf(this object target, object referenceObject)
    {
      return DevelopmentHelpers.IsTypeOf(target, referenceObject.GetType());
    }
  }
}

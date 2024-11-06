// Decompiled with JetBrains decompiler
// Type: Validation.Requires
// Assembly: System.Collections.Immutable, Version=1.0.34.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: BD72C27E-D8D4-45DB-AA51-7FAB6CCBDAA2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Validation
{
  internal static class Requires
  {
    [DebuggerStepThrough]
    public static T NotNull<T>([ValidatedNotNull] T value, string parameterName) where T : class
    {
      return (object) value != null ? value : throw new ArgumentNullException(parameterName);
    }

    [DebuggerStepThrough]
    public static T NotNullAllowStructs<T>([ValidatedNotNull] T value, string parameterName)
    {
      return (object) value != null ? value : throw new ArgumentNullException(parameterName);
    }

    [DebuggerStepThrough]
    public static void Range(bool condition, string parameterName, string message = null)
    {
      if (condition)
        return;
      Requires.FailRange(parameterName, message);
    }

    [DebuggerStepThrough]
    public static Exception FailRange(string parameterName, string message = null)
    {
      if (string.IsNullOrEmpty(message))
        throw new ArgumentOutOfRangeException(parameterName);
      throw new ArgumentOutOfRangeException(parameterName, message);
    }

    [DebuggerStepThrough]
    public static void Argument(bool condition, string parameterName, string message)
    {
      if (!condition)
        throw new ArgumentException(message, parameterName);
    }

    [DebuggerStepThrough]
    public static void Argument(bool condition)
    {
      if (!condition)
        throw new ArgumentException();
    }
  }
}

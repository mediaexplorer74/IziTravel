// Decompiled with JetBrains decompiler
// Type: System.Collections.Immutable.IOrderedCollection`1
// Assembly: System.Collections.Immutable, Version=1.0.34.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: BD72C27E-D8D4-45DB-AA51-7FAB6CCBDAA2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.xml

using System.Collections.Generic;

#nullable disable
namespace System.Collections.Immutable
{
  internal interface IOrderedCollection<out T> : IEnumerable<T>, IEnumerable
  {
    int Count { get; }

    T this[int index] { get; }
  }
}

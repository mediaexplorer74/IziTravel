// Decompiled with JetBrains decompiler
// Type: System.Collections.Immutable.AllocFreeConcurrentStack`1
// Assembly: System.Collections.Immutable, Version=1.0.34.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: BD72C27E-D8D4-45DB-AA51-7FAB6CCBDAA2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.xml

using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace System.Collections.Immutable
{
  [DebuggerDisplay("Count = {stack.Count}")]
  internal class AllocFreeConcurrentStack<T>
  {
    private readonly Stack<RefAsValueType<T>> stack = new Stack<RefAsValueType<T>>();

    public void TryAdd(T item)
    {
      lock (this.stack)
        this.stack.Push(new RefAsValueType<T>(item));
    }

    public bool TryTake(out T item)
    {
      lock (this.stack)
      {
        if (this.stack.Count > 0)
        {
          item = this.stack.Pop().Value;
          return true;
        }
      }
      item = default (T);
      return false;
    }
  }
}

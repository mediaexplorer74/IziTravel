// Decompiled with JetBrains decompiler
// Type: System.Collections.Immutable.SecurePooledObject`1
// Assembly: System.Collections.Immutable, Version=1.0.34.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: BD72C27E-D8D4-45DB-AA51-7FAB6CCBDAA2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.xml

using System.Threading;
using Validation;

#nullable disable
namespace System.Collections.Immutable
{
  internal class SecurePooledObject<T>
  {
    private readonly T value;
    private Guid owner;

    internal SecurePooledObject(T newValue)
    {
      Requires.NotNullAllowStructs<T>(newValue, nameof (newValue));
      this.value = newValue;
    }

    internal Guid Owner
    {
      get
      {
        lock (this)
          return this.owner;
      }
      set
      {
        lock (this)
          this.owner = value;
      }
    }

    internal SecurePooledObject<T>.SecurePooledObjectUser Use<TCaller>(TCaller caller) where TCaller : ISecurePooledObjectUser
    {
      this.ThrowDisposedIfNotOwned<TCaller>(caller);
      return new SecurePooledObject<T>.SecurePooledObjectUser(this);
    }

    internal void ThrowDisposedIfNotOwned<TCaller>(TCaller caller) where TCaller : ISecurePooledObjectUser
    {
      if (caller.PoolUserId != this.owner)
        throw new ObjectDisposedException(caller.GetType().FullName);
    }

    internal struct SecurePooledObjectUser : IDisposable
    {
      private readonly SecurePooledObject<T> value;

      internal SecurePooledObjectUser(SecurePooledObject<T> value)
      {
        this.value = value;
        Monitor.Enter((object) value);
      }

      internal T Value => this.value.value;

      public void Dispose() => Monitor.Exit((object) this.value);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.ArrayList
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace ICSharpCode.SharpZipLib
{
  /// <summary>Simulate ArrayList</summary>
  internal class ArrayList : List<object>
  {
    public ArrayList()
    {
    }

    public ArrayList(int capacity)
      : base(capacity)
    {
    }

    public int Add(object item)
    {
      base.Add(item);
      return this.Count - 1;
    }

    public void Sort(IComparer comparer)
    {
      this.Sort((IComparer<object>) new ArrayList.PComparer(comparer));
    }

    public virtual Array ToArray(Type type)
    {
      if (type == null)
        throw new ArgumentNullException(nameof (type));
      object[] array = this.ToArray();
      Array instance = Array.CreateInstance(type, array.Length);
      Array.Copy((Array) array, 0, instance, 0, array.Length);
      return instance;
    }

    private class PComparer : IComparer<object>
    {
      private IComparer _Cmp;

      public PComparer(IComparer cmp) => this._Cmp = cmp;

      public int Compare(object x, object y) => this._Cmp.Compare(x, y);
    }
  }
}

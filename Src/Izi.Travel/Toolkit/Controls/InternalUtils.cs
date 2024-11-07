// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Toolkit.Controls.InternalUtils
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

#nullable disable
namespace Izi.Travel.Shell.Toolkit.Controls
{
  internal static class InternalUtils
  {
    internal static bool AreValuesEqual(object o1, object o2)
    {
      if (o1 == o2)
        return true;
      if (o1 == null || o2 == null)
        return false;
      return o1.GetType().IsValueType || o1.GetType() == typeof (string) ? object.Equals(o1, o2) : o1 == o2;
    }
  }
}

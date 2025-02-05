// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Extensions.PropertyChangedExtensions
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

#nullable disable
namespace Izi.Travel.Shell.Core.Extensions
{
  public static class PropertyChangedExtensions
  {
    public static void SetProperty<T>(
      this PropertyChangedBase obj,
      ref T storage,
      T value,
      System.Action action = null,
      [CallerMemberName] string propertyName = null)
    {
      if (object.Equals((object) storage, (object) value))
        return;
      storage = value;
      obj.NotifyOfPropertyChange(propertyName);
      if (action == null)
        return;
      action();
    }

    public static void SetProperty<T, T1>(
      this PropertyChangedBase obj,
      ref T storage,
      T value,
      Expression<Func<T1>> additionalProperty,
      System.Action action = null,
      [CallerMemberName] string propertyName = null)
    {
      if (object.Equals((object) storage, (object) value))
        return;
      storage = value;
      obj.NotifyOfPropertyChange(propertyName);
      obj.NotifyOfPropertyChange<T1>(additionalProperty);
      if (action == null)
        return;
      action();
    }

    public static void SetProperty<T, T1, T2>(
      this PropertyChangedBase obj,
      ref T storage,
      T value,
      Expression<Func<T1>> additionalProperty1,
      Expression<Func<T2>> additionalProperty2,
      System.Action action = null,
      [CallerMemberName] string propertyName = null)
    {
      if (object.Equals((object) storage, (object) value))
        return;
      storage = value;
      obj.NotifyOfPropertyChange(propertyName);
      obj.NotifyOfPropertyChange<T1>(additionalProperty1);
      obj.NotifyOfPropertyChange<T2>(additionalProperty2);
      if (action == null)
        return;
      action();
    }
  }
}

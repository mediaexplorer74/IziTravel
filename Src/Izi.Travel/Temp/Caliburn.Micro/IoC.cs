﻿// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.IoC
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// Used by the framework to pull instances from an IoC container and to inject dependencies into certain existing classes.
  /// </summary>
  public static class IoC
  {
    /// <summary>Gets an instance by type and key.</summary>
    public static Func<Type, string, object> GetInstance = (Func<Type, string, object>) ((service, key) =>
    {
      throw new InvalidOperationException("IoC is not initialized.");
    });
    /// <summary>Gets all instances of a particular type.</summary>
    public static Func<Type, IEnumerable<object>> GetAllInstances = (Func<Type, IEnumerable<object>>) (service =>
    {
      throw new InvalidOperationException("IoC is not initialized.");
    });
    /// <summary>
    /// Passes an existing instance to the IoC container to enable dependencies to be injected.
    /// </summary>
    public static Action<object> BuildUp = (Action<object>) (instance =>
    {
      throw new InvalidOperationException("IoC is not initialized.");
    });

    /// <summary>Gets an instance from the container.</summary>
    /// <typeparam name="T">The type to resolve.</typeparam>
    /// <param name="key">The key to look up.</param>
    /// <returns>The resolved instance.</returns>
    public static T Get<T>(string key = null) => (T) IoC.GetInstance(typeof (T), key);

    /// <summary>Gets all instances of a particular type.</summary>
    /// <typeparam name="T">The type to resolve.</typeparam>
    /// <returns>The resolved instances.</returns>
    public static IEnumerable<T> GetAll<T>() => IoC.GetAllInstances(typeof (T)).Cast<T>();
  }
}

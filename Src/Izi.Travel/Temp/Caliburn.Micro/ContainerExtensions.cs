// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.ContainerExtensions
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// Extension methods for the <see cref="T:Caliburn.Micro.SimpleContainer" />.
  /// </summary>
  public static class ContainerExtensions
  {
    /// <summary>Registers a singleton.</summary>
    /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
    /// <param name="container">The container.</param>
    /// <param name="key">The key.</param>
    /// <returns>The container.</returns>
    public static SimpleContainer Singleton<TImplementation>(
      this SimpleContainer container,
      string key = null)
    {
      return container.Singleton<TImplementation, TImplementation>(key);
    }

    /// <summary>Registers a singleton.</summary>
    /// <typeparam name="TService">The type of the service.</typeparam>
    /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
    /// <param name="container">The container.</param>
    /// <param name="key">The key.</param>
    /// <returns>The container.</returns>
    public static SimpleContainer Singleton<TService, TImplementation>(
      this SimpleContainer container,
      string key = null)
      where TImplementation : TService
    {
      container.RegisterSingleton(typeof (TService), key, typeof (TImplementation));
      return container;
    }

    /// <summary>Registers an service to be created on each request.</summary>
    /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
    /// <param name="container">The container.</param>
    /// <param name="key">The key.</param>
    /// <returns>The container.</returns>
    public static SimpleContainer PerRequest<TImplementation>(
      this SimpleContainer container,
      string key = null)
    {
      return container.PerRequest<TImplementation, TImplementation>(key);
    }

    /// <summary>Registers an service to be created on each request.</summary>
    /// <typeparam name="TService">The type of the service.</typeparam>
    /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
    /// <param name="container">The container.</param>
    /// <param name="key">The key.</param>
    /// <returns>The container.</returns>
    public static SimpleContainer PerRequest<TService, TImplementation>(
      this SimpleContainer container,
      string key = null)
      where TImplementation : TService
    {
      container.RegisterPerRequest(typeof (TService), key, typeof (TImplementation));
      return container;
    }

    /// <summary>Registers an instance with the container.</summary>
    /// <typeparam name="TService">The type of the service.</typeparam>
    /// <param name="container">The container.</param>
    /// <param name="instance">The instance.</param>
    /// <returns>The container.</returns>
    public static SimpleContainer Instance<TService>(
      this SimpleContainer container,
      TService instance)
    {
      container.RegisterInstance(typeof (TService), (string) null, (object) instance);
      return container;
    }

    /// <summary>Registers a custom service handler with the container.</summary>
    /// <typeparam name="TService">The type of the service.</typeparam>
    /// <param name="container">The container.</param>
    /// <param name="handler">The handler.</param>
    /// <returns>The container.</returns>
    public static SimpleContainer Handler<TService>(
      this SimpleContainer container,
      Func<SimpleContainer, object> handler)
    {
      container.RegisterHandler(typeof (TService), (string) null, handler);
      return container;
    }

    /// <summary>
    /// Registers all specified types in an assembly as singleton in the container.
    /// </summary>
    /// <typeparam name="TService">The type of the service.</typeparam>
    /// <param name="container">The container.</param>
    /// <param name="assembly">The assembly.</param>
    /// <param name="filter">The type filter.</param>
    /// <returns>The container.</returns>
    public static SimpleContainer AllTypesOf<TService>(
      this SimpleContainer container,
      Assembly assembly,
      Func<Type, bool> filter = null)
    {
      if (filter == null)
        filter = (Func<Type, bool>) (type => true);
      Type serviceType = typeof (TService);
      foreach (Type implementation in assembly.GetTypes().Where<Type>((Func<Type, bool>) (type => serviceType.IsAssignableFrom(type) && !type.IsAbstract() && !type.IsInterface() && filter(type))))
        container.RegisterSingleton(typeof (TService), (string) null, implementation);
      return container;
    }

    /// <summary>Requests an instance.</summary>
    /// <typeparam name="TService">The type of the service.</typeparam>
    /// <param name="container">The container.</param>
    /// <param name="key">The key.</param>
    /// <returns>The instance.</returns>
    public static TService GetInstance<TService>(this SimpleContainer container, string key = null)
    {
      return (TService) container.GetInstance(typeof (TService), key);
    }

    /// <summary>Gets all instances of a particular type.</summary>
    /// <typeparam name="TService">The type to resolve.</typeparam>
    /// <param name="container">The container.</param>
    /// <returns>The resolved instances.</returns>
    public static IEnumerable<TService> GetAllInstances<TService>(this SimpleContainer container)
    {
      return container.GetAllInstances(typeof (TService)).Cast<TService>();
    }
  }
}

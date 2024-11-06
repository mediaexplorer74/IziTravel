// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.SimpleContainer
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>A simple IoC container.</summary>
  public class SimpleContainer
  {
    private static readonly Type delegateType = typeof (Delegate);
    private static readonly Type enumerableType = typeof (IEnumerable);
    private readonly List<SimpleContainer.ContainerEntry> entries;

    /// <summary>
    ///   Initializes a new instance of the <see cref="T:Caliburn.Micro.SimpleContainer" /> class.
    /// </summary>
    public SimpleContainer() => this.entries = new List<SimpleContainer.ContainerEntry>();

    private SimpleContainer(
      IEnumerable<SimpleContainer.ContainerEntry> entries)
    {
      this.entries = new List<SimpleContainer.ContainerEntry>(entries);
    }

    /// <summary>Registers the instance.</summary>
    /// <param name="service">The service.</param>
    /// <param name="key">The key.</param>
    /// <param name="implementation">The implementation.</param>
    public void RegisterInstance(Type service, string key, object implementation)
    {
      this.RegisterHandler(service, key, (Func<SimpleContainer, object>) (container => implementation));
    }

    /// <summary>
    ///   Registers the class so that a new instance is created on every request.
    /// </summary>
    /// <param name="service">The service.</param>
    /// <param name="key">The key.</param>
    /// <param name="implementation">The implementation.</param>
    public void RegisterPerRequest(Type service, string key, Type implementation)
    {
      this.RegisterHandler(service, key, (Func<SimpleContainer, object>) (container => container.BuildInstance(implementation)));
    }

    /// <summary>
    ///   Registers the class so that it is created once, on first request, and the same instance is returned to all requestors thereafter.
    /// </summary>
    /// <param name="service">The service.</param>
    /// <param name="key">The key.</param>
    /// <param name="implementation">The implementation.</param>
    public void RegisterSingleton(Type service, string key, Type implementation)
    {
      object singleton = (object) null;
      this.RegisterHandler(service, key, (Func<SimpleContainer, object>) (container => singleton ?? (singleton = container.BuildInstance(implementation))));
    }

    /// <summary>
    ///   Registers a custom handler for serving requests from the container.
    /// </summary>
    /// <param name="service">The service.</param>
    /// <param name="key">The key.</param>
    /// <param name="handler">The handler.</param>
    public void RegisterHandler(Type service, string key, Func<SimpleContainer, object> handler)
    {
      this.GetOrCreateEntry(service, key).Add(handler);
    }

    /// <summary>
    ///   Unregisters any handlers for the service/key that have previously been registered.
    /// </summary>
    /// <param name="service">The service.</param>
    /// <param name="key">The key.</param>
    public void UnregisterHandler(Type service, string key)
    {
      SimpleContainer.ContainerEntry entry = this.GetEntry(service, key);
      if (entry == null)
        return;
      this.entries.Remove(entry);
    }

    /// <summary>Requests an instance.</summary>
    /// <param name="service">The service.</param>
    /// <param name="key">The key.</param>
    /// <returns>The instance, or null if a handler is not found.</returns>
    public object GetInstance(Type service, string key)
    {
      SimpleContainer.ContainerEntry entry = this.GetEntry(service, key);
      if (entry != null)
        return entry.Single<Func<SimpleContainer, object>>()(this);
      if (service == null)
        return (object) null;
      if (SimpleContainer.delegateType.IsAssignableFrom(service))
      {
        Type type = typeof (SimpleContainer.FactoryFactory<>).MakeGenericType(service.GetGenericArguments()[0]);
        object instance = Activator.CreateInstance(type);
        return type.GetMethod("Create", new Type[1]
        {
          typeof (SimpleContainer)
        }).Invoke(instance, new object[1]{ (object) this });
      }
      if (!SimpleContainer.enumerableType.IsAssignableFrom(service) || !service.IsGenericType())
        return (object) null;
      Type genericArgument = service.GetGenericArguments()[0];
      List<object> list = this.GetAllInstances(genericArgument).ToList<object>();
      Array instance1 = Array.CreateInstance(genericArgument, list.Count);
      for (int index = 0; index < instance1.Length; ++index)
        instance1.SetValue(list[index], index);
      return (object) instance1;
    }

    /// <summary>
    /// Determines if a handler for the service/key has previously been registered.
    /// </summary>
    /// <param name="service">The service.</param>
    /// <param name="key">The key.</param>
    /// <returns>True if a handler is registere; false otherwise.</returns>
    public bool HasHandler(Type service, string key) => this.GetEntry(service, key) != null;

    /// <summary>Requests all instances of a given type.</summary>
    /// <param name="service">The service.</param>
    /// <returns>All the instances or an empty enumerable if none are found.</returns>
    public IEnumerable<object> GetAllInstances(Type service)
    {
      SimpleContainer.ContainerEntry entry = this.GetEntry(service, (string) null);
      return entry == null ? (IEnumerable<object>) new object[0] : entry.Select<Func<SimpleContainer, object>, object>((Func<Func<SimpleContainer, object>, object>) (x => x(this)));
    }

    /// <summary>
    ///   Pushes dependencies into an existing instance based on interface properties with setters.
    /// </summary>
    /// <param name="instance">The instance.</param>
    public void BuildUp(object instance)
    {
      foreach (PropertyInfo propertyInfo in instance.GetType().GetProperties().Where<PropertyInfo>((Func<PropertyInfo, bool>) (property => property.CanRead && property.CanWrite && property.PropertyType.IsInterface())))
      {
        object[] array = this.GetAllInstances(propertyInfo.PropertyType).ToArray<object>();
        if (((IEnumerable<object>) array).Any<object>())
          propertyInfo.SetValue(instance, ((IEnumerable<object>) array).First<object>(), (object[]) null);
      }
    }

    /// <summary>Creates a child container.</summary>
    /// <returns>A new container.</returns>
    public SimpleContainer CreateChildContainer()
    {
      return new SimpleContainer((IEnumerable<SimpleContainer.ContainerEntry>) this.entries);
    }

    private SimpleContainer.ContainerEntry GetOrCreateEntry(Type service, string key)
    {
      SimpleContainer.ContainerEntry entry = this.GetEntry(service, key);
      if (entry == null)
      {
        entry = new SimpleContainer.ContainerEntry()
        {
          Service = service,
          Key = key
        };
        this.entries.Add(entry);
      }
      return entry;
    }

    private SimpleContainer.ContainerEntry GetEntry(Type service, string key)
    {
      if (service == null)
        return this.entries.FirstOrDefault<SimpleContainer.ContainerEntry>((Func<SimpleContainer.ContainerEntry, bool>) (x => x.Key == key));
      return key == null ? this.entries.FirstOrDefault<SimpleContainer.ContainerEntry>((Func<SimpleContainer.ContainerEntry, bool>) (x => x.Service == service && x.Key == null)) ?? this.entries.FirstOrDefault<SimpleContainer.ContainerEntry>((Func<SimpleContainer.ContainerEntry, bool>) (x => x.Service == service)) : this.entries.FirstOrDefault<SimpleContainer.ContainerEntry>((Func<SimpleContainer.ContainerEntry, bool>) (x => x.Service == service && x.Key == key));
    }

    /// <summary>
    ///   Actually does the work of creating the instance and satisfying it's constructor dependencies.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    protected object BuildInstance(Type type)
    {
      object[] constructorArgs = this.DetermineConstructorArgs(type);
      return this.ActivateInstance(type, constructorArgs);
    }

    /// <summary>
    ///   Creates an instance of the type with the specified constructor arguments.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="args">The constructor args.</param>
    /// <returns>The created instance.</returns>
    protected virtual object ActivateInstance(Type type, object[] args)
    {
      object obj = args.Length > 0 ? Activator.CreateInstance(type, args) : Activator.CreateInstance(type);
      this.Activated(obj);
      return obj;
    }

    /// <summary>Occurs when a new instance is created.</summary>
    public event Action<object> Activated = param0 => { };

    private object[] DetermineConstructorArgs(Type implementation)
    {
      List<object> objectList = new List<object>();
      ConstructorInfo constructorInfo = SimpleContainer.SelectEligibleConstructor(implementation);
      if (constructorInfo != null)
        objectList.AddRange(((IEnumerable<ParameterInfo>) constructorInfo.GetParameters()).Select<ParameterInfo, object>((Func<ParameterInfo, object>) (info => this.GetInstance(info.ParameterType, (string) null))));
      return objectList.ToArray();
    }

    private static ConstructorInfo SelectEligibleConstructor(Type type)
    {
      return type.GetConstructors().OrderByDescending<ConstructorInfo, int>((Func<ConstructorInfo, int>) (c => c.GetParameters().Length)).FirstOrDefault<ConstructorInfo>();
    }

    private class ContainerEntry : List<Func<SimpleContainer, object>>
    {
      public string Key;
      public Type Service;
    }

    private class FactoryFactory<T>
    {
      public Func<T> Create(SimpleContainer container)
      {
        return (Func<T>) (() => (T) container.GetInstance(typeof (T), (string) null));
      }
    }
  }
}

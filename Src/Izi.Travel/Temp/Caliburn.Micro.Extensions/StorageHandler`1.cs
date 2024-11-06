// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.StorageHandler`1
// Assembly: Caliburn.Micro.Extensions, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: F2ADA3C9-2FAD-4D48-AC26-D2E113F06E6E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>Handles the storage of a pariticular class.</summary>
  /// <typeparam name="T">The type that this class handles.</typeparam>
  public abstract class StorageHandler<T> : IStorageHandler
  {
    private readonly List<StorageInstruction<T>> instructions = new List<StorageInstruction<T>>();
    private Func<T, object> getId = (Func<T, object>) (instance => (object) null);

    /// <summary>
    /// Provides a mechanism for obtaining an instance's unique id.
    /// </summary>
    /// <param name="getter">The getter.</param>
    public void Id(Func<T, object> getter) => this.getId = getter;

    /// <summary>Gets or sets the coordinator.</summary>
    /// <value>The coordinator.</value>
    public StorageCoordinator Coordinator { get; set; }

    /// <summary>
    /// Overrided by inheritors to configure the handler for use.
    /// </summary>
    public abstract void Configure();

    /// <summary>
    /// Instructs the handler to store the entire object graph, rather than individual properties.
    /// </summary>
    /// <typeparam name="TService">The type of the service.</typeparam>
    /// <param name="storageKey">The optional storage key.</param>
    /// <returns>The builder.</returns>
    public StorageInstructionBuilder<T> EntireGraph<TService>(string storageKey = "ObjectGraph")
    {
      return this.AddInstruction().Configure((Action<StorageInstruction<T>>) (x =>
      {
        x.Key = storageKey;
        x.Save = (Action<T, Func<string>, StorageMode>) ((instance, getKey, mode) => x.StorageMechanism.Store(getKey(), (object) instance));
        x.Restore = (Action<T, Func<string>, StorageMode>) ((instance, getKey, mode) => { });
        x.PropertyChanged += (PropertyChangedEventHandler) ((s, e) =>
        {
          if (!(e.PropertyName == "StorageMechanism") || x.StorageMechanism == null)
            return;
          x.StorageMechanism.RegisterSingleton(typeof (TService), this.GetKey(default (T), x.Key), typeof (T));
        });
      }));
    }

    /// <summary>Instructs the handler to store a property.</summary>
    /// <param name="property">The property.</param>
    /// <returns>The builder.</returns>
    public StorageInstructionBuilder<T> Property(Expression<Func<T, object>> property)
    {
      PropertyInfo info = (PropertyInfo) property.GetMemberInfo();
      return this.AddInstruction().Configure((Action<StorageInstruction<T>>) (x =>
      {
        x.Key = info.Name;
        x.Save = (Action<T, Func<string>, StorageMode>) ((instance, getKey, mode) => x.StorageMechanism.Store(getKey(), info.GetValue((object) instance, (object[]) null)));
        x.Restore = (Action<T, Func<string>, StorageMode>) ((instance, getKey, mode) =>
        {
          string key = getKey();
          object obj;
          if (!x.StorageMechanism.TryGet(key, out obj))
            return;
          x.StorageMechanism.Delete(key);
          info.SetValue((object) instance, obj, (object[]) null);
        });
      }));
    }

    /// <summary>
    /// Instructs the handler to store a child object's properties.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <returns>The builder.</returns>
    /// <remarks>This assumes that the parent instance provides the child instance, but that the child instance's properties are handled by a unique handler.</remarks>
    public StorageInstructionBuilder<T> Child(Expression<Func<T, object>> property)
    {
      PropertyInfo info = (PropertyInfo) property.GetMemberInfo();
      return this.AddInstruction().Configure((Action<StorageInstruction<T>>) (x =>
      {
        x.Key = info.Name;
        x.Save = (Action<T, Func<string>, StorageMode>) ((instance, getKey, mode) =>
        {
          object instance3 = info.GetValue((object) instance, (object[]) null);
          if (instance3 == null)
            return;
          this.Coordinator.GetStorageHandlerFor(instance3).Save(instance3, mode);
        });
        x.Restore = (Action<T, Func<string>, StorageMode>) ((instance, getKey, mode) =>
        {
          object instance4 = info.GetValue((object) instance, (object[]) null);
          if (instance4 == null)
            return;
          this.Coordinator.GetStorageHandlerFor(instance4).Restore(instance4, mode);
        });
      }));
    }

    /// <summary>Adds a new storage instruction.</summary>
    /// <returns>The builder.</returns>
    public StorageInstructionBuilder<T> AddInstruction()
    {
      StorageInstruction<T> storageInstruction = new StorageInstruction<T>()
      {
        Owner = (IStorageHandler) this
      };
      this.instructions.Add(storageInstruction);
      return new StorageInstructionBuilder<T>(storageInstruction);
    }

    /// <summary>
    /// Uses this handler to save a particular instance using instructions that support the provided mode.
    /// </summary>
    /// <param name="instance">The instance.</param>
    /// <param name="mode">The storage mode.</param>
    public virtual void Save(T instance, StorageMode mode)
    {
      foreach (StorageInstruction<T> instruction in this.instructions)
      {
        string key = instruction.Key;
        if (instruction.StorageMechanism.Supports(mode))
          instruction.Save(instance, (Func<string>) (() => this.GetKey(instance, key)), mode);
      }
    }

    /// <summary>
    /// Uses this handler to restore a particular instance using instructions that support the provided mode.
    /// </summary>
    /// <param name="instance">The instance.</param>
    /// <param name="mode">The mode.</param>
    public virtual void Restore(T instance, StorageMode mode)
    {
      foreach (StorageInstruction<T> instruction in this.instructions)
      {
        string key = instruction.Key;
        if (instruction.StorageMechanism.Supports(mode))
          instruction.Restore(instance, (Func<string>) (() => this.GetKey(instance, key)), mode);
      }
    }

    private string GetKey(T instance, string detailKey)
    {
      object obj = this.getId(instance);
      return typeof (T).FullName + (obj == null ? "" : "_" + obj) + "_" + detailKey;
    }

    bool IStorageHandler.Handles(object instance) => instance is T;

    void IStorageHandler.Save(object instance, StorageMode mode) => this.Save((T) instance, mode);

    void IStorageHandler.Restore(object instance, StorageMode mode)
    {
      this.Restore((T) instance, mode);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.StorageCoordinator
// Assembly: Caliburn.Micro.Extensions, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: F2ADA3C9-2FAD-4D48-AC26-D2E113F06E6E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.xml

using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// Coordinates the saving and loading of objects based on application lifecycle events.
  /// </summary>
  public class StorageCoordinator
  {
    private readonly List<IStorageHandler> handlers = new List<IStorageHandler>();
    private readonly IPhoneContainer container;
    private readonly IPhoneService phoneService;
    private readonly List<IStorageMechanism> storageMechanisms;
    private readonly List<WeakReference> tracked = new List<WeakReference>();
    private StorageMode currentRestoreMode = StorageMode.Permanent;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Caliburn.Micro.StorageCoordinator" /> class.
    /// </summary>
    /// <param name="container">The container.</param>
    /// <param name="phoneService">The phone service.</param>
    /// <param name="storageMechanisms">The storage mechanisms.</param>
    /// <param name="handlers">The handlers.</param>
    public StorageCoordinator(
      IPhoneContainer container,
      IPhoneService phoneService,
      IEnumerable<IStorageMechanism> storageMechanisms,
      IEnumerable<IStorageHandler> handlers)
    {
      StorageCoordinator storageCoordinator = this;
      this.container = container;
      this.phoneService = phoneService;
      this.storageMechanisms = storageMechanisms.ToList<IStorageMechanism>();
      handlers.Apply<IStorageHandler>((Action<IStorageHandler>) (x => this.AddStorageHandler(x)));
      phoneService.Resurrecting += (System.Action) (() => this.currentRestoreMode = StorageMode.Any);
      phoneService.Continuing += (System.Action) (() => storageMechanisms.Apply<IStorageMechanism>((Action<IStorageMechanism>) (x => x.ClearLastSession())));
    }

    /// <summary>Starts monitoring application and container events.</summary>
    public void Start()
    {
      this.phoneService.Closing += new EventHandler<ClosingEventArgs>(this.OnClosing);
      this.phoneService.Deactivated += new EventHandler<DeactivatedEventArgs>(this.OnDeactivated);
      this.container.Activated += new Action<object>(this.OnContainerActivated);
    }

    /// <summary>Stops monitoring application and container events.</summary>
    public void Stop()
    {
      this.phoneService.Closing -= new EventHandler<ClosingEventArgs>(this.OnClosing);
      this.phoneService.Deactivated -= new EventHandler<DeactivatedEventArgs>(this.OnDeactivated);
      this.container.Activated -= new Action<object>(this.OnContainerActivated);
    }

    /// <summary>Gets the storage mechanism.</summary>
    /// <typeparam name="T">The type of storage mechanism to get.</typeparam>
    /// <returns>The storage mechanism.</returns>
    public T GetStorageMechanism<T>() where T : IStorageMechanism
    {
      return this.storageMechanisms.OfType<T>().FirstOrDefault<T>();
    }

    /// <summary>Adds the storage mechanism.</summary>
    /// <param name="storageMechanism">The storage mechanism.</param>
    public void AddStorageMechanism(IStorageMechanism storageMechanism)
    {
      this.storageMechanisms.Add(storageMechanism);
    }

    /// <summary>Adds the storage handler.</summary>
    /// <param name="handler">The handler.</param>
    /// <returns>Itself</returns>
    public StorageCoordinator AddStorageHandler(IStorageHandler handler)
    {
      handler.Coordinator = this;
      handler.Configure();
      this.handlers.Add(handler);
      return this;
    }

    /// <summary>Gets the storage handler for a paricular instance.</summary>
    /// <param name="instance">The instance.</param>
    /// <returns>The storage handler.</returns>
    public IStorageHandler GetStorageHandlerFor(object instance)
    {
      return this.handlers.FirstOrDefault<IStorageHandler>((Func<IStorageHandler, bool>) (x => x.Handles(instance)));
    }

    /// <summary>
    /// Saves all monitored instances according to the provided mode.
    /// </summary>
    /// <param name="saveMode">The save mode.</param>
    public void Save(StorageMode saveMode)
    {
      IEnumerable<object> objects = this.tracked.Select<WeakReference, object>((Func<WeakReference, object>) (x => x.Target)).Where<object>((Func<object, bool>) (x => x != null));
      IEnumerable<IStorageMechanism> enumerable = this.storageMechanisms.Where<IStorageMechanism>((Func<IStorageMechanism, bool>) (x => x.Supports(saveMode)));
      enumerable.Apply<IStorageMechanism>((Action<IStorageMechanism>) (x => x.BeginStoring()));
      foreach (object instance in objects)
        this.GetStorageHandlerFor(instance).Save(instance, saveMode);
      enumerable.Apply<IStorageMechanism>((Action<IStorageMechanism>) (x => x.EndStoring()));
    }

    /// <summary>Restores the specified instance.</summary>
    /// <param name="instance">The instance.</param>
    /// <param name="restoreMode">The restore mode.</param>
    public void Restore(object instance, StorageMode restoreMode = StorageMode.Automatic)
    {
      IStorageHandler storageHandlerFor = this.GetStorageHandlerFor(instance);
      if (storageHandlerFor == null)
        return;
      this.tracked.Add(new WeakReference(instance));
      storageHandlerFor.Restore(instance, restoreMode == StorageMode.Automatic ? this.currentRestoreMode : restoreMode);
    }

    private void OnDeactivated(object sender, DeactivatedEventArgs e) => this.Save(StorageMode.Any);

    private void OnClosing(object sender, ClosingEventArgs e) => this.Save(StorageMode.Permanent);

    private void OnContainerActivated(object instance) => this.Restore(instance);
  }
}

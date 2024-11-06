// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.PhoneContainer
// Assembly: Caliburn.Micro.Extensions, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: F2ADA3C9-2FAD-4D48-AC26-D2E113F06E6E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.xml

using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Controls;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// A custom IoC container which integrates with the phone and properly registers all Caliburn.Micro services.
  /// </summary>
  public class PhoneContainer : SimpleContainer, IPhoneContainer
  {
    /// <summary>
    /// Registers the service as a singleton stored in the phone state.
    /// </summary>
    /// <param name="service">The service.</param>
    /// <param name="phoneStateKey">The phone state key.</param>
    /// <param name="implementation">The implementation.</param>
    public void RegisterWithPhoneService(Type service, string phoneStateKey, Type implementation)
    {
      IPhoneService instance1 = (IPhoneService) this.GetInstance(typeof (IPhoneService), (string) null);
      if (instance1 == null)
        throw new InvalidOperationException("IPhoneService instance cannot be found.");
      if (!instance1.State.ContainsKey(phoneStateKey ?? service.FullName))
        instance1.State[phoneStateKey ?? service.FullName] = this.BuildInstance(implementation);
      this.RegisterHandler(service, phoneStateKey, (Func<SimpleContainer, object>) (container =>
      {
        IPhoneService instance2 = (IPhoneService) container.GetInstance(typeof (IPhoneService), (string) null);
        return instance2.State.ContainsKey(phoneStateKey ?? service.FullName) ? instance2.State[phoneStateKey ?? service.FullName] : this.BuildInstance(implementation);
      }));
    }

    /// <summary>
    /// Registers the service as a singleton stored in the app settings.
    /// </summary>
    /// <param name="service">The service.</param>
    /// <param name="appSettingsKey">The app settings key.</param>
    /// <param name="implementation">The implementation.</param>
    public void RegisterWithAppSettings(Type service, string appSettingsKey, Type implementation)
    {
      if (!IsolatedStorageSettings.ApplicationSettings.Contains(appSettingsKey ?? service.FullName))
        IsolatedStorageSettings.ApplicationSettings[appSettingsKey ?? service.FullName] = this.BuildInstance(implementation);
      this.RegisterHandler(service, appSettingsKey, (Func<SimpleContainer, object>) (container => IsolatedStorageSettings.ApplicationSettings.Contains(appSettingsKey ?? service.FullName) ? IsolatedStorageSettings.ApplicationSettings[appSettingsKey ?? service.FullName] : this.BuildInstance(implementation)));
    }

    /// <summary>
    /// Registers the Caliburn.Micro services with the container.
    /// </summary>
    /// <param name="rootFrame">The root frame of the application.</param>
    /// <param name="treatViewAsLoaded">if set to <c>true</c> [treat view as loaded].</param>
    public virtual void RegisterPhoneServices(Frame rootFrame, bool treatViewAsLoaded = false)
    {
      this.RegisterInstance(typeof (SimpleContainer), (string) null, (object) this);
      this.RegisterInstance(typeof (PhoneContainer), (string) null, (object) this);
      this.RegisterInstance(typeof (IPhoneContainer), (string) null, (object) this);
      if (!this.HasHandler(typeof (INavigationService), (string) null))
      {
        if (rootFrame == null)
          throw new ArgumentNullException(nameof (rootFrame));
        this.RegisterInstance(typeof (INavigationService), (string) null, (object) new FrameAdapter(rootFrame, treatViewAsLoaded));
      }
      if (!this.HasHandler(typeof (IPhoneService), (string) null))
      {
        if (rootFrame == null)
          throw new ArgumentNullException(nameof (rootFrame));
        this.RegisterInstance(typeof (IPhoneService), (string) null, (object) new PhoneApplicationServiceAdapter(PhoneApplicationService.Current ?? throw new InvalidOperationException("PhoneApplicationService is not yet initialized."), rootFrame));
      }
      if (!this.HasHandler(typeof (IEventAggregator), (string) null))
        this.RegisterSingleton(typeof (IEventAggregator), (string) null, typeof (EventAggregator));
      if (!this.HasHandler(typeof (IWindowManager), (string) null))
        this.RegisterSingleton(typeof (IWindowManager), (string) null, typeof (WindowManager));
      if (!this.HasHandler(typeof (IVibrateController), (string) null))
        this.RegisterSingleton(typeof (IVibrateController), (string) null, typeof (SystemVibrateController));
      if (!this.HasHandler(typeof (ISoundEffectPlayer), (string) null))
        this.RegisterSingleton(typeof (ISoundEffectPlayer), (string) null, typeof (XnaSoundEffectPlayer));
      this.EnableStorageCoordinator();
      this.EnableTaskController();
    }

    /// <summary>
    /// Enable the <see cref="T:Caliburn.Micro.StorageCoordinator" />.
    /// </summary>
    protected void EnableStorageCoordinator()
    {
      foreach (Assembly assembly in ((IEnumerable<Assembly>) AssemblySource.Instance.ToArray<Assembly>()).Union<Assembly>((IEnumerable<Assembly>) new Assembly[1]
      {
        typeof (IStorageMechanism).Assembly
      }))
      {
        this.AllTypesOf<IStorageMechanism>(assembly);
        this.AllTypesOf<IStorageHandler>(assembly);
      }
      this.RegisterSingleton(typeof (StorageCoordinator), (string) null, typeof (StorageCoordinator));
      ((StorageCoordinator) this.GetInstance(typeof (StorageCoordinator), (string) null)).Start();
    }

    /// <summary>
    /// Enable the <see cref="T:Caliburn.Micro.TaskController" />.
    /// </summary>
    protected void EnableTaskController()
    {
      this.RegisterSingleton(typeof (TaskController), (string) null, typeof (TaskController));
      ((TaskController) this.GetInstance(typeof (TaskController), (string) null)).Start();
    }

    [SpecialName]
    void IPhoneContainer.add_Activated([In] Action<object> obj0) => this.Activated += obj0;

    [SpecialName]
    void IPhoneContainer.remove_Activated([In] Action<object> obj0) => this.Activated -= obj0;
  }
}

// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.Extras.ModuleBootstrapperBase
// Assembly: Caliburn.Micro.Extras, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75D6380B-EA35-437B-8CE3-40FC8C25A394
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extras.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extras.xml

using System;
using System.Linq;
using System.Reflection;

#nullable disable
namespace Caliburn.Micro.Extras
{
  /// <summary>Base class for all module bootstrappers.</summary>
  public abstract class ModuleBootstrapperBase : IModuleBootstrapper
  {
    /// <summary>Gets or sets the IoC container.</summary>
    public IPhoneContainer Container { get; set; }

    /// <summary>Initializes the module.</summary>
    public virtual void Initialize()
    {
      if (!(this.Container is PhoneContainer container))
        throw new InvalidOperationException("Container has to be initialized.");
      this.ConfigureStorageMechanismsAndWorkers((SimpleContainer) container);
      this.Configure(container);
    }

    /// <summary>
    /// Identify, load and configure all instances of <see cref="T:Caliburn.Micro.IStorageMechanism" /> and <see cref="T:Caliburn.Micro.IStorageHandler" />
    /// that are defined in the assembly associated with this bootstrapper.
    /// </summary>
    /// <param name="phoneContainer">The currently configured <see cref="T:Caliburn.Micro.PhoneContainer" />.</param>
    /// <remarks>
    /// Caliburn Micro will automatically load storage handlers and storage mechanisms from the assemblies configured
    /// in <see cref="F:Caliburn.Micro.AssemblySource.Instance" /> when <see cref="M:Caliburn.Micro.PhoneContainer.RegisterPhoneServices(System.Windows.Controls.Frame,System.Boolean)" /> is first invoked.
    /// Since the purpose of this bootstrapper is to allow the delayed loading of assemblies, it makes sense to locate
    /// the storage handlers alongside the view models in the same assembly.
    /// </remarks>
    private void ConfigureStorageMechanismsAndWorkers(SimpleContainer phoneContainer)
    {
      StorageCoordinator coordinator = (StorageCoordinator) phoneContainer.GetInstance(typeof (StorageCoordinator), (string) null);
      Assembly assembly = this.GetType().Assembly;
      phoneContainer.AllTypesOf<IStorageMechanism>(assembly);
      phoneContainer.AllTypesOf<IStorageHandler>(assembly);
      phoneContainer.GetAllInstances(typeof (IStorageMechanism)).Where<object>((Func<object, bool>) (m => object.ReferenceEquals((object) m.GetType().Assembly, (object) assembly))).Apply<object>((Action<object>) (m => coordinator.AddStorageMechanism((IStorageMechanism) m)));
      phoneContainer.GetAllInstances(typeof (IStorageHandler)).Where<object>((Func<object, bool>) (h => object.ReferenceEquals((object) h.GetType().Assembly, (object) assembly))).Apply<object>((Action<object>) (h => coordinator.AddStorageHandler((IStorageHandler) h)));
    }

    /// <summary>Override to setup the IoC container for this module.</summary>
    /// <param name="container">The parent IoC container.</param>
    protected abstract void Configure(PhoneContainer container);
  }
}

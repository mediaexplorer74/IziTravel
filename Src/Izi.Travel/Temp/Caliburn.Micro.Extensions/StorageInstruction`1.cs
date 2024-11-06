// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.StorageInstruction`1
// Assembly: Caliburn.Micro.Extensions, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: F2ADA3C9-2FAD-4D48-AC26-D2E113F06E6E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.xml

using System;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>An instruction for saving/loading data.</summary>
  /// <typeparam name="T">The model type.</typeparam>
  public class StorageInstruction<T> : PropertyChangedBase
  {
    private IStorageHandler owner;
    private IStorageMechanism storageMechanism;
    private string key;
    private Action<T, Func<string>, StorageMode> save;
    private Action<T, Func<string>, StorageMode> restore;

    /// <summary>Gets or sets the owner.</summary>
    /// <value>The owner.</value>
    public IStorageHandler Owner
    {
      get => this.owner;
      set
      {
        this.owner = value;
        this.NotifyOfPropertyChange(nameof (Owner));
      }
    }

    /// <summary>Gets or sets the storage mechanism.</summary>
    /// <value>The storage mechanism.</value>
    public IStorageMechanism StorageMechanism
    {
      get => this.storageMechanism;
      set
      {
        this.storageMechanism = value;
        this.NotifyOfPropertyChange(nameof (StorageMechanism));
      }
    }

    /// <summary>Gets or sets the persistence key.</summary>
    /// <value>The key.</value>
    public string Key
    {
      get => this.key;
      set
      {
        this.key = value;
        this.NotifyOfPropertyChange(nameof (Key));
      }
    }

    /// <summary>Gets or sets the save action.</summary>
    /// <value>The save action.</value>
    public Action<T, Func<string>, StorageMode> Save
    {
      get => this.save;
      set
      {
        this.save = value;
        this.NotifyOfPropertyChange(nameof (Save));
      }
    }

    /// <summary>Gets or sets the restore action.</summary>
    /// <value>The restore action.</value>
    public Action<T, Func<string>, StorageMode> Restore
    {
      get => this.restore;
      set
      {
        this.restore = value;
        this.NotifyOfPropertyChange(nameof (Restore));
      }
    }
  }
}

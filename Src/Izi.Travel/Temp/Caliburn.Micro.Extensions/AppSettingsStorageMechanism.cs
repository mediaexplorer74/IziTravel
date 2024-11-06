// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.AppSettingsStorageMechanism
// Assembly: Caliburn.Micro.Extensions, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: F2ADA3C9-2FAD-4D48-AC26-D2E113F06E6E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.xml

using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>Stores data in the application settings.</summary>
  public class AppSettingsStorageMechanism : IStorageMechanism
  {
    private readonly IPhoneContainer container;
    private List<string> keys;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Caliburn.Micro.AppSettingsStorageMechanism" /> class.
    /// </summary>
    /// <param name="container">The container.</param>
    public AppSettingsStorageMechanism(IPhoneContainer container) => this.container = container;

    /// <summary>Indicates what storage modes this mechanism provides.</summary>
    /// <param name="mode">The storage mode to check.</param>
    /// <returns>Whether or not it is supported.</returns>
    public bool Supports(StorageMode mode)
    {
      return (mode & StorageMode.Permanent) == StorageMode.Permanent;
    }

    /// <summary>Begins the storage transaction.</summary>
    public void BeginStoring() => this.keys = new List<string>();

    /// <summary>Stores the value with the specified key.</summary>
    /// <param name="key">The key.</param>
    /// <param name="data">The data.</param>
    public void Store(string key, object data)
    {
      if (!IsolatedStorageSettings.ApplicationSettings.Contains(key))
        this.keys.Add(key);
      IsolatedStorageSettings.ApplicationSettings[key] = data;
    }

    /// <summary>Ends the storage transaction.</summary>
    public void EndStoring() => IsolatedStorageSettings.ApplicationSettings.Save();

    /// <summary>
    /// Tries to get the data previously stored with the specified key.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <returns>true if found; false otherwise</returns>
    public bool TryGet(string key, out object value)
    {
      return IsolatedStorageSettings.ApplicationSettings.TryGetValue<object>(key, out value);
    }

    /// <summary>Deletes the data with the specified key.</summary>
    /// <param name="key">The key.</param>
    public void Delete(string key) => IsolatedStorageSettings.ApplicationSettings.Remove(key);

    /// <summary>Clears the data stored in the last storage transaction.</summary>
    public void ClearLastSession()
    {
      if (this.keys == null)
        return;
      this.keys.Apply<string>((Action<string>) (x => IsolatedStorageSettings.ApplicationSettings.Remove(x)));
      this.keys = (List<string>) null;
    }

    /// <summary>
    /// Registers service with the storage mechanism as a singleton.
    /// </summary>
    /// <param name="service">The service.</param>
    /// <param name="key">The key.</param>
    /// <param name="implementation">The implementation.</param>
    public void RegisterSingleton(Type service, string key, Type implementation)
    {
      this.container.RegisterWithAppSettings(service, key, implementation);
    }
  }
}

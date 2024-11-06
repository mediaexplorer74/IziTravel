// Decompiled with JetBrains decompiler
// Type: PCLStorage.FileSystem
// Assembly: PCLStorage, Version=1.0.2.0, Culture=neutral, PublicKeyToken=286fe515a2c35b64
// MVID: C962FBF1-A378-45AB-97C6-C265F8F0F86C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\PCLStorage.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\PCLStorage.xml

using System;
using System.Threading;

#nullable disable
namespace PCLStorage
{
  /// <summary>
  /// Provides access to an implementation of <see cref="T:PCLStorage.IFileSystem" /> for the current platform
  /// </summary>
  public static class FileSystem
  {
    private static Lazy<IFileSystem> _fileSystem = new Lazy<IFileSystem>((Func<IFileSystem>) (() => FileSystem.CreateFileSystem()), LazyThreadSafetyMode.PublicationOnly);

    /// <summary>
    /// The implementation of <see cref="T:PCLStorage.IFileSystem" /> for the current platform
    /// </summary>
    public static IFileSystem Current
    {
      get => FileSystem._fileSystem.Value ?? throw FileSystem.NotImplementedInReferenceAssembly();
    }

    private static IFileSystem CreateFileSystem() => (IFileSystem) new WinRTFileSystem();

    internal static Exception NotImplementedInReferenceAssembly()
    {
      return (Exception) new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the PCLStorage NuGet package from your main application project in order to reference the platform-specific implementation.");
    }
  }
}

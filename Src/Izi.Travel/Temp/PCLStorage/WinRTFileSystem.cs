// Decompiled with JetBrains decompiler
// Type: PCLStorage.WinRTFileSystem
// Assembly: PCLStorage, Version=1.0.2.0, Culture=neutral, PublicKeyToken=286fe515a2c35b64
// MVID: C962FBF1-A378-45AB-97C6-C265F8F0F86C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\PCLStorage.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\PCLStorage.xml

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;

#nullable disable
namespace PCLStorage
{
  /// <summary>
  /// Implementation of <see cref="T:PCLStorage.IFileSystem" /> over WinRT Storage APIs
  /// </summary>
  public class WinRTFileSystem : IFileSystem
  {
    private ApplicationData _applicationData;

    /// <summary>
    /// Creates a new instance of <see cref="T:PCLStorage.WinRTFileSystem" />
    /// </summary>
    public WinRTFileSystem() => this._applicationData = ApplicationData.Current;

    /// <summary>
    /// A folder representing storage which is local to the current device
    /// </summary>
    public IFolder LocalStorage
    {
      get => (IFolder) new WinRTFolder((IStorageFolder) this._applicationData.LocalFolder);
    }

    /// <summary>
    /// A folder representing storage which may be synced with other devices for the same user
    /// </summary>
    public IFolder RoamingStorage => (IFolder) null;

    /// <summary>
    /// Gets a file, given its path.  Returns null if the file does not exist.
    /// </summary>
    /// <param name="path">The path to a file, as returned from the <see cref="P:PCLStorage.IFile.Path" /> property.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A file for the given path, or null if it does not exist.</returns>
    public async Task<IFile> GetFileFromPathAsync(string path, CancellationToken cancellationToken)
    {
      Requires.NotNullOrEmpty(path, nameof (path));
      StorageFile storageFile;
      try
      {
        storageFile = await StorageFile.GetFileFromPathAsync(path).AsTask<StorageFile>(cancellationToken).ConfigureAwait(false);
      }
      catch (FileNotFoundException ex)
      {
        return (IFile) null;
      }
      return (IFile) new WinRTFile((IStorageFile) storageFile);
    }

    /// <summary>
    /// Gets a folder, given its path.  Returns null if the folder does not exist.
    /// </summary>
    /// <param name="path">The path to a folder, as returned from the <see cref="P:PCLStorage.IFolder.Path" /> property.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A folder for the specified path, or null if it does not exist.</returns>
    public async Task<IFolder> GetFolderFromPathAsync(
      string path,
      CancellationToken cancellationToken)
    {
      Requires.NotNullOrEmpty(path, nameof (path));
      StorageFolder storageFolder;
      try
      {
        storageFolder = await StorageFolder.GetFolderFromPathAsync(path).AsTask<StorageFolder>(cancellationToken).ConfigureAwait(false);
      }
      catch (FileNotFoundException ex)
      {
        return (IFolder) null;
      }
      return (IFolder) new WinRTFolder((IStorageFolder) storageFolder);
    }
  }
}

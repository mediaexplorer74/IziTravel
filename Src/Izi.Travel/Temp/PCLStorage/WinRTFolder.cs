// Decompiled with JetBrains decompiler
// Type: PCLStorage.WinRTFolder
// Assembly: PCLStorage, Version=1.0.2.0, Culture=neutral, PublicKeyToken=286fe515a2c35b64
// MVID: C962FBF1-A378-45AB-97C6-C265F8F0F86C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\PCLStorage.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\PCLStorage.xml

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;

#nullable disable
namespace PCLStorage
{
  /// <summary>
  /// Represents a folder in the <see cref="T:PCLStorage.WinRTFileSystem" />
  /// </summary>
  [DebuggerDisplay("Name = {Name}")]
  public class WinRTFolder : IFolder
  {
    private readonly IStorageFolder _wrappedFolder;
    private readonly bool _isRootFolder;

    /// <summary>
    /// Creates a new <see cref="T:PCLStorage.WinRTFolder" />
    /// </summary>
    /// <param name="wrappedFolder">The WinRT <see cref="T:Windows.Storage.IStorageFolder" /> to wrap</param>
    public WinRTFolder(IStorageFolder wrappedFolder)
    {
      this._wrappedFolder = wrappedFolder;
      if (((IStorageItem) this._wrappedFolder).Path == ApplicationData.Current.LocalFolder.Path)
        this._isRootFolder = true;
      else
        this._isRootFolder = false;
    }

    /// <summary>The name of the folder</summary>
    public string Name => ((IStorageItem) this._wrappedFolder).Name;

    /// <summary>
    /// The "full path" of the folder, which should uniquely identify it within a given <see cref="T:PCLStorage.IFileSystem" />
    /// </summary>
    public string Path => ((IStorageItem) this._wrappedFolder).Path;

    /// <summary>Creates a file in this folder</summary>
    /// <param name="desiredName">The name of the file to create</param>
    /// <param name="option">Specifies how to behave if the specified file already exists</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The newly created file</returns>
    public async Task<IFile> CreateFileAsync(
      string desiredName,
      CreationCollisionOption option,
      CancellationToken cancellationToken)
    {
      Requires.NotNullOrEmpty(desiredName, nameof (desiredName));
      await this.EnsureExistsAsync(cancellationToken).ConfigureAwait(false);
      StorageFile wrtFile;
      try
      {
        wrtFile = await this._wrappedFolder.CreateFileAsync(desiredName, this.GetWinRTCreationCollisionOption(option)).AsTask<StorageFile>(cancellationToken).ConfigureAwait(false);
      }
      catch (Exception ex)
      {
        if (ex.HResult == -2147024713)
          throw new IOException(ex.Message, ex);
        throw;
      }
      return (IFile) new WinRTFile((IStorageFile) wrtFile);
    }

    /// <summary>Gets a file in this folder</summary>
    /// <param name="name">The name of the file to get</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The requested file, or null if it does not exist</returns>
    public async Task<IFile> GetFileAsync(string name, CancellationToken cancellationToken)
    {
      Requires.NotNullOrEmpty(name, nameof (name));
      await this.EnsureExistsAsync(cancellationToken).ConfigureAwait(false);
      IFile fileAsync;
      try
      {
        StorageFile wrtFile = await this._wrappedFolder.GetFileAsync(name).AsTask<StorageFile>(cancellationToken).ConfigureAwait(false);
        fileAsync = (IFile) new WinRTFile((IStorageFile) wrtFile);
      }
      catch (System.IO.FileNotFoundException ex)
      {
        throw new PCLStorage.Exceptions.FileNotFoundException(ex.Message, (Exception) ex);
      }
      return fileAsync;
    }

    /// <summary>Gets a list of the files in this folder</summary>
    /// <returns>A list of the files in the folder</returns>
    public async Task<IList<IFile>> GetFilesAsync(CancellationToken cancellationToken)
    {
      await this.EnsureExistsAsync(cancellationToken).ConfigureAwait(false);
      IReadOnlyList<StorageFile> wrtFiles = await this._wrappedFolder.GetFilesAsync().AsTask<IReadOnlyList<StorageFile>>(cancellationToken).ConfigureAwait(false);
      List<IFile> files = ((IEnumerable<IFile>) wrtFiles.Select<StorageFile, WinRTFile>((Func<StorageFile, WinRTFile>) (f => new WinRTFile((IStorageFile) f)))).ToList<IFile>();
      return (IList<IFile>) new ReadOnlyCollection<IFile>((IList<IFile>) files);
    }

    /// <summary>Creates a subfolder in this folder</summary>
    /// <param name="desiredName">The name of the folder to create</param>
    /// <param name="option">Specifies how to behave if the specified folder already exists</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The newly created folder</returns>
    public async Task<IFolder> CreateFolderAsync(
      string desiredName,
      CreationCollisionOption option,
      CancellationToken cancellationToken)
    {
      Requires.NotNullOrEmpty(desiredName, nameof (desiredName));
      await this.EnsureExistsAsync(cancellationToken).ConfigureAwait(false);
      StorageFolder wrtFolder;
      try
      {
        wrtFolder = await this._wrappedFolder.CreateFolderAsync(desiredName, this.GetWinRTCreationCollisionOption(option)).AsTask<StorageFolder>(cancellationToken).ConfigureAwait(false);
      }
      catch (Exception ex)
      {
        if (ex.HResult == -2147024713)
          throw new IOException(ex.Message, ex);
        throw;
      }
      return (IFolder) new WinRTFolder((IStorageFolder) wrtFolder);
    }

    /// <summary>Gets a subfolder in this folder</summary>
    /// <param name="name">The name of the folder to get</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The requested folder, or null if it does not exist</returns>
    public async Task<IFolder> GetFolderAsync(string name, CancellationToken cancellationToken)
    {
      Requires.NotNullOrEmpty(name, nameof (name));
      await this.EnsureExistsAsync(cancellationToken).ConfigureAwait(false);
      StorageFolder wrtFolder;
      try
      {
        wrtFolder = await this._wrappedFolder.GetFolderAsync(name).AsTask<StorageFolder>(cancellationToken).ConfigureAwait(false);
      }
      catch (System.IO.FileNotFoundException ex)
      {
        throw new PCLStorage.Exceptions.DirectoryNotFoundException(ex.Message, (Exception) ex);
      }
      return (IFolder) new WinRTFolder((IStorageFolder) wrtFolder);
    }

    /// <summary>Gets a list of subfolders in this folder</summary>
    /// <returns>A list of subfolders in the folder</returns>
    public async Task<IList<IFolder>> GetFoldersAsync(CancellationToken cancellationToken)
    {
      await this.EnsureExistsAsync(cancellationToken).ConfigureAwait(false);
      IReadOnlyList<StorageFolder> wrtFolders = await this._wrappedFolder.GetFoldersAsync().AsTask<IReadOnlyList<StorageFolder>>(cancellationToken).ConfigureAwait(false);
      List<IFolder> folders = ((IEnumerable<IFolder>) wrtFolders.Select<StorageFolder, WinRTFolder>((Func<StorageFolder, WinRTFolder>) (f => new WinRTFolder((IStorageFolder) f)))).ToList<IFolder>();
      return (IList<IFolder>) new ReadOnlyCollection<IFolder>((IList<IFolder>) folders);
    }

    /// <summary>
    /// Checks whether a folder or file exists at the given location.
    /// </summary>
    /// <param name="name">The name of the file or folder to check for.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// A task whose result is the result of the existence check.
    /// </returns>
    public async Task<ExistenceCheckResult> CheckExistsAsync(
      string name,
      CancellationToken cancellationToken)
    {
      Requires.NotNullOrEmpty(name, nameof (name));
      Task<IStorageItem> result = await this._wrappedFolder.GetItemAsync(name).AsTaskNoThrow<IStorageItem>(cancellationToken);
      if (result.IsFaulted)
      {
        if (result.Exception.InnerException is System.IO.FileNotFoundException)
          return ExistenceCheckResult.NotFound;
        result.GetAwaiter().GetResult();
        throw result.Exception;
      }
      IStorageItem istorageItem = !result.IsCanceled ? result.Result : throw new OperationCanceledException();
      return !istorageItem.IsOfType((StorageItemTypes) 1) ? (!istorageItem.IsOfType((StorageItemTypes) 2) ? ExistenceCheckResult.NotFound : ExistenceCheckResult.FolderExists) : ExistenceCheckResult.FileExists;
    }

    /// <summary>Deletes this folder and all of its contents</summary>
    /// <returns>A task which will complete after the folder is deleted</returns>
    public async Task DeleteAsync(CancellationToken cancellationToken)
    {
      await this.EnsureExistsAsync(cancellationToken).ConfigureAwait(false);
      if (this._isRootFolder)
        throw new IOException("Cannot delete root storage folder.");
      await ((IStorageItem) this._wrappedFolder).DeleteAsync().AsTask(cancellationToken).ConfigureAwait(false);
    }

    private CreationCollisionOption GetWinRTCreationCollisionOption(CreationCollisionOption option)
    {
      switch (option)
      {
        case CreationCollisionOption.GenerateUniqueName:
          return (CreationCollisionOption) 0;
        case CreationCollisionOption.ReplaceExisting:
          return (CreationCollisionOption) 1;
        case CreationCollisionOption.FailIfExists:
          return (CreationCollisionOption) 2;
        case CreationCollisionOption.OpenIfExists:
          return (CreationCollisionOption) 3;
        default:
          throw new ArgumentException("Unrecognized CreationCollisionOption value: " + (object) option);
      }
    }

    private async Task EnsureExistsAsync(CancellationToken cancellationToken)
    {
      try
      {
        StorageFolder storageFolder = await StorageFolder.GetFolderFromPathAsync(this.Path).AsTask<StorageFolder>(cancellationToken).ConfigureAwait(false);
      }
      catch (System.IO.FileNotFoundException ex)
      {
        throw new PCLStorage.Exceptions.DirectoryNotFoundException(ex.Message, (Exception) ex);
      }
    }
  }
}

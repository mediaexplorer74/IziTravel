// Decompiled with JetBrains decompiler
// Type: PCLStorage.WinRTFile
// Assembly: PCLStorage, Version=1.0.2.0, Culture=neutral, PublicKeyToken=286fe515a2c35b64
// MVID: C962FBF1-A378-45AB-97C6-C265F8F0F86C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\PCLStorage.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\PCLStorage.xml

using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

#nullable disable
namespace PCLStorage
{
  /// <summary>
  /// Represents a file in the <see cref="T:PCLStorage.WinRTFileSystem" />
  /// </summary>
  [DebuggerDisplay("Name = {Name}")]
  public class WinRTFile : IFile
  {
    /// <summary>
    /// The HRESULT on a System.Exception thrown when a file collision occurs.
    /// </summary>
    internal const int FILE_ALREADY_EXISTS = -2147024713;
    private readonly IStorageFile _wrappedFile;

    /// <summary>
    /// Creates a new <see cref="T:PCLStorage.WinRTFile" />
    /// </summary>
    /// <param name="wrappedFile">The WinRT <see cref="T:Windows.Storage.IStorageFile" /> to wrap</param>
    public WinRTFile(IStorageFile wrappedFile) => this._wrappedFile = wrappedFile;

    /// <summary>The name of the file</summary>
    public string Name => ((IStorageItem) this._wrappedFile).Name;

    /// <summary>
    /// The "full path" of the file, which should uniquely identify it within a given <see cref="T:PCLStorage.IFileSystem" />
    /// </summary>
    public string Path => ((IStorageItem) this._wrappedFile).Path;

    /// <summary>Opens the file</summary>
    /// <param name="fileAccess">Specifies whether the file should be opened in read-only or read/write mode</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A <see cref="T:System.IO.Stream" /> which can be used to read from or write to the file</returns>
    public async Task<Stream> OpenAsync(FileAccess fileAccess, CancellationToken cancellationToken)
    {
      FileAccessMode fileAccessMode;
      if (fileAccess == FileAccess.Read)
      {
        fileAccessMode = (FileAccessMode) 0;
      }
      else
      {
        if (fileAccess != FileAccess.ReadAndWrite)
          throw new ArgumentException("Unrecognized FileAccess value: " + (object) fileAccess);
        fileAccessMode = (FileAccessMode) 1;
      }
      IRandomAccessStream wrtStream = await this._wrappedFile.OpenAsync(fileAccessMode).AsTask<IRandomAccessStream>(cancellationToken).ConfigureAwait(false);
      return wrtStream.AsStream();
    }

    /// <summary>Deletes the file</summary>
    /// <returns>A task which will complete after the file is deleted.</returns>
    public async Task DeleteAsync(CancellationToken cancellationToken)
    {
      cancellationToken.ThrowIfCancellationRequested();
      await ((IStorageItem) this._wrappedFile).DeleteAsync().AsTask(cancellationToken).ConfigureAwait(false);
    }

    /// <summary>Renames a file without changing its location.</summary>
    /// <param name="newName">The new leaf name of the file.</param>
    /// <param name="collisionOption">How to deal with collisions with existing files.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task which will complete after the file is renamed.</returns>
    public async Task RenameAsync(
      string newName,
      NameCollisionOption collisionOption,
      CancellationToken cancellationToken)
    {
      Requires.NotNullOrEmpty(newName, nameof (newName));
      try
      {
        await ((IStorageItem) this._wrappedFile).RenameAsync(newName, (NameCollisionOption) collisionOption).AsTask(cancellationToken).ConfigureAwait(false);
      }
      catch (Exception ex)
      {
        if (ex.HResult == -2147024713)
          throw new IOException("File already exists.", ex);
        throw;
      }
    }

    /// <summary>Moves a file.</summary>
    /// <param name="newPath">The new full path of the file.</param>
    /// <param name="collisionOption">How to deal with collisions with existing files.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task which will complete after the file is moved.</returns>
    public async Task MoveAsync(
      string newPath,
      NameCollisionOption collisionOption,
      CancellationToken cancellationToken)
    {
      Requires.NotNullOrEmpty(newPath, nameof (newPath));
      StorageFolder newFolder = await StorageFolder.GetFolderFromPathAsync(System.IO.Path.GetDirectoryName(newPath)).AsTask<StorageFolder>(cancellationToken).ConfigureAwait(false);
      string newName = System.IO.Path.GetFileName(newPath);
      try
      {
        await this._wrappedFile.MoveAsync((IStorageFolder) newFolder, newName, (NameCollisionOption) collisionOption).AsTask(cancellationToken).ConfigureAwait(false);
      }
      catch (Exception ex)
      {
        if (ex.HResult == -2147024713)
          throw new IOException("File already exists.", ex);
        throw;
      }
    }
  }
}

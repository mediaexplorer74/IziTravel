// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.BaseArchiveStorage
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System.IO;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>
  /// An abstract <see cref="T:ICSharpCode.SharpZipLib.Zip.IArchiveStorage" /> suitable for extension by inheritance.
  /// </summary>
  public abstract class BaseArchiveStorage : IArchiveStorage
  {
    private FileUpdateMode updateMode_;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:ICSharpCode.SharpZipLib.Zip.BaseArchiveStorage" /> class.
    /// </summary>
    /// <param name="updateMode">The update mode.</param>
    protected BaseArchiveStorage(FileUpdateMode updateMode) => this.updateMode_ = updateMode;

    /// <summary>
    /// Gets a temporary output <see cref="T:System.IO.Stream" />
    /// </summary>
    /// <returns>Returns the temporary output stream.</returns>
    /// <seealso cref="M:ICSharpCode.SharpZipLib.Zip.BaseArchiveStorage.ConvertTemporaryToFinal"></seealso>
    public abstract Stream GetTemporaryOutput();

    /// <summary>
    /// Converts the temporary <see cref="T:System.IO.Stream" /> to its final form.
    /// </summary>
    /// <returns>Returns a <see cref="T:System.IO.Stream" /> that can be used to read
    /// the final storage for the archive.</returns>
    /// <seealso cref="M:ICSharpCode.SharpZipLib.Zip.BaseArchiveStorage.GetTemporaryOutput" />
    public abstract Stream ConvertTemporaryToFinal();

    /// <summary>
    /// Make a temporary copy of a <see cref="T:System.IO.Stream" />.
    /// </summary>
    /// <param name="stream">The <see cref="T:System.IO.Stream" /> to make a copy of.</param>
    /// <returns>Returns a temporary output <see cref="T:System.IO.Stream" /> that is a copy of the input.</returns>
    public abstract Stream MakeTemporaryCopy(Stream stream);

    /// <summary>
    /// Return a stream suitable for performing direct updates on the original source.
    /// </summary>
    /// <param name="stream">The <see cref="T:System.IO.Stream" /> to open for direct update.</param>
    /// <returns>Returns a stream suitable for direct updating.</returns>
    public abstract Stream OpenForDirectUpdate(Stream stream);

    /// <summary>Disposes this instance.</summary>
    public abstract void Dispose();

    /// <summary>Gets the update mode applicable.</summary>
    /// <value>The update mode.</value>
    public FileUpdateMode UpdateMode => this.updateMode_;
  }
}

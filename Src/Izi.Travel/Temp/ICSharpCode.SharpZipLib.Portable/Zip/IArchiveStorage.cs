// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.IArchiveStorage
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System.IO;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>
  /// Defines facilities for data storage when updating Zip Archives.
  /// </summary>
  public interface IArchiveStorage
  {
    /// <summary>
    /// Get the <see cref="T:ICSharpCode.SharpZipLib.Zip.FileUpdateMode" /> to apply during updates.
    /// </summary>
    FileUpdateMode UpdateMode { get; }

    /// <summary>
    /// Get an empty <see cref="T:System.IO.Stream" /> that can be used for temporary output.
    /// </summary>
    /// <returns>Returns a temporary output <see cref="T:System.IO.Stream" /></returns>
    /// <seealso cref="M:ICSharpCode.SharpZipLib.Zip.IArchiveStorage.ConvertTemporaryToFinal"></seealso>
    Stream GetTemporaryOutput();

    /// <summary>Convert a temporary output stream to a final stream.</summary>
    /// <returns>The resulting final <see cref="T:System.IO.Stream" /></returns>
    /// <seealso cref="M:ICSharpCode.SharpZipLib.Zip.IArchiveStorage.GetTemporaryOutput" />
    Stream ConvertTemporaryToFinal();

    /// <summary>Make a temporary copy of the original stream.</summary>
    /// <param name="stream">The <see cref="T:System.IO.Stream" /> to copy.</param>
    /// <returns>Returns a temporary output <see cref="T:System.IO.Stream" /> that is a copy of the input.</returns>
    Stream MakeTemporaryCopy(Stream stream);

    /// <summary>
    /// Return a stream suitable for performing direct updates on the original source.
    /// </summary>
    /// <param name="stream">The current stream.</param>
    /// <returns>Returns a stream suitable for direct updating.</returns>
    /// <remarks>This may be the current stream passed.</remarks>
    Stream OpenForDirectUpdate(Stream stream);

    /// <summary>Dispose of this instance.</summary>
    void Dispose();
  }
}

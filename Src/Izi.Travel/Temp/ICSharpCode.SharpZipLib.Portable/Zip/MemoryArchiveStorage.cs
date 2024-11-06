// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.MemoryArchiveStorage
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using ICSharpCode.SharpZipLib.Core;
using System.IO;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>
  /// An <see cref="T:ICSharpCode.SharpZipLib.Zip.IArchiveStorage" /> implementation suitable for in memory streams.
  /// </summary>
  public class MemoryArchiveStorage : BaseArchiveStorage
  {
    private MemoryStream temporaryStream_;
    private MemoryStream finalStream_;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:ICSharpCode.SharpZipLib.Zip.MemoryArchiveStorage" /> class.
    /// </summary>
    public MemoryArchiveStorage()
      : base(FileUpdateMode.Direct)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:ICSharpCode.SharpZipLib.Zip.MemoryArchiveStorage" /> class.
    /// </summary>
    /// <param name="updateMode">The <see cref="T:ICSharpCode.SharpZipLib.Zip.FileUpdateMode" /> to use</param>
    /// <remarks>This constructor is for testing as memory streams dont really require safe mode.</remarks>
    public MemoryArchiveStorage(FileUpdateMode updateMode)
      : base(updateMode)
    {
    }

    /// <summary>
    /// Get the stream returned by <see cref="M:ICSharpCode.SharpZipLib.Zip.MemoryArchiveStorage.ConvertTemporaryToFinal" /> if this was in fact called.
    /// </summary>
    public MemoryStream FinalStream => this.finalStream_;

    /// <summary>
    /// Gets the temporary output <see cref="T:System.IO.Stream" />
    /// </summary>
    /// <returns>Returns the temporary output stream.</returns>
    public override Stream GetTemporaryOutput()
    {
      this.temporaryStream_ = new MemoryStream();
      return (Stream) this.temporaryStream_;
    }

    /// <summary>
    /// Converts the temporary <see cref="T:System.IO.Stream" /> to its final form.
    /// </summary>
    /// <returns>Returns a <see cref="T:System.IO.Stream" /> that can be used to read
    /// the final storage for the archive.</returns>
    public override Stream ConvertTemporaryToFinal()
    {
      this.finalStream_ = this.temporaryStream_ != null ? new MemoryStream(this.temporaryStream_.ToArray()) : throw new ZipException("No temporary stream has been created");
      return (Stream) this.finalStream_;
    }

    /// <summary>Make a temporary copy of the original stream.</summary>
    /// <param name="stream">The <see cref="T:System.IO.Stream" /> to copy.</param>
    /// <returns>Returns a temporary output <see cref="T:System.IO.Stream" /> that is a copy of the input.</returns>
    public override Stream MakeTemporaryCopy(Stream stream)
    {
      this.temporaryStream_ = new MemoryStream();
      stream.Position = 0L;
      StreamUtils.Copy(stream, (Stream) this.temporaryStream_, new byte[4096]);
      return (Stream) this.temporaryStream_;
    }

    /// <summary>
    /// Return a stream suitable for performing direct updates on the original source.
    /// </summary>
    /// <param name="stream">The original source stream</param>
    /// <returns>Returns a stream suitable for direct updating.</returns>
    /// <remarks>If the <paramref name="stream" /> passed is not null this is used;
    /// otherwise a new <see cref="T:System.IO.MemoryStream" /> is returned.</remarks>
    public override Stream OpenForDirectUpdate(Stream stream)
    {
      Stream destination;
      if (stream == null || !stream.CanWrite)
      {
        destination = (Stream) new MemoryStream();
        if (stream != null)
        {
          stream.Position = 0L;
          StreamUtils.Copy(stream, destination, new byte[4096]);
          stream.Dispose();
        }
      }
      else
        destination = stream;
      return destination;
    }

    /// <summary>Disposes this instance.</summary>
    public override void Dispose()
    {
      if (this.temporaryStream_ == null)
        return;
      this.temporaryStream_.Dispose();
    }
  }
}

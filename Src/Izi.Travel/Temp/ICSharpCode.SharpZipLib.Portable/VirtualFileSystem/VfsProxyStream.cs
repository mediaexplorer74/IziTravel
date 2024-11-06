// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.VirtualFileSystem.VfsProxyStream
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;
using System.IO;

#nullable disable
namespace ICSharpCode.SharpZipLib.VirtualFileSystem
{
  /// <summary>Stream proxy for VFS Stream</summary>
  public class VfsProxyStream : VfsStream
  {
    private string _Name;
    private Stream _Stream;

    /// <summary>Base stream</summary>
    protected Stream Stream
    {
      get
      {
        return this._Stream != null ? this._Stream : throw new ObjectDisposedException(nameof (VfsProxyStream));
      }
    }

    /// <summary>Create a new proxy stream</summary>
    public VfsProxyStream(Stream stream, string name)
    {
      this._Stream = stream != null ? stream : throw new ArgumentNullException(nameof (stream));
      this._Name = name;
    }

    /// <summary>Dispose resources</summary>
    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (!disposing || this._Stream == null)
        return;
      this._Stream.Dispose();
      this._Stream = (Stream) null;
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Flush() => this.Stream.Flush();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="offset"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public override int Read(byte[] buffer, int offset, int count)
    {
      return this.Stream.Read(buffer, offset, count);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="offset"></param>
    /// <param name="origin"></param>
    /// <returns></returns>
    public override long Seek(long offset, SeekOrigin origin) => this.Stream.Seek(offset, origin);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    public override void SetLength(long value) => this.Stream.SetLength(value);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="offset"></param>
    /// <param name="count"></param>
    public override void Write(byte[] buffer, int offset, int count)
    {
      this.Stream.Write(buffer, offset, count);
    }

    /// <summary>Name</summary>
    public override string Name => this._Name;

    /// <summary>
    /// 
    /// </summary>
    public override bool CanRead => this.Stream.CanRead;

    /// <summary>
    /// 
    /// </summary>
    public override bool CanSeek => this.Stream.CanSeek;

    /// <summary>
    /// 
    /// </summary>
    public override bool CanWrite => this.Stream.CanWrite;

    /// <summary>
    /// 
    /// </summary>
    public override long Length => this.Stream.Length;

    /// <summary>
    /// 
    /// </summary>
    public override long Position
    {
      get => this.Stream.Position;
      set => this.Stream.Position = value;
    }
  }
}

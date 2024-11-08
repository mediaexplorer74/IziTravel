// Decompiled with JetBrains decompiler
// Type: RestSharp.Compression.ZLib.ZlibBaseStream
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace RestSharp.Compression.ZLib
{
  internal class ZlibBaseStream : Stream
  {
    protected internal ZlibCodec _z;
    protected internal ZlibBaseStream.StreamMode _streamMode = ZlibBaseStream.StreamMode.Undefined;
    protected internal FlushType _flushMode;
    protected internal ZlibStreamFlavor _flavor;
    protected internal bool _leaveOpen;
    protected internal byte[] _workingBuffer;
    protected internal int _bufferSize = 8192;
    protected internal byte[] _buf1 = new byte[1];
    protected internal Stream _stream;
    private CRC32 crc;
    protected internal string _GzipFileName;
    protected internal string _GzipComment;
    protected internal DateTime _GzipMtime;
    protected internal int _gzipHeaderByteCount;
    private bool nomoreinput;

    internal int Crc32 => this.crc == null ? 0 : this.crc.Crc32Result;

    public ZlibBaseStream(Stream stream, ZlibStreamFlavor flavor, bool leaveOpen)
    {
      this._flushMode = FlushType.None;
      this._stream = stream;
      this._leaveOpen = leaveOpen;
      this._flavor = flavor;
      if (flavor != ZlibStreamFlavor.GZIP)
        return;
      this.crc = new CRC32();
    }

    private ZlibCodec z
    {
      get
      {
        if (this._z == null)
        {
          bool expectRfc1950Header = this._flavor == ZlibStreamFlavor.ZLIB;
          this._z = new ZlibCodec();
          this._z.InitializeInflate(expectRfc1950Header);
        }
        return this._z;
      }
    }

    private byte[] workingBuffer
    {
      get
      {
        if (this._workingBuffer == null)
          this._workingBuffer = new byte[this._bufferSize];
        return this._workingBuffer;
      }
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
      if (this.crc != null)
        this.crc.SlurpBlock(buffer, offset, count);
      if (this._streamMode == ZlibBaseStream.StreamMode.Undefined)
        this._streamMode = ZlibBaseStream.StreamMode.Writer;
      else if (this._streamMode != ZlibBaseStream.StreamMode.Writer)
        throw new ZlibException("Cannot Write after Reading.");
      if (count == 0)
        return;
      this.z.InputBuffer = buffer;
      this._z.NextIn = offset;
      this._z.AvailableBytesIn = count;
      bool flag;
      do
      {
        this._z.OutputBuffer = this.workingBuffer;
        this._z.NextOut = 0;
        this._z.AvailableBytesOut = this._workingBuffer.Length;
        switch (this._z.Inflate(this._flushMode))
        {
          case 0:
          case 1:
            this._stream.Write(this._workingBuffer, 0, this._workingBuffer.Length - this._z.AvailableBytesOut);
            flag = this._z.AvailableBytesIn == 0 && this._z.AvailableBytesOut != 0;
            if (this._flavor == ZlibStreamFlavor.GZIP)
              flag = this._z.AvailableBytesIn == 8 && this._z.AvailableBytesOut != 0;
            continue;
          default:
            throw new ZlibException("inflating: " + this._z.Message);
        }
      }
      while (!flag);
    }

    private void finish()
    {
      if (this._z == null)
        return;
      if (this._streamMode == ZlibBaseStream.StreamMode.Writer)
      {
        bool flag;
        do
        {
          this._z.OutputBuffer = this.workingBuffer;
          this._z.NextOut = 0;
          this._z.AvailableBytesOut = this._workingBuffer.Length;
          switch (this._z.Inflate(FlushType.Finish))
          {
            case 0:
            case 1:
              if (this._workingBuffer.Length - this._z.AvailableBytesOut > 0)
                this._stream.Write(this._workingBuffer, 0, this._workingBuffer.Length - this._z.AvailableBytesOut);
              flag = this._z.AvailableBytesIn == 0 && this._z.AvailableBytesOut != 0;
              if (this._flavor == ZlibStreamFlavor.GZIP)
                flag = this._z.AvailableBytesIn == 8 && this._z.AvailableBytesOut != 0;
              continue;
            default:
              throw new ZlibException("inflating: " + this._z.Message);
          }
        }
        while (!flag);
        this.Flush();
        if (this._flavor == ZlibStreamFlavor.GZIP)
          throw new ZlibException("Writing with decompression is not supported.");
      }
      else
      {
        if (this._streamMode != ZlibBaseStream.StreamMode.Reader || this._flavor != ZlibStreamFlavor.GZIP || this._z.TotalBytesOut == 0L)
          return;
        byte[] destinationArray = new byte[8];
        if (this._z.AvailableBytesIn != 8)
          throw new ZlibException(string.Format("Protocol error. AvailableBytesIn={0}, expected 8", (object) this._z.AvailableBytesIn));
        Array.Copy((Array) this._z.InputBuffer, this._z.NextIn, (Array) destinationArray, 0, destinationArray.Length);
        int int32_1 = BitConverter.ToInt32(destinationArray, 0);
        int crc32Result = this.crc.Crc32Result;
        int int32_2 = BitConverter.ToInt32(destinationArray, 4);
        int num = (int) (this._z.TotalBytesOut & (long) uint.MaxValue);
        if (crc32Result != int32_1)
          throw new ZlibException(string.Format("Bad CRC32 in GZIP stream. (actual({0:X8})!=expected({1:X8}))", (object) crc32Result, (object) int32_1));
        if (num != int32_2)
          throw new ZlibException(string.Format("Bad size in GZIP stream. (actual({0})!=expected({1}))", (object) num, (object) int32_2));
      }
    }

    private void end()
    {
      if (this.z == null)
        return;
      this._z.EndInflate();
      this._z = (ZlibCodec) null;
    }

    public /*override*/ void Close()
    {
      if (this._stream == null)
        return;
      try
      {
        this.finish();
      }
      finally
      {
        this.end();
        if (!this._leaveOpen)
          this._stream.Flush(); //.Close()
        this._stream = (Stream) null;
      }
    }

        public override void Flush()
        {
            this._stream.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
    {
      throw new NotImplementedException();
    }

    public override void SetLength(long value) => this._stream.SetLength(value);

    private string ReadZeroTerminatedString()
    {
      List<byte> byteList = new List<byte>();
      bool flag = false;
      while (this._stream.Read(this._buf1, 0, 1) == 1)
      {
        if (this._buf1[0] == (byte) 0)
          flag = true;
        else
          byteList.Add(this._buf1[0]);
        if (flag)
        {
          byte[] array = byteList.ToArray();
          return GZipStream.iso8859dash1.GetString(array, 0, array.Length);
        }
      }
      throw new ZlibException("Unexpected EOF reading GZIP header.");
    }

    private int _ReadAndValidateGzipHeader()
    {
      int num1 = 0;
      byte[] buffer1 = new byte[10];
      int num2 = this._stream.Read(buffer1, 0, buffer1.Length);
      switch (num2)
      {
        case 0:
          return 0;
        case 10:
          int num3 = buffer1[0] == (byte) 31 && buffer1[1] == (byte) 139 && buffer1[2] == (byte) 8 ? BitConverter.ToInt32(buffer1, 4) : throw new ZlibException("Bad GZIP header.");
          this._GzipMtime = GZipStream._unixEpoch.AddSeconds((double) num3);
          int num4 = num1 + num2;
          if (((int) buffer1[3] & 4) == 4)
          {
            int num5 = this._stream.Read(buffer1, 0, 2);
            int num6 = num4 + num5;
            short length = (short) ((int) buffer1[0] + (int) buffer1[1] * 256);
            byte[] buffer2 = new byte[(int) length];
            int num7 = this._stream.Read(buffer2, 0, buffer2.Length);
            if (num7 != (int) length)
              throw new ZlibException("Unexpected end-of-file reading GZIP header.");
            num4 = num6 + num7;
          }
          if (((int) buffer1[3] & 8) == 8)
            this._GzipFileName = this.ReadZeroTerminatedString();
          if (((int) buffer1[3] & 16) == 16)
            this._GzipComment = this.ReadZeroTerminatedString();
          if (((int) buffer1[3] & 2) == 2)
            this.Read(this._buf1, 0, 1);
          return num4;
        default:
          throw new ZlibException("Not a valid GZIP stream.");
      }
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
      if (this._streamMode == ZlibBaseStream.StreamMode.Undefined)
      {
        if (!this._stream.CanRead)
          throw new ZlibException("The stream is not readable.");
        this._streamMode = ZlibBaseStream.StreamMode.Reader;
        this.z.AvailableBytesIn = 0;
        if (this._flavor == ZlibStreamFlavor.GZIP)
        {
          this._gzipHeaderByteCount = this._ReadAndValidateGzipHeader();
          if (this._gzipHeaderByteCount == 0)
            return 0;
        }
      }
      if (this._streamMode != ZlibBaseStream.StreamMode.Reader)
        throw new ZlibException("Cannot Read after Writing.");
      if (count == 0)
        return 0;
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count));
      if (offset < buffer.GetLowerBound(0))
        throw new ArgumentOutOfRangeException(nameof (offset));
      if (offset + count > buffer.GetLength(0))
        throw new ArgumentOutOfRangeException(nameof (count));
      this._z.OutputBuffer = buffer;
      this._z.NextOut = offset;
      this._z.AvailableBytesOut = count;
      this._z.InputBuffer = this.workingBuffer;
      int num1;
      do
      {
        if (this._z.AvailableBytesIn == 0 && !this.nomoreinput)
        {
          this._z.NextIn = 0;
          this._z.AvailableBytesIn = this._stream.Read(this._workingBuffer, 0, this._workingBuffer.Length);
          if (this._z.AvailableBytesIn == 0)
            this.nomoreinput = true;
        }
        num1 = this._z.Inflate(this._flushMode);
        if (this.nomoreinput && num1 == -5)
          return 0;
        if (num1 != 0 && num1 != 1)
          throw new ZlibException(string.Format("inflating:  rc={0}  msg={1}", (object) num1, (object) this._z.Message));
      }
      while ((!this.nomoreinput && num1 != 1 || this._z.AvailableBytesOut != count) && this._z.AvailableBytesOut > 0 && !this.nomoreinput && num1 == 0);
      if (this._z.AvailableBytesOut > 0)
      {
        if (num1 == 0)
        {
          int availableBytesIn = this._z.AvailableBytesIn;
        }
        int num2 = this.nomoreinput ? 1 : 0;
      }
      int count1 = count - this._z.AvailableBytesOut;
      if (this.crc != null)
        this.crc.SlurpBlock(buffer, offset, count1);
      return count1;
    }

    public override bool CanRead => this._stream.CanRead;

    public override bool CanSeek => this._stream.CanSeek;

    public override bool CanWrite => this._stream.CanWrite;

    public override long Length => this._stream.Length;

    public override long Position
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    internal enum StreamMode
    {
      Writer,
      Reader,
      Undefined,
    }
  }
}

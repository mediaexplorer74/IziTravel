// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.ZipFile
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.VirtualFileSystem;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>
  /// This class represents a Zip archive.  You can ask for the contained
  /// entries, or get an input stream for a file entry.  The entry is
  /// automatically decompressed.
  /// 
  /// You can also update the archive adding or deleting entries.
  /// 
  /// This class is thread safe for input:  You can open input streams for arbitrary
  /// entries in different threads.
  /// <br />
  /// <br />Author of the original java version : Jochen Hoenicke
  /// </summary>
  /// <example>
  /// <code>
  /// using System;
  /// using System.Text;
  /// using System.Collections;
  /// using System.IO;
  /// 
  /// using ICSharpCode.SharpZipLib.Zip;
  /// 
  /// class MainClass
  /// {
  /// 	static public void Main(string[] args)
  /// 	{
  /// 		using (ZipFile zFile = new ZipFile(args[0])) {
  /// 			Console.WriteLine("Listing of : " + zFile.Name);
  /// 			Console.WriteLine("");
  /// 			Console.WriteLine("Raw Size    Size      Date     Time     Name");
  /// 			Console.WriteLine("--------  --------  --------  ------  ---------");
  /// 			foreach (ZipEntry e in zFile) {
  /// 				if ( e.IsFile ) {
  /// 					DateTime d = e.DateTime;
  /// 					Console.WriteLine("{0, -10}{1, -10}{2}  {3}   {4}", e.Size, e.CompressedSize,
  /// 						d.ToString("dd-MM-yy"), d.ToString("HH:mm"),
  /// 						e.Name);
  /// 				}
  /// 			}
  /// 		}
  /// 	}
  /// }
  /// </code>
  /// </example>
  public class ZipFile : IEnumerable, IDisposable
  {
    private const int DefaultBufferSize = 4096;
    /// <summary>Event handler for handling encryption keys.</summary>
    public ZipFile.KeysRequiredEventHandler KeysRequired;
    private bool isDisposed_;
    private string name_;
    private string comment_;
    private Stream baseStream_;
    private bool isStreamOwner;
    private long offsetOfFirstEntry;
    private ZipEntry[] entries_;
    private byte[] key;
    private bool isNewArchive_;
    private UseZip64 useZip64_ = UseZip64.Dynamic;
    private ArrayList updates_;
    private long updateCount_;
    private Hashtable updateIndex_;
    private IArchiveStorage archiveStorage_;
    private IDynamicDataSource updateDataSource_;
    private bool contentsEdited_;
    private int bufferSize_ = 4096;
    private byte[] copyBuffer_;
    private ZipFile.ZipString newComment_;
    private bool commentEdited_;
    private IEntryFactory updateEntryFactory_ = (IEntryFactory) new ZipEntryFactory();

    /// <summary>Handles getting of encryption keys when required.</summary>
    /// <param name="fileName">The file for which encryption keys are required.</param>
    private void OnKeysRequired(string fileName)
    {
      if (this.KeysRequired == null)
        return;
      KeysRequiredEventArgs e = new KeysRequiredEventArgs(fileName, this.key);
      this.KeysRequired((object) this, e);
      this.key = e.Key;
    }

    /// <summary>Get/set the encryption key value.</summary>
    private byte[] Key
    {
      get => this.key;
      set => this.key = value;
    }

    /// <summary>
    /// Get a value indicating wether encryption keys are currently available.
    /// </summary>
    private bool HaveKeys => this.key != null;

    /// <summary>Opens a Zip file with the given name for reading.</summary>
    /// <param name="name">The name of the file to open.</param>
    /// <exception cref="T:System.ArgumentNullException">The argument supplied is null.</exception>
    /// <exception cref="T:System.IO.IOException">An i/o error occurs</exception>
    /// <exception cref="T:ICSharpCode.SharpZipLib.Zip.ZipException">
    /// The file doesn't contain a valid zip archive.
    /// </exception>
    public ZipFile(string name)
    {
      this.name_ = name != null ? name : throw new ArgumentNullException(nameof (name));
      this.baseStream_ = (Stream) VFS.Current.OpenReadFile(name);
      this.isStreamOwner = true;
      try
      {
        this.ReadEntries();
      }
      catch
      {
        this.DisposeInternal(true);
        throw;
      }
    }

    /// <summary>
    /// Opens a Zip file reading the given <see cref="T:ICSharpCode.SharpZipLib.VirtualFileSystem.VfsStream" />.
    /// </summary>
    /// <param name="file">The <see cref="T:ICSharpCode.SharpZipLib.VirtualFileSystem.VfsStream" /> to read archive data from.</param>
    /// <exception cref="T:System.ArgumentNullException">The supplied argument is null.</exception>
    /// <exception cref="T:System.IO.IOException">An i/o error occurs.</exception>
    /// <exception cref="T:ICSharpCode.SharpZipLib.Zip.ZipException">
    /// The file doesn't contain a valid zip archive.
    /// </exception>
    public ZipFile(VfsStream file)
    {
      if (file == null)
        throw new ArgumentNullException(nameof (file));
      this.baseStream_ = file.CanSeek ? (Stream) file : throw new ArgumentException("Stream is not seekable", nameof (file));
      this.name_ = file.Name;
      this.isStreamOwner = true;
      try
      {
        this.ReadEntries();
      }
      catch
      {
        this.DisposeInternal(true);
        throw;
      }
    }

    /// <summary>
    /// Opens a Zip file reading the given <see cref="T:System.IO.Stream" />.
    /// </summary>
    /// <param name="stream">The <see cref="T:System.IO.Stream" /> to read archive data from.</param>
    /// <exception cref="T:System.IO.IOException">An i/o error occurs</exception>
    /// <exception cref="T:ICSharpCode.SharpZipLib.Zip.ZipException">
    /// The stream doesn't contain a valid zip archive.<br />
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    /// The <see cref="T:System.IO.Stream">stream</see> doesnt support seeking.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// The <see cref="T:System.IO.Stream">stream</see> argument is null.
    /// </exception>
    public ZipFile(Stream stream)
    {
      if (stream == null)
        throw new ArgumentNullException(nameof (stream));
      this.baseStream_ = stream.CanSeek ? stream : throw new ArgumentException("Stream is not seekable", nameof (stream));
      this.isStreamOwner = true;
      if (this.baseStream_.Length > 0L)
      {
        try
        {
          this.ReadEntries();
        }
        catch
        {
          this.DisposeInternal(true);
          throw;
        }
      }
      else
      {
        this.entries_ = new ZipEntry[0];
        this.isNewArchive_ = true;
      }
    }

    /// <summary>
    /// Initialises a default <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipFile" /> instance with no entries and no file storage.
    /// </summary>
    internal ZipFile()
    {
      this.entries_ = new ZipEntry[0];
      this.isNewArchive_ = true;
    }

    /// <summary>Finalize this instance.</summary>
    ~ZipFile() => this.Dispose(false);

    /// <summary>
    /// Closes the ZipFile.  If the stream is <see cref="P:ICSharpCode.SharpZipLib.Zip.ZipFile.IsStreamOwner">owned</see> then this also closes the underlying input stream.
    /// Once closed, no further instance methods should be called.
    /// </summary>
    /// <exception cref="T:System.IO.IOException">An i/o error occurs.</exception>
    public void Close()
    {
      this.DisposeInternal(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>
    /// Create a new <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipFile" /> whose data will be stored in a file.
    /// </summary>
    /// <param name="fileName">The name of the archive to create.</param>
    /// <returns>Returns the newly created <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipFile" /></returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="fileName"></paramref> is null</exception>
    public static ZipFile Create(string fileName)
    {
      Stream stream = fileName != null ? (Stream) VFS.Current.CreateFile(fileName) : throw new ArgumentNullException(nameof (fileName));
      return new ZipFile()
      {
        name_ = fileName,
        baseStream_ = stream,
        isStreamOwner = true
      };
    }

    /// <summary>
    /// Create a new <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipFile" /> whose data will be stored on a stream.
    /// </summary>
    /// <param name="outStream">The stream providing data storage.</param>
    /// <returns>Returns the newly created <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipFile" /></returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="outStream"> is null</paramref></exception>
    /// <exception cref="T:System.ArgumentException"><paramref name="outStream"> doesnt support writing.</paramref></exception>
    public static ZipFile Create(Stream outStream)
    {
      if (outStream == null)
        throw new ArgumentNullException(nameof (outStream));
      if (!outStream.CanWrite)
        throw new ArgumentException("Stream is not writeable", nameof (outStream));
      return outStream.CanSeek ? new ZipFile()
      {
        baseStream_ = outStream
      } : throw new ArgumentException("Stream is not seekable", nameof (outStream));
    }

    /// <summary>
    /// Get/set a flag indicating if the underlying stream is owned by the ZipFile instance.
    /// If the flag is true then the stream will be closed when <see cref="M:ICSharpCode.SharpZipLib.Zip.ZipFile.Close">Close</see> is called.
    /// </summary>
    /// <remarks>The default value is true in all cases.</remarks>
    public bool IsStreamOwner
    {
      get => this.isStreamOwner;
      set => this.isStreamOwner = value;
    }

    /// <summary>
    /// Get a value indicating wether
    /// this archive is embedded in another file or not.
    /// </summary>
    public bool IsEmbeddedArchive => this.offsetOfFirstEntry > 0L;

    /// <summary>Get a value indicating that this archive is a new one.</summary>
    public bool IsNewArchive => this.isNewArchive_;

    /// <summary>Gets the comment for the zip file.</summary>
    public string ZipFileComment => this.comment_;

    /// <summary>Gets the name of this zip file.</summary>
    public string Name => this.name_;

    /// <summary>Gets the number of entries in this zip file.</summary>
    /// <exception cref="T:System.InvalidOperationException">
    /// The Zip file has been closed.
    /// </exception>
    [Obsolete("Use the Count property instead")]
    public int Size => this.entries_.Length;

    /// <summary>
    /// Get the number of entries contained in this <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipFile" />.
    /// </summary>
    public long Count => (long) this.entries_.Length;

    /// <summary>Indexer property for ZipEntries</summary>
    [IndexerName("EntryByIndex")]
    public ZipEntry this[int index] => (ZipEntry) this.entries_[index].Clone();

    /// <summary>
    /// Gets an enumerator for the Zip entries in this Zip file.
    /// </summary>
    /// <returns>Returns an <see cref="T:System.Collections.IEnumerator" /> for this archive.</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    /// The Zip file has been closed.
    /// </exception>
    public IEnumerator GetEnumerator()
    {
      if (this.isDisposed_)
        throw new ObjectDisposedException(nameof (ZipFile));
      return (IEnumerator) new ZipFile.ZipEntryEnumerator(this.entries_);
    }

    /// <summary>Return the index of the entry with a matching name</summary>
    /// <param name="name">Entry name to find</param>
    /// <param name="ignoreCase">If true the comparison is case insensitive</param>
    /// <returns>The index position of the matching entry or -1 if not found</returns>
    /// <exception cref="T:System.ObjectDisposedException">
    /// The Zip file has been closed.
    /// </exception>
    public int FindEntry(string name, bool ignoreCase)
    {
      if (this.isDisposed_)
        throw new ObjectDisposedException(nameof (ZipFile));
      for (int entry = 0; entry < this.entries_.Length; ++entry)
      {
        if (string.Compare(name, this.entries_[entry].Name, ignoreCase ? StringComparison.CurrentCultureIgnoreCase : StringComparison.CurrentCulture) == 0)
          return entry;
      }
      return -1;
    }

    /// <summary>
    /// Searches for a zip entry in this archive with the given name.
    /// String comparisons are case insensitive
    /// </summary>
    /// <param name="name">
    /// The name to find. May contain directory components separated by slashes ('/').
    /// </param>
    /// <returns>
    /// A clone of the zip entry, or null if no entry with that name exists.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    /// The Zip file has been closed.
    /// </exception>
    public ZipEntry GetEntry(string name)
    {
      if (this.isDisposed_)
        throw new ObjectDisposedException(nameof (ZipFile));
      int entry = this.FindEntry(name, true);
      return entry < 0 ? (ZipEntry) null : (ZipEntry) this.entries_[entry].Clone();
    }

    /// <summary>
    /// Gets an input stream for reading the given zip entry data in an uncompressed form.
    /// Normally the <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry" /> should be an entry returned by GetEntry().
    /// </summary>
    /// <param name="entry">The <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry" /> to obtain a data <see cref="T:System.IO.Stream" /> for</param>
    /// <returns>An input <see cref="T:System.IO.Stream" /> containing data for this <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry" /></returns>
    /// <exception cref="T:System.ObjectDisposedException">
    /// The ZipFile has already been closed
    /// </exception>
    /// <exception cref="T:ICSharpCode.SharpZipLib.Zip.ZipException">
    /// The compression method for the entry is unknown
    /// </exception>
    /// <exception cref="T:System.IndexOutOfRangeException">
    /// The entry is not found in the ZipFile
    /// </exception>
    public Stream GetInputStream(ZipEntry entry)
    {
      if (entry == null)
        throw new ArgumentNullException(nameof (entry));
      if (this.isDisposed_)
        throw new ObjectDisposedException(nameof (ZipFile));
      long entryIndex = entry.ZipFileIndex;
      if (entryIndex < 0L || entryIndex >= (long) this.entries_.Length || this.entries_[entryIndex].Name != entry.Name)
      {
        entryIndex = (long) this.FindEntry(entry.Name, true);
        if (entryIndex < 0L)
          throw new ZipException("Entry cannot be found");
      }
      return this.GetInputStream(entryIndex);
    }

    /// <summary>Creates an input stream reading a zip entry</summary>
    /// <param name="entryIndex">The index of the entry to obtain an input stream for.</param>
    /// <returns>
    /// An input <see cref="T:System.IO.Stream" /> containing data for this <paramref name="entryIndex" />
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    /// The ZipFile has already been closed
    /// </exception>
    /// <exception cref="T:ICSharpCode.SharpZipLib.Zip.ZipException">
    /// The compression method for the entry is unknown
    /// </exception>
    /// <exception cref="T:System.IndexOutOfRangeException">
    /// The entry is not found in the ZipFile
    /// </exception>
    public Stream GetInputStream(long entryIndex)
    {
      if (this.isDisposed_)
        throw new ObjectDisposedException(nameof (ZipFile));
      long start = this.LocateEntry(this.entries_[entryIndex]);
      CompressionMethod compressionMethod = this.entries_[entryIndex].CompressionMethod;
      Stream baseInputStream = (Stream) new ZipFile.PartialInputStream(this, start, this.entries_[entryIndex].CompressedSize);
      if (this.entries_[entryIndex].IsCrypted)
        throw new ZipException("decryption not supported for Portable Class Library");
      switch (compressionMethod)
      {
        case CompressionMethod.Stored:
          return baseInputStream;
        case CompressionMethod.Deflated:
          baseInputStream = (Stream) new InflaterInputStream(baseInputStream, new Inflater(true));
          goto case CompressionMethod.Stored;
        default:
          throw new ZipException("Unsupported compression method " + (object) compressionMethod);
      }
    }

    /// <summary>Test an archive for integrity/validity</summary>
    /// <param name="testData">Perform low level data Crc check</param>
    /// <returns>true if all tests pass, false otherwise</returns>
    /// <remarks>Testing will terminate on the first error found.</remarks>
    public bool TestArchive(bool testData)
    {
      return this.TestArchive(testData, TestStrategy.FindFirstError, (ZipTestResultHandler) null);
    }

    /// <summary>Test an archive for integrity/validity</summary>
    /// <param name="testData">Perform low level data Crc check</param>
    /// <param name="strategy">The <see cref="T:ICSharpCode.SharpZipLib.Zip.TestStrategy"></see> to apply.</param>
    /// <param name="resultHandler">The <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipTestResultHandler"></see> handler to call during testing.</param>
    /// <returns>true if all tests pass, false otherwise</returns>
    /// <exception cref="T:System.ObjectDisposedException">The object has already been closed.</exception>
    public bool TestArchive(
      bool testData,
      TestStrategy strategy,
      ZipTestResultHandler resultHandler)
    {
      if (this.isDisposed_)
        throw new ObjectDisposedException(nameof (ZipFile));
      TestStatus status = new TestStatus(this);
      if (resultHandler != null)
        resultHandler(status, (string) null);
      ZipFile.HeaderTest tests = testData ? ZipFile.HeaderTest.Extract | ZipFile.HeaderTest.Header : ZipFile.HeaderTest.Header;
      bool flag = true;
      try
      {
        for (int index = 0; flag && (long) index < this.Count; ++index)
        {
          if (resultHandler != null)
          {
            status.SetEntry(this[index]);
            status.SetOperation(TestOperation.EntryHeader);
            resultHandler(status, (string) null);
          }
          try
          {
            this.TestLocalHeader(this[index], tests);
          }
          catch (ZipException ex)
          {
            status.AddError();
            if (resultHandler != null)
              resultHandler(status, string.Format("Exception during test - '{0}'", (object) ex.Message));
            if (strategy == TestStrategy.FindFirstError)
              flag = false;
          }
          if (flag && testData && this[index].IsFile)
          {
            if (resultHandler != null)
            {
              status.SetOperation(TestOperation.EntryData);
              resultHandler(status, (string) null);
            }
            Crc32 crc32 = new Crc32();
            using (Stream inputStream = this.GetInputStream(this[index]))
            {
              byte[] buffer = new byte[4096];
              long num = 0;
              int count;
              while ((count = inputStream.Read(buffer, 0, buffer.Length)) > 0)
              {
                crc32.Update(buffer, 0, count);
                if (resultHandler != null)
                {
                  num += (long) count;
                  status.SetBytesTested(num);
                  resultHandler(status, (string) null);
                }
              }
            }
            if (this[index].Crc != crc32.Value)
            {
              status.AddError();
              if (resultHandler != null)
                resultHandler(status, "CRC mismatch");
              if (strategy == TestStrategy.FindFirstError)
                flag = false;
            }
            if ((this[index].Flags & 8) != 0)
            {
              ZipHelperStream zipHelperStream = new ZipHelperStream(this.baseStream_);
              DescriptorData data = new DescriptorData();
              zipHelperStream.ReadDataDescriptor(this[index].LocalHeaderRequiresZip64, data);
              if (this[index].Crc != data.Crc)
                status.AddError();
              if (this[index].CompressedSize != data.CompressedSize)
                status.AddError();
              if (this[index].Size != data.Size)
                status.AddError();
            }
          }
          if (resultHandler != null)
          {
            status.SetOperation(TestOperation.EntryComplete);
            resultHandler(status, (string) null);
          }
        }
        if (resultHandler != null)
        {
          status.SetOperation(TestOperation.MiscellaneousTests);
          resultHandler(status, (string) null);
        }
      }
      catch (Exception ex)
      {
        status.AddError();
        if (resultHandler != null)
          resultHandler(status, string.Format("Exception during test - '{0}'", (object) ex.Message));
      }
      if (resultHandler != null)
      {
        status.SetOperation(TestOperation.Complete);
        status.SetEntry((ZipEntry) null);
        resultHandler(status, (string) null);
      }
      return status.ErrorCount == 0;
    }

    /// <summary>
    /// Test a local header against that provided from the central directory
    /// </summary>
    /// <param name="entry">The entry to test against</param>
    /// <param name="tests">The type of <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipFile.HeaderTest">tests</see> to carry out.</param>
    /// <returns>The offset of the entries data in the file</returns>
    private long TestLocalHeader(ZipEntry entry, ZipFile.HeaderTest tests)
    {
      lock (this.baseStream_)
      {
        bool flag1 = (tests & ZipFile.HeaderTest.Header) != (ZipFile.HeaderTest) 0;
        bool flag2 = (tests & ZipFile.HeaderTest.Extract) != (ZipFile.HeaderTest) 0;
        this.baseStream_.Seek(this.offsetOfFirstEntry + entry.Offset, SeekOrigin.Begin);
        if (this.ReadLEUint() != 67324752U)
          throw new ZipException(string.Format("Wrong local header signature @{0:X}", (object) (this.offsetOfFirstEntry + entry.Offset)));
        short num1 = (short) ((int) this.ReadLEUshort() & (int) byte.MaxValue);
        short flags = (short) this.ReadLEUshort();
        short num2 = (short) this.ReadLEUshort();
        short num3 = (short) this.ReadLEUshort();
        short num4 = (short) this.ReadLEUshort();
        uint num5 = this.ReadLEUint();
        long num6 = (long) this.ReadLEUint();
        long num7 = (long) this.ReadLEUint();
        int length1 = (int) this.ReadLEUshort();
        int length2 = (int) this.ReadLEUshort();
        byte[] numArray1 = new byte[length1];
        StreamUtils.ReadFully(this.baseStream_, numArray1);
        byte[] numArray2 = new byte[length2];
        StreamUtils.ReadFully(this.baseStream_, numArray2);
        ZipExtraData zipExtraData = new ZipExtraData(numArray2);
        if (zipExtraData.Find(1))
        {
          num7 = zipExtraData.ReadLong();
          num6 = zipExtraData.ReadLong();
          if (((int) flags & 8) != 0)
          {
            if (num7 != -1L && num7 != entry.Size)
              throw new ZipException("Size invalid for descriptor");
            if (num6 != -1L && num6 != entry.CompressedSize)
              throw new ZipException("Compressed size invalid for descriptor");
          }
        }
        else if (num1 >= (short) 45 && ((uint) num7 == uint.MaxValue || (uint) num6 == uint.MaxValue))
          throw new ZipException("Required Zip64 extended information missing");
        if (flag2 && entry.IsFile)
        {
          if (!entry.IsCompressionMethodSupported())
            throw new ZipException("Compression method not supported");
          if (num1 > (short) 51 || num1 > (short) 20 && num1 < (short) 45)
            throw new ZipException(string.Format("Version required to extract this entry not supported ({0})", (object) num1));
          if (((int) flags & 12384) != 0)
            throw new ZipException("The library does not support the zip version required to extract this entry");
        }
        if (flag1)
        {
          if (num1 <= (short) 63 && num1 != (short) 10 && num1 != (short) 11 && num1 != (short) 20 && num1 != (short) 21 && num1 != (short) 25 && num1 != (short) 27 && num1 != (short) 45 && num1 != (short) 46 && num1 != (short) 50 && num1 != (short) 51 && num1 != (short) 52 && num1 != (short) 61 && num1 != (short) 62 && num1 != (short) 63)
            throw new ZipException(string.Format("Version required to extract this entry is invalid ({0})", (object) num1));
          if (((int) flags & 49168) != 0)
            throw new ZipException("Reserved bit flags cannot be set.");
          if (((int) flags & 1) != 0 && num1 < (short) 20)
            throw new ZipException(string.Format("Version required to extract this entry is too low for encryption ({0})", (object) num1));
          if (((int) flags & 64) != 0)
          {
            if (((int) flags & 1) == 0)
              throw new ZipException("Strong encryption flag set but encryption flag is not set");
            if (num1 < (short) 50)
              throw new ZipException(string.Format("Version required to extract this entry is too low for encryption ({0})", (object) num1));
          }
          if (((int) flags & 32) != 0 && num1 < (short) 27)
            throw new ZipException(string.Format("Patched data requires higher version than ({0})", (object) num1));
          if ((int) flags != entry.Flags)
            throw new ZipException("Central header/local header flags mismatch");
          if (entry.CompressionMethod != (CompressionMethod) num2)
            throw new ZipException("Central header/local header compression method mismatch");
          if (entry.Version != (int) num1)
            throw new ZipException("Extract version mismatch");
          if (((int) flags & 64) != 0 && num1 < (short) 62)
            throw new ZipException("Strong encryption flag set but version not high enough");
          if (((int) flags & 8192) != 0 && (num3 != (short) 0 || num4 != (short) 0))
            throw new ZipException("Header masked set but date/time values non-zero");
          if (((int) flags & 8) == 0 && (int) num5 != (int) (uint) entry.Crc)
            throw new ZipException("Central header/local header crc mismatch");
          if (num7 == 0L && num6 == 0L && num5 != 0U)
            throw new ZipException("Invalid CRC for empty entry");
          if (entry.Name.Length > length1)
            throw new ZipException("File name length mismatch");
          string stringExt = ZipConstants.ConvertToStringExt((int) flags, numArray1);
          if (stringExt != entry.Name)
            throw new ZipException("Central header and local header file name mismatch");
          if (entry.IsDirectory)
          {
            if (num7 > 0L)
              throw new ZipException("Directory cannot have size");
            if (entry.IsCrypted)
            {
              if (num6 > 14L)
                throw new ZipException("Directory compressed size invalid");
            }
            else if (num6 > 2L)
              throw new ZipException("Directory compressed size invalid");
          }
          if (!ZipNameTransform.IsValidName(stringExt, true))
            throw new ZipException("Name is invalid");
        }
        if (((int) flags & 8) == 0 || num7 > 0L || num6 > 0L)
        {
          if (num7 != entry.Size)
            throw new ZipException(string.Format("Size mismatch between central header({0}) and local header({1})", (object) entry.Size, (object) num7));
          if (num6 != entry.CompressedSize && num6 != (long) uint.MaxValue && num6 != -1L)
            throw new ZipException(string.Format("Compressed size mismatch between central header({0}) and local header({1})", (object) entry.CompressedSize, (object) num6));
        }
        int num8 = length1 + length2;
        return this.offsetOfFirstEntry + entry.Offset + 30L + (long) num8;
      }
    }

    /// <summary>
    /// Get / set the <see cref="T:ICSharpCode.SharpZipLib.Core.INameTransform" /> to apply to names when updating.
    /// </summary>
    public INameTransform NameTransform
    {
      get => this.updateEntryFactory_.NameTransform;
      set => this.updateEntryFactory_.NameTransform = value;
    }

    /// <summary>
    /// Get/set the <see cref="T:ICSharpCode.SharpZipLib.Zip.IEntryFactory" /> used to generate <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry" /> values
    /// during updates.
    /// </summary>
    public IEntryFactory EntryFactory
    {
      get => this.updateEntryFactory_;
      set
      {
        if (value == null)
          this.updateEntryFactory_ = (IEntryFactory) new ZipEntryFactory();
        else
          this.updateEntryFactory_ = value;
      }
    }

    /// <summary>
    /// Get /set the buffer size to be used when updating this zip file.
    /// </summary>
    public int BufferSize
    {
      get => this.bufferSize_;
      set
      {
        if (value < 1024)
          throw new ArgumentOutOfRangeException(nameof (value), "cannot be below 1024");
        if (this.bufferSize_ == value)
          return;
        this.bufferSize_ = value;
        this.copyBuffer_ = (byte[]) null;
      }
    }

    /// <summary>
    /// Get a value indicating an update has <see cref="M:ICSharpCode.SharpZipLib.Zip.ZipFile.BeginUpdate">been started</see>.
    /// </summary>
    public bool IsUpdating => this.updates_ != null;

    /// <summary>
    /// Get / set a value indicating how Zip64 Extension usage is determined when adding entries.
    /// </summary>
    public UseZip64 UseZip64
    {
      get => this.useZip64_;
      set => this.useZip64_ = value;
    }

    /// <summary>
    /// Begin updating this <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipFile" /> archive.
    /// </summary>
    /// <param name="archiveStorage">The <see cref="T:ICSharpCode.SharpZipLib.Zip.IArchiveStorage">archive storage</see> for use during the update.</param>
    /// <param name="dataSource">The <see cref="T:ICSharpCode.SharpZipLib.Zip.IDynamicDataSource">data source</see> to utilise during updating.</param>
    /// <exception cref="T:System.ObjectDisposedException">ZipFile has been closed.</exception>
    /// <exception cref="T:System.ArgumentNullException">One of the arguments provided is null</exception>
    /// <exception cref="T:System.ObjectDisposedException">ZipFile has been closed.</exception>
    public void BeginUpdate(IArchiveStorage archiveStorage, IDynamicDataSource dataSource)
    {
      if (archiveStorage == null)
        throw new ArgumentNullException(nameof (archiveStorage));
      if (dataSource == null)
        throw new ArgumentNullException(nameof (dataSource));
      if (this.isDisposed_)
        throw new ObjectDisposedException(nameof (ZipFile));
      if (this.IsEmbeddedArchive)
        throw new ZipException("Cannot update embedded/SFX archives");
      this.archiveStorage_ = archiveStorage;
      this.updateDataSource_ = dataSource;
      this.updateIndex_ = new Hashtable();
      this.updates_ = new ArrayList(this.entries_.Length);
      foreach (ZipEntry entry in this.entries_)
      {
        int num = this.updates_.Add((object) new ZipFile.ZipUpdate(entry));
        this.updateIndex_.Add((object) entry.Name, (object) num);
      }
      this.updates_.Sort((IComparer) new ZipFile.UpdateComparer());
      int num1 = 0;
      foreach (ZipFile.ZipUpdate update in (List<object>) this.updates_)
      {
        if (num1 != this.updates_.Count - 1)
        {
          update.OffsetBasedSize = ((ZipFile.ZipUpdate) this.updates_[num1 + 1]).Entry.Offset - update.Entry.Offset;
          ++num1;
        }
        else
          break;
      }
      this.updateCount_ = (long) this.updates_.Count;
      this.contentsEdited_ = false;
      this.commentEdited_ = false;
      this.newComment_ = (ZipFile.ZipString) null;
    }

    /// <summary>
    /// Begin updating to this <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipFile" /> archive.
    /// </summary>
    /// <param name="archiveStorage">The storage to use during the update.</param>
    public void BeginUpdate(IArchiveStorage archiveStorage)
    {
      this.BeginUpdate(archiveStorage, (IDynamicDataSource) new DynamicDiskDataSource());
    }

    /// <summary>
    /// Begin updating this <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipFile" /> archive.
    /// </summary>
    /// <seealso cref="M:ICSharpCode.SharpZipLib.Zip.ZipFile.BeginUpdate(ICSharpCode.SharpZipLib.Zip.IArchiveStorage)" />
    /// <seealso cref="M:ICSharpCode.SharpZipLib.Zip.ZipFile.CommitUpdate"></seealso>
    /// <seealso cref="M:ICSharpCode.SharpZipLib.Zip.ZipFile.AbortUpdate"></seealso>
    public void BeginUpdate()
    {
      if (this.Name == null)
        this.BeginUpdate((IArchiveStorage) new MemoryArchiveStorage(), (IDynamicDataSource) new DynamicDiskDataSource());
      else
        this.BeginUpdate((IArchiveStorage) new DiskArchiveStorage(this), (IDynamicDataSource) new DynamicDiskDataSource());
    }

    /// <summary>Commit current updates, updating this archive.</summary>
    /// <seealso cref="M:ICSharpCode.SharpZipLib.Zip.ZipFile.BeginUpdate"></seealso>
    /// <seealso cref="M:ICSharpCode.SharpZipLib.Zip.ZipFile.AbortUpdate"></seealso>
    /// <exception cref="T:System.ObjectDisposedException">ZipFile has been closed.</exception>
    public void CommitUpdate()
    {
      if (this.isDisposed_)
        throw new ObjectDisposedException(nameof (ZipFile));
      this.CheckUpdating();
      try
      {
        this.updateIndex_.Clear();
        this.updateIndex_ = (Hashtable) null;
        if (this.contentsEdited_)
          this.RunUpdates();
        else if (this.commentEdited_)
        {
          this.UpdateCommentOnly();
        }
        else
        {
          if (this.entries_.Length != 0)
            return;
          byte[] comment = this.newComment_ != null ? this.newComment_.RawComment : ZipConstants.ConvertToArray(this.comment_);
          using (ZipHelperStream zipHelperStream = new ZipHelperStream(this.baseStream_))
            zipHelperStream.WriteEndOfCentralDirectory(0L, 0L, 0L, comment);
        }
      }
      finally
      {
        this.PostUpdateCleanup();
      }
    }

    /// <summary>Abort updating leaving the archive unchanged.</summary>
    /// <seealso cref="M:ICSharpCode.SharpZipLib.Zip.ZipFile.BeginUpdate"></seealso>
    /// <seealso cref="M:ICSharpCode.SharpZipLib.Zip.ZipFile.CommitUpdate"></seealso>
    public void AbortUpdate() => this.PostUpdateCleanup();

    /// <summary>
    /// Set the file comment to be recorded when the current update is <see cref="M:ICSharpCode.SharpZipLib.Zip.ZipFile.CommitUpdate">commited</see>.
    /// </summary>
    /// <param name="comment">The comment to record.</param>
    /// <exception cref="T:System.ObjectDisposedException">ZipFile has been closed.</exception>
    public void SetComment(string comment)
    {
      if (this.isDisposed_)
        throw new ObjectDisposedException(nameof (ZipFile));
      this.CheckUpdating();
      this.newComment_ = new ZipFile.ZipString(comment);
      if (this.newComment_.RawLength > (int) ushort.MaxValue)
      {
        this.newComment_ = (ZipFile.ZipString) null;
        throw new ZipException("Comment length exceeds maximum - 65535");
      }
      this.commentEdited_ = true;
    }

    private void AddUpdate(ZipFile.ZipUpdate update)
    {
      this.contentsEdited_ = true;
      int existingUpdate = this.FindExistingUpdate(update.Entry.Name);
      if (existingUpdate >= 0)
      {
        if (this.updates_[existingUpdate] == null)
          ++this.updateCount_;
        this.updates_[existingUpdate] = (object) update;
      }
      else
      {
        int num = this.updates_.Add((object) update);
        ++this.updateCount_;
        this.updateIndex_.Add((object) update.Entry.Name, (object) num);
      }
    }

    /// <summary>Add a new entry to the archive.</summary>
    /// <param name="fileName">The name of the file to add.</param>
    /// <param name="compressionMethod">The compression method to use.</param>
    /// <param name="useUnicodeText">Ensure Unicode text is used for name and comment for this entry.</param>
    /// <exception cref="T:System.ArgumentNullException">Argument supplied is null.</exception>
    /// <exception cref="T:System.ObjectDisposedException">ZipFile has been closed.</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">Compression method is not supported.</exception>
    public void Add(string fileName, CompressionMethod compressionMethod, bool useUnicodeText)
    {
      if (fileName == null)
        throw new ArgumentNullException(nameof (fileName));
      if (this.isDisposed_)
        throw new ObjectDisposedException(nameof (ZipFile));
      if (!ZipEntry.IsCompressionMethodSupported(compressionMethod))
        throw new ArgumentOutOfRangeException(nameof (compressionMethod));
      this.CheckUpdating();
      this.contentsEdited_ = true;
      ZipEntry entry = this.EntryFactory.MakeFileEntry(fileName);
      entry.IsUnicodeText = useUnicodeText;
      entry.CompressionMethod = compressionMethod;
      this.AddUpdate(new ZipFile.ZipUpdate(fileName, entry));
    }

    /// <summary>Add a new entry to the archive.</summary>
    /// <param name="fileName">The name of the file to add.</param>
    /// <param name="compressionMethod">The compression method to use.</param>
    /// <exception cref="T:System.ArgumentNullException">ZipFile has been closed.</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">The compression method is not supported.</exception>
    public void Add(string fileName, CompressionMethod compressionMethod)
    {
      if (fileName == null)
        throw new ArgumentNullException(nameof (fileName));
      if (!ZipEntry.IsCompressionMethodSupported(compressionMethod))
        throw new ArgumentOutOfRangeException(nameof (compressionMethod));
      this.CheckUpdating();
      this.contentsEdited_ = true;
      ZipEntry entry = this.EntryFactory.MakeFileEntry(fileName);
      entry.CompressionMethod = compressionMethod;
      this.AddUpdate(new ZipFile.ZipUpdate(fileName, entry));
    }

    /// <summary>Add a file to the archive.</summary>
    /// <param name="fileName">The name of the file to add.</param>
    /// <exception cref="T:System.ArgumentNullException">Argument supplied is null.</exception>
    public void Add(string fileName)
    {
      if (fileName == null)
        throw new ArgumentNullException(nameof (fileName));
      this.CheckUpdating();
      this.AddUpdate(new ZipFile.ZipUpdate(fileName, this.EntryFactory.MakeFileEntry(fileName)));
    }

    /// <summary>Add a file to the archive.</summary>
    /// <param name="fileName">The name of the file to add.</param>
    /// <param name="entryName">The name to use for the <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry" /> on the Zip file created.</param>
    /// <exception cref="T:System.ArgumentNullException">Argument supplied is null.</exception>
    public void Add(string fileName, string entryName)
    {
      if (fileName == null)
        throw new ArgumentNullException(nameof (fileName));
      if (entryName == null)
        throw new ArgumentNullException(nameof (entryName));
      this.CheckUpdating();
      this.AddUpdate(new ZipFile.ZipUpdate(fileName, this.EntryFactory.MakeFileEntry(fileName, entryName, true)));
    }

    /// <summary>Add a file entry with data.</summary>
    /// <param name="dataSource">The source of the data for this entry.</param>
    /// <param name="entryName">The name to give to the entry.</param>
    public void Add(IStaticDataSource dataSource, string entryName)
    {
      if (dataSource == null)
        throw new ArgumentNullException(nameof (dataSource));
      if (entryName == null)
        throw new ArgumentNullException(nameof (entryName));
      this.CheckUpdating();
      this.AddUpdate(new ZipFile.ZipUpdate(dataSource, this.EntryFactory.MakeFileEntry(entryName, false)));
    }

    /// <summary>Add a file entry with data.</summary>
    /// <param name="dataSource">The source of the data for this entry.</param>
    /// <param name="entryName">The name to give to the entry.</param>
    /// <param name="compressionMethod">The compression method to use.</param>
    public void Add(
      IStaticDataSource dataSource,
      string entryName,
      CompressionMethod compressionMethod)
    {
      if (dataSource == null)
        throw new ArgumentNullException(nameof (dataSource));
      if (entryName == null)
        throw new ArgumentNullException(nameof (entryName));
      this.CheckUpdating();
      ZipEntry entry = this.EntryFactory.MakeFileEntry(entryName, false);
      entry.CompressionMethod = compressionMethod;
      this.AddUpdate(new ZipFile.ZipUpdate(dataSource, entry));
    }

    /// <summary>Add a file entry with data.</summary>
    /// <param name="dataSource">The source of the data for this entry.</param>
    /// <param name="entryName">The name to give to the entry.</param>
    /// <param name="compressionMethod">The compression method to use.</param>
    /// <param name="useUnicodeText">Ensure Unicode text is used for name and comments for this entry.</param>
    public void Add(
      IStaticDataSource dataSource,
      string entryName,
      CompressionMethod compressionMethod,
      bool useUnicodeText)
    {
      if (dataSource == null)
        throw new ArgumentNullException(nameof (dataSource));
      if (entryName == null)
        throw new ArgumentNullException(nameof (entryName));
      this.CheckUpdating();
      ZipEntry entry = this.EntryFactory.MakeFileEntry(entryName, false);
      entry.IsUnicodeText = useUnicodeText;
      entry.CompressionMethod = compressionMethod;
      this.AddUpdate(new ZipFile.ZipUpdate(dataSource, entry));
    }

    /// <summary>
    /// Add a <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry" /> that contains no data.
    /// </summary>
    /// <param name="entry">The entry to add.</param>
    /// <remarks>This can be used to add directories, volume labels, or empty file entries.</remarks>
    public void Add(ZipEntry entry)
    {
      if (entry == null)
        throw new ArgumentNullException(nameof (entry));
      this.CheckUpdating();
      if (entry.Size != 0L || entry.CompressedSize != 0L)
        throw new ZipException("Entry cannot have any data");
      this.AddUpdate(new ZipFile.ZipUpdate(ZipFile.UpdateCommand.Add, entry));
    }

    /// <summary>Add a directory entry to the archive.</summary>
    /// <param name="directoryName">The directory to add.</param>
    public void AddDirectory(string directoryName)
    {
      if (directoryName == null)
        throw new ArgumentNullException(nameof (directoryName));
      this.CheckUpdating();
      this.AddUpdate(new ZipFile.ZipUpdate(ZipFile.UpdateCommand.Add, this.EntryFactory.MakeDirectoryEntry(directoryName)));
    }

    /// <summary>Delete an entry by name</summary>
    /// <param name="fileName">The filename to delete</param>
    /// <returns>True if the entry was found and deleted; false otherwise.</returns>
    public bool Delete(string fileName)
    {
      if (fileName == null)
        throw new ArgumentNullException(nameof (fileName));
      this.CheckUpdating();
      int existingUpdate = this.FindExistingUpdate(fileName);
      if (existingUpdate < 0 || this.updates_[existingUpdate] == null)
        throw new ZipException("Cannot find entry to delete");
      bool flag = true;
      this.contentsEdited_ = true;
      this.updates_[existingUpdate] = (object) null;
      --this.updateCount_;
      return flag;
    }

    /// <summary>
    /// Delete a <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry" /> from the archive.
    /// </summary>
    /// <param name="entry">The entry to delete.</param>
    public void Delete(ZipEntry entry)
    {
      if (entry == null)
        throw new ArgumentNullException(nameof (entry));
      this.CheckUpdating();
      int existingUpdate = this.FindExistingUpdate(entry);
      if (existingUpdate < 0)
        throw new ZipException("Cannot find entry to delete");
      this.contentsEdited_ = true;
      this.updates_[existingUpdate] = (object) null;
      --this.updateCount_;
    }

    private void WriteLEShort(int value)
    {
      this.baseStream_.WriteByte((byte) (value & (int) byte.MaxValue));
      this.baseStream_.WriteByte((byte) (value >> 8 & (int) byte.MaxValue));
    }

    /// <summary>Write an unsigned short in little endian byte order.</summary>
    private void WriteLEUshort(ushort value)
    {
      this.baseStream_.WriteByte((byte) ((uint) value & (uint) byte.MaxValue));
      this.baseStream_.WriteByte((byte) ((uint) value >> 8));
    }

    /// <summary>Write an int in little endian byte order.</summary>
    private void WriteLEInt(int value)
    {
      this.WriteLEShort(value & (int) ushort.MaxValue);
      this.WriteLEShort(value >> 16);
    }

    /// <summary>Write an unsigned int in little endian byte order.</summary>
    private void WriteLEUint(uint value)
    {
      this.WriteLEUshort((ushort) (value & (uint) ushort.MaxValue));
      this.WriteLEUshort((ushort) (value >> 16));
    }

    /// <summary>Write a long in little endian byte order.</summary>
    private void WriteLeLong(long value)
    {
      this.WriteLEInt((int) (value & (long) uint.MaxValue));
      this.WriteLEInt((int) (value >> 32));
    }

    private void WriteLEUlong(ulong value)
    {
      this.WriteLEUint((uint) (value & (ulong) uint.MaxValue));
      this.WriteLEUint((uint) (value >> 32));
    }

    private void WriteLocalEntryHeader(ZipFile.ZipUpdate update)
    {
      ZipEntry outEntry = update.OutEntry;
      outEntry.Offset = this.baseStream_.Position;
      if (update.Command != ZipFile.UpdateCommand.Copy)
      {
        if (outEntry.CompressionMethod == CompressionMethod.Deflated)
        {
          if (outEntry.Size == 0L)
          {
            outEntry.CompressedSize = outEntry.Size;
            outEntry.Crc = 0L;
            outEntry.CompressionMethod = CompressionMethod.Stored;
          }
        }
        else if (outEntry.CompressionMethod == CompressionMethod.Stored)
          outEntry.Flags &= -9;
        if (this.HaveKeys)
        {
          outEntry.IsCrypted = true;
          if (outEntry.Crc < 0L)
            outEntry.Flags |= 8;
        }
        else
          outEntry.IsCrypted = false;
        switch (this.useZip64_)
        {
          case UseZip64.On:
            outEntry.ForceZip64();
            break;
          case UseZip64.Dynamic:
            if (outEntry.Size < 0L)
            {
              outEntry.ForceZip64();
              break;
            }
            break;
        }
      }
      this.WriteLEInt(67324752);
      this.WriteLEShort(outEntry.Version);
      this.WriteLEShort(outEntry.Flags);
      this.WriteLEShort((int) (byte) outEntry.CompressionMethod);
      this.WriteLEInt((int) outEntry.DosTime);
      if (!outEntry.HasCrc)
      {
        update.CrcPatchOffset = this.baseStream_.Position;
        this.WriteLEInt(0);
      }
      else
        this.WriteLEInt((int) outEntry.Crc);
      if (outEntry.LocalHeaderRequiresZip64)
      {
        this.WriteLEInt(-1);
        this.WriteLEInt(-1);
      }
      else
      {
        if (outEntry.CompressedSize < 0L || outEntry.Size < 0L)
          update.SizePatchOffset = this.baseStream_.Position;
        this.WriteLEInt((int) outEntry.CompressedSize);
        this.WriteLEInt((int) outEntry.Size);
      }
      byte[] array = ZipConstants.ConvertToArray(outEntry.Flags, outEntry.Name);
      if (array.Length > (int) ushort.MaxValue)
        throw new ZipException("Entry name too long.");
      ZipExtraData zipExtraData = new ZipExtraData(outEntry.ExtraData);
      if (outEntry.LocalHeaderRequiresZip64)
      {
        zipExtraData.StartNewEntry();
        zipExtraData.AddLeLong(outEntry.Size);
        zipExtraData.AddLeLong(outEntry.CompressedSize);
        zipExtraData.AddNewEntry(1);
      }
      else
        zipExtraData.Delete(1);
      outEntry.ExtraData = zipExtraData.GetEntryData();
      this.WriteLEShort(array.Length);
      this.WriteLEShort(outEntry.ExtraData.Length);
      if (array.Length > 0)
        this.baseStream_.Write(array, 0, array.Length);
      if (outEntry.LocalHeaderRequiresZip64)
      {
        if (!zipExtraData.Find(1))
          throw new ZipException("Internal error cannot find extra data");
        update.SizePatchOffset = this.baseStream_.Position + (long) zipExtraData.CurrentReadIndex;
      }
      if (outEntry.ExtraData.Length <= 0)
        return;
      this.baseStream_.Write(outEntry.ExtraData, 0, outEntry.ExtraData.Length);
    }

    private int WriteCentralDirectoryHeader(ZipEntry entry)
    {
      if (entry.CompressedSize < 0L)
        throw new ZipException("Attempt to write central directory entry with unknown csize");
      if (entry.Size < 0L)
        throw new ZipException("Attempt to write central directory entry with unknown size");
      if (entry.Crc < 0L)
        throw new ZipException("Attempt to write central directory entry with unknown crc");
      this.WriteLEInt(33639248);
      this.WriteLEShort(51);
      this.WriteLEShort(entry.Version);
      this.WriteLEShort(entry.Flags);
      this.WriteLEShort((int) (byte) entry.CompressionMethod);
      this.WriteLEInt((int) entry.DosTime);
      this.WriteLEInt((int) entry.Crc);
      if (entry.IsZip64Forced() || entry.CompressedSize >= (long) uint.MaxValue)
        this.WriteLEInt(-1);
      else
        this.WriteLEInt((int) (entry.CompressedSize & (long) uint.MaxValue));
      if (entry.IsZip64Forced() || entry.Size >= (long) uint.MaxValue)
        this.WriteLEInt(-1);
      else
        this.WriteLEInt((int) entry.Size);
      byte[] array = ZipConstants.ConvertToArray(entry.Flags, entry.Name);
      if (array.Length > (int) ushort.MaxValue)
        throw new ZipException("Entry name is too long.");
      this.WriteLEShort(array.Length);
      ZipExtraData zipExtraData = new ZipExtraData(entry.ExtraData);
      if (entry.CentralHeaderRequiresZip64)
      {
        zipExtraData.StartNewEntry();
        if (entry.Size >= (long) uint.MaxValue || this.useZip64_ == UseZip64.On)
          zipExtraData.AddLeLong(entry.Size);
        if (entry.CompressedSize >= (long) uint.MaxValue || this.useZip64_ == UseZip64.On)
          zipExtraData.AddLeLong(entry.CompressedSize);
        if (entry.Offset >= (long) uint.MaxValue)
          zipExtraData.AddLeLong(entry.Offset);
        zipExtraData.AddNewEntry(1);
      }
      else
        zipExtraData.Delete(1);
      byte[] entryData = zipExtraData.GetEntryData();
      this.WriteLEShort(entryData.Length);
      this.WriteLEShort(entry.Comment != null ? entry.Comment.Length : 0);
      this.WriteLEShort(0);
      this.WriteLEShort(0);
      if (entry.ExternalFileAttributes != -1)
        this.WriteLEInt(entry.ExternalFileAttributes);
      else if (entry.IsDirectory)
        this.WriteLEUint(16U);
      else
        this.WriteLEUint(0U);
      if (entry.Offset >= (long) uint.MaxValue)
        this.WriteLEUint(uint.MaxValue);
      else
        this.WriteLEUint((uint) (int) entry.Offset);
      if (array.Length > 0)
        this.baseStream_.Write(array, 0, array.Length);
      if (entryData.Length > 0)
        this.baseStream_.Write(entryData, 0, entryData.Length);
      byte[] buffer = entry.Comment != null ? AsciiEncoding.Default.GetBytes(entry.Comment) : new byte[0];
      if (buffer.Length > 0)
        this.baseStream_.Write(buffer, 0, buffer.Length);
      return 46 + array.Length + entryData.Length + buffer.Length;
    }

    private void PostUpdateCleanup()
    {
      this.updateDataSource_ = (IDynamicDataSource) null;
      this.updates_ = (ArrayList) null;
      this.updateIndex_ = (Hashtable) null;
      if (this.archiveStorage_ == null)
        return;
      this.archiveStorage_.Dispose();
      this.archiveStorage_ = (IArchiveStorage) null;
    }

    private string GetTransformedFileName(string name)
    {
      INameTransform nameTransform = this.NameTransform;
      return nameTransform == null ? name : nameTransform.TransformFile(name);
    }

    private string GetTransformedDirectoryName(string name)
    {
      INameTransform nameTransform = this.NameTransform;
      return nameTransform == null ? name : nameTransform.TransformDirectory(name);
    }

    /// <summary>Get a raw memory buffer.</summary>
    /// <returns>Returns a raw memory buffer.</returns>
    private byte[] GetBuffer()
    {
      if (this.copyBuffer_ == null)
        this.copyBuffer_ = new byte[this.bufferSize_];
      return this.copyBuffer_;
    }

    private void CopyDescriptorBytes(ZipFile.ZipUpdate update, Stream dest, Stream source)
    {
      int descriptorSize = this.GetDescriptorSize(update);
      if (descriptorSize <= 0)
        return;
      byte[] buffer = this.GetBuffer();
      int count1;
      for (; descriptorSize > 0; descriptorSize -= count1)
      {
        int count2 = Math.Min(buffer.Length, descriptorSize);
        count1 = source.Read(buffer, 0, count2);
        if (count1 <= 0)
          throw new ZipException("Unxpected end of stream");
        dest.Write(buffer, 0, count1);
      }
    }

    private void CopyBytes(
      ZipFile.ZipUpdate update,
      Stream destination,
      Stream source,
      long bytesToCopy,
      bool updateCrc)
    {
      if (destination == source)
        throw new InvalidOperationException("Destination and source are the same");
      Crc32 crc32 = new Crc32();
      byte[] buffer = this.GetBuffer();
      long num1 = bytesToCopy;
      long num2 = 0;
      int count1;
      do
      {
        int count2 = buffer.Length;
        if (bytesToCopy < (long) count2)
          count2 = (int) bytesToCopy;
        count1 = source.Read(buffer, 0, count2);
        if (count1 > 0)
        {
          if (updateCrc)
            crc32.Update(buffer, 0, count1);
          destination.Write(buffer, 0, count1);
          bytesToCopy -= (long) count1;
          num2 += (long) count1;
        }
      }
      while (count1 > 0 && bytesToCopy > 0L);
      if (num2 != num1)
        throw new ZipException(string.Format("Failed to copy bytes expected {0} read {1}", (object) num1, (object) num2));
      if (!updateCrc)
        return;
      update.OutEntry.Crc = crc32.Value;
    }

    /// <summary>
    /// Get the size of the source descriptor for a <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipFile.ZipUpdate" />.
    /// </summary>
    /// <param name="update">The update to get the size for.</param>
    /// <returns>The descriptor size, zero if there isnt one.</returns>
    private int GetDescriptorSize(ZipFile.ZipUpdate update)
    {
      int descriptorSize = 0;
      if ((update.Entry.Flags & 8) != 0)
      {
        descriptorSize = 12;
        if (update.Entry.LocalHeaderRequiresZip64)
          descriptorSize = 20;
      }
      return descriptorSize;
    }

    private void CopyDescriptorBytesDirect(
      ZipFile.ZipUpdate update,
      Stream stream,
      ref long destinationPosition,
      long sourcePosition)
    {
      int descriptorSize = this.GetDescriptorSize(update);
      while (descriptorSize > 0)
      {
        int count1 = descriptorSize;
        byte[] buffer = this.GetBuffer();
        stream.Position = sourcePosition;
        int count2 = stream.Read(buffer, 0, count1);
        if (count2 <= 0)
          throw new ZipException("Unxpected end of stream");
        stream.Position = destinationPosition;
        stream.Write(buffer, 0, count2);
        descriptorSize -= count2;
        destinationPosition += (long) count2;
        sourcePosition += (long) count2;
      }
    }

    private void CopyEntryDataDirect(
      ZipFile.ZipUpdate update,
      Stream stream,
      bool updateCrc,
      ref long destinationPosition,
      ref long sourcePosition)
    {
      long compressedSize = update.Entry.CompressedSize;
      Crc32 crc32 = new Crc32();
      byte[] buffer = this.GetBuffer();
      long num1 = compressedSize;
      long num2 = 0;
      int count1;
      do
      {
        int count2 = buffer.Length;
        if (compressedSize < (long) count2)
          count2 = (int) compressedSize;
        stream.Position = sourcePosition;
        count1 = stream.Read(buffer, 0, count2);
        if (count1 > 0)
        {
          if (updateCrc)
            crc32.Update(buffer, 0, count1);
          stream.Position = destinationPosition;
          stream.Write(buffer, 0, count1);
          destinationPosition += (long) count1;
          sourcePosition += (long) count1;
          compressedSize -= (long) count1;
          num2 += (long) count1;
        }
      }
      while (count1 > 0 && compressedSize > 0L);
      if (num2 != num1)
        throw new ZipException(string.Format("Failed to copy bytes expected {0} read {1}", (object) num1, (object) num2));
      if (!updateCrc)
        return;
      update.OutEntry.Crc = crc32.Value;
    }

    private int FindExistingUpdate(ZipEntry entry)
    {
      int existingUpdate = -1;
      string transformedFileName = this.GetTransformedFileName(entry.Name);
      if (this.updateIndex_.ContainsKey((object) transformedFileName))
        existingUpdate = (int) this.updateIndex_[(object) transformedFileName];
      return existingUpdate;
    }

    private int FindExistingUpdate(string fileName)
    {
      int existingUpdate = -1;
      string transformedFileName = this.GetTransformedFileName(fileName);
      if (this.updateIndex_.ContainsKey((object) transformedFileName))
        existingUpdate = (int) this.updateIndex_[(object) transformedFileName];
      return existingUpdate;
    }

    /// <summary>
    /// Get an output stream for the specified <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry" />
    /// </summary>
    /// <param name="entry">The entry to get an output stream for.</param>
    /// <returns>The output stream obtained for the entry.</returns>
    private Stream GetOutputStream(ZipEntry entry)
    {
      Stream baseStream = this.baseStream_;
      if (entry.IsCrypted)
        throw new ZipException("Encryption not supported for Portable Class Library");
      switch (entry.CompressionMethod)
      {
        case CompressionMethod.Stored:
          return (Stream) new ZipFile.UncompressedStream(baseStream);
        case CompressionMethod.Deflated:
          return (Stream) new DeflaterOutputStream(baseStream, new Deflater(9, true))
          {
            IsStreamOwner = false
          };
        default:
          throw new ZipException("Unknown compression method " + (object) entry.CompressionMethod);
      }
    }

    private void AddEntry(ZipFile workFile, ZipFile.ZipUpdate update)
    {
      Stream source = (Stream) null;
      if (update.Entry.IsFile)
        source = update.GetSource() ?? this.updateDataSource_.GetSource(update.Entry, update.Filename);
      if (source != null)
      {
        using (source)
        {
          long length = source.Length;
          if (update.OutEntry.Size < 0L)
            update.OutEntry.Size = length;
          else if (update.OutEntry.Size != length)
            throw new ZipException("Entry size/stream size mismatch");
          workFile.WriteLocalEntryHeader(update);
          long position1 = workFile.baseStream_.Position;
          using (Stream outputStream = workFile.GetOutputStream(update.OutEntry))
            this.CopyBytes(update, outputStream, source, length, true);
          long position2 = workFile.baseStream_.Position;
          update.OutEntry.CompressedSize = position2 - position1;
          if ((update.OutEntry.Flags & 8) != 8)
            return;
          new ZipHelperStream(workFile.baseStream_).WriteDataDescriptor(update.OutEntry);
        }
      }
      else
      {
        workFile.WriteLocalEntryHeader(update);
        update.OutEntry.CompressedSize = 0L;
      }
    }

    private void ModifyEntry(ZipFile workFile, ZipFile.ZipUpdate update)
    {
      workFile.WriteLocalEntryHeader(update);
      long position1 = workFile.baseStream_.Position;
      if (update.Entry.IsFile && update.Filename != null)
      {
        using (Stream outputStream = workFile.GetOutputStream(update.OutEntry))
        {
          using (Stream inputStream = this.GetInputStream(update.Entry))
            this.CopyBytes(update, outputStream, inputStream, inputStream.Length, true);
        }
      }
      long position2 = workFile.baseStream_.Position;
      update.Entry.CompressedSize = position2 - position1;
    }

    private void CopyEntryDirect(
      ZipFile workFile,
      ZipFile.ZipUpdate update,
      ref long destinationPosition)
    {
      bool flag = false;
      if (update.Entry.Offset == destinationPosition)
        flag = true;
      if (!flag)
      {
        this.baseStream_.Position = destinationPosition;
        workFile.WriteLocalEntryHeader(update);
        destinationPosition = this.baseStream_.Position;
      }
      long offset = update.Entry.Offset + 26L;
      this.baseStream_.Seek(offset, SeekOrigin.Begin);
      long sourcePosition = this.baseStream_.Position + (long) this.ReadLEUshort() + (long) this.ReadLEUshort();
      if (flag)
      {
        if (update.OffsetBasedSize != -1L)
          destinationPosition += update.OffsetBasedSize;
        else
          destinationPosition += sourcePosition - offset + 26L + update.Entry.CompressedSize + (long) this.GetDescriptorSize(update);
      }
      else
      {
        if (update.Entry.CompressedSize > 0L)
          this.CopyEntryDataDirect(update, this.baseStream_, false, ref destinationPosition, ref sourcePosition);
        this.CopyDescriptorBytesDirect(update, this.baseStream_, ref destinationPosition, sourcePosition);
      }
    }

    private void CopyEntry(ZipFile workFile, ZipFile.ZipUpdate update)
    {
      workFile.WriteLocalEntryHeader(update);
      if (update.Entry.CompressedSize > 0L)
      {
        this.baseStream_.Seek(update.Entry.Offset + 26L, SeekOrigin.Begin);
        this.baseStream_.Seek((long) ((uint) this.ReadLEUshort() + (uint) this.ReadLEUshort()), SeekOrigin.Current);
        this.CopyBytes(update, workFile.baseStream_, this.baseStream_, update.Entry.CompressedSize, false);
      }
      this.CopyDescriptorBytes(update, workFile.baseStream_, this.baseStream_);
    }

    private void Reopen(Stream source)
    {
      if (source == null)
        throw new ZipException("Failed to reopen archive - no source");
      this.isNewArchive_ = false;
      this.baseStream_ = source;
      this.ReadEntries();
    }

    private void Reopen()
    {
      if (this.Name == null)
        throw new InvalidOperationException("Name is not known cannot Reopen");
      this.Reopen((Stream) VFS.Current.OpenReadFile(this.Name));
    }

    private void UpdateCommentOnly()
    {
      long length = this.baseStream_.Length;
      ZipHelperStream zipHelperStream;
      if (this.archiveStorage_.UpdateMode == FileUpdateMode.Safe)
      {
        zipHelperStream = new ZipHelperStream(this.archiveStorage_.MakeTemporaryCopy(this.baseStream_));
        zipHelperStream.IsStreamOwner = true;
        this.baseStream_.Dispose();
        this.baseStream_ = (Stream) null;
      }
      else if (this.archiveStorage_.UpdateMode == FileUpdateMode.Direct)
      {
        this.baseStream_ = this.archiveStorage_.OpenForDirectUpdate(this.baseStream_);
        zipHelperStream = new ZipHelperStream(this.baseStream_);
      }
      else
      {
        this.baseStream_.Dispose();
        this.baseStream_ = (Stream) null;
        zipHelperStream = new ZipHelperStream(this.Name);
      }
      using (zipHelperStream)
      {
        if (zipHelperStream.LocateBlockWithSignature(101010256, length, 22, (int) ushort.MaxValue) < 0L)
          throw new ZipException("Cannot find central directory");
        zipHelperStream.Position += 16L;
        byte[] rawComment = this.newComment_.RawComment;
        zipHelperStream.WriteLEShort(rawComment.Length);
        zipHelperStream.Write(rawComment, 0, rawComment.Length);
        zipHelperStream.SetLength(zipHelperStream.Position);
      }
      if (this.archiveStorage_.UpdateMode == FileUpdateMode.Safe)
        this.Reopen(this.archiveStorage_.ConvertTemporaryToFinal());
      else
        this.ReadEntries();
    }

    private void RunUpdates()
    {
      long sizeEntries = 0;
      bool flag = false;
      long destinationPosition = 0;
      ZipFile workFile;
      if (this.IsNewArchive)
      {
        workFile = this;
        workFile.baseStream_.Position = 0L;
        flag = true;
      }
      else if (this.archiveStorage_.UpdateMode == FileUpdateMode.Direct)
      {
        workFile = this;
        workFile.baseStream_.Position = 0L;
        flag = true;
        this.updates_.Sort((IComparer) new ZipFile.UpdateComparer());
      }
      else
      {
        workFile = ZipFile.Create(this.archiveStorage_.GetTemporaryOutput());
        workFile.UseZip64 = this.UseZip64;
        if (this.key != null)
          workFile.key = (byte[]) this.key.Clone();
      }
      long position1;
      try
      {
        foreach (ZipFile.ZipUpdate update in (List<object>) this.updates_)
        {
          if (update != null)
          {
            switch (update.Command)
            {
              case ZipFile.UpdateCommand.Copy:
                if (flag)
                {
                  this.CopyEntryDirect(workFile, update, ref destinationPosition);
                  continue;
                }
                this.CopyEntry(workFile, update);
                continue;
              case ZipFile.UpdateCommand.Modify:
                this.ModifyEntry(workFile, update);
                continue;
              case ZipFile.UpdateCommand.Add:
                if (!this.IsNewArchive && flag)
                  workFile.baseStream_.Position = destinationPosition;
                this.AddEntry(workFile, update);
                if (flag)
                {
                  destinationPosition = workFile.baseStream_.Position;
                  continue;
                }
                continue;
              default:
                continue;
            }
          }
        }
        if (!this.IsNewArchive && flag)
          workFile.baseStream_.Position = destinationPosition;
        long position2 = workFile.baseStream_.Position;
        foreach (ZipFile.ZipUpdate update in (List<object>) this.updates_)
        {
          if (update != null)
            sizeEntries += (long) workFile.WriteCentralDirectoryHeader(update.OutEntry);
        }
        byte[] comment = this.newComment_ != null ? this.newComment_.RawComment : ZipConstants.ConvertToArray(this.comment_);
        using (ZipHelperStream zipHelperStream = new ZipHelperStream(workFile.baseStream_))
          zipHelperStream.WriteEndOfCentralDirectory(this.updateCount_, sizeEntries, position2, comment);
        position1 = workFile.baseStream_.Position;
        foreach (ZipFile.ZipUpdate update in (List<object>) this.updates_)
        {
          if (update != null)
          {
            if (update.CrcPatchOffset > 0L && update.OutEntry.CompressedSize > 0L)
            {
              workFile.baseStream_.Position = update.CrcPatchOffset;
              workFile.WriteLEInt((int) update.OutEntry.Crc);
            }
            if (update.SizePatchOffset > 0L)
            {
              workFile.baseStream_.Position = update.SizePatchOffset;
              if (update.OutEntry.LocalHeaderRequiresZip64)
              {
                workFile.WriteLeLong(update.OutEntry.Size);
                workFile.WriteLeLong(update.OutEntry.CompressedSize);
              }
              else
              {
                workFile.WriteLEInt((int) update.OutEntry.CompressedSize);
                workFile.WriteLEInt((int) update.OutEntry.Size);
              }
            }
          }
        }
      }
      catch
      {
        workFile.Close();
        if (!flag && workFile.Name != null)
          VFS.Current.DeleteFile(workFile.Name);
        throw;
      }
      if (flag)
      {
        workFile.baseStream_.SetLength(position1);
        workFile.baseStream_.Flush();
        this.isNewArchive_ = false;
        this.ReadEntries();
      }
      else
      {
        this.baseStream_.Dispose();
        this.Reopen(this.archiveStorage_.ConvertTemporaryToFinal());
      }
    }

    private void CheckUpdating()
    {
      if (this.updates_ == null)
        throw new InvalidOperationException("BeginUpdate has not been called");
    }

    void IDisposable.Dispose() => this.Close();

    private void DisposeInternal(bool disposing)
    {
      if (this.isDisposed_)
        return;
      this.isDisposed_ = true;
      this.entries_ = new ZipEntry[0];
      if (this.IsStreamOwner && this.baseStream_ != null)
      {
        lock (this.baseStream_)
          this.baseStream_.Dispose();
      }
      this.PostUpdateCleanup();
    }

    /// <summary>
    /// Releases the unmanaged resources used by the this instance and optionally releases the managed resources.
    /// </summary>
    /// <param name="disposing">true to release both managed and unmanaged resources;
    /// false to release only unmanaged resources.</param>
    protected virtual void Dispose(bool disposing) => this.DisposeInternal(disposing);

    /// <summary>Read an unsigned short in little endian byte order.</summary>
    /// <returns>Returns the value read.</returns>
    /// <exception cref="T:System.IO.EndOfStreamException">
    /// The stream ends prematurely
    /// </exception>
    private ushort ReadLEUshort()
    {
      int num1 = this.baseStream_.ReadByte();
      if (num1 < 0)
        throw new EndOfStreamException("End of stream");
      int num2 = this.baseStream_.ReadByte();
      if (num2 < 0)
        throw new EndOfStreamException("End of stream");
      return (ushort) ((uint) (ushort) num1 | (uint) (ushort) (num2 << 8));
    }

    /// <summary>Read a uint in little endian byte order.</summary>
    /// <returns>Returns the value read.</returns>
    /// <exception cref="T:System.IO.IOException">An i/o error occurs.</exception>
    /// <exception cref="T:System.IO.EndOfStreamException">
    /// The file ends prematurely
    /// </exception>
    private uint ReadLEUint() => (uint) this.ReadLEUshort() | (uint) this.ReadLEUshort() << 16;

    private ulong ReadLEUlong() => (ulong) this.ReadLEUint() | (ulong) this.ReadLEUint() << 32;

    private long LocateBlockWithSignature(
      int signature,
      long endLocation,
      int minimumBlockSize,
      int maximumVariableData)
    {
      using (ZipHelperStream zipHelperStream = new ZipHelperStream(this.baseStream_))
        return zipHelperStream.LocateBlockWithSignature(signature, endLocation, minimumBlockSize, maximumVariableData);
    }

    /// <summary>
    /// Search for and read the central directory of a zip file filling the entries array.
    /// </summary>
    /// <exception cref="T:System.IO.IOException">An i/o error occurs.</exception>
    /// <exception cref="T:ICSharpCode.SharpZipLib.Zip.ZipException">
    /// The central directory is malformed or cannot be found
    /// </exception>
    private void ReadEntries()
    {
      long endLocation = this.baseStream_.CanSeek ? this.LocateBlockWithSignature(101010256, this.baseStream_.Length, 22, (int) ushort.MaxValue) : throw new ZipException("ZipFile stream must be seekable");
      if (endLocation < 0L)
        throw new ZipException("Cannot find central directory");
      ushort num1 = this.ReadLEUshort();
      ushort num2 = this.ReadLEUshort();
      ulong length1 = (ulong) this.ReadLEUshort();
      ulong num3 = (ulong) this.ReadLEUshort();
      ulong num4 = (ulong) this.ReadLEUint();
      long num5 = (long) this.ReadLEUint();
      uint length2 = (uint) this.ReadLEUshort();
      if (length2 > 0U)
      {
        byte[] numArray = new byte[(IntPtr) length2];
        StreamUtils.ReadFully(this.baseStream_, numArray);
        this.comment_ = ZipConstants.ConvertToString(numArray);
      }
      else
        this.comment_ = string.Empty;
      bool flag = false;
      if (num1 == ushort.MaxValue || num2 == ushort.MaxValue || length1 == (ulong) ushort.MaxValue || num3 == (ulong) ushort.MaxValue || num4 == (ulong) uint.MaxValue || num5 == (long) uint.MaxValue)
      {
        flag = true;
        if (this.LocateBlockWithSignature(117853008, endLocation, 0, 4096) < 0L)
          throw new ZipException("Cannot find Zip64 locator");
        int num6 = (int) this.ReadLEUint();
        ulong num7 = this.ReadLEUlong();
        int num8 = (int) this.ReadLEUint();
        this.baseStream_.Position = (long) num7;
        if (this.ReadLEUint() != 101075792U)
          throw new ZipException(string.Format("Invalid Zip64 Central directory signature at {0:X}", (object) num7));
        long num9 = (long) this.ReadLEUlong();
        int num10 = (int) this.ReadLEUshort();
        int num11 = (int) this.ReadLEUshort();
        int num12 = (int) this.ReadLEUint();
        int num13 = (int) this.ReadLEUint();
        length1 = this.ReadLEUlong();
        this.ReadLEUlong();
        num4 = this.ReadLEUlong();
        num5 = (long) this.ReadLEUlong();
      }
      this.entries_ = new ZipEntry[length1];
      if (!flag && num5 < endLocation - (4L + (long) num4))
      {
        this.offsetOfFirstEntry = endLocation - (4L + (long) num4 + num5);
        if (this.offsetOfFirstEntry <= 0L)
          throw new ZipException("Invalid embedded zip archive");
      }
      this.baseStream_.Seek(this.offsetOfFirstEntry + num5, SeekOrigin.Begin);
      for (ulong index = 0; index < length1; ++index)
      {
        if (this.ReadLEUint() != 33639248U)
          throw new ZipException("Wrong Central Directory signature");
        int madeByInfo = (int) this.ReadLEUshort();
        int versionRequiredToExtract = (int) this.ReadLEUshort();
        int flags = (int) this.ReadLEUshort();
        int method = (int) this.ReadLEUshort();
        uint num14 = this.ReadLEUint();
        uint num15 = this.ReadLEUint();
        long num16 = (long) this.ReadLEUint();
        long num17 = (long) this.ReadLEUint();
        int num18 = (int) this.ReadLEUshort();
        int length3 = (int) this.ReadLEUshort();
        int num19 = (int) this.ReadLEUshort();
        int num20 = (int) this.ReadLEUshort();
        int num21 = (int) this.ReadLEUshort();
        uint num22 = this.ReadLEUint();
        long num23 = (long) this.ReadLEUint();
        byte[] numArray = new byte[Math.Max(num18, num19)];
        StreamUtils.ReadFully(this.baseStream_, numArray, 0, num18);
        ZipEntry zipEntry = new ZipEntry(ZipConstants.ConvertToStringExt(flags, numArray, num18), versionRequiredToExtract, madeByInfo, (CompressionMethod) method);
        zipEntry.Crc = (long) num15 & (long) uint.MaxValue;
        zipEntry.Size = num17 & (long) uint.MaxValue;
        zipEntry.CompressedSize = num16 & (long) uint.MaxValue;
        zipEntry.Flags = flags;
        zipEntry.DosTime = (long) num14;
        zipEntry.ZipFileIndex = (long) index;
        zipEntry.Offset = num23;
        zipEntry.ExternalFileAttributes = (int) num22;
        zipEntry.CryptoCheckValue = (flags & 8) != 0 ? (byte) (num14 >> 8 & (uint) byte.MaxValue) : (byte) (num15 >> 24);
        if (length3 > 0)
        {
          byte[] buffer = new byte[length3];
          StreamUtils.ReadFully(this.baseStream_, buffer);
          zipEntry.ExtraData = buffer;
        }
        zipEntry.ProcessExtraData(false);
        if (num19 > 0)
        {
          StreamUtils.ReadFully(this.baseStream_, numArray, 0, num19);
          zipEntry.Comment = ZipConstants.ConvertToStringExt(flags, numArray, num19);
        }
        this.entries_[index] = zipEntry;
      }
    }

    /// <summary>Locate the data for a given entry.</summary>
    /// <returns>The start offset of the data.</returns>
    /// <exception cref="T:System.IO.EndOfStreamException">
    /// The stream ends prematurely
    /// </exception>
    /// <exception cref="T:ICSharpCode.SharpZipLib.Zip.ZipException">
    /// The local header signature is invalid, the entry and central header file name lengths are different
    /// or the local and entry compression methods dont match
    /// </exception>
    private long LocateEntry(ZipEntry entry)
    {
      return this.TestLocalHeader(entry, ZipFile.HeaderTest.Extract);
    }

    private static void WriteEncryptionHeader(Stream stream, long crcValue)
    {
      byte[] buffer = new byte[12];
      new Random().NextBytes(buffer);
      buffer[11] = (byte) (crcValue >> 24);
      stream.Write(buffer, 0, buffer.Length);
    }

    /// <summary>
    /// Delegate for handling keys/password setting during compresion/decompression.
    /// </summary>
    public delegate void KeysRequiredEventHandler(object sender, KeysRequiredEventArgs e);

    [Flags]
    private enum HeaderTest
    {
      Extract = 1,
      Header = 2,
    }

    /// <summary>The kind of update to apply.</summary>
    private enum UpdateCommand
    {
      Copy,
      Modify,
      Add,
    }

    /// <summary>Class used to sort updates.</summary>
    private class UpdateComparer : IComparer
    {
      /// <summary>
      /// Compares two objects and returns a value indicating whether one is
      /// less than, equal to or greater than the other.
      /// </summary>
      /// <param name="x">First object to compare</param>
      /// <param name="y">Second object to compare.</param>
      /// <returns>Compare result.</returns>
      public int Compare(object x, object y)
      {
        ZipFile.ZipUpdate zipUpdate1 = x as ZipFile.ZipUpdate;
        ZipFile.ZipUpdate zipUpdate2 = y as ZipFile.ZipUpdate;
        int num1;
        if (zipUpdate1 == null)
          num1 = zipUpdate2 != null ? -1 : 0;
        else if (zipUpdate2 == null)
        {
          num1 = 1;
        }
        else
        {
          num1 = (zipUpdate1.Command == ZipFile.UpdateCommand.Copy || zipUpdate1.Command == ZipFile.UpdateCommand.Modify ? 0 : 1) - (zipUpdate2.Command == ZipFile.UpdateCommand.Copy || zipUpdate2.Command == ZipFile.UpdateCommand.Modify ? 0 : 1);
          if (num1 == 0)
          {
            long num2 = zipUpdate1.Entry.Offset - zipUpdate2.Entry.Offset;
            num1 = num2 >= 0L ? (num2 != 0L ? 1 : 0) : -1;
          }
        }
        return num1;
      }
    }

    /// <summary>Represents a pending update to a Zip file.</summary>
    private class ZipUpdate
    {
      private ZipEntry entry_;
      private ZipEntry outEntry_;
      private ZipFile.UpdateCommand command_;
      private IStaticDataSource dataSource_;
      private string filename_;
      private long sizePatchOffset_ = -1;
      private long crcPatchOffset_ = -1;
      private long _offsetBasedSize = -1;

      public ZipUpdate(string fileName, ZipEntry entry)
      {
        this.command_ = ZipFile.UpdateCommand.Add;
        this.entry_ = entry;
        this.filename_ = fileName;
      }

      [Obsolete]
      public ZipUpdate(string fileName, string entryName, CompressionMethod compressionMethod)
      {
        this.command_ = ZipFile.UpdateCommand.Add;
        this.entry_ = new ZipEntry(entryName);
        this.entry_.CompressionMethod = compressionMethod;
        this.filename_ = fileName;
      }

      [Obsolete]
      public ZipUpdate(string fileName, string entryName)
        : this(fileName, entryName, CompressionMethod.Deflated)
      {
      }

      [Obsolete]
      public ZipUpdate(
        IStaticDataSource dataSource,
        string entryName,
        CompressionMethod compressionMethod)
      {
        this.command_ = ZipFile.UpdateCommand.Add;
        this.entry_ = new ZipEntry(entryName);
        this.entry_.CompressionMethod = compressionMethod;
        this.dataSource_ = dataSource;
      }

      public ZipUpdate(IStaticDataSource dataSource, ZipEntry entry)
      {
        this.command_ = ZipFile.UpdateCommand.Add;
        this.entry_ = entry;
        this.dataSource_ = dataSource;
      }

      public ZipUpdate(ZipEntry original, ZipEntry updated)
      {
        throw new ZipException("Modify not currently supported");
      }

      public ZipUpdate(ZipFile.UpdateCommand command, ZipEntry entry)
      {
        this.command_ = command;
        this.entry_ = (ZipEntry) entry.Clone();
      }

      /// <summary>Copy an existing entry.</summary>
      /// <param name="entry">The existing entry to copy.</param>
      public ZipUpdate(ZipEntry entry)
        : this(ZipFile.UpdateCommand.Copy, entry)
      {
      }

      /// <summary>
      /// Get the <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry" /> for this update.
      /// </summary>
      /// <remarks>This is the source or original entry.</remarks>
      public ZipEntry Entry => this.entry_;

      /// <summary>
      /// Get the <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry" /> that will be written to the updated/new file.
      /// </summary>
      public ZipEntry OutEntry
      {
        get
        {
          if (this.outEntry_ == null)
            this.outEntry_ = (ZipEntry) this.entry_.Clone();
          return this.outEntry_;
        }
      }

      /// <summary>Get the command for this update.</summary>
      public ZipFile.UpdateCommand Command => this.command_;

      /// <summary>
      /// Get the filename if any for this update.  Null if none exists.
      /// </summary>
      public string Filename => this.filename_;

      /// <summary>Get/set the location of the size patch for this update.</summary>
      public long SizePatchOffset
      {
        get => this.sizePatchOffset_;
        set => this.sizePatchOffset_ = value;
      }

      /// <summary>Get /set the location of the crc patch for this update.</summary>
      public long CrcPatchOffset
      {
        get => this.crcPatchOffset_;
        set => this.crcPatchOffset_ = value;
      }

      /// <summary>
      /// Get/set the size calculated by offset.
      /// Specifically, the difference between this and next entry's starting offset.
      /// </summary>
      public long OffsetBasedSize
      {
        get => this._offsetBasedSize;
        set => this._offsetBasedSize = value;
      }

      public Stream GetSource()
      {
        Stream source = (Stream) null;
        if (this.dataSource_ != null)
          source = this.dataSource_.GetSource();
        return source;
      }
    }

    /// <summary>
    /// Represents a string from a <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipFile" /> which is stored as an array of bytes.
    /// </summary>
    private class ZipString
    {
      private string comment_;
      private byte[] rawComment_;
      private bool isSourceString_;

      /// <summary>
      /// Initialise a <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipFile.ZipString" /> with a string.
      /// </summary>
      /// <param name="comment">The textual string form.</param>
      public ZipString(string comment)
      {
        this.comment_ = comment;
        this.isSourceString_ = true;
      }

      /// <summary>
      /// Initialise a <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipFile.ZipString" /> using a string in its binary 'raw' form.
      /// </summary>
      /// <param name="rawString"></param>
      public ZipString(byte[] rawString) => this.rawComment_ = rawString;

      /// <summary>
      /// Get a value indicating the original source of data for this instance.
      /// True if the source was a string; false if the source was binary data.
      /// </summary>
      public bool IsSourceString => this.isSourceString_;

      /// <summary>
      /// Get the length of the comment when represented as raw bytes.
      /// </summary>
      public int RawLength
      {
        get
        {
          this.MakeBytesAvailable();
          return this.rawComment_.Length;
        }
      }

      /// <summary>Get the comment in its 'raw' form as plain bytes.</summary>
      public byte[] RawComment
      {
        get
        {
          this.MakeBytesAvailable();
          return (byte[]) this.rawComment_.Clone();
        }
      }

      /// <summary>Reset the comment to its initial state.</summary>
      public void Reset()
      {
        if (this.isSourceString_)
          this.rawComment_ = (byte[]) null;
        else
          this.comment_ = (string) null;
      }

      private void MakeTextAvailable()
      {
        if (this.comment_ != null)
          return;
        this.comment_ = ZipConstants.ConvertToString(this.rawComment_);
      }

      private void MakeBytesAvailable()
      {
        if (this.rawComment_ != null)
          return;
        this.rawComment_ = ZipConstants.ConvertToArray(this.comment_);
      }

      /// <summary>Implicit conversion of comment to a string.</summary>
      /// <param name="zipString">The <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipFile.ZipString" /> to convert to a string.</param>
      /// <returns>The textual equivalent for the input value.</returns>
      public static implicit operator string(ZipFile.ZipString zipString)
      {
        zipString.MakeTextAvailable();
        return zipString.comment_;
      }
    }

    /// <summary>
    /// An <see cref="T:System.Collections.IEnumerator">enumerator</see> for <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipEntry">Zip entries</see>
    /// </summary>
    private class ZipEntryEnumerator : IEnumerator
    {
      private ZipEntry[] array;
      private int index = -1;

      public ZipEntryEnumerator(ZipEntry[] entries) => this.array = entries;

      public object Current => (object) this.array[this.index];

      public void Reset() => this.index = -1;

      public bool MoveNext() => ++this.index < this.array.Length;
    }

    /// <summary>
    /// An <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipFile.UncompressedStream" /> is a stream that you can write uncompressed data
    /// to and flush, but cannot read, seek or do anything else to.
    /// </summary>
    private class UncompressedStream : Stream
    {
      private Stream baseStream_;

      public UncompressedStream(Stream baseStream) => this.baseStream_ = baseStream;

      /// <summary>
      /// Gets a value indicating whether the current stream supports reading.
      /// </summary>
      public override bool CanRead => false;

      /// <summary>Write any buffered data to underlying storage.</summary>
      public override void Flush() => this.baseStream_.Flush();

      /// <summary>
      /// Gets a value indicating whether the current stream supports writing.
      /// </summary>
      public override bool CanWrite => this.baseStream_.CanWrite;

      /// <summary>
      /// Gets a value indicating whether the current stream supports seeking.
      /// </summary>
      public override bool CanSeek => false;

      /// <summary>Get the length in bytes of the stream.</summary>
      public override long Length => 0;

      /// <summary>Gets or sets the position within the current stream.</summary>
      public override long Position
      {
        get => this.baseStream_.Position;
        set
        {
        }
      }

      /// <summary>
      /// Reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
      /// </summary>
      /// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between offset and (offset + count - 1) replaced by the bytes read from the current source.</param>
      /// <param name="offset">The zero-based byte offset in buffer at which to begin storing the data read from the current stream.</param>
      /// <param name="count">The maximum number of bytes to be read from the current stream.</param>
      /// <returns>
      /// The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.
      /// </returns>
      /// <exception cref="T:System.ArgumentException">The sum of offset and count is larger than the buffer length. </exception>
      /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
      /// <exception cref="T:System.NotSupportedException">The stream does not support reading. </exception>
      /// <exception cref="T:System.ArgumentNullException">buffer is null. </exception>
      /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
      /// <exception cref="T:System.ArgumentOutOfRangeException">offset or count is negative. </exception>
      public override int Read(byte[] buffer, int offset, int count) => 0;

      /// <summary>Sets the position within the current stream.</summary>
      /// <param name="offset">A byte offset relative to the origin parameter.</param>
      /// <param name="origin">A value of type <see cref="T:System.IO.SeekOrigin"></see> indicating the reference point used to obtain the new position.</param>
      /// <returns>The new position within the current stream.</returns>
      /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
      /// <exception cref="T:System.NotSupportedException">The stream does not support seeking, such as if the stream is constructed from a pipe or console output. </exception>
      /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
      public override long Seek(long offset, SeekOrigin origin) => 0;

      /// <summary>Sets the length of the current stream.</summary>
      /// <param name="value">The desired length of the current stream in bytes.</param>
      /// <exception cref="T:System.NotSupportedException">The stream does not support both writing and seeking, such as if the stream is constructed from a pipe or console output. </exception>
      /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
      /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
      public override void SetLength(long value)
      {
      }

      /// <summary>
      /// Writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
      /// </summary>
      /// <param name="buffer">An array of bytes. This method copies count bytes from buffer to the current stream.</param>
      /// <param name="offset">The zero-based byte offset in buffer at which to begin copying bytes to the current stream.</param>
      /// <param name="count">The number of bytes to be written to the current stream.</param>
      /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
      /// <exception cref="T:System.NotSupportedException">The stream does not support writing. </exception>
      /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
      /// <exception cref="T:System.ArgumentNullException">buffer is null. </exception>
      /// <exception cref="T:System.ArgumentException">The sum of offset and count is greater than the buffer length. </exception>
      /// <exception cref="T:System.ArgumentOutOfRangeException">offset or count is negative. </exception>
      public override void Write(byte[] buffer, int offset, int count)
      {
        this.baseStream_.Write(buffer, offset, count);
      }
    }

    /// <summary>
    /// A <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipFile.PartialInputStream" /> is an <see cref="T:ICSharpCode.SharpZipLib.Zip.Compression.Streams.InflaterInputStream" />
    /// whose data is only a part or subsection of a file.
    /// </summary>
    private class PartialInputStream : Stream
    {
      private ZipFile zipFile_;
      private Stream baseStream_;
      private long start_;
      private long length_;
      private long readPos_;
      private long end_;

      /// <summary>
      /// Initialise a new instance of the <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipFile.PartialInputStream" /> class.
      /// </summary>
      /// <param name="zipFile">The <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipFile" /> containing the underlying stream to use for IO.</param>
      /// <param name="start">The start of the partial data.</param>
      /// <param name="length">The length of the partial data.</param>
      public PartialInputStream(ZipFile zipFile, long start, long length)
      {
        this.start_ = start;
        this.length_ = length;
        this.zipFile_ = zipFile;
        this.baseStream_ = this.zipFile_.baseStream_;
        this.readPos_ = start;
        this.end_ = start + length;
      }

      /// <summary>Read a byte from this stream.</summary>
      /// <returns>Returns the byte read or -1 on end of stream.</returns>
      public override int ReadByte()
      {
        if (this.readPos_ >= this.end_)
          return -1;
        lock (this.baseStream_)
        {
          this.baseStream_.Seek(this.readPos_++, SeekOrigin.Begin);
          return this.baseStream_.ReadByte();
        }
      }

      /// <summary>
      /// Reads a sequence of bytes from the current stream and advances the position within the stream by the number of bytes read.
      /// </summary>
      /// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between offset and (offset + count - 1) replaced by the bytes read from the current source.</param>
      /// <param name="offset">The zero-based byte offset in buffer at which to begin storing the data read from the current stream.</param>
      /// <param name="count">The maximum number of bytes to be read from the current stream.</param>
      /// <returns>
      /// The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the stream has been reached.
      /// </returns>
      /// <exception cref="T:System.ArgumentException">The sum of offset and count is larger than the buffer length. </exception>
      /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
      /// <exception cref="T:System.NotSupportedException">The stream does not support reading. </exception>
      /// <exception cref="T:System.ArgumentNullException">buffer is null. </exception>
      /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
      /// <exception cref="T:System.ArgumentOutOfRangeException">offset or count is negative. </exception>
      public override int Read(byte[] buffer, int offset, int count)
      {
        lock (this.baseStream_)
        {
          if ((long) count > this.end_ - this.readPos_)
          {
            count = (int) (this.end_ - this.readPos_);
            if (count == 0)
              return 0;
          }
          this.baseStream_.Seek(this.readPos_, SeekOrigin.Begin);
          int num = this.baseStream_.Read(buffer, offset, count);
          if (num > 0)
            this.readPos_ += (long) num;
          return num;
        }
      }

      /// <summary>
      /// Writes a sequence of bytes to the current stream and advances the current position within this stream by the number of bytes written.
      /// </summary>
      /// <param name="buffer">An array of bytes. This method copies count bytes from buffer to the current stream.</param>
      /// <param name="offset">The zero-based byte offset in buffer at which to begin copying bytes to the current stream.</param>
      /// <param name="count">The number of bytes to be written to the current stream.</param>
      /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
      /// <exception cref="T:System.NotSupportedException">The stream does not support writing. </exception>
      /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
      /// <exception cref="T:System.ArgumentNullException">buffer is null. </exception>
      /// <exception cref="T:System.ArgumentException">The sum of offset and count is greater than the buffer length. </exception>
      /// <exception cref="T:System.ArgumentOutOfRangeException">offset or count is negative. </exception>
      public override void Write(byte[] buffer, int offset, int count)
      {
        throw new NotSupportedException();
      }

      /// <summary>
      /// When overridden in a derived class, sets the length of the current stream.
      /// </summary>
      /// <param name="value">The desired length of the current stream in bytes.</param>
      /// <exception cref="T:System.NotSupportedException">The stream does not support both writing and seeking, such as if the stream is constructed from a pipe or console output. </exception>
      /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
      /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
      public override void SetLength(long value) => throw new NotSupportedException();

      /// <summary>
      /// When overridden in a derived class, sets the position within the current stream.
      /// </summary>
      /// <param name="offset">A byte offset relative to the origin parameter.</param>
      /// <param name="origin">A value of type <see cref="T:System.IO.SeekOrigin"></see> indicating the reference point used to obtain the new position.</param>
      /// <returns>The new position within the current stream.</returns>
      /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
      /// <exception cref="T:System.NotSupportedException">The stream does not support seeking, such as if the stream is constructed from a pipe or console output. </exception>
      /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
      public override long Seek(long offset, SeekOrigin origin)
      {
        long num = this.readPos_;
        switch (origin)
        {
          case SeekOrigin.Begin:
            num = this.start_ + offset;
            break;
          case SeekOrigin.Current:
            num = this.readPos_ + offset;
            break;
          case SeekOrigin.End:
            num = this.end_ + offset;
            break;
        }
        if (num < this.start_)
          throw new ArgumentException("Negative position is invalid");
        this.readPos_ = num < this.end_ ? num : throw new IOException("Cannot seek past end");
        return this.readPos_;
      }

      /// <summary>
      /// Clears all buffers for this stream and causes any buffered data to be written to the underlying device.
      /// </summary>
      /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
      public override void Flush()
      {
      }

      /// <summary>Gets or sets the position within the current stream.</summary>
      /// <value></value>
      /// <returns>The current position within the stream.</returns>
      /// <exception cref="T:System.IO.IOException">An I/O error occurs. </exception>
      /// <exception cref="T:System.NotSupportedException">The stream does not support seeking. </exception>
      /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
      public override long Position
      {
        get => this.readPos_ - this.start_;
        set
        {
          long num = this.start_ + value;
          if (num < this.start_)
            throw new ArgumentException("Negative position is invalid");
          this.readPos_ = num < this.end_ ? num : throw new InvalidOperationException("Cannot seek past end");
        }
      }

      /// <summary>Gets the length in bytes of the stream.</summary>
      /// <value></value>
      /// <returns>A long value representing the length of the stream in bytes.</returns>
      /// <exception cref="T:System.NotSupportedException">A class derived from Stream does not support seeking. </exception>
      /// <exception cref="T:System.ObjectDisposedException">Methods were called after the stream was closed. </exception>
      public override long Length => this.length_;

      /// <summary>
      /// Gets a value indicating whether the current stream supports writing.
      /// </summary>
      /// <value>false</value>
      /// <returns>true if the stream supports writing; otherwise, false.</returns>
      public override bool CanWrite => false;

      /// <summary>
      /// Gets a value indicating whether the current stream supports seeking.
      /// </summary>
      /// <value>true</value>
      /// <returns>true if the stream supports seeking; otherwise, false.</returns>
      public override bool CanSeek => true;

      /// <summary>
      /// Gets a value indicating whether the current stream supports reading.
      /// </summary>
      /// <value>true.</value>
      /// <returns>true if the stream supports reading; otherwise, false.</returns>
      public override bool CanRead => true;

      /// <summary>
      /// Gets a value that determines whether the current stream can time out.
      /// </summary>
      /// <value></value>
      /// <returns>A value that determines whether the current stream can time out.</returns>
      public override bool CanTimeout => this.baseStream_.CanTimeout;
    }
  }
}

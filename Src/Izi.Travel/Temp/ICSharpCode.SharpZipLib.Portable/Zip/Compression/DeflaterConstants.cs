// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.Compression.DeflaterConstants
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip.Compression
{
  /// <summary>This class contains constants used for deflation.</summary>
  public class DeflaterConstants
  {
    /// <summary>Set to true to enable debugging</summary>
    public const bool DEBUGGING = false;
    /// <summary>Written to Zip file to identify a stored block</summary>
    public const int STORED_BLOCK = 0;
    /// <summary>Identifies static tree in Zip file</summary>
    public const int STATIC_TREES = 1;
    /// <summary>Identifies dynamic tree in Zip file</summary>
    public const int DYN_TREES = 2;
    /// <summary>
    /// Header flag indicating a preset dictionary for deflation
    /// </summary>
    public const int PRESET_DICT = 32;
    /// <summary>Sets internal buffer sizes for Huffman encoding</summary>
    public const int DEFAULT_MEM_LEVEL = 8;
    /// <summary>Internal compression engine constant</summary>
    public const int MAX_MATCH = 258;
    /// <summary>Internal compression engine constant</summary>
    public const int MIN_MATCH = 3;
    /// <summary>Internal compression engine constant</summary>
    public const int MAX_WBITS = 15;
    /// <summary>Internal compression engine constant</summary>
    public const int WSIZE = 32768;
    /// <summary>Internal compression engine constant</summary>
    public const int WMASK = 32767;
    /// <summary>Internal compression engine constant</summary>
    public const int HASH_BITS = 15;
    /// <summary>Internal compression engine constant</summary>
    public const int HASH_SIZE = 32768;
    /// <summary>Internal compression engine constant</summary>
    public const int HASH_MASK = 32767;
    /// <summary>Internal compression engine constant</summary>
    public const int HASH_SHIFT = 5;
    /// <summary>Internal compression engine constant</summary>
    public const int MIN_LOOKAHEAD = 262;
    /// <summary>Internal compression engine constant</summary>
    public const int MAX_DIST = 32506;
    /// <summary>Internal compression engine constant</summary>
    public const int PENDING_BUF_SIZE = 65536;
    /// <summary>Internal compression engine constant</summary>
    public const int DEFLATE_STORED = 0;
    /// <summary>Internal compression engine constant</summary>
    public const int DEFLATE_FAST = 1;
    /// <summary>Internal compression engine constant</summary>
    public const int DEFLATE_SLOW = 2;
    /// <summary>Internal compression engine constant</summary>
    public static int MAX_BLOCK_SIZE = Math.Min((int) ushort.MaxValue, 65531);
    /// <summary>Internal compression engine constant</summary>
    public static int[] GOOD_LENGTH = new int[10]
    {
      0,
      4,
      4,
      4,
      4,
      8,
      8,
      8,
      32,
      32
    };
    /// <summary>Internal compression engine constant</summary>
    public static int[] MAX_LAZY = new int[10]
    {
      0,
      4,
      5,
      6,
      4,
      16,
      16,
      32,
      128,
      258
    };
    /// <summary>Internal compression engine constant</summary>
    public static int[] NICE_LENGTH = new int[10]
    {
      0,
      8,
      16,
      32,
      16,
      32,
      128,
      128,
      258,
      258
    };
    /// <summary>Internal compression engine constant</summary>
    public static int[] MAX_CHAIN = new int[10]
    {
      0,
      4,
      8,
      32,
      16,
      32,
      128,
      256,
      1024,
      4096
    };
    /// <summary>Internal compression engine constant</summary>
    public static int[] COMPR_FUNC = new int[10]
    {
      0,
      1,
      1,
      1,
      1,
      2,
      2,
      2,
      2,
      2
    };
  }
}

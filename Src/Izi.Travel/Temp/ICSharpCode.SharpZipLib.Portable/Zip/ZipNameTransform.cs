// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.ZipNameTransform
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using ICSharpCode.SharpZipLib.Core;
using System;
using System.IO;
using System.Text;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>
  /// ZipNameTransform transforms names as per the Zip file naming convention.
  /// </summary>
  /// <remarks>The use of absolute names is supported although its use is not valid
  /// according to Zip naming conventions, and should not be used if maximum compatability is desired.</remarks>
  public class ZipNameTransform : INameTransform
  {
    private string trimPrefix_;
    private static readonly char[] InvalidEntryChars;
    private static readonly char[] InvalidEntryCharsRelaxed;

    /// <summary>
    /// Initialize a new instance of <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipNameTransform"></see>
    /// </summary>
    public ZipNameTransform()
    {
    }

    /// <summary>
    /// Initialize a new instance of <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipNameTransform"></see>
    /// </summary>
    /// <param name="trimPrefix">The string to trim from the front of paths if found.</param>
    public ZipNameTransform(string trimPrefix) => this.TrimPrefix = trimPrefix;

    /// <summary>Static constructor.</summary>
    static ZipNameTransform()
    {
      char[] invalidPathChars = Path.GetInvalidPathChars();
      int length1 = invalidPathChars.Length + 2;
      ZipNameTransform.InvalidEntryCharsRelaxed = new char[length1];
      Array.Copy((Array) invalidPathChars, 0, (Array) ZipNameTransform.InvalidEntryCharsRelaxed, 0, invalidPathChars.Length);
      ZipNameTransform.InvalidEntryCharsRelaxed[length1 - 1] = '*';
      ZipNameTransform.InvalidEntryCharsRelaxed[length1 - 2] = '?';
      int length2 = invalidPathChars.Length + 4;
      ZipNameTransform.InvalidEntryChars = new char[length2];
      Array.Copy((Array) invalidPathChars, 0, (Array) ZipNameTransform.InvalidEntryChars, 0, invalidPathChars.Length);
      ZipNameTransform.InvalidEntryChars[length2 - 1] = ':';
      ZipNameTransform.InvalidEntryChars[length2 - 2] = '\\';
      ZipNameTransform.InvalidEntryChars[length2 - 3] = '*';
      ZipNameTransform.InvalidEntryChars[length2 - 4] = '?';
    }

    /// <summary>
    /// Transform a windows directory name according to the Zip file naming conventions.
    /// </summary>
    /// <param name="name">The directory name to transform.</param>
    /// <returns>The transformed name.</returns>
    public string TransformDirectory(string name)
    {
      name = this.TransformFile(name);
      if (name.Length <= 0)
        throw new ZipException("Cannot have an empty directory name");
      if (!name.EndsWith("/"))
        name += "/";
      return name;
    }

    /// <summary>
    /// Transform a windows file name according to the Zip file naming conventions.
    /// </summary>
    /// <param name="name">The file name to transform.</param>
    /// <returns>The transformed name.</returns>
    public string TransformFile(string name)
    {
      if (name != null)
      {
        string lower = name.ToLower();
        if (this.trimPrefix_ != null && lower.IndexOf(this.trimPrefix_) == 0)
          name = name.Substring(this.trimPrefix_.Length);
        name = name.Replace("\\", "/");
        name = WindowsPathUtils.DropPathRoot(name);
        while (name.Length > 0 && name[0] == '/')
          name = name.Remove(0, 1);
        while (name.Length > 0 && name[name.Length - 1] == '/')
          name = name.Remove(name.Length - 1, 1);
        for (int startIndex = name.IndexOf("//"); startIndex >= 0; startIndex = name.IndexOf("//"))
          name = name.Remove(startIndex, 1);
        name = ZipNameTransform.MakeValidName(name, '_');
      }
      else
        name = string.Empty;
      return name;
    }

    /// <summary>
    /// Get/set the path prefix to be trimmed from paths if present.
    /// </summary>
    /// <remarks>The prefix is trimmed before any conversion from
    /// a windows path is done.</remarks>
    public string TrimPrefix
    {
      get => this.trimPrefix_;
      set
      {
        this.trimPrefix_ = value;
        if (this.trimPrefix_ == null)
          return;
        this.trimPrefix_ = this.trimPrefix_.ToLower();
      }
    }

    /// <summary>
    /// Force a name to be valid by replacing invalid characters with a fixed value
    /// </summary>
    /// <param name="name">The name to force valid</param>
    /// <param name="replacement">The replacement character to use.</param>
    /// <returns>Returns a valid name</returns>
    private static string MakeValidName(string name, char replacement)
    {
      int index = name.IndexOfAny(ZipNameTransform.InvalidEntryChars);
      if (index >= 0)
      {
        StringBuilder stringBuilder = new StringBuilder(name);
        for (; index >= 0; index = index < name.Length ? name.IndexOfAny(ZipNameTransform.InvalidEntryChars, index + 1) : -1)
          stringBuilder[index] = replacement;
        name = stringBuilder.ToString();
      }
      return name.Length <= (int) ushort.MaxValue ? name : throw new PathTooLongException();
    }

    /// <summary>
    /// Test a name to see if it is a valid name for a zip entry.
    /// </summary>
    /// <param name="name">The name to test.</param>
    /// <param name="relaxed">If true checking is relaxed about windows file names and absolute paths.</param>
    /// <returns>Returns true if the name is a valid zip name; false otherwise.</returns>
    /// <remarks>Zip path names are actually in Unix format, and should only contain relative paths.
    /// This means that any path stored should not contain a drive or
    /// device letter, or a leading slash.  All slashes should forward slashes '/'.
    /// An empty name is valid for a file where the input comes from standard input.
    /// A null name is not considered valid.
    /// </remarks>
    public static bool IsValidName(string name, bool relaxed)
    {
      bool flag = name != null;
      if (flag)
        flag = !relaxed ? name.IndexOfAny(ZipNameTransform.InvalidEntryChars) < 0 && name.IndexOf('/') != 0 : name.IndexOfAny(ZipNameTransform.InvalidEntryCharsRelaxed) < 0;
      return flag;
    }

    /// <summary>
    /// Test a name to see if it is a valid name for a zip entry.
    /// </summary>
    /// <param name="name">The name to test.</param>
    /// <returns>Returns true if the name is a valid zip name; false otherwise.</returns>
    /// <remarks>Zip path names are actually in unix format,
    /// and should only contain relative paths if a path is present.
    /// This means that the path stored should not contain a drive or
    /// device letter, or a leading slash.  All slashes should forward slashes '/'.
    /// An empty name is valid where the input comes from standard input.
    /// A null name is not considered valid.
    /// </remarks>
    public static bool IsValidName(string name)
    {
      return name != null && name.IndexOfAny(ZipNameTransform.InvalidEntryChars) < 0 && name.IndexOf('/') != 0;
    }
  }
}

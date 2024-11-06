// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.WindowsNameTransform
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
  /// WindowsNameTransform transforms <see cref="T:ICSharpCode.SharpZipLib.Zip.ZipFile" /> names to windows compatible ones.
  /// </summary>
  public class WindowsNameTransform : INameTransform
  {
    /// <summary>The maximum windows path name permitted.</summary>
    /// <remarks>This may not valid for all windows systems - CE?, etc but I cant find the equivalent in the CLR.</remarks>
    private const int MaxPath = 260;
    private string _baseDirectory;
    private bool _trimIncomingPaths;
    private char _replacementChar = '_';
    private static readonly char[] InvalidEntryChars;

    /// <summary>
    /// Initialises a new instance of <see cref="T:ICSharpCode.SharpZipLib.Zip.WindowsNameTransform" />
    /// </summary>
    /// <param name="baseDirectory"></param>
    public WindowsNameTransform(string baseDirectory)
    {
      this.BaseDirectory = baseDirectory != null ? baseDirectory : throw new ArgumentNullException(nameof (baseDirectory), "Directory name is invalid");
    }

    /// <summary>
    /// Initialise a default instance of <see cref="T:ICSharpCode.SharpZipLib.Zip.WindowsNameTransform" />
    /// </summary>
    public WindowsNameTransform()
    {
    }

    /// <summary>
    /// Gets or sets a value containing the target directory to prefix values with.
    /// </summary>
    public string BaseDirectory
    {
      get => this._baseDirectory;
      set
      {
        this._baseDirectory = value != null ? VFS.Current.GetFullPath(value) : throw new ArgumentNullException(nameof (value));
      }
    }

    /// <summary>
    /// Gets or sets a value indicating wether paths on incoming values should be removed.
    /// </summary>
    public bool TrimIncomingPaths
    {
      get => this._trimIncomingPaths;
      set => this._trimIncomingPaths = value;
    }

    /// <summary>
    /// Transform a Zip directory name to a windows directory name.
    /// </summary>
    /// <param name="name">The directory name to transform.</param>
    /// <returns>The transformed name.</returns>
    public string TransformDirectory(string name)
    {
      name = this.TransformFile(name);
      if (name.Length <= 0)
        throw new ZipException("Cannot have an empty directory name");
      while (name.EndsWith("\\"))
        name = name.Remove(name.Length - 1, 1);
      return name;
    }

    /// <summary>
    /// Transform a Zip format file name to a windows style one.
    /// </summary>
    /// <param name="name">The file name to transform.</param>
    /// <returns>The transformed name.</returns>
    public string TransformFile(string name)
    {
      if (name != null)
      {
        name = WindowsNameTransform.MakeValidName(name, this._replacementChar);
        if (this._trimIncomingPaths)
          name = Path.GetFileName(name);
        if (this._baseDirectory != null)
          name = Path.Combine(this._baseDirectory, name);
      }
      else
        name = string.Empty;
      return name;
    }

    /// <summary>
    /// Test a name to see if it is a valid name for a windows filename as extracted from a Zip archive.
    /// </summary>
    /// <param name="name">The name to test.</param>
    /// <returns>Returns true if the name is a valid zip name; false otherwise.</returns>
    /// <remarks>The filename isnt a true windows path in some fundamental ways like no absolute paths, no rooted paths etc.</remarks>
    public static bool IsValidName(string name)
    {
      return name != null && name.Length <= 260 && string.Compare(name, WindowsNameTransform.MakeValidName(name, '_')) == 0;
    }

    /// <summary>Initialise static class information.</summary>
    static WindowsNameTransform()
    {
      char[] invalidPathChars = Path.GetInvalidPathChars();
      int length = invalidPathChars.Length + 3;
      WindowsNameTransform.InvalidEntryChars = new char[length];
      Array.Copy((Array) invalidPathChars, 0, (Array) WindowsNameTransform.InvalidEntryChars, 0, invalidPathChars.Length);
      WindowsNameTransform.InvalidEntryChars[length - 1] = '*';
      WindowsNameTransform.InvalidEntryChars[length - 2] = '?';
      WindowsNameTransform.InvalidEntryChars[length - 3] = ':';
    }

    /// <summary>
    /// Force a name to be valid by replacing invalid characters with a fixed value
    /// </summary>
    /// <param name="name">The name to make valid</param>
    /// <param name="replacement">The replacement character to use for any invalid characters.</param>
    /// <returns>Returns a valid name</returns>
    public static string MakeValidName(string name, char replacement)
    {
      name = name != null ? WindowsPathUtils.DropPathRoot(name.Replace("/", "\\")) : throw new ArgumentNullException(nameof (name));
      while (name.Length > 0 && name[0] == '\\')
        name = name.Remove(0, 1);
      while (name.Length > 0 && name[name.Length - 1] == '\\')
        name = name.Remove(name.Length - 1, 1);
      for (int startIndex = name.IndexOf("\\\\"); startIndex >= 0; startIndex = name.IndexOf("\\\\"))
        name = name.Remove(startIndex, 1);
      int index = name.IndexOfAny(WindowsNameTransform.InvalidEntryChars);
      if (index >= 0)
      {
        StringBuilder stringBuilder = new StringBuilder(name);
        for (; index >= 0; index = index < name.Length ? name.IndexOfAny(WindowsNameTransform.InvalidEntryChars, index + 1) : -1)
          stringBuilder[index] = replacement;
        name = stringBuilder.ToString();
      }
      return name.Length <= 260 ? name : throw new PathTooLongException();
    }

    /// <summary>
    /// Gets or set the character to replace invalid characters during transformations.
    /// </summary>
    public char Replacement
    {
      get => this._replacementChar;
      set
      {
        for (int index = 0; index < WindowsNameTransform.InvalidEntryChars.Length; ++index)
        {
          if ((int) WindowsNameTransform.InvalidEntryChars[index] == (int) value)
            throw new ArgumentException("invalid path character");
        }
        this._replacementChar = value != '\\' && value != '/' ? value : throw new ArgumentException("invalid replacement character");
      }
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.KeysRequiredEventArgs
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;

#nullable disable
namespace ICSharpCode.SharpZipLib.Zip
{
  /// <summary>Arguments used with KeysRequiredEvent</summary>
  public class KeysRequiredEventArgs : EventArgs
  {
    private string fileName;
    private byte[] key;

    /// <summary>
    /// Initialise a new instance of <see cref="T:ICSharpCode.SharpZipLib.Zip.KeysRequiredEventArgs"></see>
    /// </summary>
    /// <param name="name">The name of the file for which keys are required.</param>
    public KeysRequiredEventArgs(string name) => this.fileName = name;

    /// <summary>
    /// Initialise a new instance of <see cref="T:ICSharpCode.SharpZipLib.Zip.KeysRequiredEventArgs"></see>
    /// </summary>
    /// <param name="name">The name of the file for which keys are required.</param>
    /// <param name="keyValue">The current key value.</param>
    public KeysRequiredEventArgs(string name, byte[] keyValue)
    {
      this.fileName = name;
      this.key = keyValue;
    }

    /// <summary>Gets the name of the file for which keys are required.</summary>
    public string FileName => this.fileName;

    /// <summary>Gets or sets the key value</summary>
    public byte[] Key
    {
      get => this.key;
      set => this.key = value;
    }
  }
}

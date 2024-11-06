// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Core.ScanEventArgs
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;

#nullable disable
namespace ICSharpCode.SharpZipLib.Core
{
  /// <summary>Event arguments for scanning.</summary>
  public class ScanEventArgs : EventArgs
  {
    private string name_;
    private bool continueRunning_ = true;

    /// <summary>
    /// Initialise a new instance of <see cref="T:ICSharpCode.SharpZipLib.Core.ScanEventArgs" />
    /// </summary>
    /// <param name="name">The file or directory name.</param>
    public ScanEventArgs(string name) => this.name_ = name;

    /// <summary>The file or directory name for this event.</summary>
    public string Name => this.name_;

    /// <summary>
    /// Get set a value indicating if scanning should continue or not.
    /// </summary>
    public bool ContinueRunning
    {
      get => this.continueRunning_;
      set => this.continueRunning_ = value;
    }
  }
}

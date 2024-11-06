// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Core.ScanFailureEventArgs
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;

#nullable disable
namespace ICSharpCode.SharpZipLib.Core
{
  /// <summary>Arguments passed when scan failures are detected.</summary>
  public class ScanFailureEventArgs : EventArgs
  {
    private string name_;
    private Exception exception_;
    private bool continueRunning_;

    /// <summary>
    /// Initialise a new instance of <see cref="T:ICSharpCode.SharpZipLib.Core.ScanFailureEventArgs"></see>
    /// </summary>
    /// <param name="name">The name to apply.</param>
    /// <param name="e">The exception to use.</param>
    public ScanFailureEventArgs(string name, Exception e)
    {
      this.name_ = name;
      this.exception_ = e;
      this.continueRunning_ = true;
    }

    /// <summary>The applicable name.</summary>
    public string Name => this.name_;

    /// <summary>The applicable exception.</summary>
    public Exception Exception => this.exception_;

    /// <summary>
    /// Get / set a value indicating wether scanning should continue.
    /// </summary>
    public bool ContinueRunning
    {
      get => this.continueRunning_;
      set => this.continueRunning_ = value;
    }
  }
}

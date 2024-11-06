// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Core.IScanFilter
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

#nullable disable
namespace ICSharpCode.SharpZipLib.Core
{
  /// <summary>Scanning filters support filtering of names.</summary>
  public interface IScanFilter
  {
    /// <summary>Test a name to see if it 'matches' the filter.</summary>
    /// <param name="name">The name to test.</param>
    /// <returns>Returns true if the name matches the filter, false if it does not match.</returns>
    bool IsMatch(string name);
  }
}

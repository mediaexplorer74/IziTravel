// Decompiled with JetBrains decompiler
// Type: PCLStorage.Exceptions.DirectoryNotFoundException
// Assembly: PCLStorage, Version=1.0.2.0, Culture=neutral, PublicKeyToken=286fe515a2c35b64
// MVID: C962FBF1-A378-45AB-97C6-C265F8F0F86C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\PCLStorage.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\PCLStorage.xml

using System;

#nullable disable
namespace PCLStorage.Exceptions
{
  /// <exclude />
  public class DirectoryNotFoundException : System.IO.DirectoryNotFoundException
  {
    /// <exclude />
    public DirectoryNotFoundException(string message)
      : base(message)
    {
    }

    /// <exclude />
    public DirectoryNotFoundException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}

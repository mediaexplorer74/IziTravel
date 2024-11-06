// Decompiled with JetBrains decompiler
// Type: PCLStorage.Exceptions.FileNotFoundException
// Assembly: PCLStorage, Version=1.0.2.0, Culture=neutral, PublicKeyToken=286fe515a2c35b64
// MVID: C962FBF1-A378-45AB-97C6-C265F8F0F86C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\PCLStorage.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\PCLStorage.xml

using System;

#nullable disable
namespace PCLStorage.Exceptions
{
  /// <exclude />
  public class FileNotFoundException : System.IO.FileNotFoundException
  {
    /// <exclude />
    public FileNotFoundException(string message)
      : base(message)
    {
    }

    /// <exclude />
    public FileNotFoundException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}

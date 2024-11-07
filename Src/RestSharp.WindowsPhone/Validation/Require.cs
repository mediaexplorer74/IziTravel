// Decompiled with JetBrains decompiler
// Type: RestSharp.Validation.Require
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using System;

#nullable disable
namespace RestSharp.Validation
{
  /// <summary>Helper methods for validating required values</summary>
  public class Require
  {
    /// <summary>Require a parameter to not be null</summary>
    /// <param name="argumentName">Name of the parameter</param>
    /// <param name="argumentValue">Value of the parameter</param>
    public static void Argument(string argumentName, object argumentValue)
    {
      if (argumentValue == null)
        throw new ArgumentException("Argument cannot be null.", argumentName);
    }
  }
}

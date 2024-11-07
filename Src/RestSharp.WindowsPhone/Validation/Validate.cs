﻿// Decompiled with JetBrains decompiler
// Type: RestSharp.Validation.Validate
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using System;

#nullable disable
namespace RestSharp.Validation
{
  /// <summary>Helper methods for validating values</summary>
  public class Validate
  {
    /// <summary>
    /// Validate an integer value is between the specified values (exclusive of min/max)
    /// </summary>
    /// <param name="value">Value to validate</param>
    /// <param name="min">Exclusive minimum value</param>
    /// <param name="max">Exclusive maximum value</param>
    public static void IsBetween(int value, int min, int max)
    {
      if (value < min || value > max)
        throw new ArgumentException(string.Format("Value ({0}) is not between {1} and {2}.", (object) value, (object) min, (object) max));
    }

    /// <summary>Validate a string length</summary>
    /// <param name="value">String to be validated</param>
    /// <param name="maxSize">Maximum length of the string</param>
    public static void IsValidLength(string value, int maxSize)
    {
      if (value != null && value.Length > maxSize)
        throw new ArgumentException(string.Format("String is longer than max allowed size ({0}).", (object) maxSize));
    }
  }
}

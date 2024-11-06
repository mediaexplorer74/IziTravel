// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.Extras.MessageResult
// Assembly: Caliburn.Micro.Extras, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75D6380B-EA35-437B-8CE3-40FC8C25A394
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extras.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extras.xml

#nullable disable
namespace Caliburn.Micro.Extras
{
  /// <summary>Available message results.</summary>
  public enum MessageResult
  {
    /// <summary>No result available.</summary>
    None = 0,
    /// <summary>Message is acknowledged.</summary>
    OK = 1,
    /// <summary>Message is canceled.</summary>
    Cancel = 2,
    /// <summary>Message is acknowledged with yes.</summary>
    Yes = 6,
    /// <summary>Message is acknowledged with no.</summary>
    No = 7,
  }
}

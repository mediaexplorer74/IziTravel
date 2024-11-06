// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.IHaveParameters
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>Indicates that a message is parameterized.</summary>
  public interface IHaveParameters
  {
    /// <summary>Represents the parameters of a message.</summary>
    AttachedCollection<Parameter> Parameters { get; }
  }
}

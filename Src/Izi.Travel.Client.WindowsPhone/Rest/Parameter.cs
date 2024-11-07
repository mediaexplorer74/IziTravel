// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Rest.Parameter
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

#nullable disable
namespace Izi.Travel.Client.Rest
{
  public class Parameter
  {
    public string Name { get; set; }

    public object Value { get; set; }

    public ParameterType Type { get; set; }

    public Parameter()
      : this((string) null, (object) null)
    {
    }

    public Parameter(string name, object value)
      : this(name, value, ParameterType.QueryString)
    {
    }

    public Parameter(string name, object value, ParameterType type)
    {
      this.Name = name;
      this.Value = value;
      this.Type = type;
    }
  }
}

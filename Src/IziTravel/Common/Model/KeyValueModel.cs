// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Common.Model.KeyValueModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

#nullable disable
namespace Izi.Travel.Shell.Common.Model
{
  public class KeyValueModel
  {
    public object Key { get; private set; }

    public object Value { get; private set; }

    public KeyValueModel(object key, object value)
    {
      this.Key = key;
      this.Value = value;
    }

    public override string ToString() => this.Value == null ? string.Empty : this.Value.ToString();
  }
}

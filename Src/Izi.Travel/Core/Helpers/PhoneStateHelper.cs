// ********************************************************************
// Type: Izi.Travel.Core.Helpers.PhoneStateHelper
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using System.Collections.Generic;

#nullable disable
namespace Izi.Travel.Core.Helpers
{
  public class PhoneStateHelper
  {
    private static readonly Dictionary<string, object> State = new Dictionary<string, object>();

    public static void SetParameter<T>(string name, T value)
    {
      if (State.ContainsKey(name))
        State.Remove(name);
      State[name] = (object) value;
    }

    public static T GetParameter<T>(string name)
    {
      return !State.ContainsKey(name) ? default (T) : (T) State[name];
    }
  }
}

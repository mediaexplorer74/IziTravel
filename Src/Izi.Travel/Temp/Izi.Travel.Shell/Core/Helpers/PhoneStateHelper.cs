// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Helpers.PhoneStateHelper
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;

#nullable disable
namespace Izi.Travel.Shell.Core.Helpers
{
  public class PhoneStateHelper
  {
    private static readonly IPhoneService PhoneService = IoC.Get<IPhoneService>();

    public static void SetParameter<T>(string name, T value)
    {
      if (PhoneStateHelper.PhoneService.State.ContainsKey(name))
        PhoneStateHelper.PhoneService.State.Remove(name);
      PhoneStateHelper.PhoneService.State[name] = (object) value;
    }

    public static T GetParameter<T>(string name)
    {
      return !PhoneStateHelper.PhoneService.State.ContainsKey(name) ? default (T) : (T) PhoneStateHelper.PhoneService.State[name];
    }
  }
}

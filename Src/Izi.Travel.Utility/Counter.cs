// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Utility.Counter
// Assembly: Izi.Travel.Utility, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 6E74EF73-7EB1-46AA-A84C-A1A7E0B11FE0
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Utility.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

#nullable disable
namespace Izi.Travel.Utility
{
  public class Counter
  {
    private static bool _isEnabled = false;
    private static Dictionary<string, int> _data = new Dictionary<string, int>();

    static Counter()
    {
      if (!Counter._isEnabled)
        return;
      DispatcherTimer dispatcherTimer = new DispatcherTimer();
      dispatcherTimer.Interval = TimeSpan.FromSeconds(3.0);
      dispatcherTimer.Tick += (EventHandler) ((s, e) => { });
      dispatcherTimer.Start();
    }

    public static void Construct([CallerFilePath] string cfp = null)
    {
      if (!Counter._isEnabled)
        return;
      string key = Counter.GetKey(cfp);
      if (!Counter._data.ContainsKey(key))
        Counter._data.Add(key, 0);
      Counter._data[key]++;
    }

    public static void Destruct([CallerFilePath] string cfp = null)
    {
      if (!Counter._isEnabled)
        return;
      string key = Counter.GetKey(cfp);
      Counter._data[key]--;
    }

    private static string GetKey(string input)
    {
      return ((IEnumerable<string>) input.Split('\\')).LastOrDefault<string>();
    }
  }
}

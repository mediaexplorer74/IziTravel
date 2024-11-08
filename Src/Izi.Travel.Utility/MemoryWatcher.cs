// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Utility.MemoryWatcher
// Assembly: Izi.Travel.Utility, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 6E74EF73-7EB1-46AA-A84C-A1A7E0B11FE0
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Utility.dll

//using Caliburn.Micro;
//using Microsoft.Phone.Info;
using System;
using System.Diagnostics;
using Windows.UI.Xaml;
//using System.Windows.Threading;

#nullable disable
namespace Izi.Travel.Utility
{
  public class MemoryWatcher
  {
    private static bool _initialized;
    private static int _memoryUsage;

    public static void Start()
    {
      if (MemoryWatcher._initialized)
        return;
      //ILog log = LogManager.GetLog(typeof (MemoryWatcher));
      DispatcherTimer dispatcherTimer = new DispatcherTimer();
      dispatcherTimer.Interval = TimeSpan.FromSeconds(3.0);
      
      dispatcherTimer.Tick += ((s, e) =>
      {
        int num = (int) (/*DeviceStatus.ApplicationCurrentMemoryUsage*/1400000000L / 1024L / 1024L);
        if (Math.Abs(num - MemoryWatcher._memoryUsage) < 3)
          return;

          //log.Info(num.ToString());
          
        Debug.WriteLine("[ex] MemoryWatcher: memoryusage =" + num.ToString());

        MemoryWatcher._memoryUsage = num;
      });

      dispatcherTimer.Start();
      MemoryWatcher._initialized = true;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Common.SafeDispatcher
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Threading;
using System.Windows.Threading;

#nullable disable
namespace Coding4Fun.Toolkit.Controls.Common
{
  public class SafeDispatcher
  {
    public static void Run(Action func)
    {
      Dispatcher currentDispatcher = ApplicationSpace.CurrentDispatcher;
      if (currentDispatcher == null)
        return;
      if (!currentDispatcher.CheckAccess())
        currentDispatcher.BeginInvoke(func);
      else
        func();
    }

    public static T Run<T>(Func<T> func)
    {
      T returnData = default (T);
      Dispatcher currentDispatcher = ApplicationSpace.CurrentDispatcher;
      if (currentDispatcher == null)
        return returnData;
      if (!currentDispatcher.CheckAccess())
      {
        AutoResetEvent holdMutex = new AutoResetEvent(true);
        currentDispatcher.BeginInvoke((Action) (() =>
        {
          returnData = func();
          holdMutex.Set();
        }));
        holdMutex.Reset();
        holdMutex.WaitOne();
      }
      else
        returnData = func();
      return returnData;
    }
  }
}

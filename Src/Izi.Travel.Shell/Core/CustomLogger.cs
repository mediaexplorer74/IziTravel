// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.CustomLogger
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Shell.Core
{
  public class CustomLogger : ILog
  {
    private readonly bool _isSuppressed;
    private readonly string _typeFullName;
    private const string Path = "Logs";
    private static readonly Buffer Buffer = new Buffer();
    private static readonly CultureInfo CultureInfo = new CultureInfo("ru");
    private static readonly List<Type> BlackList = new List<Type>()
    {
      typeof (ViewModelBinder),
      typeof (ConventionManager),
      typeof (Caliburn.Micro.Action)
    };

    public CustomLogger(Type type)
    {
      this._isSuppressed = CustomLogger.BlackList.Contains(type);
      this._typeFullName = type.FullName;
    }

    public void Info(string format, params object[] args)
    {
      this.Log("INFO", string.Format(format, args));
    }

    public void Warn(string format, params object[] args)
    {
      this.Log("WARN", string.Format(format, args));
    }

    public void Error(Exception exception)
    {
      if (exception == null)
        return;
      this.Log("ERROR", exception.ToString());
    }

    private void Log(string level, string message)
    {
      if (this._isSuppressed)
        return;
      string data = string.Format("{0} [{1}] {2}: {3}", (object) DateTime.Now.ToString("G", (IFormatProvider) CustomLogger.CultureInfo), (object) this._typeFullName, (object) level, (object) message);
      CustomLogger.Buffer.Push(data);
    }

    static CustomLogger()
    {
      CustomLogger.Clear();
      Task.Run((Func<Task>) (() =>
      {
        while (true)
        {
          CustomLogger.Flush();
          Thread.Sleep(1000);
        }
      }));
    }

    public static void Flush()
    {
      try
      {
        string data = CustomLogger.Buffer.Pop();
        if (string.IsNullOrWhiteSpace(data))
          return;
        using (IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication())
          CustomLogger.Append(storeForApplication, DateTime.Now.ToString("yyyyMMdd"), data);
      }
      catch
      {
        if (!Debugger.IsAttached)
          return;
        Debugger.Break();
      }
    }

    public static Task<string> GetLogAsync()
    {
      return Task.Run<string>((Func<string>) (() => CustomLogger.GetLog()));
    }

    public static string GetLog()
    {
      try
      {
        CustomLogger.Flush();
        StringBuilder stringBuilder = new StringBuilder();
        using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
          ((IEnumerable<string>) storage.GetFileNames(CustomLogger.GetPath("*"))).OrderBy<string, string>((Func<string, string>) (x => x)).ToList<string>().ForEach((Action<string>) (fileName => stringBuilder.Append(CustomLogger.Read(storage, fileName))));
        return stringBuilder.ToString();
      }
      catch
      {
        if (Debugger.IsAttached)
          Debugger.Break();
      }
      return string.Empty;
    }

    private static string GetPath(string fileName) => System.IO.Path.Combine("Logs", fileName);

    private static void Clear()
    {
      try
      {
        using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
        {
          if (storage.DirectoryExists("Logs"))
            ((IEnumerable<string>) storage.GetFileNames(CustomLogger.GetPath("*"))).OrderByDescending<string, string>((Func<string, string>) (x => x)).Skip<string>(3).ToList<string>().ForEach((Action<string>) (fileName => CustomLogger.Delete(storage, fileName)));
          else
            storage.CreateDirectory("Logs");
        }
      }
      catch
      {
        if (!Debugger.IsAttached)
          return;
        Debugger.Break();
      }
    }

    private static void Append(IsolatedStorageFile storage, string fileName, string data)
    {
      using (IsolatedStorageFileStream storageFileStream = storage.OpenFile(CustomLogger.GetPath(fileName), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
      {
        using (StreamWriter streamWriter = new StreamWriter((Stream) storageFileStream))
        {
          streamWriter.BaseStream.Seek(0L, SeekOrigin.End);
          streamWriter.Write(data);
        }
      }
    }

    private static string Read(IsolatedStorageFile storage, string fileName)
    {
      using (IsolatedStorageFileStream storageFileStream = storage.OpenFile(CustomLogger.GetPath(fileName), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
      {
        using (StreamReader streamReader = new StreamReader((Stream) storageFileStream))
          return streamReader.ReadToEnd();
      }
    }

    private static void Delete(IsolatedStorageFile storage, string fileName)
    {
      storage.DeleteFile(CustomLogger.GetPath(fileName));
    }
  }
}

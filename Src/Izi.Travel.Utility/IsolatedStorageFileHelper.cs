// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Utility.IsolatedStorageFileHelper
// Assembly: Izi.Travel.Utility, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 6E74EF73-7EB1-46AA-A84C-A1A7E0B11FE0
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Utility.dll

using Caliburn.Micro;
using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Utility
{
  public static class IsolatedStorageFileHelper
  {
    private const double MegabytesDivider = 1048576.0;
    private static readonly ILog Logger = LogManager.GetLog(typeof (IsolatedStorageFileHelper));

    public static Task SerializeAsync<T>(string path, T obj)
    {
      return Task.Factory.StartNew((Action) (() =>
      {
        using (IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication())
        {
          if (storeForApplication.FileExists(path))
            storeForApplication.DeleteFile(path);
          string directoryName = Path.GetDirectoryName(path);
          if (directoryName != null && !storeForApplication.DirectoryExists(directoryName))
            storeForApplication.CreateDirectory(directoryName);
          using (IsolatedStorageFileStream storageFileStream = storeForApplication.OpenFile(path, FileMode.CreateNew, FileAccess.ReadWrite))
          {
            byte[] byteArray = JsonSerializerHelper.SerializeToByteArray<T>(obj);
            if (byteArray == null)
              return;
            storageFileStream.Write(byteArray, 0, byteArray.Length);
          }
        }
      }));
    }

    public static Task<T> DeserializeAsync<T>(string path)
    {
      return Task<T>.Factory.StartNew((Func<T>) (() =>
      {
        try
        {
          using (IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication())
          {
            if (!storeForApplication.FileExists(path))
              return default (T);
            using (IsolatedStorageFileStream storageFileStream = storeForApplication.OpenFile(path, FileMode.Open, FileAccess.Read))
              return JsonSerializerHelper.Deserialize<T>((Stream) storageFileStream);
          }
        }
        catch (Exception ex)
        {
          IsolatedStorageFileHelper.Logger.Error(ex);
          return default (T);
        }
      }));
    }

    public static void DeleteFile(string path)
    {
      if (string.IsNullOrWhiteSpace(path))
        return;
      using (IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication())
      {
        if (!storeForApplication.FileExists(path))
          return;
        storeForApplication.DeleteFile(path);
      }
    }

    public static void DeleteDirectory(string path)
    {
      using (IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication())
      {
        if (!storeForApplication.DirectoryExists(path))
          return;
        string searchPattern = Path.Combine(path, "*");
        foreach (string directoryName in storeForApplication.GetDirectoryNames(searchPattern))
          IsolatedStorageFileHelper.DeleteDirectory(Path.Combine(path, directoryName));
        foreach (string fileName in storeForApplication.GetFileNames(searchPattern))
          storeForApplication.DeleteFile(Path.Combine(path, fileName));
        storeForApplication.DeleteDirectory(path);
      }
    }

    public static string[] GetFileNames(string pattern)
    {
      using (IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication())
      {
        string directoryName = Path.GetDirectoryName(pattern);
        return string.IsNullOrWhiteSpace(directoryName) || !storeForApplication.DirectoryExists(directoryName) ? (string[]) null : storeForApplication.GetFileNames(pattern);
      }
    }

    public static double GetAvailableSize()
    {
      try
      {
        using (IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication())
          return (double) storeForApplication.AvailableFreeSpace;
      }
      catch (Exception ex)
      {
        IsolatedStorageFileHelper.Logger.Error(ex);
        return 0.0;
      }
    }

    public static double GetDirectorySize(string path)
    {
      try
      {
        double directorySize = 0.0;
        using (IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication())
        {
          if (storeForApplication.DirectoryExists(path))
          {
            foreach (string fileName in storeForApplication.GetFileNames(Path.Combine(path, "*")))
            {
              using (IsolatedStorageFileStream storageFileStream = storeForApplication.OpenFile(Path.Combine(path, fileName), FileMode.Open))
                directorySize += (double) storageFileStream.Length;
            }
          }
        }
        return directorySize;
      }
      catch (Exception ex)
      {
        IsolatedStorageFileHelper.Logger.Error(ex);
        return 0.0;
      }
    }

    public static double ToMegabytes(this double bytes) => bytes / 1048576.0;
  }
}

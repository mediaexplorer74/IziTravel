// Decompiled with JetBrains decompiler
// Type: BugSense.Core.Helpers.EntropyUUID
// Assembly: BugSense-WP8, Version=3.6.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A6D84D1-19B1-4B1A-84A1-50F2A1BD7889
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\BugSense-WP8.dll

using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

#nullable disable
namespace BugSense.Core.Helpers
{
  internal static class EntropyUUID
  {
    private static RNGCryptoServiceProvider _global = new RNGCryptoServiceProvider();

    public static string Get()
    {
      FileRepository fileRepository = new FileRepository();
      string jsonRequest = fileRepository.Read(BugSenseProperties.GeneralFolderName + "\\bugsense.udid");
      if (string.IsNullOrEmpty(jsonRequest))
      {
        jsonRequest = EntropyUUID.GetNew();
        fileRepository.CreateDirectoriesIfNotExist();
        fileRepository.Save(BugSenseProperties.GeneralFolderName + "\\bugsense.udid", jsonRequest);
      }
      return jsonRequest;
    }

    private static string GetNew()
    {
      string str1 = DateTime.Now.Millisecond.ToString();
      string str2 = new object().GetHashCode().ToString();
      DateTime now = DateTime.Now;
      Thread.Sleep(256);
      string str3 = (DateTime.Now.Ticks - now.Ticks).ToString();
      byte[] data = new byte[4];
      EntropyUUID._global.GetBytes(data);
      string str4 = (new Random(BitConverter.ToInt32(data, 0)).Next() % 65536).ToString();
      string str5 = (DateTime.Now.Ticks % 10L).ToString();
      return EntropyUUID.GetSha1Hash(str1 + str2 + str3 + str4 + str5);
    }

    private static string GetSha1Hash(string input)
    {
      SHA1Managed shA1Managed = new SHA1Managed();
      UTF8Encoding utF8Encoding = new UTF8Encoding();
      shA1Managed.ComputeHash(utF8Encoding.GetBytes(input.ToCharArray()));
      return BitConverter.ToString(shA1Managed.Hash).Replace("-", "").ToLowerInvariant();
    }
  }
}

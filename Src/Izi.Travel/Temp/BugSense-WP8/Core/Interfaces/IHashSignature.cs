// Decompiled with JetBrains decompiler
// Type: BugSense.Core.Interfaces.IHashSignature
// Assembly: BugSense-WP8, Version=3.6.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A6D84D1-19B1-4B1A-84A1-50F2A1BD7889
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\BugSense-WP8.dll

using BugSense.Core.Model;

#nullable disable
namespace BugSense.Core.Interfaces
{
  public interface IHashSignature
  {
    HashSignature GetHashSignature(
      string appName,
      string appVersion,
      string stacktrace,
      string message,
      string hresult);
  }
}

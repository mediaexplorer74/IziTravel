// Decompiled with JetBrains decompiler
// Type: GoogleAnalytics.Core.IServiceManager
// Assembly: GoogleAnalytics.Core, Version=1.2.11.25892, Culture=neutral, PublicKeyToken=null
// MVID: DA6701CD-FFEA-4833-995F-5D20607A09B2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\GoogleAnalytics.Core.dll

#nullable disable
namespace GoogleAnalytics.Core
{
  public interface IServiceManager
  {
    void SendPayload(Payload payload);

    string UserAgent { get; set; }
  }
}

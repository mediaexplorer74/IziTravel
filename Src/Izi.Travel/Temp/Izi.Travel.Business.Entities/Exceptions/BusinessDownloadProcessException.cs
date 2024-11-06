// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Exceptions.BusinessDownloadProcessException
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

using Izi.Travel.Business.Entities.Download;
using System;

#nullable disable
namespace Izi.Travel.Business.Entities.Exceptions
{
  public class BusinessDownloadProcessException : BusinessException
  {
    public DownloadProcessError Error { get; private set; }

    public BusinessDownloadProcessException(
      DownloadProcessError error,
      string message,
      Exception innerException)
      : base(message, innerException)
    {
      this.Error = error;
    }

    public BusinessDownloadProcessException(DownloadProcessError error, Exception innerException)
      : this(error, (string) null, innerException)
    {
    }

    public BusinessDownloadProcessException(DownloadProcessError error)
      : this(error, (string) null, (Exception) null)
    {
    }
  }
}

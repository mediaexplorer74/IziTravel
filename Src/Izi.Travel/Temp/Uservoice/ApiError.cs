// Decompiled with JetBrains decompiler
// Type: UserVoice.ApiError
// Assembly: Uservoice, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 038B5345-2117-47AA-93A0-4A054BBF5C1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Uservoice.dll

using System;

#nullable disable
namespace UserVoice
{
  public class ApiError : Exception
  {
    public ApiError(string msg)
      : base(msg)
    {
    }
  }
}

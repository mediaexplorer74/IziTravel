// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Components.ExceptionTranslator
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Exceptions;
using System;

#nullable disable
namespace Izi.Travel.Business.Components
{
  public class ExceptionTranslator
  {
    public static BusinessException Translate(Exception exception)
    {
      if (exception == null)
        return (BusinessException) null;
      return exception is AggregateException aggregateException && aggregateException.InnerException != null ? ExceptionTranslator.Translate(aggregateException.InnerException) : new BusinessException(exception.Message, exception);
    }
  }
}

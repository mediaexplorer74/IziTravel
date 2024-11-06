// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Quiz.QuizData
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

using Izi.Travel.Business.Entities.Data;
using System;

#nullable disable
namespace Izi.Travel.Business.Entities.Quiz
{
  public class QuizData
  {
    public int Id { get; set; }

    public string Uid { get; set; }

    public string ParentUid { get; set; }

    public string Language { get; set; }

    public MtgObjectType Type { get; set; }

    public string Title { get; set; }

    public string ContentProviderUid { get; set; }

    public string ImageUid { get; set; }

    public int AnswerIndex { get; set; }

    public bool AnswerCorrect { get; set; }

    public DateTime DateTime { get; set; }

    public Izi.Travel.Business.Entities.Data.Quiz Quiz { get; set; }
  }
}

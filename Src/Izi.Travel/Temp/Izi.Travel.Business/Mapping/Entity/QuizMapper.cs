// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Entity.QuizMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using System;
using System.Linq;

#nullable disable
namespace Izi.Travel.Business.Mapping.Entity
{
  internal class QuizMapper : MapperBase<Izi.Travel.Business.Entities.Data.Quiz, Izi.Travel.Client.Entities.Quiz>
  {
    private readonly QuizAnswerMapper _quizAnswerMapper;

    public QuizMapper(QuizAnswerMapper quizAnswerMapper)
    {
      this._quizAnswerMapper = quizAnswerMapper;
    }

    public override Izi.Travel.Client.Entities.Quiz Convert(Izi.Travel.Business.Entities.Data.Quiz source)
    {
      throw new NotImplementedException();
    }

    public override Izi.Travel.Business.Entities.Data.Quiz ConvertBack(Izi.Travel.Client.Entities.Quiz target)
    {
      if (target == null)
        return (Izi.Travel.Business.Entities.Data.Quiz) null;
      return new Izi.Travel.Business.Entities.Data.Quiz()
      {
        Answers = target.Answers != null ? target.Answers.Select<Izi.Travel.Client.Entities.QuizAnswer, Izi.Travel.Business.Entities.Data.QuizAnswer>((Func<Izi.Travel.Client.Entities.QuizAnswer, Izi.Travel.Business.Entities.Data.QuizAnswer>) (x => this._quizAnswerMapper.ConvertBack(x))).ToArray<Izi.Travel.Business.Entities.Data.QuizAnswer>() : (Izi.Travel.Business.Entities.Data.QuizAnswer[]) null,
        Comment = target.Comment,
        Question = target.Question
      };
    }
  }
}

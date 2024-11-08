// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Entity.QuizDataMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Utility;
using System;

#nullable disable
namespace Izi.Travel.Business.Mapping.Entity
{
  internal class QuizDataMapper : MapperBase<Izi.Travel.Business.Entities.Quiz.QuizData, Izi.Travel.Data.Entities.Local.QuizData>
  {
    public override Izi.Travel.Data.Entities.Local.QuizData Convert(Izi.Travel.Business.Entities.Quiz.QuizData source)
    {
      if (source == null)
        return (Izi.Travel.Data.Entities.Local.QuizData) null;
      return new Izi.Travel.Data.Entities.Local.QuizData()
      {
        Id = source.Id,
        Uid = source.Uid,
        Language = source.Language,
        ParentUid = source.ParentUid,
        Type = source.Type.ToString(),
        ContentProviderUid = source.ContentProviderUid,
        Title = source.Title,
        AnswerCorrect = source.AnswerCorrect,
        AnswerIndex = source.AnswerIndex,
        DateTime = source.DateTime,
        ImageUid = source.ImageUid,
        Data = JsonSerializerHelper.SerializeToByteArray<Izi.Travel.Business.Entities.Data.Quiz>(source.Quiz)
      };
    }

    public override Izi.Travel.Business.Entities.Quiz.QuizData ConvertBack(Izi.Travel.Data.Entities.Local.QuizData target)
    {
      if (target == null)
        return (Izi.Travel.Business.Entities.Quiz.QuizData) null;
      MtgObjectType result = MtgObjectType.Unknown;
      //if (!string.IsNullOrWhiteSpace(target.Type))
      //  Enum.TryParse<MtgObjectType>(target.Type, true, out result);
      return new Izi.Travel.Business.Entities.Quiz.QuizData()
      {
        Id = target.Id,
        Uid = target.Uid,
        Language = target.Language,
        ParentUid = target.ParentUid,
        Type = result,
        ContentProviderUid = target.ContentProviderUid,
        Title = target.Title,
        AnswerIndex = target.AnswerIndex,
        AnswerCorrect = target.AnswerCorrect,
        DateTime = target.DateTime,
        ImageUid = target.ImageUid,
        Quiz = JsonSerializerHelper.DeserializeFromByteArray<Izi.Travel.Business.Entities.Data.Quiz>(target.Data)
      };
    }
  }
}

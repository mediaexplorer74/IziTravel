// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Entity.QuizDataStatisticsMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

#nullable disable
namespace Izi.Travel.Business.Mapping.Entity
{
  internal class QuizDataStatisticsMapper : MapperBase<Izi.Travel.Business.Entities.Quiz.QuizDataStatistics, Izi.Travel.Data.Entities.Local.QuizDataStatistics>
  {
    public override Izi.Travel.Data.Entities.Local.QuizDataStatistics Convert(
      Izi.Travel.Business.Entities.Quiz.QuizDataStatistics source)
    {
      if (source == null)
        return (Izi.Travel.Data.Entities.Local.QuizDataStatistics) null;
      return new Izi.Travel.Data.Entities.Local.QuizDataStatistics()
      {
        CorrectAnswerCount = source.CorrectAnswerCount,
        IncorrectAnswerCount = source.IncorrectAnswerCount
      };
    }

    public override Izi.Travel.Business.Entities.Quiz.QuizDataStatistics ConvertBack(
      Izi.Travel.Data.Entities.Local.QuizDataStatistics target)
    {
      if (target == null)
        return (Izi.Travel.Business.Entities.Quiz.QuizDataStatistics) null;
      return new Izi.Travel.Business.Entities.Quiz.QuizDataStatistics()
      {
        CorrectAnswerCount = target.CorrectAnswerCount,
        IncorrectAnswerCount = target.IncorrectAnswerCount
      };
    }
  }
}

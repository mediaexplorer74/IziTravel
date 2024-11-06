// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Entity.QuizAnswerMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

#nullable disable
namespace Izi.Travel.Business.Mapping.Entity
{
  internal class QuizAnswerMapper : MapperBase<Izi.Travel.Business.Entities.Data.QuizAnswer, Izi.Travel.Client.Entities.QuizAnswer>
  {
    public override Izi.Travel.Client.Entities.QuizAnswer Convert(Izi.Travel.Business.Entities.Data.QuizAnswer source)
    {
      if (source == null)
        return (Izi.Travel.Client.Entities.QuizAnswer) null;
      return new Izi.Travel.Client.Entities.QuizAnswer()
      {
        Content = source.Content,
        Correct = source.Correct
      };
    }

    public override Izi.Travel.Business.Entities.Data.QuizAnswer ConvertBack(Izi.Travel.Client.Entities.QuizAnswer target)
    {
      if (target == null)
        return (Izi.Travel.Business.Entities.Data.QuizAnswer) null;
      return new Izi.Travel.Business.Entities.Data.QuizAnswer()
      {
        Content = target.Content,
        Correct = target.Correct
      };
    }
  }
}

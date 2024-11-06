// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Services.Implementation.QuizService
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Caliburn.Micro;
using Izi.Travel.Business.Components;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Mapping.Entity;
using Izi.Travel.Business.Services.Contract;
using Izi.Travel.Data.Entities.Local.Query;
using Izi.Travel.Data.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Business.Services.Implementation
{
  public class QuizService : IQuizService
  {
    private static readonly ILog Logger = LogManager.GetLog(typeof (QuizService));
    private readonly ILocalDataService _dataService;
    private readonly QuizDataMapper _quizDataMapper = IoC.Get<QuizDataMapper>();
    private readonly QuizDataStatisticsMapper _quizDataStatisticsMapper = IoC.Get<QuizDataStatisticsMapper>();

    public QuizService(ILocalDataService dataService) => this._dataService = dataService;

    public Task<Izi.Travel.Business.Entities.Quiz.QuizData> GetQuizDataAsync(QuizDataFilter filter)
    {
      if (filter == null)
        throw new ArgumentNullException(nameof (filter));
      return Task<Izi.Travel.Business.Entities.Quiz.QuizData>.Factory.StartNew((Func<Izi.Travel.Business.Entities.Quiz.QuizData>) (() =>
      {
        try
        {
          return this._quizDataMapper.ConvertBack(this._dataService.GetQuizData(new QuizDataQuery()
          {
            Uid = filter.Uid,
            Language = filter.Language
          }));
        }
        catch (Exception ex)
        {
          QuizService.Logger.Error(ex);
          throw ExceptionTranslator.Translate(ex);
        }
      }));
    }

    public Task<Izi.Travel.Business.Entities.Quiz.QuizData[]> GetQuizDataListAsync(
      QuizDataListFilter filter)
    {
      if (filter == null)
        throw new ArgumentNullException(nameof (filter));
      return Task<Izi.Travel.Business.Entities.Quiz.QuizData[]>.Factory.StartNew((Func<Izi.Travel.Business.Entities.Quiz.QuizData[]>) (() =>
      {
        try
        {
          return ((IEnumerable<Izi.Travel.Data.Entities.Local.QuizData>) (this._dataService.GetQuizDataList(new QuizDataListQuery()
          {
            UidList = filter.UidList,
            LanguageList = filter.LanguageList,
            Limit = filter.Limit,
            Offset = filter.Offset
          }) ?? new Izi.Travel.Data.Entities.Local.QuizData[0])).Select<Izi.Travel.Data.Entities.Local.QuizData, Izi.Travel.Business.Entities.Quiz.QuizData>((Func<Izi.Travel.Data.Entities.Local.QuizData, Izi.Travel.Business.Entities.Quiz.QuizData>) (x => this._quizDataMapper.ConvertBack(x))).ToArray<Izi.Travel.Business.Entities.Quiz.QuizData>();
        }
        catch (Exception ex)
        {
          QuizService.Logger.Error(ex);
          throw ExceptionTranslator.Translate(ex);
        }
      }));
    }

    public Task<Izi.Travel.Business.Entities.Quiz.QuizDataStatistics> GetQuizDataStatisticsAsync(
      QuizDataStatisticsFilter filter)
    {
      if (filter == null)
        throw new ArgumentNullException(nameof (filter));
      return Task<Izi.Travel.Business.Entities.Quiz.QuizDataStatistics>.Factory.StartNew((Func<Izi.Travel.Business.Entities.Quiz.QuizDataStatistics>) (() =>
      {
        try
        {
          return this._quizDataStatisticsMapper.ConvertBack(this._dataService.GetQuizDataStatistics(new QuizDataStatisticsQuery()
          {
            UidList = filter.UidList,
            LanguageList = filter.LanguageList
          }));
        }
        catch (Exception ex)
        {
          QuizService.Logger.Error(ex);
          throw ExceptionTranslator.Translate(ex);
        }
      }));
    }

    public Task CreateOrUpdateQuizDataAsync(Izi.Travel.Business.Entities.Quiz.QuizData quizData)
    {
      return Task.Factory.StartNew((Action) (() =>
      {
        try
        {
          this._dataService.CreateOrUpdateQuizData(this._quizDataMapper.Convert(quizData));
        }
        catch (Exception ex)
        {
          QuizService.Logger.Error(ex);
          throw ExceptionTranslator.Translate(ex);
        }
      }));
    }

    public Task DeleteQuizDataAsync(string uid, string language, bool recursive = false)
    {
      return Task.Factory.StartNew((Action) (() =>
      {
        try
        {
          this._dataService.DeleteQuizData(uid, language, recursive);
        }
        catch (Exception ex)
        {
          QuizService.Logger.Error(ex);
          throw ExceptionTranslator.Translate(ex);
        }
      }));
    }
  }
}

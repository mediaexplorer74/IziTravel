// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Data.Services.Implementation.LocalDataService
// Assembly: Izi.Travel.Data, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 9765AC3B-732C-4703-A0F8-C0EBF29D8E89
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Data.dll

using Izi.Travel.Data.Context;
using Izi.Travel.Data.DbVersion.Updaters;
using Izi.Travel.Data.DbVersion.Updaters.Base;
using Izi.Travel.Data.Entities.Local;
using Izi.Travel.Data.Entities.Local.Query;
using Izi.Travel.Data.Services.Contract;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Data.Services.Implementation
{
  public class LocalDataService : ILocalDataService
  {
    public void CreateOrUpdateDataBase()
    {
      DbUpdaterBase[] dbUpdaterBaseArray = new DbUpdaterBase[2]
      {
        (DbUpdaterBase) new LocalDbUpdater(),
        (DbUpdaterBase) new DownloadDbUpdater()
      };
      foreach (DbUpdaterBase dbUpdaterBase in dbUpdaterBaseArray)
        dbUpdaterBase.Update();
    }

    public int CreatePurchase(Purchase purchase)
    {
      if (purchase == null)
        throw new ArgumentNullException(nameof (purchase));
      using (LocalDataContext localDataContext = new LocalDataContext())
      {
        localDataContext.Purchases.InsertOnSubmit(purchase);
        localDataContext.SubmitChanges();
        return purchase.Id;
      }
    }

    public Purchase[] GetPurchaseList(PurchaseLocalListQuery query)
    {
      if (query == null)
        throw new ArgumentNullException(nameof (query));
      using (LocalDataContext localDataContext = new LocalDataContext())
      {
        IQueryable<Purchase> source1 = localDataContext.Purchases.AsQueryable<Purchase>();
        if (query.Types != null)
          source1 = source1.Where<Purchase>((Expression<Func<Purchase, bool>>) (x => query.Types.Contains<string>(x.Type)));
        if (query.Languages != null)
          source1 = source1.Where<Purchase>((Expression<Func<Purchase, bool>>) (x => query.Languages.Contains<string>(x.Language)));
        int? nullable = query.Offset;
        if (nullable.HasValue)
        {
          IQueryable<Purchase> source2 = source1;
          nullable = query.Offset;
          int count = nullable.Value;
          source1 = source2.Skip<Purchase>(count);
        }
        nullable = query.Limit;
        if (nullable.HasValue)
        {
          IQueryable<Purchase> source3 = source1;
          nullable = query.Limit;
          int count = nullable.Value;
          source1 = source3.Take<Purchase>(count);
        }
        return source1.ToArray<Purchase>();
      }
    }

    public string[] GetPurchaseUidList()
    {
      using (LocalDataContext localDataContext = new LocalDataContext())
        return localDataContext.Purchases.Select<Purchase, string>((Expression<Func<Purchase, string>>) (x => x.Uid)).ToArray<string>();
    }

    public int CreateBookmark(Bookmark bookmark)
    {
      if (bookmark == null)
        throw new ArgumentNullException(nameof (bookmark));
      using (LocalDataContext localDataContext = new LocalDataContext())
      {
        localDataContext.Bookmarks.InsertOnSubmit(bookmark);
        localDataContext.SubmitChanges();
        return bookmark.Id;
      }
    }

    public bool IsBookmarkExists(string uid, string language)
    {
      if (string.IsNullOrWhiteSpace(uid))
        throw new ArgumentNullException(nameof (uid));
      using (LocalDataContext localDataContext = new LocalDataContext())
        return localDataContext.Bookmarks.Any<Bookmark>((Expression<Func<Bookmark, bool>>) (x => x.Uid == uid && x.Language == language));
    }

    public void DeleteBookmark(string uid, string language)
    {
      if (string.IsNullOrWhiteSpace(uid))
        throw new ArgumentNullException(nameof (uid));
      using (LocalDataContext localDataContext = new LocalDataContext())
      {
        Bookmark entity = localDataContext.Bookmarks.FirstOrDefault<Bookmark>((Expression<Func<Bookmark, bool>>) (x => x.Uid == uid && x.Language == language));
        if (entity == null)
          return;
        localDataContext.Bookmarks.DeleteOnSubmit(entity);
        localDataContext.SubmitChanges();
      }
    }

    public Bookmark[] GetBookmarkList(BookmarkLocalListQuery query)
    {
      if (query == null)
        throw new ArgumentNullException(nameof (query));
      string[] languages = query.Languages ?? new string[0];
      using (LocalDataContext localDataContext = new LocalDataContext())
      {
        IQueryable<Bookmark> source1 = localDataContext.Bookmarks.Where<Bookmark>((Expression<Func<Bookmark, bool>>) (x => query.Types == default (object) || query.Types.Contains<string>(x.Type))).Where<Bookmark>((Expression<Func<Bookmark, bool>>) (x => languages.Length == 0 || languages.Contains<string>(x.Language)));
        int? nullable = query.Offset;
        int count;
        if (!nullable.HasValue)
        {
          count = 0;
        }
        else
        {
          nullable = query.Offset;
          count = nullable.Value;
        }
        IQueryable<Bookmark> source2 = source1.Skip<Bookmark>(count);
        nullable = query.Limit;
        int maxValue;
        if (!nullable.HasValue)
        {
          maxValue = int.MaxValue;
        }
        else
        {
          nullable = query.Limit;
          maxValue = nullable.Value;
        }
        return source2.Take<Bookmark>(maxValue).ToArray<Bookmark>();
      }
    }

    public void ClearBookmarkList()
    {
      using (LocalDataContext localDataContext = new LocalDataContext())
      {
        localDataContext.Bookmarks.DeleteAllOnSubmit<Bookmark>((IEnumerable<Bookmark>) localDataContext.Bookmarks);
        localDataContext.SubmitChanges();
      }
    }

    public int CreateHistory(History history)
    {
      if (history == null)
        throw new ArgumentNullException(nameof (history));
      using (LocalDataContext localDataContext = new LocalDataContext())
      {
        localDataContext.Histories.InsertOnSubmit(history);
        localDataContext.SubmitChanges();
        return history.Id;
      }
    }

    public void UpdateHistory(History history)
    {
      if (history == null)
        throw new ArgumentNullException();
      using (LocalDataContext localDataContext = new LocalDataContext())
      {
        History history1 = localDataContext.Histories.FirstOrDefault<History>((Expression<Func<History, bool>>) (x => x.Id == history.Id));
        if (history1 == null)
          return;
        history1.Title = history.Title;
        history1.Uid = history.Uid;
        history1.ContentProviderUid = history.ContentProviderUid;
        history1.ImageUid = history.ImageUid;
        history1.Language = history.Language;
        history1.DateTime = history.DateTime;
        history1.Type = history.Type;
        history1.ParentUid = history.ParentUid;
        localDataContext.SubmitChanges();
      }
    }

    public History GetHistory(string uid, string language)
    {
      if (string.IsNullOrWhiteSpace(uid))
        throw new ArgumentNullException(nameof (uid));
      using (LocalDataContext localDataContext = new LocalDataContext())
        return localDataContext.Histories.FirstOrDefault<History>((Expression<Func<History, bool>>) (x => x.Uid == uid && x.Language == language));
    }

    public History[] GetHistoryList(HistoryLocalListQuery query)
    {
      if (query == null)
        throw new ArgumentNullException(nameof (query));
      string[] languages = query.Languages ?? new string[0];
      using (LocalDataContext localDataContext = new LocalDataContext())
      {
        IQueryable<History> source1 = localDataContext.Histories.Where<History>((Expression<Func<History, bool>>) (x => query.Types == default (object) || query.Types.Contains<string>(x.Type))).Where<History>((Expression<Func<History, bool>>) (x => languages.Length == 0 || languages.Contains<string>(x.Language))).Where<History>((Expression<Func<History, bool>>) (x => x.DateTime >= query.From && x.DateTime <= query.To));
        int? nullable = query.Offset;
        int count;
        if (!nullable.HasValue)
        {
          count = 0;
        }
        else
        {
          nullable = query.Offset;
          count = nullable.Value;
        }
        IQueryable<History> source2 = source1.Skip<History>(count);
        nullable = query.Limit;
        int maxValue;
        if (!nullable.HasValue)
        {
          maxValue = int.MaxValue;
        }
        else
        {
          nullable = query.Limit;
          maxValue = nullable.Value;
        }
        return source2.Take<History>(maxValue).ToArray<History>();
      }
    }

    public void ClearHistoryList()
    {
      using (LocalDataContext localDataContext = new LocalDataContext())
      {
        localDataContext.Histories.DeleteAllOnSubmit<History>((IEnumerable<History>) localDataContext.Histories);
        localDataContext.SubmitChanges();
      }
    }

    public TourPlaybackItem[] GetTourPlaybackItemList(string tourUid)
    {
      if (string.IsNullOrWhiteSpace(tourUid))
        return (TourPlaybackItem[]) null;
      using (LocalDataContext localDataContext = new LocalDataContext())
        return localDataContext.TourPlaybackItems.Where<TourPlaybackItem>((Expression<Func<TourPlaybackItem, bool>>) (x => x.TourUid == tourUid)).OrderBy<TourPlaybackItem, DateTime>((Expression<Func<TourPlaybackItem, DateTime>>) (x => x.DateTime)).ToArray<TourPlaybackItem>();
    }

    public TourPlaybackItem[] GetLastTourPlaybackItemList()
    {
      using (LocalDataContext localDataContext = new LocalDataContext())
      {
        TourPlaybackItem lastItem = localDataContext.TourPlaybackItems.OrderByDescending<TourPlaybackItem, DateTime>((Expression<Func<TourPlaybackItem, DateTime>>) (x => x.DateTime)).FirstOrDefault<TourPlaybackItem>();
        TourPlaybackItem[] playbackItemList;
        if (lastItem == null)
          playbackItemList = (TourPlaybackItem[]) null;
        else
          playbackItemList = localDataContext.TourPlaybackItems.Where<TourPlaybackItem>((Expression<Func<TourPlaybackItem, bool>>) (x => x.TourUid == lastItem.TourUid)).OrderBy<TourPlaybackItem, DateTime>((Expression<Func<TourPlaybackItem, DateTime>>) (x => x.DateTime)).ToArray<TourPlaybackItem>();
        return playbackItemList;
      }
    }

    public int CreateTourPlaybackItem(TourPlaybackItem tourPlaybackItem)
    {
      if (tourPlaybackItem == null)
        throw new ArgumentNullException(nameof (tourPlaybackItem));
      using (LocalDataContext localDataContext = new LocalDataContext())
      {
        localDataContext.TourPlaybackItems.InsertOnSubmit(tourPlaybackItem);
        localDataContext.SubmitChanges();
        return tourPlaybackItem.Id;
      }
    }

    public void ClearTourPlaybackItemList(string tourUid)
    {
      if (string.IsNullOrWhiteSpace(tourUid))
        return;
      using (LocalDataContext localDataContext = new LocalDataContext())
      {
        IQueryable<TourPlaybackItem> entities = localDataContext.TourPlaybackItems.Where<TourPlaybackItem>((Expression<Func<TourPlaybackItem, bool>>) (x => x.TourUid == tourUid));
        localDataContext.TourPlaybackItems.DeleteAllOnSubmit<TourPlaybackItem>((IEnumerable<TourPlaybackItem>) entities);
        localDataContext.SubmitChanges();
      }
    }

    public AudioTrackData[] GetAudioTrackList(AudioTrackListQuery query)
    {
      if (query == null)
        throw new ArgumentNullException(nameof (query));
      if (query.StateList == null)
        query.StateList = new AudioTrackState[0];
      if (query.IdList == null)
        query.IdList = new int[0];
      if (query.UidList == null)
        query.UidList = new string[0];
      using (LocalDataContext localDataContext = new LocalDataContext())
        return localDataContext.Playlist.Where<AudioTrackData>((Expression<Func<AudioTrackData, bool>>) (x => query.StateList.Length == 0 || query.StateList.Contains<AudioTrackState>(x.State))).Where<AudioTrackData>((Expression<Func<AudioTrackData, bool>>) (x => query.IdList.Length == 0 || query.IdList.Contains<int>(x.Id))).Where<AudioTrackData>((Expression<Func<AudioTrackData, bool>>) (x => query.UidList.Length == 0 || query.UidList.Contains<string>(x.Uid))).OrderBy<AudioTrackData, DateTime>((Expression<Func<AudioTrackData, DateTime>>) (x => x.DateTime)).ToArray<AudioTrackData>();
    }

    public int CreateAudioTrack(AudioTrackData audioTrackData)
    {
      if (audioTrackData == null)
        throw new ArgumentNullException(nameof (audioTrackData));
      using (LocalDataContext localDataContext = new LocalDataContext())
      {
        localDataContext.Playlist.InsertOnSubmit(audioTrackData);
        localDataContext.SubmitChanges();
        return audioTrackData.Id;
      }
    }

    public void UpdateAudioTrack(AudioTrackData audioTrackData)
    {
      if (audioTrackData == null)
        throw new ArgumentNullException(nameof (audioTrackData));
      if (audioTrackData.Id < 1)
        throw new Exception("ID is not defined for object");
      using (LocalDataContext localDataContext = new LocalDataContext())
      {
        AudioTrackData audioTrackData1 = localDataContext.Playlist.FirstOrDefault<AudioTrackData>((Expression<Func<AudioTrackData, bool>>) (x => x.Id == audioTrackData.Id));
        if (audioTrackData1 == null)
          return;
        audioTrackData1.Uid = audioTrackData.Uid;
        audioTrackData1.Title = audioTrackData.Title;
        audioTrackData1.State = audioTrackData.State;
        audioTrackData1.Tag = audioTrackData.Tag;
        audioTrackData1.DateTime = audioTrackData.DateTime;
        localDataContext.SubmitChanges();
      }
    }

    public void DeleteAudioTrackList(int[] idList)
    {
      if (idList == null)
        throw new ArgumentNullException(nameof (idList));
      if (idList.Length == 0)
        return;
      using (LocalDataContext localDataContext = new LocalDataContext())
      {
        IQueryable<AudioTrackData> entities = localDataContext.Playlist.Where<AudioTrackData>((Expression<Func<AudioTrackData, bool>>) (x => idList.Contains<int>(x.Id)));
        localDataContext.Playlist.DeleteAllOnSubmit<AudioTrackData>((IEnumerable<AudioTrackData>) entities);
        localDataContext.SubmitChanges();
      }
    }

    public void ClearPlaylist()
    {
      using (LocalDataContext localDataContext = new LocalDataContext())
      {
        localDataContext.Playlist.DeleteAllOnSubmit<AudioTrackData>((IEnumerable<AudioTrackData>) localDataContext.Playlist);
        localDataContext.SubmitChanges();
      }
    }

    public QuizData GetQuizData(QuizDataQuery query)
    {
      if (query == null)
        throw new ArgumentNullException(nameof (query));
      if (string.IsNullOrWhiteSpace(query.Uid) || string.IsNullOrWhiteSpace(query.Language))
        return (QuizData) null;
      string uid = query.Uid.Trim().ToLower();
      string language = query.Language.Trim().ToLower();
      using (LocalDataContext localDataContext = new LocalDataContext())
        return localDataContext.QuizData.FirstOrDefault<QuizData>((Expression<Func<QuizData, bool>>) (x => x.Uid == uid && x.Language == language));
    }

    public QuizData[] GetQuizDataList(QuizDataListQuery query)
    {
      int? nullable = query != null ? query.Offset : throw new ArgumentNullException(nameof (query));
      int num;
      if (!nullable.HasValue)
      {
        num = 0;
      }
      else
      {
        nullable = query.Offset;
        num = nullable.Value;
      }
      int count1 = num;
      nullable = query.Limit;
      int maxValue;
      if (!nullable.HasValue)
      {
        maxValue = int.MaxValue;
      }
      else
      {
        nullable = query.Limit;
        maxValue = nullable.Value;
      }
      int count2 = maxValue;
      string[] queryUidList = query.UidList == null || query.UidList.Length == 0 ? new string[0] : ((IEnumerable<string>) query.UidList).Where<string>((Func<string, bool>) (x => !string.IsNullOrWhiteSpace(x))).Select<string, string>((Func<string, string>) (x => x.Trim().ToLower())).ToArray<string>();
      string[] queryLanguageList = query.LanguageList == null || query.LanguageList.Length == 0 ? new string[0] : ((IEnumerable<string>) query.LanguageList).Where<string>((Func<string, bool>) (x => !string.IsNullOrWhiteSpace(x))).Select<string, string>((Func<string, string>) (x => x.Trim().ToLower())).ToArray<string>();
      using (LocalDataContext localDataContext = new LocalDataContext())
        return localDataContext.QuizData.Where<QuizData>((Expression<Func<QuizData, bool>>) (quizData => (queryUidList.Length == 0 || queryUidList.Contains<string>(quizData.Uid)) && (queryLanguageList.Length == 0 || queryLanguageList.Contains<string>(quizData.Language)))).OrderByDescending<QuizData, DateTime>((Expression<Func<QuizData, DateTime>>) (x => x.DateTime)).Skip<QuizData>(count1).Take<QuizData>(count2).ToArray<QuizData>();
    }

    public QuizDataStatistics GetQuizDataStatistics(QuizDataStatisticsQuery query)
    {
      if (query == null)
        throw new ArgumentNullException(nameof (query));
      string[] queryUidList = query.UidList == null || query.UidList.Length == 0 ? new string[0] : ((IEnumerable<string>) query.UidList).Where<string>((Func<string, bool>) (x => !string.IsNullOrWhiteSpace(x))).Select<string, string>((Func<string, string>) (x => x.Trim().ToLower())).ToArray<string>();
      string[] queryLanguageList = query.LanguageList == null || query.LanguageList.Length == 0 ? new string[0] : ((IEnumerable<string>) query.LanguageList).Where<string>((Func<string, bool>) (x => !string.IsNullOrWhiteSpace(x))).Select<string, string>((Func<string, string>) (x => x.Trim().ToLower())).ToArray<string>();
      QuizDataStatistics quizDataStatistics = new QuizDataStatistics();
      using (LocalDataContext localDataContext = new LocalDataContext())
      {
        IQueryable<QuizData> source = localDataContext.QuizData.Where<QuizData>((Expression<Func<QuizData, bool>>) (x => (queryUidList.Length == 0 || queryUidList.Contains<string>(x.Uid)) && (queryLanguageList.Length == 0 || queryLanguageList.Contains<string>(x.Language)))).AsQueryable<QuizData>();
        quizDataStatistics.CorrectAnswerCount = source.Count<QuizData>((Expression<Func<QuizData, bool>>) (x => x.AnswerCorrect));
        quizDataStatistics.IncorrectAnswerCount = source.Count<QuizData>((Expression<Func<QuizData, bool>>) (x => !x.AnswerCorrect));
      }
      return quizDataStatistics;
    }

    public int CreateOrUpdateQuizData(QuizData quizData)
    {
      if (quizData == null)
        throw new ArgumentNullException(nameof (quizData));
      if (!string.IsNullOrWhiteSpace(quizData.ParentUid))
        quizData.ParentUid = quizData.ParentUid.Trim().ToLower();
      if (string.IsNullOrWhiteSpace(quizData.Uid))
        quizData.Uid = quizData.Uid.Trim().ToLower();
      if (string.IsNullOrWhiteSpace(quizData.Language))
        quizData.Language = quizData.Language.Trim().ToLower();
      quizData.DateTime = DateTime.Now;
      using (LocalDataContext localDataContext = new LocalDataContext())
      {
        QuizData quizData1 = localDataContext.QuizData.FirstOrDefault<QuizData>((Expression<Func<QuizData, bool>>) (x => x.Uid == quizData.Uid && x.Language == quizData.Language));
        if (quizData1 != null)
        {
          quizData1.Uid = quizData.Uid;
          quizData1.ParentUid = quizData.ParentUid;
          quizData1.Language = quizData.Language;
          quizData1.Title = quizData.Title;
          quizData1.Type = quizData.Type;
          quizData1.ImageUid = quizData.ImageUid;
          quizData1.ContentProviderUid = quizData.ContentProviderUid;
          quizData1.AnswerCorrect = quizData.AnswerCorrect;
          quizData1.AnswerIndex = quizData.AnswerIndex;
          quizData1.DateTime = quizData.DateTime;
          quizData1.Data = quizData.Data;
        }
        else
          localDataContext.QuizData.InsertOnSubmit(quizData);
        localDataContext.SubmitChanges();
        return quizData1 != null ? quizData1.Id : quizData.Id;
      }
    }

    public void DeleteQuizData(string uid, string language, bool recursive = false)
    {
      if (string.IsNullOrWhiteSpace(uid) || string.IsNullOrWhiteSpace(language))
        return;
      string uidValue = uid.Trim().ToLower();
      string languageValue = language.Trim().ToLower();
      using (LocalDataContext localDataContext = new LocalDataContext())
      {
        QuizData entity = localDataContext.QuizData.FirstOrDefault<QuizData>((Expression<Func<QuizData, bool>>) (x => x.Uid == uidValue && x.Language == languageValue));
        if (entity == null)
          return;
        if (recursive)
        {
          IQueryable<QuizData> queryable = localDataContext.QuizData.Where<QuizData>((Expression<Func<QuizData, bool>>) (x => x.ParentUid == uidValue && x.Language == languageValue));
          if (queryable.Any<QuizData>())
            localDataContext.QuizData.DeleteAllOnSubmit<QuizData>((IEnumerable<QuizData>) queryable);
        }
        localDataContext.QuizData.DeleteOnSubmit(entity);
        localDataContext.SubmitChanges(ConflictMode.FailOnFirstConflict);
      }
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Data.Services.Contract.ILocalDataService
// Assembly: Izi.Travel.Data, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 9765AC3B-732C-4703-A0F8-C0EBF29D8E89
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Data.dll

using Izi.Travel.Data.Entities.Local;
using Izi.Travel.Data.Entities.Local.Query;

#nullable disable
namespace Izi.Travel.Data.Services.Contract
{
  public interface ILocalDataService
  {
    void CreateOrUpdateDataBase();

    int CreatePurchase(Purchase purchase);

    Purchase[] GetPurchaseList(PurchaseLocalListQuery query);

    string[] GetPurchaseUidList();

    int CreateBookmark(Bookmark bookmark);

    bool IsBookmarkExists(string uid, string language);

    void DeleteBookmark(string uid, string language);

    Bookmark[] GetBookmarkList(BookmarkLocalListQuery query);

    void ClearBookmarkList();

    int CreateHistory(History history);

    void UpdateHistory(History history);

    History GetHistory(string uid, string language);

    History[] GetHistoryList(HistoryLocalListQuery query);

    void ClearHistoryList();

    TourPlaybackItem[] GetTourPlaybackItemList(string tourUid);

    TourPlaybackItem[] GetLastTourPlaybackItemList();

    int CreateTourPlaybackItem(TourPlaybackItem tourPlaybackItem);

    void ClearTourPlaybackItemList(string tourUid);

    AudioTrackData[] GetAudioTrackList(AudioTrackListQuery query);

    void UpdateAudioTrack(AudioTrackData audioTrackData);

    int CreateAudioTrack(AudioTrackData audioTrackData);

    void DeleteAudioTrackList(int[] idList);

    void ClearPlaylist();

    QuizData GetQuizData(QuizDataQuery query);

    QuizData[] GetQuizDataList(QuizDataListQuery query);

    QuizDataStatistics GetQuizDataStatistics(QuizDataStatisticsQuery query);

    int CreateOrUpdateQuizData(QuizData quizData);

    void DeleteQuizData(string uid, string language, bool recursive = false);
  }
}

// ********************************************************************
// Type: Izi.Travel.Data.Context.DownloadDataContext
// Assembly: Izi.Travel.Data, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 9765AC3B-732C-4703-A0F8-C0EBF29D8E89
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Data.dll

using Izi.Travel.Data.Entities.Download;
using Izi.Travel.Data.Entities.Local;
using System;
using System.Collections.Generic;
using Windows.Media.Core;

namespace Izi.Travel.Data.Context
{
    public class Table<T>
    {
        public void DeleteAllOnSubmit<T1>(IEnumerable<T1> entities)
        {
            throw new NotImplementedException();
        }

       
        public void InsertOnSubmit(Purchase purchase)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AudioTrackData> Where(Func<IEnumerable<AudioTrack>> entity1, Func<IEnumerable<AudioTrack>> entity2)
        {
            throw new NotImplementedException();
        }

        internal void DeleteOnSubmit(Bookmark entity)
        {
            throw new NotImplementedException();
        }

        internal void DeleteOnSubmit(DownloadObject entity)
        {
            throw new NotImplementedException();
        }

        internal void DeleteOnSubmit(DownloadObjectLink entity)
        {
            throw new NotImplementedException();
        }

        internal void DeleteOnSubmit(QuizData entity)
        {
            throw new NotImplementedException();
        }

        internal void InsertAllOnSubmit<T1>(IEnumerable<T1> downloadMediaItems)
        {
            throw new NotImplementedException();
        }

        internal void InsertOnSubmit(DownloadMedia downloadMedia)
        {
            throw new NotImplementedException();
        }

        internal void InsertOnSubmit(DownloadObject downloadObject)
        {
            throw new NotImplementedException();
        }

        internal void InsertOnSubmit(DownloadObjectLink downloadObjectLink)
        {
            throw new NotImplementedException();
        }

        internal void InsertOnSubmit(AudioTrackData audioTrackData)
        {
            throw new NotImplementedException();
        }

        internal void InsertOnSubmit(Bookmark bookmark)
        {
            throw new NotImplementedException();
        }

        internal void InsertOnSubmit(History history)
        {
            throw new NotImplementedException();
        }

        internal void InsertOnSubmit(QuizData quizData)
        {
            throw new NotImplementedException();
        }

        internal void InsertOnSubmit(TourPlaybackItem tourPlaybackItem)
        {
            throw new NotImplementedException();
        }

        internal IEnumerable<AudioTrackData> Where(Func<SomeTrack, IEnumerable<AudioTrack>> value)
        {
            throw new NotImplementedException();
        }
    }
}
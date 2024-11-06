// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Data.ScheduleDay
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

using System;
using System.Globalization;

#nullable disable
namespace Izi.Travel.Business.Entities.Data
{
  public class ScheduleDay
  {
    public DayOfWeek Day { get; set; }

    public TimeSpan? From { get; set; }

    public TimeSpan? To { get; set; }

    public string AbbreviatedName
    {
      get
      {
        DateTimeFormatInfo currentInfo = DateTimeFormatInfo.CurrentInfo;
        return currentInfo == null ? string.Empty : currentInfo.GetAbbreviatedDayName(this.Day);
      }
    }

    public string Period
    {
      get
      {
        return !this.From.HasValue || !this.To.HasValue ? string.Empty : string.Format("{0:hh\\:mm} - {1:hh\\:mm}", (object) this.From, (object) this.To);
      }
    }

    public ScheduleDay()
      : this(DayOfWeek.Monday)
    {
    }

    public ScheduleDay(DayOfWeek day) => this.Day = day;
  }
}

// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Entity.ScheduleMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Data;
using System;
using System.Collections.Generic;

#nullable disable
namespace Izi.Travel.Business.Mapping.Entity
{
  internal class ScheduleMapper : MapperBase<Izi.Travel.Business.Entities.Data.Schedule, Izi.Travel.Client.Entities.Schedule>
  {
    public override Izi.Travel.Client.Entities.Schedule Convert(Izi.Travel.Business.Entities.Data.Schedule source)
    {
      throw new NotImplementedException();
    }

    public override Izi.Travel.Business.Entities.Data.Schedule ConvertBack(Izi.Travel.Client.Entities.Schedule target)
    {
      if (target == null)
        return (Izi.Travel.Business.Entities.Data.Schedule) null;
      return new Izi.Travel.Business.Entities.Data.Schedule()
      {
        Days = new ScheduleDay[7]
        {
          ScheduleMapper.GetSheduleDay(DayOfWeek.Monday, (IReadOnlyList<string>) target.Mon),
          ScheduleMapper.GetSheduleDay(DayOfWeek.Tuesday, (IReadOnlyList<string>) target.Tue),
          ScheduleMapper.GetSheduleDay(DayOfWeek.Wednesday, (IReadOnlyList<string>) target.Wed),
          ScheduleMapper.GetSheduleDay(DayOfWeek.Thursday, (IReadOnlyList<string>) target.Thu),
          ScheduleMapper.GetSheduleDay(DayOfWeek.Friday, (IReadOnlyList<string>) target.Fri),
          ScheduleMapper.GetSheduleDay(DayOfWeek.Saturday, (IReadOnlyList<string>) target.Sat),
          ScheduleMapper.GetSheduleDay(DayOfWeek.Sunday, (IReadOnlyList<string>) target.Sun)
        }
      };
    }

    private static ScheduleDay GetSheduleDay(DayOfWeek day, IReadOnlyList<string> hours)
    {
      ScheduleDay sheduleDay = new ScheduleDay(day);
      if (hours != null && hours.Count == 2)
      {
        TimeSpan result1;
        if (TimeSpan.TryParse(hours[0], out result1))
          sheduleDay.From = new TimeSpan?(result1);
        TimeSpan result2;
        if (TimeSpan.TryParse(hours[1], out result2))
          sheduleDay.To = new TimeSpan?(result2);
      }
      return sheduleDay;
    }
  }
}

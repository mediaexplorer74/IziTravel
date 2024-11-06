// Decompiled with JetBrains decompiler
// Type: GoogleAnalytics.Core.TokenBucket
// Assembly: GoogleAnalytics.Core, Version=1.2.11.25892, Culture=neutral, PublicKeyToken=null
// MVID: DA6701CD-FFEA-4833-995F-5D20607A09B2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\GoogleAnalytics.Core.dll

using System;

#nullable disable
namespace GoogleAnalytics.Core
{
  internal class TokenBucket
  {
    private double capacity;
    private double tokens;
    private double fillRate;
    private DateTime timeStamp;
    private object locker = new object();

    public TokenBucket(double tokens, double fillRate)
    {
      this.capacity = tokens;
      this.tokens = tokens;
      this.fillRate = fillRate;
      this.timeStamp = DateTime.Now;
    }

    public bool Consume(double tokens = 1.0)
    {
      lock (this.locker)
      {
        if (this.GetTokens() - tokens <= 0.0)
          return false;
        this.tokens -= tokens;
        return true;
      }
    }

    private double GetTokens()
    {
      DateTime now = DateTime.Now;
      if (this.tokens < this.capacity)
      {
        this.tokens = Math.Min(this.capacity, this.tokens + this.fillRate * (now - this.timeStamp).TotalSeconds);
        this.timeStamp = now;
      }
      return this.tokens;
    }
  }
}

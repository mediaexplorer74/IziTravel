﻿// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Checksums.StrangeCRC
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51802, Culture=neutral, PublicKeyToken=null
// MVID: 6C361F8B-A5CA-49B8-ABC1-A50EE493C003
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\ICSharpCode.SharpZipLib.Portable.xml

using System;

#nullable disable
namespace ICSharpCode.SharpZipLib.Checksums
{
  /// <summary>Bzip2 checksum algorithm</summary>
  public class StrangeCRC : IChecksum
  {
    private static readonly uint[] crc32Table = new uint[256]
    {
      0U,
      79764919U,
      159529838U,
      222504665U,
      319059676U,
      398814059U,
      445009330U,
      507990021U,
      638119352U,
      583659535U,
      797628118U,
      726387553U,
      890018660U,
      835552979U,
      1015980042U,
      944750013U,
      1276238704U,
      1221641927U,
      1167319070U,
      1095957929U,
      1595256236U,
      1540665371U,
      1452775106U,
      1381403509U,
      1780037320U,
      1859660671U,
      1671105958U,
      1733955601U,
      2031960084U,
      2111593891U,
      1889500026U,
      1952343757U,
      2552477408U,
      2632100695U,
      2443283854U,
      2506133561U,
      2334638140U,
      2414271883U,
      2191915858U,
      2254759653U,
      3190512472U,
      3135915759U,
      3081330742U,
      3009969537U,
      2905550212U,
      2850959411U,
      2762807018U,
      2691435357U,
      3560074640U,
      3505614887U,
      3719321342U,
      3648080713U,
      3342211916U,
      3287746299U,
      3467911202U,
      3396681109U,
      4063920168U,
      4143685023U,
      4223187782U,
      4286162673U,
      3779000052U,
      3858754371U,
      3904687514U,
      3967668269U,
      881225847U,
      809987520U,
      1023691545U,
      969234094U,
      662832811U,
      591600412U,
      771767749U,
      717299826U,
      311336399U,
      374308984U,
      453813921U,
      533576470U,
      25881363U,
      88864420U,
      134795389U,
      214552010U,
      2023205639U,
      2086057648U,
      1897238633U,
      1976864222U,
      1804852699U,
      1867694188U,
      1645340341U,
      1724971778U,
      1587496639U,
      1516133128U,
      1461550545U,
      1406951526U,
      1302016099U,
      1230646740U,
      1142491917U,
      1087903418U,
      2896545431U,
      2825181984U,
      2770861561U,
      2716262478U,
      3215044683U,
      3143675388U,
      3055782693U,
      3001194130U,
      2326604591U,
      2389456536U,
      2200899649U,
      2280525302U,
      2578013683U,
      2640855108U,
      2418763421U,
      2498394922U,
      3769900519U,
      3832873040U,
      3912640137U,
      3992402750U,
      4088425275U,
      4151408268U,
      4197601365U,
      4277358050U,
      3334271071U,
      3263032808U,
      3476998961U,
      3422541446U,
      3585640067U,
      3514407732U,
      3694837229U,
      3640369242U,
      1762451694U,
      1842216281U,
      1619975040U,
      1682949687U,
      2047383090U,
      2127137669U,
      1938468188U,
      2001449195U,
      1325665622U,
      1271206113U,
      1183200824U,
      1111960463U,
      1543535498U,
      1489069629U,
      1434599652U,
      1363369299U,
      622672798U,
      568075817U,
      748617968U,
      677256519U,
      907627842U,
      853037301U,
      1067152940U,
      995781531U,
      51762726U,
      131386257U,
      177728840U,
      240578815U,
      269590778U,
      349224269U,
      429104020U,
      491947555U,
      4046411278U,
      4126034873U,
      4172115296U,
      4234965207U,
      3794477266U,
      3874110821U,
      3953728444U,
      4016571915U,
      3609705398U,
      3555108353U,
      3735388376U,
      3664026991U,
      3290680682U,
      3236090077U,
      3449943556U,
      3378572211U,
      3174993278U,
      3120533705U,
      3032266256U,
      2961025959U,
      2923101090U,
      2868635157U,
      2813903052U,
      2742672763U,
      2604032198U,
      2683796849U,
      2461293480U,
      2524268063U,
      2284983834U,
      2364738477U,
      2175806836U,
      2238787779U,
      1569362073U,
      1498123566U,
      1409854455U,
      1355396672U,
      1317987909U,
      1246755826U,
      1192025387U,
      1137557660U,
      2072149281U,
      2135122070U,
      1912620623U,
      1992383480U,
      1753615357U,
      1816598090U,
      1627664531U,
      1707420964U,
      295390185U,
      358241886U,
      404320391U,
      483945776U,
      43990325U,
      106832002U,
      186451547U,
      266083308U,
      932423249U,
      861060070U,
      1041341759U,
      986742920U,
      613929101U,
      542559546U,
      756411363U,
      701822548U,
      3316196985U,
      3244833742U,
      3425377559U,
      3370778784U,
      3601682597U,
      3530312978U,
      3744426955U,
      3689838204U,
      3819031489U,
      3881883254U,
      3928223919U,
      4007849240U,
      4037393693U,
      4100235434U,
      4180117107U,
      4259748804U,
      2310601993U,
      2373574846U,
      2151335527U,
      2231098320U,
      2596047829U,
      2659030626U,
      2470359227U,
      2550115596U,
      2947551409U,
      2876312838U,
      2788305887U,
      2733848168U,
      3165939309U,
      3094707162U,
      3040238851U,
      2985771188U
    };
    private int globalCrc;

    /// <summary>
    /// Initialise a default instance of <see cref="T:ICSharpCode.SharpZipLib.Checksums.StrangeCRC"></see>
    /// </summary>
    public StrangeCRC() => this.Reset();

    /// <summary>Reset the state of Crc.</summary>
    public void Reset() => this.globalCrc = -1;

    /// <summary>Get the current Crc value.</summary>
    public long Value => (long) ~this.globalCrc;

    /// <summary>Update the Crc value.</summary>
    /// <param name="value">data update is based on</param>
    public void Update(int value)
    {
      int index = this.globalCrc >> 24 ^ value;
      if (index < 0)
        index = 256 + index;
      this.globalCrc = (int) ((long) (this.globalCrc << 8) ^ (long) StrangeCRC.crc32Table[index]);
    }

    /// <summary>Update Crc based on a block of data</summary>
    /// <param name="buffer">The buffer containing data to update the crc with.</param>
    public void Update(byte[] buffer)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      this.Update(buffer, 0, buffer.Length);
    }

    /// <summary>Update Crc based on a portion of a block of data</summary>
    /// <param name="buffer">block of data</param>
    /// <param name="offset">index of first byte to use</param>
    /// <param name="count">number of bytes to use</param>
    public void Update(byte[] buffer, int offset, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      if (offset < 0)
        throw new ArgumentOutOfRangeException(nameof (offset), "cannot be less than zero");
      if (count < 0)
        throw new ArgumentOutOfRangeException(nameof (count), "cannot be less than zero");
      if (offset + count > buffer.Length)
        throw new ArgumentOutOfRangeException(nameof (count));
      for (int index = 0; index < count; ++index)
        this.Update((int) buffer[offset++]);
    }
  }
}

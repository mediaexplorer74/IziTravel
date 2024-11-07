// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Analytics.Parameters.CompletionReasonParameter
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

#nullable disable
namespace Izi.Travel.Business.Entities.Analytics.Parameters
{
  public sealed class CompletionReasonParameter : BaseParameter
  {
    private const int ParamIndex = 8;
    public static readonly CompletionReasonParameter Finished = new CompletionReasonParameter(nameof (Finished));
    public static readonly CompletionReasonParameter Interrupted = new CompletionReasonParameter(nameof (Interrupted));
    public static readonly CompletionReasonParameter Skipped = new CompletionReasonParameter(nameof (Skipped));
    public static readonly CompletionReasonParameter Error = new CompletionReasonParameter(nameof (Error));

    private CompletionReasonParameter(string value)
      : base(8, value)
    {
    }
  }
}

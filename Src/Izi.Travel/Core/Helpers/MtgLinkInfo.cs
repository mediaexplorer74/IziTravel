// ********************************************************************
// Type: Izi.Travel.Core.Helpers.MtgLinkInfo
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

#nullable disable
using System;

namespace Izi.Travel.Core.Helpers
{
  public class MtgLinkInfo
  {
        internal string ObjectId;
        internal string ParentId;

        public string Uid { get; set; }

    public string ParentUid { get; set; }

    public string Number { get; set; }

    public string Language { get; set; }

    public string Passcode { get; set; }

    public MtgLinkType Type { get; set; }

        internal static MtgLinkInfo Parse(string data)
        {
            throw new NotImplementedException();
        }
    }
}

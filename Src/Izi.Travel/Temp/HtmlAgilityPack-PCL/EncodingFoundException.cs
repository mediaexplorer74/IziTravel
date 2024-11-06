// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.EncodingFoundException
// Assembly: HtmlAgilityPack-PCL, Version=1.4.6.0, Culture=neutral, PublicKeyToken=null
// MVID: A611BE5D-A211-439D-AF0B-7D7BA44DC844
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\HtmlAgilityPack-PCL.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\HtmlAgilityPack-PCL.xml

using System;
using System.Text;

#nullable disable
namespace HtmlAgilityPack
{
  internal class EncodingFoundException : Exception
  {
    public EncodingFoundException(Encoding Encoding) => this.Encoding = Encoding;

    public Encoding Encoding { get; private set; }
  }
}

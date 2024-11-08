// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Entities.Quiz
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using Newtonsoft.Json;
using System.Collections.Generic;

#nullable disable
namespace Izi.Travel.Client.Entities
{
  [JsonObject]
  public class Quiz
  {
    [JsonProperty(PropertyName = "question")]
    public string Question { get; set; }

    [JsonProperty(PropertyName = "comment")]
    public string Comment { get; set; }

    [JsonProperty(PropertyName = "answers")]
    public List<QuizAnswer> Answers { get; set; }
  }
}

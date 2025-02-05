// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Buffer
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System.Text;

#nullable disable
namespace Izi.Travel.Shell.Core
{
  public class Buffer
  {
    private readonly object _lock;
    private readonly StringBuilder _stringBuilder;

    public Buffer()
    {
      this._lock = new object();
      this._stringBuilder = new StringBuilder();
    }

    public void Push(string data)
    {
      lock (this._lock)
        this._stringBuilder.AppendLine(data);
    }

    public string Pop()
    {
      lock (this._lock)
      {
        string str = this._stringBuilder.ToString();
        this._stringBuilder.Clear();
        return str;
      }
    }
  }
}

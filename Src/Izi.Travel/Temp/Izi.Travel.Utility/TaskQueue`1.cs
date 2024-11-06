// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Utility.TaskQueue`1
// Assembly: Izi.Travel.Utility, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 6E74EF73-7EB1-46AA-A84C-A1A7E0B11FE0
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Utility.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Utility
{
  public class TaskQueue<T> where T : class
  {
    private readonly Queue<T> _queue;
    private readonly object _syncRoot = new object();

    public int Count
    {
      get
      {
        lock (this._syncRoot)
          return this._queue.Count;
      }
    }

    public TaskQueue(IEnumerable<T> items) => this._queue = new Queue<T>(items);

    public Task ProcessAsync(int threadCount, Func<T, Task> action)
    {
      return Task.WhenAll(Enumerable.Range(0, threadCount).Select<int, Task>((Func<int, Task>) (x => Task.Run((Func<Task>) (async () => await this.ProcessInternal(action))))));
    }

    private async Task ProcessInternal(Func<T, Task> action)
    {
      while (true)
      {
        T obj = this.TryDequeue();
        if ((object) obj != null)
          await action(obj);
        else
          break;
      }
    }

    private T TryDequeue()
    {
      lock (this._syncRoot)
        return this._queue.Count > 0 ? this._queue.Dequeue() : default (T);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: Weakly.DisposableAction
// Assembly: Weakly, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e9c206b2200b970
// MVID: 59987104-5B29-48EC-89B5-2E7347C0D910
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Weakly.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Weakly.xml

using System;

#nullable disable
namespace Weakly
{
  /// <summary>Executes an action when disposed.</summary>
  public sealed class DisposableAction : IDisposable
  {
    private Action _action;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Weakly.DisposableAction" /> class.
    /// </summary>
    /// <param name="action">The action to execute on dispose.</param>
    public DisposableAction(Action action)
    {
      this._action = action != null ? action : throw new ArgumentNullException(nameof (action));
    }

    /// <summary>Executes the supplied action.</summary>
    public void Dispose()
    {
      if (this._action == null)
        return;
      this._action();
      this._action = (Action) null;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Controls.Tiles.TileServiceBase
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;

#nullable disable
namespace Izi.Travel.Shell.Core.Controls.Tiles
{
  internal abstract class TileServiceBase : ITileService
  {
    public const double DefaultTimerInterval = 2500.0;
    private static readonly Random Random = new Random(Environment.TickCount);
    private static readonly DispatcherTimer Timer = new DispatcherTimer();
    private static readonly List<WeakReference> EnabledPool = new List<WeakReference>();
    private static readonly List<WeakReference> FrozenPool = new List<WeakReference>();
    private static readonly List<WeakReference> StalledPipiline = new List<WeakReference>();

    protected TileServiceBase() => TileServiceBase.Timer.Tick += new EventHandler(this.OnTimerTick);

    public void Initialize(BaseTileControl control)
    {
      WeakReference tile = new WeakReference((object) control, false);
      if (control.IsFrozen)
        TileServiceBase.AddReferenceToFrozenPool(tile);
      else
        TileServiceBase.AddReferenceToEnabledPool(tile);
      TileServiceBase.RestartTimer();
    }

    public void Deinitialize(BaseTileControl control)
    {
      WeakReference tile = new WeakReference((object) control, false);
      TileServiceBase.RemoveReferenceFromEnabledPool(tile);
      TileServiceBase.RemoveReferenceFromFrozenPool(tile);
      TileServiceBase.RemoveReferenceFromStalledPipeline(tile);
    }

    public void FreezeTile(BaseTileControl control)
    {
      WeakReference tile = new WeakReference((object) control, false);
      TileServiceBase.AddReferenceToFrozenPool(tile);
      TileServiceBase.RemoveReferenceFromEnabledPool(tile);
      TileServiceBase.RemoveReferenceFromStalledPipeline(tile);
    }

    public void UnfreezeTile(BaseTileControl control)
    {
      WeakReference tile = new WeakReference((object) control, false);
      TileServiceBase.AddReferenceToEnabledPool(tile);
      TileServiceBase.RemoveReferenceFromFrozenPool(tile);
      TileServiceBase.RemoveReferenceFromStalledPipeline(tile);
      TileServiceBase.RestartTimer();
    }

    private static void RestartTimer()
    {
      if (TileServiceBase.Timer.IsEnabled)
        return;
      TileServiceBase.Timer.Interval = TimeSpan.FromMilliseconds(2500.0);
      TileServiceBase.Timer.Start();
    }

    private static void AddReferenceToEnabledPool(WeakReference tile)
    {
      if (TileServiceBase.ContainsTarget((IEnumerable<WeakReference>) TileServiceBase.EnabledPool, tile.Target))
        return;
      TileServiceBase.EnabledPool.Add(tile);
    }

    private static void AddReferenceToFrozenPool(WeakReference tile)
    {
      if (TileServiceBase.ContainsTarget((IEnumerable<WeakReference>) TileServiceBase.FrozenPool, tile.Target))
        return;
      TileServiceBase.FrozenPool.Add(tile);
    }

    private static void AddReferenceToStalledPipeline(WeakReference tile)
    {
      if (TileServiceBase.ContainsTarget((IEnumerable<WeakReference>) TileServiceBase.StalledPipiline, tile.Target))
        return;
      TileServiceBase.StalledPipiline.Add(tile);
    }

    private static void RemoveReferenceFromEnabledPool(WeakReference tile)
    {
      TileServiceBase.RemoveTarget((IList<WeakReference>) TileServiceBase.EnabledPool, tile.Target);
    }

    private static void RemoveReferenceFromFrozenPool(WeakReference tile)
    {
      TileServiceBase.RemoveTarget((IList<WeakReference>) TileServiceBase.FrozenPool, tile.Target);
    }

    private static void RemoveReferenceFromStalledPipeline(WeakReference tile)
    {
      TileServiceBase.RemoveTarget((IList<WeakReference>) TileServiceBase.StalledPipiline, tile.Target);
    }

    private static bool ContainsTarget(IEnumerable<WeakReference> list, object target)
    {
      return list.Any<WeakReference>((Func<WeakReference, bool>) (t => t.Target == target));
    }

    private static void RemoveTarget(IList<WeakReference> list, object target)
    {
      for (int index = 0; index < list.Count; ++index)
      {
        if (list[index].Target == target)
        {
          list.RemoveAt(index);
          break;
        }
      }
    }

    protected virtual void ProcessTile(BaseTileControl control)
    {
    }

    protected virtual TimeSpan GetTimerInterval() => TimeSpan.FromMilliseconds(2500.0);

    private void OnTimerTick(object sender, EventArgs eventArgs)
    {
      TileServiceBase.Timer.Stop();
      for (int index = 0; index < TileServiceBase.StalledPipiline.Count; ++index)
      {
        if (TileServiceBase.StalledPipiline[index].Target is BaseTileControl target && target.StallingCounter-- == 0)
        {
          TileServiceBase.AddReferenceToEnabledPool(TileServiceBase.StalledPipiline[index]);
          TileServiceBase.RemoveReferenceFromStalledPipeline(TileServiceBase.StalledPipiline[index]);
          --index;
        }
      }
      if (TileServiceBase.EnabledPool.Count > 0)
      {
        for (int index1 = 0; index1 < 1; ++index1)
        {
          int index2 = TileServiceBase.Random.Next(TileServiceBase.EnabledPool.Count);
          if (TileServiceBase.EnabledPool[index2].Target is BaseTileControl target)
          {
            this.ProcessTile(target);
            target.StallingCounter = 3;
            TileServiceBase.AddReferenceToStalledPipeline(TileServiceBase.EnabledPool[index2]);
            TileServiceBase.RemoveReferenceFromEnabledPool(TileServiceBase.EnabledPool[index2]);
          }
        }
      }
      else if (TileServiceBase.StalledPipiline.Count == 0)
        return;
      TileServiceBase.Timer.Interval = TimeSpan.FromMilliseconds((double) (TileServiceBase.Random.Next(1, 31) * 100));
      TileServiceBase.Timer.Start();
    }
  }
}

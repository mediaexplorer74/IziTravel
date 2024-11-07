// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Geofencing.Geotracker.BaseGeotracker
// Assembly: Izi.Travel.Geofencing, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 67B57F63-A085-4500-9D6D-5D3E58E5548F
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Geofencing.dll

using Caliburn.Micro;
using Izi.Travel.Geofencing.Primitives;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Windows.Foundation;

#nullable disable
namespace Izi.Travel.Geofencing.Geotracker
{
  public abstract class BaseGeotracker : IGeotracker
  {
    private readonly ILog _log = LogManager.GetLog(typeof (BaseGeotracker));

    public bool IsStarted { get; private set; }

    public abstract bool IsEnabled { get; }

    private int PositionChangedInternalSubscribers
    {
      get
      {
        return this.PositionChangedInternal == null ? 0 : ((Delegate) this.PositionChangedInternal).GetInvocationList().Length;
      }
    }

    public event TypedEventHandler<IGeotracker, Geolocation> PositionChanged
    {
      add
      {
        this.PositionChangedInternal += value;
        if (this.PositionChangedInternalSubscribers != 1)
          return;
        this.Start();
      }
      remove
      {
        this.PositionChangedInternal -= value;
        if (this.PositionChangedInternalSubscribers != 0)
          return;
        this.Stop();
      }
    }

    private event TypedEventHandler<IGeotracker, Geolocation> PositionChangedInternal
    {
      add
      {
        TypedEventHandler<IGeotracker, Geolocation> typedEventHandler1 = this.PositionChangedInternal;
        TypedEventHandler<IGeotracker, Geolocation> typedEventHandler2;
        do
        {
          typedEventHandler2 = typedEventHandler1;
          typedEventHandler1 = Interlocked.CompareExchange<TypedEventHandler<IGeotracker, Geolocation>>(ref this.PositionChangedInternal, (TypedEventHandler<IGeotracker, Geolocation>) Delegate.Combine((Delegate) typedEventHandler2, (Delegate) value), typedEventHandler2);
        }
        while (typedEventHandler1 != typedEventHandler2);
      }
      remove
      {
        TypedEventHandler<IGeotracker, Geolocation> typedEventHandler1 = this.PositionChangedInternal;
        TypedEventHandler<IGeotracker, Geolocation> typedEventHandler2;
        do
        {
          typedEventHandler2 = typedEventHandler1;
          typedEventHandler1 = Interlocked.CompareExchange<TypedEventHandler<IGeotracker, Geolocation>>(ref this.PositionChangedInternal, (TypedEventHandler<IGeotracker, Geolocation>) Delegate.Remove((Delegate) typedEventHandler2, (Delegate) value), typedEventHandler2);
        }
        while (typedEventHandler1 != typedEventHandler2);
      }
    }

    private void Start()
    {
      try
      {
        if (!this.IsStarted)
        {
          this.OnStart();
          this.IsStarted = true;
        }
        else
          this._log.Info("Start when started");
      }
      catch (Exception ex)
      {
        this._log.Error(ex);
      }
    }

    private void Stop()
    {
      try
      {
        if (this.IsStarted)
        {
          this.OnStop();
          this.IsStarted = false;
        }
        else
          this._log.Info("Stop when stoped");
      }
      catch (Exception ex)
      {
        this._log.Error(ex);
      }
    }

    protected abstract void OnStart();

    protected abstract void OnStop();

    public virtual Task<Geolocation> GetPosition() => (Task<Geolocation>) null;

    protected virtual void OnPositionChanged(Geolocation position)
    {
      // ISSUE: reference to a compiler-generated field
      TypedEventHandler<IGeotracker, Geolocation> handler = this.PositionChangedInternal;
      if (handler == null)
        return;
      Deployment.Current.Dispatcher.BeginInvoke((Action) (() => handler.Invoke((IGeotracker) this, position)));
    }
  }
}

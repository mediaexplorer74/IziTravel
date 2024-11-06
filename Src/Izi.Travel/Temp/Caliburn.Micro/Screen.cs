// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.Screen
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;
using System.ComponentModel;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// A base implementation of <see cref="T:Caliburn.Micro.IScreen" />.
  /// </summary>
  public class Screen : 
    ViewAware,
    IScreen,
    IHaveDisplayName,
    IActivate,
    IDeactivate,
    IGuardClose,
    IClose,
    INotifyPropertyChangedEx,
    INotifyPropertyChanged,
    IChild
  {
    private static readonly ILog Log = LogManager.GetLog(typeof (Screen));
    private bool isActive;
    private bool isInitialized;
    private object parent;
    private string displayName;

    /// <summary>Creates an instance of the screen.</summary>
    public Screen() => this.displayName = this.GetType().FullName;

    /// <summary>
    /// Gets or Sets the Parent <see cref="T:Caliburn.Micro.IConductor" />
    /// </summary>
    public virtual object Parent
    {
      get => this.parent;
      set
      {
        this.parent = value;
        this.NotifyOfPropertyChange(nameof (Parent));
      }
    }

    /// <summary>Gets or Sets the Display Name</summary>
    public virtual string DisplayName
    {
      get => this.displayName;
      set
      {
        this.displayName = value;
        this.NotifyOfPropertyChange(nameof (DisplayName));
      }
    }

    /// <summary>
    /// Indicates whether or not this instance is currently active.
    /// </summary>
    public bool IsActive
    {
      get => this.isActive;
      private set
      {
        this.isActive = value;
        this.NotifyOfPropertyChange(nameof (IsActive));
      }
    }

    /// <summary>
    /// Indicates whether or not this instance is currently initialized.
    /// </summary>
    public bool IsInitialized
    {
      get => this.isInitialized;
      private set
      {
        this.isInitialized = value;
        this.NotifyOfPropertyChange(nameof (IsInitialized));
      }
    }

    /// <summary>Raised after activation occurs.</summary>
    public event EventHandler<ActivationEventArgs> Activated = (param0, param1) => { };

    /// <summary>Raised before deactivation.</summary>
    public event EventHandler<DeactivationEventArgs> AttemptingDeactivation = (param0, param1) => { };

    /// <summary>Raised after deactivation.</summary>
    public event EventHandler<DeactivationEventArgs> Deactivated = (param0, param1) => { };

    void IActivate.Activate()
    {
      if (this.IsActive)
        return;
      bool flag = false;
      if (!this.IsInitialized)
      {
        this.IsInitialized = flag = true;
        this.OnInitialize();
      }
      this.IsActive = true;
      Screen.Log.Info("Activating {0}.", (object) this);
      this.OnActivate();
      this.Activated((object) this, new ActivationEventArgs()
      {
        WasInitialized = flag
      });
    }

    /// <summary>Called when initializing.</summary>
    protected virtual void OnInitialize()
    {
    }

    /// <summary>Called when activating.</summary>
    protected virtual void OnActivate()
    {
    }

    void IDeactivate.Deactivate(bool close)
    {
      if (!this.IsActive && (!this.IsInitialized || !close))
        return;
      this.AttemptingDeactivation((object) this, new DeactivationEventArgs()
      {
        WasClosed = close
      });
      this.IsActive = false;
      Screen.Log.Info("Deactivating {0}.", (object) this);
      this.OnDeactivate(close);
      this.Deactivated((object) this, new DeactivationEventArgs()
      {
        WasClosed = close
      });
      if (!close)
        return;
      this.Views.Clear();
      Screen.Log.Info("Closed {0}.", (object) this);
    }

    /// <summary>Called when deactivating.</summary>
    /// <param name="close">Inidicates whether this instance will be closed.</param>
    protected virtual void OnDeactivate(bool close)
    {
    }

    /// <summary>Called to check whether or not this instance can close.</summary>
    /// <param name="callback">The implementor calls this action with the result of the close check.</param>
    public virtual void CanClose(Action<bool> callback) => callback(true);

    /// <summary>
    /// Tries to close this instance by asking its Parent to initiate shutdown or by asking its corresponding view to close.
    /// Also provides an opportunity to pass a dialog result to it's corresponding view.
    /// </summary>
    /// <param name="dialogResult">The dialog result.</param>
    public virtual void TryClose(bool? dialogResult = null)
    {
      PlatformProvider.Current.GetViewCloseAction((object) this, this.Views.Values, dialogResult).OnUIThread();
    }
  }
}

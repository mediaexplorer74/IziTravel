// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.BootstrapperBase
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// Inherit from this class in order to customize the configuration of the framework.
  /// </summary>
  public abstract class BootstrapperBase
  {
    private readonly bool useApplication;
    private bool isInitialized;

    /// <summary>The application.</summary>
    protected Application Application { get; set; }

    /// <summary>Creates an instance of the bootstrapper.</summary>
    /// <param name="useApplication">Set this to false when hosting Caliburn.Micro inside and Office or WinForms application. The default is true.</param>
    protected BootstrapperBase(bool useApplication = true) => this.useApplication = useApplication;

    /// <summary>Initialize the framework.</summary>
    public void Initialize()
    {
      if (this.isInitialized)
        return;
      this.isInitialized = true;
      PlatformProvider.Current = (IPlatformProvider) new XamlPlatformProvider();
      if (Execute.InDesignMode)
      {
        try
        {
          this.StartDesignTime();
        }
        catch
        {
          this.isInitialized = false;
          throw;
        }
      }
      else
        this.StartRuntime();
    }

    /// <summary>
    /// Called by the bootstrapper's constructor at design time to start the framework.
    /// </summary>
    protected virtual void StartDesignTime()
    {
      AssemblySource.Instance.Clear();
      AssemblySource.Instance.AddRange(this.SelectAssemblies());
      this.Configure();
      IoC.GetInstance = new Func<Type, string, object>(this.GetInstance);
      IoC.GetAllInstances = new Func<Type, IEnumerable<object>>(this.GetAllInstances);
      IoC.BuildUp = new Action<object>(this.BuildUp);
    }

    /// <summary>
    /// Called by the bootstrapper's constructor at runtime to start the framework.
    /// </summary>
    protected virtual void StartRuntime()
    {
      EventAggregator.HandlerResultProcessing = (Action<object, object>) ((target, result) =>
      {
        if (result is Task task2)
          result = (object) new IResult[1]
          {
            (IResult) task2.AsResult()
          };
        if (!(result is IEnumerable<IResult> results2))
          return;
        object view = target is IViewAware viewAware2 ? viewAware2.GetView() : (object) null;
        CoroutineExecutionContext context = new CoroutineExecutionContext()
        {
          Target = target,
          View = view
        };
        Coroutine.BeginExecute(results2.GetEnumerator(), context);
      });
      AssemblySourceCache.Install();
      AssemblySource.Instance.AddRange(this.SelectAssemblies());
      if (this.useApplication)
      {
        this.Application = Application.Current;
        this.PrepareApplication();
      }
      this.Configure();
      IoC.GetInstance = new Func<Type, string, object>(this.GetInstance);
      IoC.GetAllInstances = new Func<Type, IEnumerable<object>>(this.GetAllInstances);
      IoC.BuildUp = new Action<object>(this.BuildUp);
    }

    /// <summary>
    /// Provides an opportunity to hook into the application object.
    /// </summary>
    protected virtual void PrepareApplication()
    {
      this.Application.Startup += new StartupEventHandler(this.OnStartup);
      this.Application.UnhandledException += new EventHandler<ApplicationUnhandledExceptionEventArgs>(this.OnUnhandledException);
      this.Application.Exit += new EventHandler(this.OnExit);
    }

    /// <summary>
    /// Override to configure the framework and setup your IoC container.
    /// </summary>
    protected virtual void Configure()
    {
    }

    /// <summary>
    /// Override to tell the framework where to find assemblies to inspect for views, etc.
    /// </summary>
    /// <returns>A list of assemblies to inspect.</returns>
    protected virtual IEnumerable<Assembly> SelectAssemblies()
    {
      return (IEnumerable<Assembly>) new Assembly[1]
      {
        this.GetType().Assembly
      };
    }

    /// <summary>
    /// Override this to provide an IoC specific implementation.
    /// </summary>
    /// <param name="service">The service to locate.</param>
    /// <param name="key">The key to locate.</param>
    /// <returns>The located service.</returns>
    protected virtual object GetInstance(Type service, string key)
    {
      return Activator.CreateInstance(service);
    }

    /// <summary>Override this to provide an IoC specific implementation</summary>
    /// <param name="service">The service to locate.</param>
    /// <returns>The located services.</returns>
    protected virtual IEnumerable<object> GetAllInstances(Type service)
    {
      return (IEnumerable<object>) new object[1]
      {
        Activator.CreateInstance(service)
      };
    }

    /// <summary>
    /// Override this to provide an IoC specific implementation.
    /// </summary>
    /// <param name="instance">The instance to perform injection on.</param>
    protected virtual void BuildUp(object instance)
    {
    }

    /// <summary>
    /// Override this to add custom behavior to execute after the application starts.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The args.</param>
    protected virtual void OnStartup(object sender, StartupEventArgs e)
    {
    }

    /// <summary>Override this to add custom behavior on exit.</summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event args.</param>
    protected virtual void OnExit(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// Override this to add custom behavior for unhandled exceptions.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The event args.</param>
    protected virtual void OnUnhandledException(
      object sender,
      ApplicationUnhandledExceptionEventArgs e)
    {
    }
  }
}

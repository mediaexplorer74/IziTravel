// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.StorageInstructionExtensions
// Assembly: Caliburn.Micro.Extensions, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: F2ADA3C9-2FAD-4D48-AC26-D2E113F06E6E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.xml

using System;
using System.Linq;
using System.Windows;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>Extension methods for configuring storage instructions.</summary>
  public static class StorageInstructionExtensions
  {
    /// <summary>Stores the data in the transient phone State.</summary>
    /// <typeparam name="T">The model type.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <returns>The builder.</returns>
    public static StorageInstructionBuilder<T> InPhoneState<T>(
      this StorageInstructionBuilder<T> builder)
    {
      return builder.Configure((Action<StorageInstruction<T>>) (x => x.StorageMechanism = (IStorageMechanism) x.Owner.Coordinator.GetStorageMechanism<PhoneStateStorageMechanism>()));
    }

    /// <summary>Stores the data in the permanent ApplicationSettings.</summary>
    /// <typeparam name="T">The model type.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <returns>The builder.</returns>
    public static StorageInstructionBuilder<T> InAppSettings<T>(
      this StorageInstructionBuilder<T> builder)
    {
      return builder.Configure((Action<StorageInstruction<T>>) (x => x.StorageMechanism = (IStorageMechanism) x.Owner.Coordinator.GetStorageMechanism<AppSettingsStorageMechanism>()));
    }

    /// <summary>Restores the data when IActivate.Activated is raised.</summary>
    /// <typeparam name="T">The model type.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <returns>The builder.</returns>
    public static StorageInstructionBuilder<T> RestoreAfterActivation<T>(
      this StorageInstructionBuilder<T> builder)
      where T : IActivate
    {
      return builder.Configure((Action<StorageInstruction<T>>) (x =>
      {
        Action<T, Func<string>, StorageMode> original = x.Restore;
        x.Restore = (Action<T, Func<string>, StorageMode>) ((instance, getKey, mode) =>
        {
          if (instance.IsActive)
          {
            original(instance, getKey, mode);
          }
          else
          {
            EventHandler<ActivationEventArgs> onActivate = (EventHandler<ActivationEventArgs>) null;
            onActivate = (EventHandler<ActivationEventArgs>) ((s, e) =>
            {
              original(instance, getKey, mode);
              instance.Activated -= onActivate;
            });
            instance.Activated += onActivate;
          }
        });
      }));
    }

    /// <summary>Restores the data after view's Loaded event is raised.</summary>
    /// <typeparam name="T">The model type.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <returns>The builder.</returns>
    public static StorageInstructionBuilder<T> RestoreAfterViewLoad<T>(
      this StorageInstructionBuilder<T> builder)
      where T : IViewAware
    {
      return builder.Configure((Action<StorageInstruction<T>>) (x =>
      {
        Action<T, Func<string>, StorageMode> original = x.Restore;
        x.Restore = (Action<T, Func<string>, StorageMode>) ((instance, getKey, mode) =>
        {
          EventHandler<ViewAttachedEventArgs> onViewAttached = (EventHandler<ViewAttachedEventArgs>) null;
          onViewAttached = (EventHandler<ViewAttachedEventArgs>) ((s, e) =>
          {
            View.ExecuteOnLoad((FrameworkElement) e.View, (RoutedEventHandler) ((s2, e2) => original(instance, getKey, mode)));
            instance.ViewAttached -= onViewAttached;
          });
          instance.ViewAttached += onViewAttached;
        });
      }));
    }

    /// <summary>
    /// Restores the data after view's LayoutUpdated event is raised.
    /// </summary>
    /// <typeparam name="T">The model type.</typeparam>
    /// <param name="builder">The builder.</param>
    /// <returns>The builder.</returns>
    public static StorageInstructionBuilder<T> RestoreAfterViewReady<T>(
      this StorageInstructionBuilder<T> builder)
      where T : IViewAware
    {
      return builder.Configure((Action<StorageInstruction<T>>) (x =>
      {
        Action<T, Func<string>, StorageMode> original = x.Restore;
        x.Restore = (Action<T, Func<string>, StorageMode>) ((instance, getKey, mode) =>
        {
          EventHandler<ViewAttachedEventArgs> onViewAttached = (EventHandler<ViewAttachedEventArgs>) null;
          onViewAttached = (EventHandler<ViewAttachedEventArgs>) ((s, e) =>
          {
            FrameworkElement fe = (FrameworkElement) e.View;
            instance.ViewAttached -= onViewAttached;
            EventHandler handler = (EventHandler) null;
            handler = (EventHandler) ((s2, e2) =>
            {
              original(instance, getKey, mode);
              fe.LayoutUpdated -= handler;
            });
            fe.LayoutUpdated += handler;
          });
          instance.ViewAttached += onViewAttached;
        });
      }));
    }

    /// <summary>Stores the index of the Conductor's ActiveItem.</summary>
    /// <typeparam name="T">The model type.</typeparam>
    /// <param name="handler">The handler.</param>
    /// <returns>The builder.</returns>
    public static StorageInstructionBuilder<T> ActiveItemIndex<T>(this StorageHandler<T> handler) where T : IParent, IHaveActiveItem, IActivate
    {
      return handler.AddInstruction().Configure((Action<StorageInstruction<T>>) (x =>
      {
        x.Key = nameof (ActiveItemIndex);
        x.Save = (Action<T, Func<string>, StorageMode>) ((instance, getKey, mode) =>
        {
          int data = instance.GetChildren().OfType<object>().ToList<object>().IndexOf(instance.ActiveItem);
          x.StorageMechanism.Store(getKey(), (object) data);
        });
        x.Restore = (Action<T, Func<string>, StorageMode>) ((instance, getKey, mode) =>
        {
          string key = getKey();
          object obj3;
          if (!x.StorageMechanism.TryGet(key, out obj3))
            return;
          x.StorageMechanism.Delete(key);
          int int32 = Convert.ToInt32(obj3);
          object obj4 = instance.GetChildren().OfType<object>().ElementAtOrDefault<object>(int32);
          instance.ActiveItem = obj4;
        });
      }));
    }
  }
}

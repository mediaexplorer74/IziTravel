// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.Conductor`1
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// An implementation of <see cref="T:Caliburn.Micro.IConductor" /> that holds on to and activates only one item at a time.
  /// </summary>
  public class Conductor<T> : ConductorBaseWithActiveItem<T> where T : class
  {
    /// <summary>Activates the specified item.</summary>
    /// <param name="item">The item to activate.</param>
    public override void ActivateItem(T item)
    {
      if ((object) item != null && item.Equals((object) this.ActiveItem))
      {
        if (!this.IsActive)
          return;
        ScreenExtensions.TryActivate((object) item);
        this.OnActivationProcessed(item, true);
      }
      else
        this.CloseStrategy.Execute((IEnumerable<T>) new T[1]
        {
          this.ActiveItem
        }, (Action<bool, IEnumerable<T>>) ((canClose, items) =>
        {
          if (canClose)
            this.ChangeActiveItem(item, true);
          else
            this.OnActivationProcessed(item, false);
        }));
    }

    /// <summary>Deactivates the specified item.</summary>
    /// <param name="item">The item to close.</param>
    /// <param name="close">Indicates whether or not to close the item after deactivating it.</param>
    public override void DeactivateItem(T item, bool close)
    {
      if ((object) item == null || !item.Equals((object) this.ActiveItem))
        return;
      this.CloseStrategy.Execute((IEnumerable<T>) new T[1]
      {
        this.ActiveItem
      }, (Action<bool, IEnumerable<T>>) ((canClose, items) =>
      {
        if (!canClose)
          return;
        this.ChangeActiveItem(default (T), close);
      }));
    }

    /// <summary>Called to check whether or not this instance can close.</summary>
    /// <param name="callback">The implementor calls this action with the result of the close check.</param>
    public override void CanClose(Action<bool> callback)
    {
      this.CloseStrategy.Execute((IEnumerable<T>) new T[1]
      {
        this.ActiveItem
      }, (Action<bool, IEnumerable<T>>) ((canClose, items) => callback(canClose)));
    }

    /// <summary>Called when activating.</summary>
    protected override void OnActivate() => ScreenExtensions.TryActivate((object) this.ActiveItem);

    /// <summary>Called when deactivating.</summary>
    /// <param name="close">Inidicates whether this instance will be closed.</param>
    protected override void OnDeactivate(bool close)
    {
      ScreenExtensions.TryDeactivate((object) this.ActiveItem, close);
    }

    /// <summary>Gets the children.</summary>
    /// <returns>The collection of children.</returns>
    public override IEnumerable<T> GetChildren()
    {
      return (IEnumerable<T>) new T[1]{ this.ActiveItem };
    }

    /// <summary>
    /// An implementation of <see cref="T:Caliburn.Micro.IConductor" /> that holds on many items.
    /// </summary>
    /// <summary>
    /// An implementation of <see cref="T:Caliburn.Micro.IConductor" /> that holds on many items.
    /// </summary>
    public class Collection
    {
      /// <summary>
      /// An implementation of <see cref="T:Caliburn.Micro.IConductor" /> that holds on to many items wich are all activated.
      /// </summary>
      public class AllActive : ConductorBase<T>
      {
        private readonly BindableCollection<T> items = new BindableCollection<T>();
        private readonly bool openPublicItems;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Caliburn.Micro.Conductor`1.Collection.AllActive" /> class.
        /// </summary>
        /// <param name="openPublicItems">if set to <c>true</c> opens public items that are properties of this class.</param>
        public AllActive(bool openPublicItems)
          : this()
        {
          this.openPublicItems = openPublicItems;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Caliburn.Micro.Conductor`1.Collection.AllActive" /> class.
        /// </summary>
        public AllActive()
        {
          this.items.CollectionChanged += (NotifyCollectionChangedEventHandler) ((s, e) =>
          {
            switch (e.Action)
            {
              case NotifyCollectionChangedAction.Add:
                e.NewItems.OfType<IChild>().Apply<IChild>((Action<IChild>) (x => x.Parent = (object) this));
                break;
              case NotifyCollectionChangedAction.Remove:
                e.OldItems.OfType<IChild>().Apply<IChild>((Action<IChild>) (x => x.Parent = (object) null));
                break;
              case NotifyCollectionChangedAction.Replace:
                e.NewItems.OfType<IChild>().Apply<IChild>((Action<IChild>) (x => x.Parent = (object) this));
                e.OldItems.OfType<IChild>().Apply<IChild>((Action<IChild>) (x => x.Parent = (object) null));
                break;
              case NotifyCollectionChangedAction.Reset:
                this.items.OfType<IChild>().Apply<IChild>((Action<IChild>) (x => x.Parent = (object) this));
                break;
            }
          });
        }

        /// <summary>Gets the items that are currently being conducted.</summary>
        public IObservableCollection<T> Items => (IObservableCollection<T>) this.items;

        /// <summary>Called when activating.</summary>
        protected override void OnActivate()
        {
          this.items.OfType<IActivate>().Apply<IActivate>((Action<IActivate>) (x => x.Activate()));
        }

        /// <summary>Called when deactivating.</summary>
        /// <param name="close">Inidicates whether this instance will be closed.</param>
        protected override void OnDeactivate(bool close)
        {
          this.items.OfType<IDeactivate>().Apply<IDeactivate>((Action<IDeactivate>) (x => x.Deactivate(close)));
          if (!close)
            return;
          this.items.Clear();
        }

        /// <summary>Called to check whether or not this instance can close.</summary>
        /// <param name="callback">The implementor calls this action with the result of the close check.</param>
        public override void CanClose(Action<bool> callback)
        {
          this.CloseStrategy.Execute((IEnumerable<T>) this.items.ToList<T>(), (Action<bool, IEnumerable<T>>) ((canClose, closable) =>
          {
            if (!canClose && closable.Any<T>())
            {
              closable.OfType<IDeactivate>().Apply<IDeactivate>((Action<IDeactivate>) (x => x.Deactivate(true)));
              this.items.RemoveRange(closable);
            }
            callback(canClose);
          }));
        }

        /// <summary>Called when initializing.</summary>
        protected override void OnInitialize()
        {
          if (!this.openPublicItems)
            return;
          this.GetType().GetProperties().Where<PropertyInfo>((Func<PropertyInfo, bool>) (x => x.Name != "Parent" && typeof (T).IsAssignableFrom(x.PropertyType))).Select<PropertyInfo, object>((Func<PropertyInfo, object>) (x => x.GetValue((object) this, (object[]) null))).Cast<T>().Apply<T>(new Action<T>(((ConductorBase<T>) this).ActivateItem));
        }

        /// <summary>Activates the specified item.</summary>
        /// <param name="item">The item to activate.</param>
        public override void ActivateItem(T item)
        {
          if ((object) item == null)
            return;
          item = this.EnsureItem(item);
          if (this.IsActive)
            ScreenExtensions.TryActivate((object) item);
          this.OnActivationProcessed(item, true);
        }

        /// <summary>Deactivates the specified item.</summary>
        /// <param name="item">The item to close.</param>
        /// <param name="close">Indicates whether or not to close the item after deactivating it.</param>
        public override void DeactivateItem(T item, bool close)
        {
          if ((object) item == null)
            return;
          if (close)
            this.CloseStrategy.Execute((IEnumerable<T>) new T[1]
            {
              item
            }, (Action<bool, IEnumerable<T>>) ((canClose, closable) =>
            {
              if (!canClose)
                return;
              this.CloseItemCore(item);
            }));
          else
            ScreenExtensions.TryDeactivate((object) item, false);
        }

        /// <summary>Gets the children.</summary>
        /// <returns>The collection of children.</returns>
        public override IEnumerable<T> GetChildren() => (IEnumerable<T>) this.items;

        private void CloseItemCore(T item)
        {
          ScreenExtensions.TryDeactivate((object) item, true);
          this.items.Remove(item);
        }

        /// <summary>Ensures that an item is ready to be activated.</summary>
        /// <param name="newItem"></param>
        /// <returns>The item to be activated.</returns>
        protected override T EnsureItem(T newItem)
        {
          int index = this.items.IndexOf(newItem);
          if (index == -1)
            this.items.Add(newItem);
          else
            newItem = this.items[index];
          return base.EnsureItem(newItem);
        }
      }

      /// <summary>
      /// An implementation of <see cref="T:Caliburn.Micro.IConductor" /> that holds on many items but only activates one at a time.
      /// </summary>
      public class OneActive : ConductorBaseWithActiveItem<T>
      {
        private readonly BindableCollection<T> items = new BindableCollection<T>();

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Caliburn.Micro.Conductor`1.Collection.OneActive" /> class.
        /// </summary>
        public OneActive()
        {
          this.items.CollectionChanged += (NotifyCollectionChangedEventHandler) ((s, e) =>
          {
            switch (e.Action)
            {
              case NotifyCollectionChangedAction.Add:
                e.NewItems.OfType<IChild>().Apply<IChild>((Action<IChild>) (x => x.Parent = (object) this));
                break;
              case NotifyCollectionChangedAction.Remove:
                e.OldItems.OfType<IChild>().Apply<IChild>((Action<IChild>) (x => x.Parent = (object) null));
                break;
              case NotifyCollectionChangedAction.Replace:
                e.NewItems.OfType<IChild>().Apply<IChild>((Action<IChild>) (x => x.Parent = (object) this));
                e.OldItems.OfType<IChild>().Apply<IChild>((Action<IChild>) (x => x.Parent = (object) null));
                break;
              case NotifyCollectionChangedAction.Reset:
                this.items.OfType<IChild>().Apply<IChild>((Action<IChild>) (x => x.Parent = (object) this));
                break;
            }
          });
        }

        /// <summary>Gets the items that are currently being conducted.</summary>
        public IObservableCollection<T> Items => (IObservableCollection<T>) this.items;

        /// <summary>Gets the children.</summary>
        /// <returns>The collection of children.</returns>
        public override IEnumerable<T> GetChildren() => (IEnumerable<T>) this.items;

        /// <summary>Activates the specified item.</summary>
        /// <param name="item">The item to activate.</param>
        public override void ActivateItem(T item)
        {
          if ((object) item != null && item.Equals((object) this.ActiveItem))
          {
            if (!this.IsActive)
              return;
            ScreenExtensions.TryActivate((object) item);
            this.OnActivationProcessed(item, true);
          }
          else
            this.ChangeActiveItem(item, false);
        }

        /// <summary>Deactivates the specified item.</summary>
        /// <param name="item">The item to close.</param>
        /// <param name="close">Indicates whether or not to close the item after deactivating it.</param>
        public override void DeactivateItem(T item, bool close)
        {
          if ((object) item == null)
            return;
          if (!close)
            ScreenExtensions.TryDeactivate((object) item, false);
          else
            this.CloseStrategy.Execute((IEnumerable<T>) new T[1]
            {
              item
            }, (Action<bool, IEnumerable<T>>) ((canClose, closable) =>
            {
              if (!canClose)
                return;
              this.CloseItemCore(item);
            }));
        }

        private void CloseItemCore(T item)
        {
          if (item.Equals((object) this.ActiveItem))
            this.ChangeActiveItem(this.DetermineNextItemToActivate((IList<T>) this.items, this.items.IndexOf(item)), true);
          else
            ScreenExtensions.TryDeactivate((object) item, true);
          this.items.Remove(item);
        }

        /// <summary>
        /// Determines the next item to activate based on the last active index.
        /// </summary>
        /// <param name="list">The list of possible active items.</param>
        /// <param name="lastIndex">The index of the last active item.</param>
        /// <returns>The next item to activate.</returns>
        /// <remarks>Called after an active item is closed.</remarks>
        protected virtual T DetermineNextItemToActivate(IList<T> list, int lastIndex)
        {
          int index = lastIndex - 1;
          if (index == -1 && list.Count > 1)
            return list[1];
          return index > -1 && index < list.Count - 1 ? list[index] : default (T);
        }

        /// <summary>Called to check whether or not this instance can close.</summary>
        /// <param name="callback">The implementor calls this action with the result of the close check.</param>
        public override void CanClose(Action<bool> callback)
        {
          this.CloseStrategy.Execute((IEnumerable<T>) this.items.ToList<T>(), (Action<bool, IEnumerable<T>>) ((canClose, closable) =>
          {
            if (!canClose && closable.Any<T>())
            {
              if (closable.Contains<T>(this.ActiveItem))
              {
                List<T> list1 = this.items.ToList<T>();
                T newItem = this.ActiveItem;
                do
                {
                  T obj = newItem;
                  newItem = this.DetermineNextItemToActivate((IList<T>) list1, list1.IndexOf(obj));
                  list1.Remove(obj);
                }
                while (closable.Contains<T>(newItem));
                T activeItem = this.ActiveItem;
                this.ChangeActiveItem(newItem, true);
                this.items.Remove(activeItem);
                List<T> list2 = closable.ToList<T>();
                list2.Remove(activeItem);
                closable = (IEnumerable<T>) list2;
              }
              closable.OfType<IDeactivate>().Apply<IDeactivate>((Action<IDeactivate>) (x => x.Deactivate(true)));
              this.items.RemoveRange(closable);
            }
            callback(canClose);
          }));
        }

        /// <summary>Called when activating.</summary>
        protected override void OnActivate()
        {
          ScreenExtensions.TryActivate((object) this.ActiveItem);
        }

        /// <summary>Called when deactivating.</summary>
        /// <param name="close">Inidicates whether this instance will be closed.</param>
        protected override void OnDeactivate(bool close)
        {
          if (close)
          {
            this.items.OfType<IDeactivate>().Apply<IDeactivate>((Action<IDeactivate>) (x => x.Deactivate(true)));
            this.items.Clear();
          }
          else
            ScreenExtensions.TryDeactivate((object) this.ActiveItem, false);
        }

        /// <summary>Ensures that an item is ready to be activated.</summary>
        /// <param name="newItem"></param>
        /// <returns>The item to be activated.</returns>
        protected override T EnsureItem(T newItem)
        {
          if ((object) newItem == null)
          {
            newItem = this.DetermineNextItemToActivate((IList<T>) this.items, (object) this.ActiveItem != null ? this.items.IndexOf(this.ActiveItem) : 0);
          }
          else
          {
            int index = this.items.IndexOf(newItem);
            if (index == -1)
              this.items.Add(newItem);
            else
              newItem = this.items[index];
          }
          return base.EnsureItem(newItem);
        }
      }
    }
  }
}

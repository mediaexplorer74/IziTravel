// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.IObservableCollection`1
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>Represents a collection that is observable.</summary>
  /// <typeparam name="T">The type of elements contained in the collection.</typeparam>
  public interface IObservableCollection<T> : 
    IList<T>,
    ICollection<T>,
    IEnumerable<T>,
    IEnumerable,
    INotifyPropertyChangedEx,
    INotifyPropertyChanged,
    INotifyCollectionChanged
  {
    /// <summary>Adds the range.</summary>
    /// <param name="items">The items.</param>
    void AddRange(IEnumerable<T> items);

    /// <summary>Removes the range.</summary>
    /// <param name="items">The items.</param>
    void RemoveRange(IEnumerable<T> items);
  }
}

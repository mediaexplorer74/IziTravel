// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Toolkit.Controls.Maps.QueryExtensions
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Microsoft.Phone.Maps.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Shell.Toolkit.Controls.Maps
{
  public static class QueryExtensions
  {
    public static Task<IList<MapLocation>> GetMapLocationsAsync(this GeocodeQuery geocodeQuery)
    {
      return geocodeQuery.QueryAsync<IList<MapLocation>>();
    }

    public static Task<IList<MapLocation>> GetMapLocationsAsync(
      this ReverseGeocodeQuery reverseGeocodeQuery)
    {
      return reverseGeocodeQuery.QueryAsync<IList<MapLocation>>();
    }

    public static Task<Route> GetRouteAsync(this RouteQuery routeQuery)
    {
      return routeQuery.QueryAsync<Route>();
    }

    private static Task<TResult> QueryAsync<TResult>(this Query<TResult> query)
    {
      EventHandler<QueryCompletedEventArgs<TResult>> queryCompletedHandler = (EventHandler<QueryCompletedEventArgs<TResult>>) null;
      TaskCompletionSource<TResult> taskCompletionSource = QueryExtensions.CreateSource<TResult>((object) query);
      queryCompletedHandler = (EventHandler<QueryCompletedEventArgs<TResult>>) ((sender, e) => QueryExtensions.TransferCompletion<TResult>(taskCompletionSource, (AsyncCompletedEventArgs) e, (Func<TResult>) (() => e.Result), (Action) (() => query.QueryCompleted -= queryCompletedHandler)));
      query.QueryCompleted += queryCompletedHandler;
      try
      {
        query.QueryAsync();
      }
      catch
      {
        query.QueryCompleted -= queryCompletedHandler;
        throw;
      }
      return taskCompletionSource.Task;
    }

    private static TaskCompletionSource<TResult> CreateSource<TResult>(object state)
    {
      return new TaskCompletionSource<TResult>(state, TaskCreationOptions.None);
    }

    private static void TransferCompletion<TResult>(
      TaskCompletionSource<TResult> tcs,
      AsyncCompletedEventArgs e,
      Func<TResult> getResult,
      Action unregisterHandler)
    {
      if (e.UserState != tcs.Task.AsyncState)
        return;
      if (unregisterHandler != null)
        unregisterHandler();
      if (e.Cancelled)
        tcs.TrySetCanceled();
      else if (e.Error != null)
        tcs.TrySetException(e.Error);
      else
        tcs.TrySetResult(getResult());
    }
  }
}

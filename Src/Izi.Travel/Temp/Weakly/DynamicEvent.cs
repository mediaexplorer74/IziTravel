// Decompiled with JetBrains decompiler
// Type: Weakly.DynamicEvent
// Assembly: Weakly, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e9c206b2200b970
// MVID: 59987104-5B29-48EC-89B5-2E7347C0D910
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Weakly.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Weakly.xml

using System;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace Weakly
{
  /// <summary>
  /// Helper methods to register or unregister an event handler using reflection.
  /// </summary>
  public static class DynamicEvent
  {
    private static readonly SimpleCache<MethodInfo, Action<object, Delegate>> Cache = new SimpleCache<MethodInfo, Action<object, Delegate>>();

    /// <summary>
    /// Gets the method that adds an event handler to an event source.
    /// </summary>
    /// <param name="eventInfo">The event information.</param>
    /// <returns>The method used to add an event handler delegate to the event source.</returns>
    public static Action<object, Delegate> GetAddMethod(EventInfo eventInfo)
    {
      return DynamicEvent.GetEventMethod(eventInfo.AddMethod);
    }

    /// <summary>
    /// Gets the method that removes an event handler from an event source.
    /// </summary>
    /// <param name="eventInfo">The event information.</param>
    /// <returns>The method used to remove an event handler delegate from the event source.</returns>
    public static Action<object, Delegate> GetRemoveMethod(EventInfo eventInfo)
    {
      return DynamicEvent.GetEventMethod(eventInfo.RemoveMethod);
    }

    private static Action<object, Delegate> GetEventMethod(MethodInfo method)
    {
      Action<object, Delegate> valueOrDefault = DynamicEvent.Cache.GetValueOrDefault(method);
      if (valueOrDefault != null)
        return valueOrDefault;
      Action<object, Delegate> eventMethod = DynamicEvent.CompileEventMethod(method);
      DynamicEvent.Cache.AddOrUpdate(method, eventMethod);
      return eventMethod;
    }

    private static Action<object, Delegate> CompileEventMethod(MethodInfo method)
    {
      ParameterExpression parameterExpression1 = Expression.Parameter(typeof (object), "target");
      ParameterExpression parameterExpression2 = Expression.Parameter(typeof (Delegate), "handler");
      Expression instance = (Expression) null;
      if (!method.IsStatic)
        instance = (Expression) Expression.Convert((Expression) parameterExpression1, method.DeclaringType);
      UnaryExpression unaryExpression = Expression.Convert((Expression) parameterExpression2, method.GetParameters()[0].ParameterType);
      return Expression.Lambda<Action<object, Delegate>>((Expression) Expression.Call(instance, method, (Expression) unaryExpression), parameterExpression1, parameterExpression2).Compile();
    }
  }
}

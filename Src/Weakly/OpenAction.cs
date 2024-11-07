// Decompiled with JetBrains decompiler
// Type: Weakly.OpenAction
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
  /// <summary>Helper to create open delegate actions.</summary>
  public static class OpenAction
  {
    private static readonly SimpleCache<MethodInfo, Delegate> Cache = new SimpleCache<MethodInfo, Delegate>();

    /// <summary>Create an open delegate from the specified method.</summary>
    /// <param name="method">The method.</param>
    /// <returns>The open delegate.</returns>
    public static Action<object> From(MethodInfo method)
    {
      Action<object> valueOrDefault = OpenAction.Cache.GetValueOrDefault<Action<object>>(method);
      if (valueOrDefault != null)
        return valueOrDefault;
      Action<object> action = OpenAction.CompileAction(method);
      OpenAction.Cache.AddOrUpdate(method, (Delegate) action);
      return action;
    }

    private static Action<object> CompileAction(MethodInfo method)
    {
      return ((Expression<Action<object>>) (instance => Expression.Call((Expression) Expression.Convert(instance, method.DeclaringType), method))).Compile();
    }

    /// <summary>Create an open delegate from the specified method.</summary>
    /// <typeparam name="T">The type of the parameter of the method that this delegate encapsulates.</typeparam>
    /// <param name="method">The method.</param>
    /// <returns>The open delegate.</returns>
    public static Action<object, T> From<T>(MethodInfo method)
    {
      Action<object, T> valueOrDefault = OpenAction.Cache.GetValueOrDefault<Action<object, T>>(method);
      if (valueOrDefault != null)
        return valueOrDefault;
      Action<object, T> action = OpenAction.CompileAction<T>(method);
      OpenAction.Cache.AddOrUpdate(method, (Delegate) action);
      return action;
    }

    private static Action<object, T> CompileAction<T>(MethodInfo method)
    {
      ParameterExpression parameterExpression1;
      ParameterExpression parameterExpression2;
      return Expression.Lambda<Action<object, T>>((Expression) Expression.Call((Expression) Expression.Convert((Expression) parameterExpression1, method.DeclaringType), method, (Expression) parameterExpression2), parameterExpression1, parameterExpression2).Compile();
    }

    /// <summary>Create an open delegate from the specified method.</summary>
    /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
    /// <param name="method">The method.</param>
    /// <returns>The open delegate.</returns>
    public static Action<object, T1, T2> From<T1, T2>(MethodInfo method)
    {
      Action<object, T1, T2> valueOrDefault = OpenAction.Cache.GetValueOrDefault<Action<object, T1, T2>>(method);
      if (valueOrDefault != null)
        return valueOrDefault;
      Action<object, T1, T2> action = OpenAction.CompileAction<T1, T2>(method);
      OpenAction.Cache.AddOrUpdate(method, (Delegate) action);
      return action;
    }

    private static Action<object, T1, T2> CompileAction<T1, T2>(MethodInfo method)
    {
      return ((Expression<Action<object, T1, T2>>) ((instance, arg1, arg2) => Expression.Call((Expression) Expression.Convert(instance, method.DeclaringType), method, arg1, arg2))).Compile();
    }

    /// <summary>Create an open delegate from the specified method.</summary>
    /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
    /// <param name="method">The method.</param>
    /// <returns>The open delegate.</returns>
    public static Action<object, T1, T2, T3> From<T1, T2, T3>(MethodInfo method)
    {
      Action<object, T1, T2, T3> valueOrDefault = OpenAction.Cache.GetValueOrDefault<Action<object, T1, T2, T3>>(method);
      if (valueOrDefault != null)
        return valueOrDefault;
      Action<object, T1, T2, T3> action = OpenAction.CompileAction<T1, T2, T3>(method);
      OpenAction.Cache.AddOrUpdate(method, (Delegate) action);
      return action;
    }

    private static Action<object, T1, T2, T3> CompileAction<T1, T2, T3>(MethodInfo method)
    {
      return ((Expression<Action<object, T1, T2, T3>>) ((instance, arg1, arg2, arg3) => Expression.Call((Expression) Expression.Convert(instance, method.DeclaringType), method, arg1, arg2, arg3))).Compile();
    }

    /// <summary>Create an open delegate from the specified method.</summary>
    /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
    /// <param name="method">The method.</param>
    /// <returns>The open delegate.</returns>
    public static Action<object, T1, T2, T3, T4> From<T1, T2, T3, T4>(MethodInfo method)
    {
      Action<object, T1, T2, T3, T4> valueOrDefault = OpenAction.Cache.GetValueOrDefault<Action<object, T1, T2, T3, T4>>(method);
      if (valueOrDefault != null)
        return valueOrDefault;
      Action<object, T1, T2, T3, T4> action = OpenAction.CompileAction<T1, T2, T3, T4>(method);
      OpenAction.Cache.AddOrUpdate(method, (Delegate) action);
      return action;
    }

    private static Action<object, T1, T2, T3, T4> CompileAction<T1, T2, T3, T4>(MethodInfo method)
    {
      ParameterExpression parameterExpression1;
      ParameterExpression parameterExpression2;
      ParameterExpression parameterExpression3;
      ParameterExpression parameterExpression4;
      ParameterExpression parameterExpression5;
      return Expression.Lambda<Action<object, T1, T2, T3, T4>>((Expression) Expression.Call((Expression) Expression.Convert((Expression) parameterExpression1, method.DeclaringType), method, (Expression) parameterExpression2, (Expression) parameterExpression3, (Expression) parameterExpression4, (Expression) parameterExpression5), parameterExpression1, parameterExpression2, parameterExpression3, parameterExpression4, parameterExpression5).Compile();
    }

    /// <summary>Create an open delegate from the specified method.</summary>
    /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates.</typeparam>
    /// <param name="method">The method.</param>
    /// <returns>The open delegate.</returns>
    public static Action<object, T1, T2, T3, T4, T5> From<T1, T2, T3, T4, T5>(MethodInfo method)
    {
      Action<object, T1, T2, T3, T4, T5> valueOrDefault = OpenAction.Cache.GetValueOrDefault<Action<object, T1, T2, T3, T4, T5>>(method);
      if (valueOrDefault != null)
        return valueOrDefault;
      Action<object, T1, T2, T3, T4, T5> action = OpenAction.CompileAction<T1, T2, T3, T4, T5>(method);
      OpenAction.Cache.AddOrUpdate(method, (Delegate) action);
      return action;
    }

    private static Action<object, T1, T2, T3, T4, T5> CompileAction<T1, T2, T3, T4, T5>(
      MethodInfo method)
    {
      ParameterExpression parameterExpression1;
      ParameterExpression parameterExpression2;
      ParameterExpression parameterExpression3;
      ParameterExpression parameterExpression4;
      ParameterExpression parameterExpression5;
      ParameterExpression parameterExpression6;
      return Expression.Lambda<Action<object, T1, T2, T3, T4, T5>>((Expression) Expression.Call((Expression) Expression.Convert((Expression) parameterExpression1, method.DeclaringType), method, (Expression) parameterExpression2, (Expression) parameterExpression3, (Expression) parameterExpression4, (Expression) parameterExpression5, (Expression) parameterExpression6), parameterExpression1, parameterExpression2, parameterExpression3, parameterExpression4, parameterExpression5, parameterExpression6).Compile();
    }
  }
}

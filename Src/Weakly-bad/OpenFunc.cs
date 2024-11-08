// Decompiled with JetBrains decompiler
// Type: Weakly.OpenFunc
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
  /// <summary>Helper to create open delegate functions.</summary>
  public static class OpenFunc
  {
    private static readonly SimpleCache<MethodInfo, Delegate> Cache = new SimpleCache<MethodInfo, Delegate>();

    /// <summary>Create an open delegate from the specified method.</summary>
    /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
    /// <param name="method">The method.</param>
    /// <returns>The open delegate.</returns>
    public static Func<object, TResult> From<TResult>(MethodInfo method)
    {
      Func<object, TResult> valueOrDefault 
                = OpenFunc.Cache.GetValueOrDefault<Func<object, TResult>>(method);
      if (valueOrDefault != null)
        return valueOrDefault;
      Func<object, TResult> func = OpenFunc.CompileFunc<TResult>(method);
      OpenFunc.Cache.AddOrUpdate(method, (Delegate) func);
      return func;
    }

    private static Func<object, TResult> CompileFunc<TResult>(MethodInfo method)
    {
      return ((Expression<Func<object, TResult>>) (instance => Expression.Call((Expression) Expression.Convert
          ((Expression)instance, method.DeclaringType), method))).Compile();
    }

    /// <summary>Create an open delegate from the specified method.</summary>
    /// <typeparam name="T">The type of the parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
    /// <param name="method">The method.</param>
    /// <returns>The open delegate.</returns>
    public static Func<object, T, TResult> From<T, TResult>(MethodInfo method)
    {
      Func<object, T, TResult> valueOrDefault = OpenFunc.Cache.GetValueOrDefault<Func<object, T, TResult>>(method);
      if (valueOrDefault != null)
        return valueOrDefault;
      Func<object, T, TResult> func = OpenFunc.CompileFunc<T, TResult>(method);
      OpenFunc.Cache.AddOrUpdate(method, (Delegate) func);
      return func;
    }

    private static Func<object, T, TResult> CompileFunc<T, TResult>(MethodInfo method)
    {
      ParameterExpression parameterExpression1 = default;
      ParameterExpression parameterExpression2 = default;
      return Expression.Lambda<Func<object, T, TResult>>(
          (Expression) Expression.Call((Expression) Expression.Convert((Expression) parameterExpression1, method.DeclaringType), method, (Expression) parameterExpression2), parameterExpression1, parameterExpression2).Compile();
    }

    /// <summary>Create an open delegate from the specified method.</summary>
    /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
    /// <param name="method">The method.</param>
    /// <returns>The open delegate.</returns>
    public static Func<object, T1, T2, TResult> From<T1, T2, TResult>(MethodInfo method)
    {
      Func<object, T1, T2, TResult> valueOrDefault 
                = OpenFunc.Cache.GetValueOrDefault<Func<object, T1, T2, TResult>>(method);
      if (valueOrDefault != null)
        return valueOrDefault;
      Func<object, T1, T2, TResult> func = OpenFunc.CompileFunc<T1, T2, TResult>(method);
      OpenFunc.Cache.AddOrUpdate(method, (Delegate) func);
      return func;
    }

    private static Func<object, T1, T2, TResult> CompileFunc<T1, T2, TResult>(MethodInfo method)
    {
      return ((Expression<Func<object, T1, T2, TResult>>) ((instance, arg1, arg2) 
                => Expression.Call((Expression) Expression.Convert((Expression)instance, method.DeclaringType), 
          method, (Expression)arg1, (Expression)arg2))).Compile();
    }

    /// <summary>Create an open delegate from the specified method.</summary>
    /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
    /// <param name="method">The method.</param>
    /// <returns>The open delegate</returns>
    public static Func<object, T1, T2, T3, TResult> From<T1, T2, T3, TResult>(MethodInfo method)
    {
      Func<object, T1, T2, T3, TResult> valueOrDefault = OpenFunc.Cache.GetValueOrDefault<Func<object, T1, T2, T3, TResult>>(method);
      if (valueOrDefault != null)
        return valueOrDefault;
      Func<object, T1, T2, T3, TResult> func = OpenFunc.CompileFunc<T1, T2, T3, TResult>(method);
      OpenFunc.Cache.AddOrUpdate(method, (Delegate) func);
      return func;
    }

    private static Func<object, T1, T2, T3, TResult> CompileFunc<T1, T2, T3, TResult>(
      MethodInfo method)
    {
      return ((Expression<Func<object, T1, T2, T3, TResult>>) ((instance, arg1, arg2, arg3) 
                => Expression.Call((Expression) Expression.Convert((Expression)instance, 
                method.DeclaringType), 
                method, (Expression)arg1, (Expression)arg2, (Expression)arg3))).Compile();
    }

    /// <summary>Create an open delegate from the specified method.</summary>
    /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
    /// <param name="method">The method.</param>
    /// <returns>The open delegate</returns>
    public static Func<object, T1, T2, T3, T4, TResult> From<T1, T2, T3, T4, TResult>(
      MethodInfo method)
    {
      Func<object, T1, T2, T3, T4, TResult> valueOrDefault = OpenFunc.Cache.GetValueOrDefault<Func<object, T1, T2, T3, T4, TResult>>(method);
      if (valueOrDefault != null)
        return valueOrDefault;
      Func<object, T1, T2, T3, T4, TResult> func = OpenFunc.CompileFunc<T1, T2, T3, T4, TResult>(method);
      OpenFunc.Cache.AddOrUpdate(method, (Delegate) func);
      return func;
    }

    private static Func<object, T1, T2, T3, T4, TResult> CompileFunc<T1, T2, T3, T4, TResult>(
      MethodInfo method)
    {
      ParameterExpression parameterExpression1 = default;
      ParameterExpression parameterExpression2 = default;
      ParameterExpression parameterExpression3 = default;
      ParameterExpression parameterExpression4 = default;
      ParameterExpression parameterExpression5 = default;
      return Expression.Lambda<Func<object, T1, T2, T3, T4, TResult>>((Expression) Expression.Call((Expression) Expression.Convert((Expression) parameterExpression1, method.DeclaringType), method, (Expression) parameterExpression2, (Expression) parameterExpression3, (Expression) parameterExpression4, (Expression) parameterExpression5), parameterExpression1, parameterExpression2, parameterExpression3, parameterExpression4, parameterExpression5).Compile();
    }

    /// <summary>Create an open delegate from the specified method.</summary>
    /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="T5">The type of the fifth parameter of the method that this delegate encapsulates.</typeparam>
    /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
    /// <param name="method">The method.</param>
    /// <returns>The open delegate</returns>
    public static Func<object, T1, T2, T3, T4, T5, TResult> From<T1, T2, T3, T4, T5, TResult>(
      MethodInfo method)
    {
      Func<object, T1, T2, T3, T4, T5, TResult> valueOrDefault = OpenFunc.Cache.GetValueOrDefault<Func<object, T1, T2, T3, T4, T5, TResult>>(method);
      if (valueOrDefault != null)
        return valueOrDefault;
      Func<object, T1, T2, T3, T4, T5, TResult> func = OpenFunc.CompileFunc<T1, T2, T3, T4, T5, TResult>(method);
      OpenFunc.Cache.AddOrUpdate(method, (Delegate) func);
      return func;
    }

    private static Func<object, T1, T2, T3, T4, T5, TResult> CompileFunc<T1, T2, T3, T4, T5, TResult>(
      MethodInfo method)
    {
      ParameterExpression parameterExpression1 = default;
      ParameterExpression parameterExpression2 = default;
      ParameterExpression parameterExpression3 = default;
      ParameterExpression parameterExpression4 = default;
      ParameterExpression parameterExpression5 = default;
      ParameterExpression parameterExpression6 = default;
      return Expression.Lambda<Func<object, T1, T2, T3, T4, T5, TResult>>((Expression) Expression.Call((Expression) Expression.Convert((Expression) parameterExpression1, method.DeclaringType), method, (Expression) parameterExpression2, (Expression) parameterExpression3, (Expression) parameterExpression4, (Expression) parameterExpression5, (Expression) parameterExpression6), parameterExpression1, parameterExpression2, parameterExpression3, parameterExpression4, parameterExpression5, parameterExpression6).Compile();
    }
  }
}

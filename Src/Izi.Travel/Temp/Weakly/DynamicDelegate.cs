// Decompiled with JetBrains decompiler
// Type: Weakly.DynamicDelegate
// Assembly: Weakly, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e9c206b2200b970
// MVID: 59987104-5B29-48EC-89B5-2E7347C0D910
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Weakly.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Weakly.xml

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace Weakly
{
  /// <summary>Helper to create dynamic delegate functions.</summary>
  public static class DynamicDelegate
  {
    private static readonly SimpleCache<MethodInfo, Func<object, object[], object>> Cache = new SimpleCache<MethodInfo, Func<object, object[], object>>();

    /// <summary>Create a dynamic delegate from the specified method.</summary>
    /// <param name="method">The method.</param>
    /// <returns>The dynamic delegate.</returns>
    public static Func<object, object[], object> From(MethodInfo method)
    {
      Func<object, object[], object> valueOrDefault = DynamicDelegate.Cache.GetValueOrDefault(method);
      if (valueOrDefault != null)
        return valueOrDefault;
      Func<object, object[], object> func = DynamicDelegate.CompileFunction(method);
      DynamicDelegate.Cache.AddOrUpdate(method, func);
      return func;
    }

    private static Func<object, object[], object> CompileFunction(MethodInfo method)
    {
      ParameterInfo[] parameters1 = method.GetParameters();
      ParameterExpression instance1 = Expression.Parameter(typeof (object), "instance");
      ParameterExpression parameters2 = Expression.Parameter(typeof (object[]), "parameters");
      List<Expression> expressionList = new List<Expression>();
      expressionList.Add((Expression) DynamicDelegate.CheckParametersLength((Expression) parameters2, parameters1.Length));
      Expression instance2 = DynamicDelegate.ConvertInstance((Expression) instance1, (MethodBase) method);
      Expression[] expressionArray = DynamicDelegate.ConvertParameters(parameters2, parameters1);
      MethodCallExpression methodCallExpression = Expression.Call(instance2, method, expressionArray);
      if (method.ReturnType != typeof (void))
      {
        UnaryExpression unaryExpression = Expression.Convert((Expression) methodCallExpression, typeof (object));
        expressionList.Add((Expression) unaryExpression);
      }
      else
      {
        expressionList.Add((Expression) methodCallExpression);
        expressionList.Add((Expression) Expression.Constant((object) null, typeof (object)));
      }
      return ((Expression<Func<object, object[], object>>) ((parameterExpression1, parameterExpression2) => Expression.Block((IEnumerable<Expression>) expressionList))).Compile();
    }

    private static Expression ConvertInstance(Expression instance, MethodBase method)
    {
      Expression expression = (Expression) null;
      if (!method.IsStatic)
        expression = (Expression) Expression.Convert(instance, method.DeclaringType);
      return expression;
    }

    private static Expression[] ConvertParameters(
      ParameterExpression parameters,
      ParameterInfo[] parameterInfos)
    {
      Expression[] expressionArray = new Expression[parameterInfos.Length];
      for (int index = 0; index < parameterInfos.Length; ++index)
      {
        BinaryExpression binaryExpression = Expression.ArrayIndex((Expression) parameters, (Expression) Expression.Constant((object) index, typeof (int)));
        expressionArray[index] = (Expression) Expression.Convert((Expression) binaryExpression, parameterInfos[index].ParameterType);
      }
      return expressionArray;
    }

    private static ConditionalExpression CheckParametersLength(Expression parameters, int length)
    {
      return Expression.IfThen((Expression) Expression.NotEqual((Expression) Expression.ArrayLength(parameters), (Expression) Expression.Constant((object) length, typeof (int))), (Expression) Expression.Throw((Expression) Expression.Constant((object) new TargetParameterCountException())));
    }
  }
}

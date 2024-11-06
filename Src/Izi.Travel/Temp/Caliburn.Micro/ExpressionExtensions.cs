// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.ExpressionExtensions
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// Extension for <see cref="T:System.Linq.Expressions.Expression" />.
  /// </summary>
  public static class ExpressionExtensions
  {
    /// <summary>
    /// Converts an expression into a <see cref="T:System.Reflection.MemberInfo" />.
    /// </summary>
    /// <param name="expression">The expression to convert.</param>
    /// <returns>The member info.</returns>
    public static MemberInfo GetMemberInfo(this Expression expression)
    {
      LambdaExpression lambdaExpression = (LambdaExpression) expression;
      return (!(lambdaExpression.Body is UnaryExpression) ? (MemberExpression) lambdaExpression.Body : (MemberExpression) ((UnaryExpression) lambdaExpression.Body).Operand).Member;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: Weakly.ExpressionHelper
// Assembly: Weakly, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e9c206b2200b970
// MVID: 59987104-5B29-48EC-89B5-2E7347C0D910
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Weakly.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Weakly.xml

using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace Weakly
{
  /// <summary>
  /// Extension for <see cref="T:System.Linq.Expressions.Expression" />.
  /// </summary>
  public static class ExpressionHelper
  {
    /// <summary>
    /// Converts an expression into a <see cref="T:System.Reflection.MemberInfo" />.
    /// </summary>
    /// <param name="expression">The expression to convert.</param>
    /// <returns>The member info.</returns>
    public static MemberInfo GetMemberInfo(this Expression expression)
    {
      LambdaExpression lambdaExpression = (LambdaExpression) expression;
      return (!(lambdaExpression.Body is UnaryExpression body) ? (MemberExpression) lambdaExpression.Body : (MemberExpression) body.Operand).Member;
    }
  }
}

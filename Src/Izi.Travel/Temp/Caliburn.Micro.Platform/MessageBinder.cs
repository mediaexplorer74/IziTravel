// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.MessageBinder
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// A service that is capable of properly binding values to a method's parameters and creating instances of <see cref="T:Caliburn.Micro.IResult" />.
  /// </summary>
  public static class MessageBinder
  {
    /// <summary>
    /// The special parameter values recognized by the message binder along with their resolvers.
    /// </summary>
    public static readonly Dictionary<string, Func<ActionExecutionContext, object>> SpecialValues = new Dictionary<string, Func<ActionExecutionContext, object>>()
    {
      {
        "$eventargs",
        (Func<ActionExecutionContext, object>) (c => c.EventArgs)
      },
      {
        "$datacontext",
        (Func<ActionExecutionContext, object>) (c => c.Source.DataContext)
      },
      {
        "$source",
        (Func<ActionExecutionContext, object>) (c => (object) c.Source)
      },
      {
        "$executioncontext",
        (Func<ActionExecutionContext, object>) (c => (object) c)
      },
      {
        "$view",
        (Func<ActionExecutionContext, object>) (c => (object) c.View)
      }
    };
    /// <summary>
    /// Custom converters used by the framework registered by destination type for which they will be selected.
    /// The converter is passed the existing value to convert and a "context" object.
    /// </summary>
    public static readonly Dictionary<Type, Func<object, object, object>> CustomConverters = new Dictionary<Type, Func<object, object, object>>()
    {
      {
        typeof (DateTime),
        (Func<object, object, object>) ((value, context) =>
        {
          DateTime result;
          DateTime.TryParse(value.ToString(), out result);
          return (object) result;
        })
      }
    };
    /// <summary>
    /// Transforms the textual parameter into the actual parameter.
    /// </summary>
    public static Func<string, Type, ActionExecutionContext, object> EvaluateParameter = (Func<string, Type, ActionExecutionContext, object>) ((text, parameterType, context) =>
    {
      string lower = text.ToLower(CultureInfo.InvariantCulture);
      Func<ActionExecutionContext, object> func;
      return !MessageBinder.SpecialValues.TryGetValue(lower, out func) ? (object) text : func(context);
    });

    /// <summary>
    /// Determines the parameters that a method should be invoked with.
    /// </summary>
    /// <param name="context">The action execution context.</param>
    /// <param name="requiredParameters">The parameters required to complete the invocation.</param>
    /// <returns>The actual parameter values.</returns>
    public static object[] DetermineParameters(
      ActionExecutionContext context,
      ParameterInfo[] requiredParameters)
    {
      object[] array = context.Message.Parameters.OfType<Parameter>().Select<Parameter, object>((Func<Parameter, object>) (x => x.Value)).ToArray<object>();
      object[] parameters = new object[requiredParameters.Length];
      for (int index = 0; index < requiredParameters.Length; ++index)
      {
        Type parameterType = requiredParameters[index].ParameterType;
        object providedValue = array[index];
        parameters[index] = !(providedValue is string str) ? MessageBinder.CoerceValue(parameterType, providedValue, (object) context) : MessageBinder.CoerceValue(parameterType, MessageBinder.EvaluateParameter(str, parameterType, context), (object) context);
      }
      return parameters;
    }

    /// <summary>Coerces the provided value to the destination type.</summary>
    /// <param name="destinationType">The destination type.</param>
    /// <param name="providedValue">The provided value.</param>
    /// <param name="context">An optional context value which can be used during conversion.</param>
    /// <returns>The coerced value.</returns>
    public static object CoerceValue(Type destinationType, object providedValue, object context)
    {
      if (providedValue == null)
        return MessageBinder.GetDefaultValue(destinationType);
      Type type = providedValue.GetType();
      if (destinationType.IsAssignableFrom(type))
        return providedValue;
      if (MessageBinder.CustomConverters.ContainsKey(destinationType))
        return MessageBinder.CustomConverters[destinationType](providedValue, context);
      try
      {
        TypeConverter converter1 = TypeDescriptor.GetConverter(destinationType);
        if (converter1.CanConvertFrom(type))
          return converter1.ConvertFrom(providedValue);
        TypeConverter converter2 = TypeDescriptor.GetConverter(type);
        if (converter2.CanConvertTo(destinationType))
          return converter2.ConvertTo(providedValue, destinationType);
        if (destinationType.IsEnum)
          return providedValue is string str ? Enum.Parse(destinationType, str, true) : Enum.ToObject(destinationType, providedValue);
        if (typeof (Guid).IsAssignableFrom(destinationType))
        {
          if (providedValue is string g)
            return (object) new Guid(g);
        }
      }
      catch
      {
        return MessageBinder.GetDefaultValue(destinationType);
      }
      try
      {
        return Convert.ChangeType(providedValue, destinationType, (IFormatProvider) CultureInfo.CurrentCulture);
      }
      catch
      {
        return MessageBinder.GetDefaultValue(destinationType);
      }
    }

    /// <summary>Gets the default value for a type.</summary>
    /// <param name="type">The type.</param>
    /// <returns>The default value.</returns>
    public static object GetDefaultValue(Type type)
    {
      return !type.IsClass && !type.IsInterface ? Activator.CreateInstance(type) : (object) null;
    }
  }
}

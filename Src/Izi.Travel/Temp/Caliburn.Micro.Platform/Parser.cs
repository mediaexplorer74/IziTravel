// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.Parser
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// Parses text into a fully functional set of <see cref="T:System.Windows.Interactivity.TriggerBase" /> instances with <see cref="T:Caliburn.Micro.ActionMessage" />.
  /// </summary>
  public static class Parser
  {
    private static readonly Regex LongFormatRegularExpression = new Regex("^[\\s]*\\[[^\\]]*\\][\\s]*=[\\s]*\\[[^\\]]*\\][\\s]*$");
    private static readonly ILog Log = LogManager.GetLog(typeof (Parser));
    /// <summary>The function used to generate a trigger.</summary>
    /// <remarks>The parameters passed to the method are the the target of the trigger and string representing the trigger.</remarks>
    public static Func<DependencyObject, string, System.Windows.Interactivity.TriggerBase> CreateTrigger = (Func<DependencyObject, string, System.Windows.Interactivity.TriggerBase>) ((target, triggerText) =>
    {
      if (triggerText == null)
        return ConventionManager.GetElementConvention(target.GetType()).CreateTrigger();
      string str = triggerText.Replace("[", string.Empty).Replace("]", string.Empty).Replace("Event", string.Empty).Trim();
      return (System.Windows.Interactivity.TriggerBase) new System.Windows.Interactivity.EventTrigger()
      {
        EventName = str
      };
    });
    /// <summary>
    /// Function used to parse a string identified as a message.
    /// </summary>
    public static Func<DependencyObject, string, System.Windows.Interactivity.TriggerAction> InterpretMessageText = (Func<DependencyObject, string, System.Windows.Interactivity.TriggerAction>) ((target, text) => (System.Windows.Interactivity.TriggerAction) new ActionMessage()
    {
      MethodName = Regex.Replace(text, "^Action", string.Empty).Trim()
    });
    /// <summary>
    /// Function used to parse a string identified as a message parameter.
    /// </summary>
    public static Func<DependencyObject, string, Parameter> CreateParameter = (Func<DependencyObject, string, Parameter>) ((target, parameterText) =>
    {
      Parameter actualParameter = new Parameter();
      if (parameterText.StartsWith("'") && parameterText.EndsWith("'"))
        actualParameter.Value = (object) parameterText.Substring(1, parameterText.Length - 2);
      else if (MessageBinder.SpecialValues.ContainsKey(parameterText.ToLower()) || char.IsNumber(parameterText[0]))
        actualParameter.Value = (object) parameterText;
      else if (target is FrameworkElement)
      {
        FrameworkElement fe = (FrameworkElement) target;
        string[] nameAndBindingMode = ((IEnumerable<string>) parameterText.Split(':')).Select<string, string>((Func<string, string>) (x => x.Trim())).ToArray<string>();
        int index = nameAndBindingMode[0].IndexOf('.');
        View.ExecuteOnLoad(fe, (RoutedEventHandler) ((param0, param1) => Parser.BindParameter(fe, actualParameter, index == -1 ? nameAndBindingMode[0] : nameAndBindingMode[0].Substring(0, index), index == -1 ? (string) null : nameAndBindingMode[0].Substring(index + 1), nameAndBindingMode.Length == 2 ? (BindingMode) Enum.Parse(typeof (BindingMode), nameAndBindingMode[1], true) : BindingMode.OneWay)));
      }
      return actualParameter;
    });

    /// <summary>Parses the specified message text.</summary>
    /// <param name="target">The target.</param>
    /// <param name="text">The message text.</param>
    /// <returns>The triggers parsed from the text.</returns>
    public static IEnumerable<System.Windows.Interactivity.TriggerBase> Parse(
      DependencyObject target,
      string text)
    {
      if (string.IsNullOrEmpty(text))
        return (IEnumerable<System.Windows.Interactivity.TriggerBase>) new System.Windows.Interactivity.TriggerBase[0];
      List<System.Windows.Interactivity.TriggerBase> triggerBaseList = new List<System.Windows.Interactivity.TriggerBase>();
      foreach (string str in StringSplitter.Split(text, ';'))
      {
        string[] strArray;
        if (!Parser.LongFormatRegularExpression.IsMatch(str))
          strArray = new string[2]{ null, str };
        else
          strArray = StringSplitter.Split(str, '=');
        string[] source = strArray;
        string messageText = ((IEnumerable<string>) source).Last<string>().Replace("[", string.Empty).Replace("]", string.Empty).Trim();
        System.Windows.Interactivity.TriggerBase triggerBase = Parser.CreateTrigger(target, source.Length == 1 ? (string) null : source[0]);
        System.Windows.Interactivity.TriggerAction message = Parser.CreateMessage(target, messageText);
        triggerBase.Actions.Add(message);
        triggerBaseList.Add(triggerBase);
      }
      return (IEnumerable<System.Windows.Interactivity.TriggerBase>) triggerBaseList;
    }

    /// <summary>
    /// Creates an instance of <see cref="T:Caliburn.Micro.ActionMessage" /> by parsing out the textual dsl.
    /// </summary>
    /// <param name="target">The target of the message.</param>
    /// <param name="messageText">The textual message dsl.</param>
    /// <returns>The created message.</returns>
    public static System.Windows.Interactivity.TriggerAction CreateMessage(
      DependencyObject target,
      string messageText)
    {
      int length = messageText.IndexOf('(');
      if (length < 0)
        length = messageText.Length;
      int num = messageText.LastIndexOf(')');
      if (num < 0)
        num = messageText.Length;
      string str = messageText.Substring(0, length).Trim();
      System.Windows.Interactivity.TriggerAction message = Parser.InterpretMessageText(target, str);
      if (message is IHaveParameters haveParameters && num - length > 1)
      {
        foreach (string splitParameter in StringSplitter.SplitParameters(messageText.Substring(length + 1, num - length - 1)))
          haveParameters.Parameters.Add(Parser.CreateParameter(target, splitParameter.Trim()));
      }
      return message;
    }

    /// <summary>
    /// Creates a binding on a <see cref="T:Caliburn.Micro.Parameter" />.
    /// </summary>
    /// <param name="target">The target to which the message is applied.</param>
    /// <param name="parameter">The parameter object.</param>
    /// <param name="elementName">The name of the element to bind to.</param>
    /// <param name="path">The path of the element to bind to.</param>
    /// <param name="bindingMode">The binding mode to use.</param>
    public static void BindParameter(
      FrameworkElement target,
      Parameter parameter,
      string elementName,
      string path,
      BindingMode bindingMode)
    {
      FrameworkElement element = elementName == "$this" ? target : BindingScope.GetNamedElements((DependencyObject) target).FindName(elementName);
      if (element == null)
        return;
      if (string.IsNullOrEmpty(path))
        path = ConventionManager.GetElementConvention(element.GetType()).ParameterProperty;
      Binding binding = new Binding(path)
      {
        Source = (object) element,
        Mode = bindingMode
      };
      BindingExpression expression = (BindingExpression) BindingOperations.SetBinding((DependencyObject) parameter, Parameter.ValueProperty, (BindingBase) binding);
      FieldInfo field = element.GetType().GetField(path + "Property", BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Static);
      if (field == null)
        return;
      ConventionManager.ApplySilverlightTriggers((DependencyObject) element, (DependencyProperty) field.GetValue((object) null), (Func<FrameworkElement, BindingExpression>) (x => expression), (PropertyInfo) null, (Binding) null);
    }
  }
}

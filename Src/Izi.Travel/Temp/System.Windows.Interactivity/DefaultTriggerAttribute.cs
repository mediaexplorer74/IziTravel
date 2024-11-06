// Decompiled with JetBrains decompiler
// Type: System.Windows.Interactivity.DefaultTriggerAttribute
// Assembly: System.Windows.Interactivity, Version=3.9.5.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: AF3F364D-9511-45E0-99E0-CAF6B3A2782E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.xml

using System.Collections;
using System.Globalization;

#nullable disable
namespace System.Windows.Interactivity
{
  /// <summary>
  /// Provides design tools information about what <see cref="T:System.Windows.Interactivity.TriggerBase" /> to instantiate for a given action or command.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
  [CLSCompliant(false)]
  public sealed class DefaultTriggerAttribute : Attribute
  {
    private Type targetType;
    private Type triggerType;
    private object[] parameters;

    /// <summary>
    /// Gets the type that this DefaultTriggerAttribute applies to.
    /// </summary>
    /// <value>The type this DefaultTriggerAttribute applies to.</value>
    public Type TargetType => this.targetType;

    /// <summary>
    /// Gets the type of the <see cref="T:System.Windows.Interactivity.TriggerBase" /> to instantiate.
    /// </summary>
    /// <value>The type of the <see cref="T:System.Windows.Interactivity.TriggerBase" /> to instantiate.</value>
    public Type TriggerType => this.triggerType;

    /// <summary>
    /// Gets the parameters to pass to the <see cref="T:System.Windows.Interactivity.TriggerBase" /> constructor.
    /// </summary>
    /// <value>The parameters to pass to the <see cref="T:System.Windows.Interactivity.TriggerBase" /> constructor.</value>
    public IEnumerable Parameters => (IEnumerable) this.parameters;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Windows.Interactivity.DefaultTriggerAttribute" /> class.
    /// </summary>
    /// <param name="targetType">The type this attribute applies to.</param>
    /// <param name="triggerType">The type of <see cref="T:System.Windows.Interactivity.TriggerBase" /> to instantiate.</param>
    /// <param name="parameters">A single argument for the specified <see cref="T:System.Windows.Interactivity.TriggerBase" />.</param>
    /// <exception cref="T:System.ArgumentException"><c cref="F:System.Windows.Interactivity.DefaultTriggerAttribute.triggerType" /> is not derived from TriggerBase.</exception>
    /// <remarks>This constructor is useful if the specifed <see cref="T:System.Windows.Interactivity.TriggerBase" /> has a single argument. The
    /// resulting code will be CLS compliant.</remarks>
    public DefaultTriggerAttribute(Type targetType, Type triggerType, object parameter)
      : this(targetType, triggerType, new object[1]
      {
        parameter
      })
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Windows.Interactivity.DefaultTriggerAttribute" /> class.
    /// </summary>
    /// <param name="targetType">The type this attribute applies to.</param>
    /// <param name="triggerType">The type of <see cref="T:System.Windows.Interactivity.TriggerBase" /> to instantiate.</param>
    /// <param name="parameters">The constructor arguments for the specified <see cref="T:System.Windows.Interactivity.TriggerBase" />.</param>
    /// <exception cref="T:System.ArgumentException"><c cref="F:System.Windows.Interactivity.DefaultTriggerAttribute.triggerType" /> is not derived from TriggerBase.</exception>
    public DefaultTriggerAttribute(Type targetType, Type triggerType, params object[] parameters)
    {
      if (!typeof (TriggerBase).IsAssignableFrom(triggerType))
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, ExceptionStringTable.DefaultTriggerAttributeInvalidTriggerTypeSpecifiedExceptionMessage, (object) triggerType.Name));
      this.targetType = targetType;
      this.triggerType = triggerType;
      this.parameters = parameters;
    }

    /// <summary>Instantiates this instance.</summary>
    /// <returns>The <see cref="T:System.Windows.Interactivity.TriggerBase" /> specified by the DefaultTriggerAttribute.</returns>
    public TriggerBase Instantiate()
    {
      object obj = (object) null;
      try
      {
        obj = Activator.CreateInstance(this.TriggerType, this.parameters);
      }
      catch
      {
      }
      return (TriggerBase) obj;
    }
  }
}

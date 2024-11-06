// Decompiled with JetBrains decompiler
// Type: System.Windows.Interactivity.InvokeCommandAction
// Assembly: System.Windows.Interactivity, Version=3.9.5.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: AF3F364D-9511-45E0-99E0-CAF6B3A2782E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.xml

using System.Reflection;
using System.Windows.Input;

#nullable disable
namespace System.Windows.Interactivity
{
  /// <summary>Executes a specified ICommand when invoked.</summary>
  public sealed class InvokeCommandAction : TriggerAction<DependencyObject>
  {
    private string commandName;
    public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(nameof (Command), typeof (ICommand), typeof (InvokeCommandAction), (PropertyMetadata) null);
    public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(nameof (CommandParameter), typeof (object), typeof (InvokeCommandAction), (PropertyMetadata) null);

    /// <summary>
    /// Gets or sets the name of the command this action should invoke.
    /// </summary>
    /// <value>The name of the command this action should invoke.</value>
    /// <remarks>This property will be superseded by the Command property if both are set.</remarks>
    public string CommandName
    {
      get => this.commandName;
      set
      {
        if (!(this.CommandName != value))
          return;
        this.commandName = value;
      }
    }

    /// <summary>
    /// Gets or sets the command this action should invoke. This is a dependency property.
    /// </summary>
    /// <value>The command to execute.</value>
    /// <remarks>This property will take precedence over the CommandName property if both are set.</remarks>
    public ICommand Command
    {
      get => (ICommand) this.GetValue(InvokeCommandAction.CommandProperty);
      set => this.SetValue(InvokeCommandAction.CommandProperty, (object) value);
    }

    /// <summary>
    /// Gets or sets the command parameter. This is a dependency property.
    /// </summary>
    /// <value>The command parameter.</value>
    /// <remarks>This is the value passed to ICommand.CanExecute and ICommand.Execute.</remarks>
    public object CommandParameter
    {
      get => this.GetValue(InvokeCommandAction.CommandParameterProperty);
      set => this.SetValue(InvokeCommandAction.CommandParameterProperty, value);
    }

    /// <summary>Invokes the action.</summary>
    /// <param name="parameter">The parameter to the action. If the action does not require a parameter, the parameter may be set to a null reference.</param>
    protected override void Invoke(object parameter)
    {
      if (this.AssociatedObject == null)
        return;
      ICommand command = this.ResolveCommand();
      if (command == null || !command.CanExecute(this.CommandParameter))
        return;
      command.Execute(this.CommandParameter);
    }

    private ICommand ResolveCommand()
    {
      ICommand command = (ICommand) null;
      if (this.Command != null)
        command = this.Command;
      else if (this.AssociatedObject != null)
      {
        foreach (PropertyInfo property in this.AssociatedObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
        {
          if (typeof (ICommand).IsAssignableFrom(property.PropertyType) && string.Equals(property.Name, this.CommandName, StringComparison.Ordinal))
            command = (ICommand) property.GetValue((object) this.AssociatedObject, (object[]) null);
        }
      }
      return command;
    }
  }
}

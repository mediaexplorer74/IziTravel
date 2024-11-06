// Decompiled with JetBrains decompiler
// Type: System.Windows.Interactivity.ExceptionStringTable
// Assembly: System.Windows.Interactivity, Version=3.9.5.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: AF3F364D-9511-45E0-99E0-CAF6B3A2782E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.xml

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace System.Windows.Interactivity
{
  /// <summary>
  ///   A strongly-typed resource class, for looking up localized strings, etc.
  /// </summary>
  [DebuggerNonUserCode]
  [CompilerGenerated]
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  internal class ExceptionStringTable
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal ExceptionStringTable()
    {
    }

    /// <summary>
    ///   Returns the cached ResourceManager instance used by this class.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals((object) ExceptionStringTable.resourceMan, (object) null))
          ExceptionStringTable.resourceMan = new ResourceManager("System.Windows.Interactivity.ExceptionStringTable", typeof (ExceptionStringTable).Assembly);
        return ExceptionStringTable.resourceMan;
      }
    }

    /// <summary>
    ///   Overrides the current thread's CurrentUICulture property for all
    ///   resource lookups using this strongly typed resource class.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => ExceptionStringTable.resourceCulture;
      set => ExceptionStringTable.resourceCulture = value;
    }

    /// <summary>
    ///   Looks up a localized string similar to Cannot set the same BehaviorCollection on multiple objects..
    /// </summary>
    internal static string CannotHostBehaviorCollectionMultipleTimesExceptionMessage
    {
      get
      {
        return ExceptionStringTable.ResourceManager.GetString(nameof (CannotHostBehaviorCollectionMultipleTimesExceptionMessage), ExceptionStringTable.resourceCulture);
      }
    }

    /// <summary>
    ///   Looks up a localized string similar to An instance of a Behavior cannot be attached to more than one object at a time..
    /// </summary>
    internal static string CannotHostBehaviorMultipleTimesExceptionMessage
    {
      get
      {
        return ExceptionStringTable.ResourceManager.GetString(nameof (CannotHostBehaviorMultipleTimesExceptionMessage), ExceptionStringTable.resourceCulture);
      }
    }

    /// <summary>
    ///   Looks up a localized string similar to Cannot host an instance of a TriggerAction in multiple TriggerCollections simultaneously. Remove it from one TriggerCollection before adding it to another..
    /// </summary>
    internal static string CannotHostTriggerActionMultipleTimesExceptionMessage
    {
      get
      {
        return ExceptionStringTable.ResourceManager.GetString(nameof (CannotHostTriggerActionMultipleTimesExceptionMessage), ExceptionStringTable.resourceCulture);
      }
    }

    /// <summary>
    ///   Looks up a localized string similar to Cannot set the same TriggerCollection on multiple objects..
    /// </summary>
    internal static string CannotHostTriggerCollectionMultipleTimesExceptionMessage
    {
      get
      {
        return ExceptionStringTable.ResourceManager.GetString(nameof (CannotHostTriggerCollectionMultipleTimesExceptionMessage), ExceptionStringTable.resourceCulture);
      }
    }

    /// <summary>
    ///   Looks up a localized string similar to An instance of a trigger cannot be attached to more than one object at a time..
    /// </summary>
    internal static string CannotHostTriggerMultipleTimesExceptionMessage
    {
      get
      {
        return ExceptionStringTable.ResourceManager.GetString(nameof (CannotHostTriggerMultipleTimesExceptionMessage), ExceptionStringTable.resourceCulture);
      }
    }

    /// <summary>
    ///   Looks up a localized string similar to The command "{0}" does not exist or is not publicly exposed on {1}..
    /// </summary>
    internal static string CommandDoesNotExistOnBehaviorWarningMessage
    {
      get
      {
        return ExceptionStringTable.ResourceManager.GetString(nameof (CommandDoesNotExistOnBehaviorWarningMessage), ExceptionStringTable.resourceCulture);
      }
    }

    /// <summary>
    ///   Looks up a localized string similar to "{0}" is not a valid type for the TriggerType parameter. Make sure "{0}" derives from TriggerBase..
    /// </summary>
    internal static string DefaultTriggerAttributeInvalidTriggerTypeSpecifiedExceptionMessage
    {
      get
      {
        return ExceptionStringTable.ResourceManager.GetString(nameof (DefaultTriggerAttributeInvalidTriggerTypeSpecifiedExceptionMessage), ExceptionStringTable.resourceCulture);
      }
    }

    /// <summary>
    ///   Looks up a localized string similar to Cannot add the same instance of "{0}" to a "{1}" more than once..
    /// </summary>
    internal static string DuplicateItemInCollectionExceptionMessage
    {
      get
      {
        return ExceptionStringTable.ResourceManager.GetString(nameof (DuplicateItemInCollectionExceptionMessage), ExceptionStringTable.resourceCulture);
      }
    }

    /// <summary>
    ///   Looks up a localized string similar to The event "{0}" on type "{1}" has an incompatible signature. Make sure the event is public and satisfies the EventHandler delegate..
    /// </summary>
    internal static string EventTriggerBaseInvalidEventExceptionMessage
    {
      get
      {
        return ExceptionStringTable.ResourceManager.GetString(nameof (EventTriggerBaseInvalidEventExceptionMessage), ExceptionStringTable.resourceCulture);
      }
    }

    /// <summary>
    ///   Looks up a localized string similar to Cannot find an event named "{0}" on type "{1}"..
    /// </summary>
    internal static string EventTriggerCannotFindEventNameExceptionMessage
    {
      get
      {
        return ExceptionStringTable.ResourceManager.GetString(nameof (EventTriggerCannotFindEventNameExceptionMessage), ExceptionStringTable.resourceCulture);
      }
    }

    /// <summary>
    ///   Looks up a localized string similar to An object of type "{0}" cannot have a {3} property of type "{1}". Instances of type "{0}" can have only a {3} property of type "{2}"..
    /// </summary>
    internal static string RetargetedTypeConstraintViolatedExceptionMessage
    {
      get
      {
        return ExceptionStringTable.ResourceManager.GetString(nameof (RetargetedTypeConstraintViolatedExceptionMessage), ExceptionStringTable.resourceCulture);
      }
    }

    /// <summary>
    ///   Looks up a localized string similar to Cannot attach type "{0}" to type "{1}". Instances of type "{0}" can only be attached to objects of type "{2}"..
    /// </summary>
    internal static string TypeConstraintViolatedExceptionMessage
    {
      get
      {
        return ExceptionStringTable.ResourceManager.GetString(nameof (TypeConstraintViolatedExceptionMessage), ExceptionStringTable.resourceCulture);
      }
    }

    /// <summary>
    ///   Looks up a localized string similar to Unable to resolve TargetName "{0}"..
    /// </summary>
    internal static string UnableToResolveTargetNameWarningMessage
    {
      get
      {
        return ExceptionStringTable.ResourceManager.GetString(nameof (UnableToResolveTargetNameWarningMessage), ExceptionStringTable.resourceCulture);
      }
    }
  }
}

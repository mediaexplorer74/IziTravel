// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.Extras.ModuleConventions
// Assembly: Caliburn.Micro.Extras, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75D6380B-EA35-437B-8CE3-40FC8C25A394
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extras.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extras.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;

#nullable disable
namespace Caliburn.Micro.Extras
{
  /// <summary>Conventions installer for ExternalModules support.</summary>
  public static class ModuleConventions
  {
    private static readonly List<string> InitializedAssemblies = new List<string>();

    /// <summary>
    /// Installs the conventions needed for <see cref="T:Caliburn.Micro.Extras.IModuleBootstrapper" />.
    /// </summary>
    public static void Install()
    {
      Func<Type, bool, Type> locateTypeForViewType = ViewModelLocator.LocateTypeForViewType;
      ViewModelLocator.LocateTypeForViewType = (Func<Type, bool, Type>) ((viewType, searchForInterface) =>
      {
        ModuleConventions.InitializeAssembly(viewType.Assembly);
        return locateTypeForViewType(viewType, searchForInterface);
      });
      Func<Type, DependencyObject, object, Type> locateTypeForModelType = ViewLocator.LocateTypeForModelType;
      ViewLocator.LocateTypeForModelType = (Func<Type, DependencyObject, object, Type>) ((modelType, displayLocation, context) =>
      {
        ModuleConventions.InitializeAssembly(modelType.Assembly);
        return locateTypeForModelType(modelType, displayLocation, context);
      });
    }

    /// <summary>Initializes the assembly.</summary>
    /// <param name="assembly">The assembly.</param>
    public static void InitializeAssembly(Assembly assembly)
    {
      if (ModuleConventions.InitializedAssemblies.Contains(assembly.FullName))
        return;
      ModuleConventions.InitializedAssemblies.Add(assembly.FullName);
      if (!AssemblySource.Instance.Contains(assembly))
        AssemblySource.Instance.Add(assembly);
      foreach (Type type in ((IEnumerable<Type>) assembly.GetExportedTypes()).Where<Type>((Func<Type, bool>) (type => !type.IsAbstract && !type.IsInterface && typeof (IModuleBootstrapper).IsAssignableFrom(type))))
      {
        IModuleBootstrapper instance = (IModuleBootstrapper) Activator.CreateInstance(type);
        IoC.BuildUp((object) instance);
        instance.Initialize();
      }
    }
  }
}

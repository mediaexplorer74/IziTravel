// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.StorageMode
// Assembly: Caliburn.Micro.Extensions, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: F2ADA3C9-2FAD-4D48-AC26-D2E113F06E6E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.xml

using System;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>The mode used to save/restore data.</summary>
  [Flags]
  public enum StorageMode
  {
    /// <summary>Automatic Determine the Mode</summary>
    Automatic = 0,
    /// <summary>Use Temporary storage.</summary>
    Temporary = 2,
    /// <summary>Use Permenent storage.</summary>
    Permanent = 4,
    /// <summary>Use any storage mechanism available.</summary>
    Any = Permanent | Temporary, // 0x00000006
  }
}

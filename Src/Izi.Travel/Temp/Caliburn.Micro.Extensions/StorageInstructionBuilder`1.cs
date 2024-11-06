// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.StorageInstructionBuilder`1
// Assembly: Caliburn.Micro.Extensions, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: F2ADA3C9-2FAD-4D48-AC26-D2E113F06E6E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.xml

using System;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// Used to create a fluent interface for building up a storage instruction.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class StorageInstructionBuilder<T>
  {
    private readonly StorageInstruction<T> storageInstruction;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Caliburn.Micro.StorageInstructionBuilder`1" /> class.
    /// </summary>
    /// <param name="storageInstruction">The storage instruction.</param>
    public StorageInstructionBuilder(StorageInstruction<T> storageInstruction)
    {
      this.storageInstruction = storageInstruction;
    }

    /// <summary>Configures the instruction with the specified behavior.</summary>
    /// <param name="configure">The configuration callback.</param>
    /// <returns>Itself</returns>
    public StorageInstructionBuilder<T> Configure(Action<StorageInstruction<T>> configure)
    {
      configure(this.storageInstruction);
      return this;
    }
  }
}

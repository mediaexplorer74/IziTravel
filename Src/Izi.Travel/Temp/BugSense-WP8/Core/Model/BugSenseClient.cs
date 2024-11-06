// Decompiled with JetBrains decompiler
// Type: BugSense.Core.Model.BugSenseClient
// Assembly: BugSense-WP8, Version=3.6.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A6D84D1-19B1-4B1A-84A1-50F2A1BD7889
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\BugSense-WP8.dll

using System;
using System.Runtime.Serialization;

#nullable disable
namespace BugSense.Core.Model
{
  [DataContract]
  public class BugSenseClient : IEquatable<BugSenseClient>
  {
    private Guid Id { get; set; }

    [DataMember(Name = "version", EmitDefaultValue = false)]
    public string Version { get; set; }

    [DataMember(Name = "name", EmitDefaultValue = false)]
    public string Name { get; set; }

    [DataMember(Name = "flavor", EmitDefaultValue = false)]
    public string Flavor { get; set; }

    public BugSenseClient()
    {
      this.Id = Guid.NewGuid();
      this.Version = "bugsense-version-" + BugSenseProperties.BugSenseVersion;
      this.Name = BugSenseProperties.BugSenseName;
      this.Flavor = BugSenseProperties.Flavor;
    }

    public bool Equals(BugSenseClient other) => this.Id.Equals(other.Id);

    public override int GetHashCode() => this.Id.GetHashCode();
  }
}

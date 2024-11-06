// Decompiled with JetBrains decompiler
// Type: BugSense.Core.Model.BugSenseInternalRequest
// Assembly: BugSense-WP8, Version=3.6.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A6D84D1-19B1-4B1A-84A1-50F2A1BD7889
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\BugSense-WP8.dll

using System;
using System.Runtime.Serialization;

#nullable disable
namespace BugSense.Core.Model
{
  [DataContract]
  public class BugSenseInternalRequest : IEquatable<BugSenseInternalRequest>
  {
    private Guid Id { get; set; }

    [DataMember(Name = "comment", EmitDefaultValue = false)]
    public string Comment { get; set; }

    [DataMember(Name = "user_id", EmitDefaultValue = false)]
    public string UserIdentifier { get; set; }

    public BugSenseInternalRequest()
    {
      this.Id = Guid.NewGuid();
      this.UserIdentifier = string.IsNullOrWhiteSpace(BugSenseProperties.UserIdentifier) ? string.Empty : BugSenseProperties.UserIdentifier;
      this.Comment = string.Empty;
    }

    public bool Equals(BugSenseInternalRequest other) => this.Id.Equals(other.Id);

    public override int GetHashCode() => this.Id.GetHashCode();
  }
}

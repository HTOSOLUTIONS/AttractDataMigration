using System;
using System.Collections.Generic;

namespace TargetDDContext.Models;

public partial class FamilyPath
{
    public string? Parentpath { get; set; }

    public string PktableOwner { get; set; } = null!;

    public string PktableName { get; set; } = null!;

    public string FktableOwner { get; set; } = null!;

    public string FktableName { get; set; } = null!;

    public string FkName { get; set; } = null!;

    public int? Children { get; set; }

    public string Fullpath { get; set; } = null!;

    public virtual ForeignKey ForeignKey { get; set; } = null!;

    public virtual Table ChildTable { get; set; } = null!;

    public virtual Table ParentTable { get; set; } = null!;

}

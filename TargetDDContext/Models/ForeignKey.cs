using System;
using System.Collections.Generic;

namespace TargetDDContext.Models;

public partial class ForeignKey
{
    public string PktableQualifier { get; set; } = null!;

    public string PktableOwner { get; set; } = null!;

    public string PktableName { get; set; } = null!;

    public string PkcolumnName { get; set; } = null!;

    public string FktableQualifier { get; set; } = null!;

    public string FktableOwner { get; set; } = null!;

    public string FktableName { get; set; } = null!;

    public string FkcolumnName { get; set; } = null!;

    public short? KeySeq { get; set; }

    public short? UpdateRule { get; set; }

    public short? DeleteRule { get; set; }

    public string FkName { get; set; } = null!;

    public string PkName { get; set; } = null!;

    public short? Deferrability { get; set; }

    public virtual Column ForeignKeyColumn { get; set; } = null!;

    public virtual Column PrimaryKeyColumn { get; set; } = null!;

    public virtual ICollection<FamilyPath> FamilyPaths { get; set; } = new List<FamilyPath>();


}

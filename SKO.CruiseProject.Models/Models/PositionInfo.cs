using System;
using System.Collections.Generic;

namespace CruiseProject.Models;

public partial class PositionInfo
{
    public int PositionId { get; set; }

    public string Position { get; set; } = null!;

    public int DepartmentId { get; set; }

    public virtual ICollection<CrewInfo> CrewInfos { get; set; } = new List<CrewInfo>();

    public virtual DepartmentInfo Department { get; set; } = null!;
}

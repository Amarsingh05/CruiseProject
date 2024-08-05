using System;
using System.Collections.Generic;

namespace CruiseProject.Models;

public partial class DepartmentInfo
{
    public int DeptId { get; set; }

    public string Department { get; set; } = null!;

    public virtual ICollection<CrewInfo> CrewInfos { get; set; } = new List<CrewInfo>();

    public virtual ICollection<PositionInfo> PositionInfos { get; set; } = new List<PositionInfo>();

    public virtual ICollection<ShipDepartmentConnection> ShipDepartmentConnections { get; set; } = new List<ShipDepartmentConnection>();
}

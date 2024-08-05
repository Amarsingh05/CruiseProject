using System;
using System.Collections.Generic;

namespace CruiseProject.Models;

public partial class ShipInfo
{
    public string ShipId { get; set; } = null!;

    public string ShipName { get; set; } = null!;

    public virtual ICollection<ShipDepartmentConnection> ShipDepartmentConnections { get; set; } = new List<ShipDepartmentConnection>();

    public virtual ICollection<VoyageInfo> VoyageInfos { get; set; } = new List<VoyageInfo>();
}

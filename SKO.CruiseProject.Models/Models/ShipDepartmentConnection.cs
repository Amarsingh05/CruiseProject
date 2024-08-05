using System;
using System.Collections.Generic;

namespace CruiseProject.Models;

public partial class ShipDepartmentConnection
{
    public int Id { get; set; }

    public int DeptId { get; set; }

    public string ShipId { get; set; } = null!;

    public virtual DepartmentInfo Dept { get; set; } = null!;

    public virtual ShipInfo Ship { get; set; } = null!;
}

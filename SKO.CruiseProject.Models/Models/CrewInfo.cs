using System;
using System.Collections.Generic;

namespace CruiseProject.Models;

public partial class CrewInfo
{
    public string PersonId { get; set; } = null!;

    public int SafetyNo { get; set; }

    public DateTime SignOnDate { get; set; }

    public DateTime SignOffDate { get; set; }

    public int DeptId { get; set; }

    public int PositionId { get; set; }

    public virtual DepartmentInfo Dept { get; set; } = null!;

    public virtual GuestCrewInfo Person { get; set; } = null!;

    public virtual PositionInfo Position { get; set; } = null!;
}

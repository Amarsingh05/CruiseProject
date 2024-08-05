using System;
using System.Collections.Generic;

namespace CruiseProject.Models;

public partial class VoyageInfo
{
    public string VoyageNumber { get; set; } = null!;

    public string? ShipId { get; set; }

    public DateTime? VoyageStartDate { get; set; }

    public DateTime? VoyageEndDate { get; set; }

    public string? IsVoyageActive { get; set; }

    public string? PortName { get; set; }

    public virtual ICollection<GuestInfo> GuestInfos { get; set; } = new List<GuestInfo>();

    public virtual ShipInfo? Ship { get; set; }
}

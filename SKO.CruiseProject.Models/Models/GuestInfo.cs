using System;
using System.Collections.Generic;

namespace CruiseProject.Models;

public partial class GuestInfo
{
    public string PersonId { get; set; } = null!;

    public string IsActive { get; set; } = null!;

    public string ReservationNumber { get; set; } = null!;

    public int SequenceNo { get; set; }

    public DateTime EmbarkDate { get; set; }

    public DateTime DbarkDate { get; set; }

    public string VoyageNumber { get; set; } = null!;

    public virtual GuestCrewInfo Person { get; set; } = null!;

    public virtual VoyageInfo VoyageNumberNavigation { get; set; } = null!;
}

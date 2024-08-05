using System;
using System.Collections.Generic;

namespace CruiseProject.Models;

public partial class GuestCrewInfo
{
    public string PersonId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime Dob { get; set; }

    public string Gender { get; set; } = null!;

    public string Nationality { get; set; } = null!;

    public string IsOnboard { get; set; } = null!;

    public int CabinNo { get; set; }

    public string IsCheckedIn { get; set; } = null!;

    public string CheckedinTerminal { get; set; } = null!;

    public string GuestOrCrew { get; set; } = null!;

    public string? ProfileImage { get; set; }

    public virtual CrewInfo? CrewInfo { get; set; }

    public virtual GuestInfo? GuestInfo { get; set; }
}

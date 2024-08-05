namespace CruiseProject.Entities
{
    public class ManifestDTO
    {
        public List<GuestManifestDTO> Guests { get; set; }
        public List<CrewManifestDTO> Crew {  get; set; }    
    }

    public class GuestManifestDTO
    {  
        public string IsActive { get; set; }
        public string ReservationNumber { get; set; }
        public DateTime EmbarkDate { get; set; }
        public DateTime DebarkDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IsOnBoard {  get; set; }
        public string IsCheckedIn { get; set; }
        public string CheckedInTerminal { get; set; }
        public string GuestOrCrew { get; set; }
        public string? ProfileImage { get; set; }
    }

    public class CrewManifestDTO
    {
        public int DeptId { get; set; }
        public int SafetyNo { get; set; }
        public DateTime SignOnDate { get; set; }
        public DateTime SignOffDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IsOnBoard { get; set; }
        public string IsCheckedIn { get; set; }
        public string CheckedInTerminal { get; set; }
        public string GuestOrCrew { get; set; }
        public string? ProfileImage { get; set; }
    }
}

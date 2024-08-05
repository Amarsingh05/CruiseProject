namespace CruiseProject.Entities
{
    public class CrewInfoDto
    {
        public string Title { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime SignOnDate { get; set; }
        public DateTime SignOffDate { get; set; }
    }
}

namespace EventMangamentAPI.Entities
{
    public class Registration
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int ParticipantId { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Status { get; set; }
        public Event Event { get; set; }
        public Participant Participant { get; set; }
    }
}

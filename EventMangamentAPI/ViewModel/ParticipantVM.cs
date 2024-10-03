namespace EventMangamentAPI.ViewModel
{
    public class ParticipantVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime RegisteredAt { get; set; }
        public int ParticipantId { get; internal set; }
    }
}

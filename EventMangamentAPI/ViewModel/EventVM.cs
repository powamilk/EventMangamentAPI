namespace EventMangamentAPI.ViewModel
{
    public class EventVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NameLenght { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int MaxParticipants { get; set; }
    }
}

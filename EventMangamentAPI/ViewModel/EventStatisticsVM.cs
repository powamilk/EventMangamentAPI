namespace EventMangamentAPI.ViewModel
{
    public class EventStatisticsVM
    {
        public int TotalEvents { get; set; }
        public int TotalParticipants { get; set; }
        public int AverageParticipantsPerEvent { get; set; }
        public Dictionary<string, EventStatusStatisticsVM> EventsByStatus { get; set; }
        public EventRangeStatisticsVM EventsInDateRange { get; set; }
    }

}

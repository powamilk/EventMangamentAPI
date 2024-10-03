using EventMangamentAPI.ViewModel;

namespace EventMangamentAPI.Service.Interface
{
    public interface IOrtherService
    {
        List<EventVM> SearchEvents(string name, string location, DateTime? startDate, DateTime? endDate);
        List<EventVM> FilterEventsByStatus(string status);
        EventStatisticsVM GetEventStatistics(DateTime? startDate, DateTime? endDate, string status);
        List<RegistrationVM> BulkRegisterParticipants(BulkRegistrationVM request);
        RegistrationVM UpdateRegistrationStatus(int id, string status);
        ReviewVM CreateOrganizerReview(CreateReviewVM request);
    }
}

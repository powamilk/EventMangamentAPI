using EventMangamentAPI.Entities;
using EventMangamentAPI.Service.Interface;
using EventMangamentAPI.ViewModel;

namespace EventMangamentAPI.Service.Implement
{
    public class OrtherService : IOrtherService
    {
        private List<Event> _events = new List<Event>(); // Giả lập dữ liệu sự kiện
        private List<Registration> _registrations = new List<Registration>();
        private List<Review> _reviews = new List<Review>();

        public List<EventVM> SearchEvents(string name, string location, DateTime? startDate, DateTime? endDate)
        {
            var query = _events.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(e => e.Name.Contains(name));

            if (!string.IsNullOrEmpty(location))
                query = query.Where(e => e.Location.Contains(location));

            if (startDate.HasValue)
                query = query.Where(e => e.StartTime >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(e => e.EndTime <= endDate.Value);

            return query.Select(e => new EventVM
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Location = e.Location,
                StartTime = e.StartTime,
                EndTime = e.EndTime,
                MaxParticipants = e.MaxParticipants
            }).ToList();
        }

        public List<EventVM> FilterEventsByStatus(string status)
        {
            var query = _events.AsQueryable();

            if (!string.IsNullOrEmpty(status))
                query = query.Where(e => e.Status == status);

            return query.Select(e => new EventVM
            {
                Id = e.Id,
                Name = e.Name,
                Status = e.Status
            }).ToList();
        }

        public EventStatisticsVM GetEventStatistics(DateTime? startDate, DateTime? endDate, string status)
        {
            var query = _events.AsQueryable();

            if (startDate.HasValue)
                query = query.Where(e => e.StartTime >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(e => e.EndTime <= endDate.Value);

            if (!string.IsNullOrEmpty(status))
                query = query.Where(e => e.Status == status);

            var totalEvents = query.Count();
            var totalParticipants = _registrations.Count();
            var averageParticipantsPerEvent = totalEvents > 0 ? totalParticipants / totalEvents : 0;

            return new EventStatisticsVM
            {
                TotalEvents = totalEvents,
                TotalParticipants = totalParticipants,
                AverageParticipantsPerEvent = averageParticipantsPerEvent,
                EventsByStatus = new Dictionary<string, EventStatusStatisticsVM>
                {
                    { "ongoing", new EventStatusStatisticsVM { Count = 4, Participants = 200 } },
                    { "completed", new EventStatusStatisticsVM { Count = 5, Participants = 250 } },
                    { "upcoming", new EventStatusStatisticsVM { Count = 1, Participants = 50 } }
                },
                EventsInDateRange = new EventRangeStatisticsVM
                {
                    Count = query.Count(),
                    Participants = totalParticipants
                }
            };
        }

        public List<RegistrationVM> BulkRegisterParticipants(BulkRegistrationVM request)
        {
            var newRegistrations = new List<RegistrationVM>();

            foreach (var participant in request.Participants)
            {
                var registration = new RegistrationVM
                {
                    Id = _registrations.Any() ? _registrations.Max(r => r.Id) + 1 : 1,
                    EventId = request.EventId,
                    ParticipantId = participant.ParticipantId,
                    RegistrationDate = DateTime.Now,
                    Status = "đã xác nhận"
                };

                _registrations.Add(new Registration { Id = registration.Id, EventId = registration.EventId, ParticipantId = registration.ParticipantId, RegistrationDate = DateTime.Now, Status = "đã xác nhận" });
                newRegistrations.Add(registration);
            }

            return newRegistrations;
        }

        public RegistrationVM UpdateRegistrationStatus(int id, string status)
        {
            var registration = _registrations.FirstOrDefault(r => r.Id == id);
            if (registration != null)
            {
                registration.Status = status;
                return new RegistrationVM
                {
                    Id = registration.Id,
                    EventId = registration.EventId,
                    ParticipantId = registration.ParticipantId,
                    RegistrationDate = registration.RegistrationDate,
                    Status = registration.Status
                };
            }
            return null;
        }

        public ReviewVM CreateOrganizerReview(CreateReviewVM request)
        {
            var review = new ReviewVM
            {
                Id = _reviews.Any() ? _reviews.Max(r => r.Id) + 1 : 1,
                EventId = request.EventId,
                ParticipantId = request.ParticipantId,
                Rating = request.Rating,
                Comment = request.Comment,
                CreatedAt = DateTime.Now
            };

            _reviews.Add(new Review { Id = review.Id, EventId = review.EventId, ParticipantId = review.ParticipantId, Rating = review.Rating, Comment = review.Comment, CreatedAt = DateTime.Now });

            return review;
        }
    }
}

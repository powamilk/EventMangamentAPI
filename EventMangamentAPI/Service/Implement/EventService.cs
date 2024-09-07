using EventMangamentAPI.Entities;
using EventMangamentAPI.Service.Interface;
using EventMangamentAPI.ViewModel;
using FluentValidation;
using FluentValidation.Results;
using AutoMapper;

namespace EventMangamentAPI.Service.Implement
{
    public class EventService : IEventService
    {
        private static List<Event> _events = new();
        private readonly ILogger<EventService> _logger;
        private readonly IValidator<CreateEventVM> _createValidator;
        private readonly IValidator<UpdateEventVM> _updateValidator;
        private readonly IMapper _mapper;

        public EventService(ILogger<EventService> logger, IValidator<CreateEventVM> createValidator, IValidator<UpdateEventVM> updateValidator, IMapper mapper)
        {
            _logger = logger;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _mapper = mapper;
        }

        public bool CreateEvent(CreateEventVM request, out string errorMessage)
        {
            try
            {
                ValidationResult result = _createValidator.Validate(request);
                if (!result.IsValid)
                {
                    errorMessage = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                    return false;
                }

                var newEvent = new Event
                {
                    Id = _events.Any() ? _events.Max(e => e.Id) + 1 : 1,
                    Name = request.Name,
                    Description = request.Description,
                    Location = request.Location,
                    StartTime = request.StartTime,
                    EndTime = request.EndTime,
                    MaxParticipants = request.MaxParticipants
                };

                _events.Add(newEvent);
                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Đã xảy ra lỗi khi tạo sự kiện.");
                errorMessage = "Đã xảy ra lỗi khi tạo sự kiện.";
                return false;
            }
        }

        public List<EventVM> GetAllEvents(out string errorMessage)
        {
            var events = _events.ToList();

            if (events.Any())
            {
                var eventsVM = _mapper.Map<List<EventVM>>(events);
                errorMessage = null;
                return eventsVM;
            }

            errorMessage = "Không có sự kiện nào trong danh sách.";
            return null;
        }

        public EventVM GetEventById(int id, out string errorMessage)
        {
            var eventItem = _events.FirstOrDefault(e => e.Id == id);
            if (eventItem == null)
            {
                errorMessage = "Không tìm thấy sự kiện với ID này.";
                return null;
            }
            int nameLength = eventItem.Name.Length;

            var eventVM = new EventVM
            {
                Id = eventItem.Id,
                Name = eventItem.Name,
                Description = eventItem.Description,
                Location = eventItem.Location,
                StartTime = eventItem.StartTime,
                EndTime = eventItem.EndTime,
                MaxParticipants = eventItem.MaxParticipants,
                NameLenght = eventItem.Name.Length 
            };
            //var eventVM = _mapper.Map<EventVM>(eventItem);
            errorMessage = null;
            return eventVM;
        }

        public bool UpdateEvent(int id, UpdateEventVM request, out string errorMessage)
        {
            try
            {
                var eventItem = _events.FirstOrDefault(e => e.Id == id);
                if (eventItem == null)
                {
                    errorMessage = "Không tìm thấy sự kiện với ID này.";
                    return false;
                }

                ValidationResult result = _updateValidator.Validate(request);
                if (!result.IsValid)
                {
                    errorMessage = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                    return false;
                }

                eventItem.Name = request.Name;
                eventItem.Description = request.Description;
                eventItem.Location = request.Location;
                eventItem.StartTime = request.StartTime;
                eventItem.EndTime = request.EndTime;
                eventItem.MaxParticipants = request.MaxParticipants;

                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Đã xảy ra lỗi khi cập nhật sự kiện.");
                errorMessage = "Đã xảy ra lỗi khi cập nhật sự kiện.";
                return false;
            }
        }

        public bool DeleteEvent(int id, out string errorMessage)
        {
            try
            {
                var eventItem = _events.FirstOrDefault(e => e.Id == id);
                if (eventItem == null)
                {
                    errorMessage = "Không tìm thấy sự kiện với ID này.";
                    return false;
                }

                _events.Remove(eventItem);
                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Đã xảy ra lỗi khi xóa sự kiện.");
                errorMessage = "Đã xảy ra lỗi khi xóa sự kiện.";
                return false;
            }
        }
    }
}

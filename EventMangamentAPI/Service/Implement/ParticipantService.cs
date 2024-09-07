using AutoMapper;
using EventMangamentAPI.Entities;
using EventMangamentAPI.Service.Interface;
using EventMangamentAPI.ViewModel;
using FluentValidation;
using FluentValidation.Results;
namespace EventMangamentAPI.Service.Implement
{
    public class ParticipantService : IParticipantService
    {
        private static List<Participant> _participants = new();
        private readonly ILogger<ParticipantService> _logger;
        private readonly IValidator<CreateParticipantVM> _createValidator;
        private readonly IValidator<UpdateParticipantVM> _updateValidator;
        private readonly IMapper _mapper;

        public ParticipantService(ILogger<ParticipantService> logger, IValidator<CreateParticipantVM> createValidator, IValidator<UpdateParticipantVM> updateValidator, IMapper mapper)
        {
            _logger = logger;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _mapper = mapper;
        }

        public bool CreateParticipant(CreateParticipantVM request, out string errorMessage)
        {
            try
            {
                ValidationResult result = _createValidator.Validate(request);
                if (!result.IsValid)
                {
                    errorMessage = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                    return false;
                }

                var participant = new Participant
                {
                    Id = _participants.Any() ? _participants.Max(p => p.Id) + 1 : 1,
                    Name = request.Name,
                    Email = request.Email,
                    Phone = request.Phone,
                    RegisteredAt = DateTime.Now
                };

                _participants.Add(participant);
                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Đã xảy ra lỗi khi tạo người tham gia.");
                errorMessage = "Đã xảy ra lỗi khi tạo người tham gia.";
                return false;
            }
        }

        public List<ParticipantVM> GetAllParticipants(out string errorMessage)
        {
            if (_participants.Any())
            {
                var participantsVM = _mapper.Map<List<ParticipantVM>>(_participants);
                errorMessage = null;
                return participantsVM;
            }

            errorMessage = "Không có người tham gia nào trong danh sách.";
            return null;
        }

        public ParticipantVM GetParticipantById(int id, out string errorMessage)
        {
            var participant = _participants.FirstOrDefault(p => p.Id == id);
            if (participant == null)
            {
                errorMessage = "Không tìm thấy người tham gia với ID này.";
                return null;
            }

            var participantVM = _mapper.Map<ParticipantVM>(participant);
            errorMessage = null;
            return participantVM;
        }

        public bool UpdateParticipant(int id, UpdateParticipantVM request, out string errorMessage)
        {
            try
            {
                var participant = _participants.FirstOrDefault(p => p.Id == id);
                if (participant == null)
                {
                    errorMessage = "Không tìm thấy người tham gia với ID này.";
                    return false;
                }

                ValidationResult result = _updateValidator.Validate(request);
                if (!result.IsValid)
                {
                    errorMessage = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                    return false;
                }

                participant.Name = request.Name;
                participant.Email = request.Email;
                participant.Phone = request.Phone;

                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Đã xảy ra lỗi khi cập nhật người tham gia.");
                errorMessage = "Đã xảy ra lỗi khi cập nhật người tham gia.";
                return false;
            }
        }

        public bool DeleteParticipant(int id, out string errorMessage)
        {
            try
            {
                var participant = _participants.FirstOrDefault(p => p.Id == id);
                if (participant == null)
                {
                    errorMessage = "Không tìm thấy người tham gia với ID này.";
                    return false;
                }

                _participants.Remove(participant);
                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Đã xảy ra lỗi khi xóa người tham gia.");
                errorMessage = "Đã xảy ra lỗi khi xóa người tham gia.";
                return false;
            }
        }
    }
}

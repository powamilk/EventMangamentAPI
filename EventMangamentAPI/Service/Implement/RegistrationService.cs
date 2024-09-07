using AutoMapper;
using EventMangamentAPI.Entities;
using EventMangamentAPI.Service.Interface;
using EventMangamentAPI.ViewModel;
using FluentValidation;
using FluentValidation.Results;
namespace EventMangamentAPI.Service.Implement
{
    public class RegistrationService : IRegistrationService
    {
        private static List<Registration> _registrations = new();
        private readonly ILogger<RegistrationService> _logger;
        private readonly IValidator<CreateRegistrationVM> _createValidator;
        private readonly IValidator<UpdateRegistrationVM> _updateValidator;
        private readonly IMapper _mapper;

        public RegistrationService(ILogger<RegistrationService> logger, IValidator<CreateRegistrationVM> createValidator, IValidator<UpdateRegistrationVM> updateValidator, IMapper mapper)
        {
            _logger = logger;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _mapper = mapper;
        }

        public bool CreateRegistration(CreateRegistrationVM request, out string errorMessage)
        {
            try
            {
                ValidationResult result = _createValidator.Validate(request);
                if (!result.IsValid)
                {
                    errorMessage = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                    return false;
                }

                var registration = new Registration
                {
                    Id = _registrations.Any() ? _registrations.Max(r => r.Id) + 1 : 1,
                    EventId = request.EventId,
                    ParticipantId = request.ParticipantId,
                    RegistrationDate = request.RegistrationDate,
                    Status = request.Status
                };

                _registrations.Add(registration);
                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Đã xảy ra lỗi khi tạo đăng ký.");
                errorMessage = "Đã xảy ra lỗi khi tạo đăng ký.";
                return false;
            }
        }

        public List<RegistrationVM> GetAllRegistrations(out string errorMessage)
        {
            if (_registrations.Any())
            {
                var registrationsVM = _mapper.Map<List<RegistrationVM>>(_registrations);
                errorMessage = null;
                return registrationsVM;
            }

            errorMessage = "Không có đăng ký nào trong danh sách.";
            return null;
        }

        public RegistrationVM GetRegistrationById(int id, out string errorMessage)
        {
            var registration = _registrations.FirstOrDefault(r => r.Id == id);
            if (registration == null)
            {
                errorMessage = "Không tìm thấy đăng ký với ID này.";
                return null;
            }

            var registrationVM = _mapper.Map<RegistrationVM>(registration);
            errorMessage = null;
            return registrationVM;
        }

        public bool UpdateRegistration(int id, UpdateRegistrationVM request, out string errorMessage)
        {
            try
            {
                var registration = _registrations.FirstOrDefault(r => r.Id == id);
                if (registration == null)
                {
                    errorMessage = "Không tìm thấy đăng ký với ID này.";
                    return false;
                }

                ValidationResult result = _updateValidator.Validate(request);
                if (!result.IsValid)
                {
                    errorMessage = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                    return false;
                }

                registration.EventId = request.EventId;
                registration.ParticipantId = request.ParticipantId;
                registration.RegistrationDate = request.RegistrationDate;
                registration.Status = request.Status;

                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Đã xảy ra lỗi khi cập nhật đăng ký.");
                errorMessage = "Đã xảy ra lỗi khi cập nhật đăng ký.";
                return false;
            }
        }

        public bool DeleteRegistration(int id, out string errorMessage)
        {
            try
            {
                var registration = _registrations.FirstOrDefault(r => r.Id == id);
                if (registration == null)
                {
                    errorMessage = "Không tìm thấy đăng ký với ID này.";
                    return false;
                }

                _registrations.Remove(registration);
                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Đã xảy ra lỗi khi xóa đăng ký.");
                errorMessage = "Đã xảy ra lỗi khi xóa đăng ký.";
                return false;
            }
        }
    }
}


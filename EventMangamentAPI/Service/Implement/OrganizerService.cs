using EventMangamentAPI.Entities;
using EventMangamentAPI.Service.Interface;
using EventMangamentAPI.ViewModel;
using FluentValidation;
using FluentValidation.Results;
namespace EventMangamentAPI.Service.Implement
{
    public class OrganizerService : IOrganizerService
    {
        private static List<Organizer> _organizers = new();
        private readonly ILogger<OrganizerService> _logger;
        private readonly IValidator<CreateOrganizerVM> _createValidator;
        private readonly IValidator<UpdateOrganizerVM> _updateValidator;

        public OrganizerService(ILogger<OrganizerService> logger, IValidator<CreateOrganizerVM> createValidator, IValidator<UpdateOrganizerVM> updateValidator)
        {
            _logger = logger;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public bool CreateOrganizer(CreateOrganizerVM request, out string errorMessage)
        {
            try
            {
                ValidationResult result = _createValidator.Validate(request);
                if (!result.IsValid)
                {
                    errorMessage = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                    return false;
                }

                var organizer = new Organizer
                {
                    Id = _organizers.Any() ? _organizers.Max(o => o.Id) + 1 : 1,
                    Name = request.Name,
                    ContactEmail = request.ContactEmail,
                    Phone = request.Phone
                };

                _organizers.Add(organizer);
                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Đã xảy ra lỗi khi tạo người tổ chức.");
                errorMessage = "Đã xảy ra lỗi khi tạo người tổ chức.";
                return false;
            }
        }

        public List<OrganizerVM> GetAllOrganizers(out string errorMessage)
        {
            if (_organizers.Any())
            {
                var organizersVM = _organizers.Select(o => new OrganizerVM
                {
                    Id = o.Id,
                    Name = o.Name,
                    ContactEmail = o.ContactEmail,
                    Phone = o.Phone
                }).ToList();

                errorMessage = null;
                return organizersVM;
            }

            errorMessage = "Không có người tổ chức nào trong danh sách.";
            return null;
        }

        public OrganizerVM GetOrganizerById(int id, out string errorMessage)
        {
            var organizer = _organizers.FirstOrDefault(o => o.Id == id);
            if (organizer == null)
            {
                errorMessage = "Không tìm thấy người tổ chức với ID này.";
                return null;
            }

            var organizerVM = new OrganizerVM
            {
                Id = organizer.Id,
                Name = organizer.Name,
                ContactEmail = organizer.ContactEmail,
                Phone = organizer.Phone
            };

            errorMessage = null;
            return organizerVM;
        }

        public bool UpdateOrganizer(int id, UpdateOrganizerVM request, out string errorMessage)
        {
            try
            {
                var organizer = _organizers.FirstOrDefault(o => o.Id == id);
                if (organizer == null)
                {
                    errorMessage = "Không tìm thấy người tổ chức với ID này.";
                    return false;
                }

                ValidationResult result = _updateValidator.Validate(request);
                if (!result.IsValid)
                {
                    errorMessage = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                    return false;
                }

                organizer.Name = request.Name;
                organizer.ContactEmail = request.ContactEmail;
                organizer.Phone = request.Phone;

                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Đã xảy ra lỗi khi cập nhật người tổ chức.");
                errorMessage = "Đã xảy ra lỗi khi cập nhật người tổ chức.";
                return false;
            }
        }

        public bool DeleteOrganizer(int id, out string errorMessage)
        {
            try
            {
                var organizer = _organizers.FirstOrDefault(o => o.Id == id);
                if (organizer == null)
                {
                    errorMessage = "Không tìm thấy người tổ chức với ID này.";
                    return false;
                }

                _organizers.Remove(organizer);
                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Đã xảy ra lỗi khi xóa người tổ chức.");
                errorMessage = "Đã xảy ra lỗi khi xóa người tổ chức.";
                return false;
            }
        }
    }
}

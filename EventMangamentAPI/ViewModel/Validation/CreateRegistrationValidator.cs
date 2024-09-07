using FluentValidation;

namespace EventMangamentAPI.ViewModel.Validation
{
    public class CreateRegistrationValidator : AbstractValidator<CreateRegistrationVM>
    {
        public CreateRegistrationValidator() //To do; IdEvent
        {
            RuleFor(x => x.EventId)
                .GreaterThan(0).WithMessage("ID sự kiện không hợp lệ.");

            RuleFor(x => x.ParticipantId)
                .GreaterThan(0).WithMessage("ID người tham gia không hợp lệ.");

            RuleFor(x => x.RegistrationDate)
                .NotEmpty().WithMessage("Ngày đăng ký không được để trống.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Ngày đăng ký không được ở trong tương lai.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Trạng thái đăng ký không được để trống.")
                .Must(x => x == "đã xác nhận" || x == "đã hủy").WithMessage("Trạng thái đăng ký phải là 'đã xác nhận' hoặc 'đã hủy'.");
        }
    }
}

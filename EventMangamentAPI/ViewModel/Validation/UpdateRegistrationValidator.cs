using FluentValidation;

namespace EventMangamentAPI.ViewModel.Validation
{
    public class UpdateRegistrationValidator : AbstractValidator<UpdateRegistrationVM>
    {
        public UpdateRegistrationValidator()
        {
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Trạng thái đăng ký không được để trống.")
                .Must(x => x == "đã xác nhận" || x == "đã hủy").WithMessage("Trạng thái phải là 'đã xác nhận' hoặc 'đã hủy'.");
        }
    }
}

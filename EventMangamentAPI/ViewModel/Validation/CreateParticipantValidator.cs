using FluentValidation;

namespace EventMangamentAPI.ViewModel.Validation
{
    public class CreateParticipantValidator : AbstractValidator<CreateParticipantVM>
    {
        public CreateParticipantValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên người tham gia không được để trống.")
                .MaximumLength(255).WithMessage("Tên người tham gia không được vượt quá 255 ký tự.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Địa chỉ email không được để trống.")
                .EmailAddress().WithMessage("Địa chỉ email không hợp lệ.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Số điện thoại không được để trống.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Số điện thoại không hợp lệ.");

            RuleFor(x => x.RegisteredAt)
                .NotEmpty().WithMessage("Thời gian đăng ký tham gia không được để trống.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Thời gian đăng ký không được ở trong tương lai.");
        }
    }
}

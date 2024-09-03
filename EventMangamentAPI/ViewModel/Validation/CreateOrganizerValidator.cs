using FluentValidation;
namespace EventMangamentAPI.ViewModel.Validation
{
    public class CreateOrganizerValidator : AbstractValidator<CreateOrganizerVM>
    {
        public CreateOrganizerValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên người tổ chức không được để trống.")
                .MaximumLength(255).WithMessage("Tên người tổ chức không được vượt quá 255 ký tự.");

            RuleFor(x => x.ContactEmail)
                .NotEmpty().WithMessage("Địa chỉ email không được để trống.")
                .EmailAddress().WithMessage("Địa chỉ email không hợp lệ.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Số điện thoại không được để trống.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Số điện thoại không hợp lệ.");
        }
    }
}

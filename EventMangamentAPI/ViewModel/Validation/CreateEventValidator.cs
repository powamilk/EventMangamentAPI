using FluentValidation;
namespace EventMangamentAPI.ViewModel.Validation
{
    public class CreateEventValidator : AbstractValidator<CreateEventVM>
    {
        public CreateEventValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên sự kiện không được để trống.")
                .MaximumLength(255).WithMessage("Tên sự kiện không được vượt quá 255 ký tự.");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Mô tả sự kiện không được vượt quá 1000 ký tự.");

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Địa điểm tổ chức sự kiện không được để trống.")
                .MaximumLength(255).WithMessage("Địa điểm tổ chức không được vượt quá 255 ký tự.");

            RuleFor(x => x.StartTime)
                .NotEmpty().WithMessage("Thời gian bắt đầu sự kiện không được để trống.")
                .GreaterThan(DateTime.Now).WithMessage("Thời gian bắt đầu phải lớn hơn thời gian hiện tại.");

            RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage("Thời gian kết thúc sự kiện không được để trống.")
                .GreaterThan(x => x.StartTime).WithMessage("Thời gian kết thúc phải lớn hơn thời gian bắt đầu.");

            RuleFor(x => x.MaxParticipants)
                .GreaterThan(0).WithMessage("Số lượng người tham gia tối đa phải là số nguyên dương.");
        }
    }
}

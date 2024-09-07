using FluentValidation;

namespace EventMangamentAPI.ViewModel.Validation
{
    public class CreateReviewValidator : AbstractValidator<CreateReviewVM>
    {
        public CreateReviewValidator() //to do , eventId, participantId
        {
            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5).WithMessage("Điểm đánh giá phải nằm trong khoảng từ 1 đến 5.");

            RuleFor(x => x.Comment)
                .MaximumLength(1000).WithMessage("Nhận xét không được vượt quá 1000 ký tự.");

            RuleFor(x => x.CreatedAt)
                .NotEmpty().WithMessage("Thời gian tạo đánh giá không được để trống.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Thời gian tạo đánh giá không hợp lệ.");
        }
    }
}

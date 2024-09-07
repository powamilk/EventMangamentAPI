using FluentValidation;

namespace EventMangamentAPI.ViewModel.Validation
{
    public class UpdateReviewValidator : AbstractValidator<UpdateReviewVM>
    {
        public UpdateReviewValidator()
        {
            RuleFor(x => x.Rating)
                .InclusiveBetween(1, 5).WithMessage("Điểm đánh giá phải từ 1 đến 5.");

            RuleFor(x => x.Comment)
                .MaximumLength(1000).WithMessage("Nhận xét không được vượt quá 1000 ký tự.");
        }
    }
}

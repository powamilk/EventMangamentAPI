using EventMangamentAPI.Entities;
using EventMangamentAPI.Service.Interface;
using EventMangamentAPI.ViewModel;
using FluentValidation;
using FluentValidation.Results;

namespace EventMangamentAPI.Service.Implement
{
    public class ReviewService : IReviewService
    {
        private static List<Review> _reviews = new();
        private readonly ILogger<ReviewService> _logger;
        private readonly IValidator<CreateReviewVM> _createValidator;
        private readonly IValidator<UpdateReviewVM> _updateValidator;

        public ReviewService(ILogger<ReviewService> logger, IValidator<CreateReviewVM> createValidator, IValidator<UpdateReviewVM> updateValidator)
        {
            _logger = logger;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public bool CreateReview(CreateReviewVM request, out string errorMessage)
        {
            try
            {
                ValidationResult result = _createValidator.Validate(request);
                if (!result.IsValid)
                {
                    errorMessage = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                    return false;
                }

                var review = new Review
                {
                    Id = _reviews.Any() ? _reviews.Max(r => r.Id) + 1 : 1,
                    EventId = request.EventId,
                    ParticipantId = request.ParticipantId,
                    Rating = request.Rating,
                    Comment = request.Comment,
                    CreatedAt = DateTime.Now
                };

                _reviews.Add(review);
                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Đã xảy ra lỗi khi tạo đánh giá.");
                errorMessage = "Đã xảy ra lỗi khi tạo đánh giá.";
                return false;
            }
        }

        public List<ReviewVM> GetAllReviews(out string errorMessage)
        {
            if (_reviews.Any())
            {
                var reviewsVM = _reviews.Select(r => new ReviewVM
                {
                    Id = r.Id,
                    EventId = r.EventId,
                    ParticipantId = r.ParticipantId,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    CreatedAt = r.CreatedAt
                }).ToList();

                errorMessage = null;
                return reviewsVM;
            }

            errorMessage = "Không có đánh giá nào trong danh sách.";
            return null;
        }

        public ReviewVM GetReviewById(int id, out string errorMessage)
        {
            var review = _reviews.FirstOrDefault(r => r.Id == id);
            if (review == null)
            {
                errorMessage = "Không tìm thấy đánh giá với ID này.";
                return null;
            }

            var reviewVM = new ReviewVM
            {
                Id = review.Id,
                EventId = review.EventId,
                ParticipantId = review.ParticipantId,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt
            };

            errorMessage = null;
            return reviewVM;
        }

        public bool UpdateReview(int id, UpdateReviewVM request, out string errorMessage)
        {
            try
            {
                var review = _reviews.FirstOrDefault(r => r.Id == id);
                if (review == null)
                {
                    errorMessage = "Không tìm thấy đánh giá với ID này.";
                    return false;
                }

                ValidationResult result = _updateValidator.Validate(request);
                if (!result.IsValid)
                {
                    errorMessage = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                    return false;
                }

                review.EventId = request.EventId;
                review.ParticipantId = request.ParticipantId;
                review.Rating = request.Rating;
                review.Comment = request.Comment;
                review.CreatedAt = request.CreatedAt;

                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Đã xảy ra lỗi khi cập nhật đánh giá.");
                errorMessage = "Đã xảy ra lỗi khi cập nhật đánh giá.";
                return false;
            }
        }

        public bool DeleteReview(int id, out string errorMessage)
        {
            try
            {
                var review = _reviews.FirstOrDefault(r => r.Id == id);
                if (review == null)
                {
                    errorMessage = "Không tìm thấy đánh giá với ID này.";
                    return false;
                }

                _reviews.Remove(review);
                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Đã xảy ra lỗi khi xóa đánh giá.");
                errorMessage = "Đã xảy ra lỗi khi xóa đánh giá.";
                return false;
            }
        }
    }
}

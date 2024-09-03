using EventMangamentAPI.ViewModel;
namespace EventMangamentAPI.Service.Interface
{
    public interface IReviewService
    {
        bool CreateReview(CreateReviewVM request, out string errorMessage);
        List<ReviewVM> GetAllReviews(out string errorMessage);
        ReviewVM GetReviewById(int id, out string errorMessage);
        bool UpdateReview(int id, UpdateReviewVM request, out string errorMessage);
        bool DeleteReview(int id, out string errorMessage);
    }
}

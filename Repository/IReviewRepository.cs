using AppointmentAPI.Entities;

namespace AppointmentAPI.Repository
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetAllReviews();
        Task<Review> GetReviewById(int reviewId);
        Task<List<Review>> GetReviewByUserId(int userId);
        Task<List<Review>> GetReviewByServiceId(int serviceId);
        Task<Review> AddReview(Review review);
        Task<Review> UpdateReview(int id, Review review);
        Task<bool> DeleteReview(int id);
    }
}

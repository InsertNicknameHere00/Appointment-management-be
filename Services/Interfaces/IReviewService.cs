using AppointmentAPI.Entities;

namespace AppointmentAPI.Services
{
    public interface IReviewService
    {
        Task<List<Review>> GetAllReviews();
        Task<Review> GetReviewById(int id);
        Task<List<Review>> GetReviewByServiceId(int id);
        Task<List<Review>> GetReviewByUserId(int id);
        Task<Review> Save(Review review);
        Task<Review> Update(int id, Review review);
        Task<bool> Delete(int id);
    }
}

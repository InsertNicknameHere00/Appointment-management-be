using AppointmentAPI.Entities;
using AppointmentAPI.Repository;

namespace AppointmentAPI.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        public ReviewService(IReviewRepository reviewRepository) {
            _reviewRepository = reviewRepository;
        }

        public async Task<bool> Delete(int id)
        {
            return await _reviewRepository.DeleteReview(id);
        }

        public async Task<List<Review>> GetAllReviews()
        {
            return await _reviewRepository.GetAllReviews();
        }

        public async Task<Review> GetReviewById(int id)
        {
            return await _reviewRepository.GetReviewById(id);
        }

        public async Task<List<Review>> GetReviewByServiceId(int id)
        {
            return await _reviewRepository.GetReviewByServiceId(id);
        }

        public async Task<List<Review>> GetReviewByUserId(int id)
        {
            return await _reviewRepository.GetReviewByUserId(id);
        }

        public async Task<Review> Save(Review review)
        {
            return await _reviewRepository.AddReview(review);
        }

        public async Task<Review> Update(int id, Review review)
        {
            return await _reviewRepository.UpdateReview(id, review);
        }
    }
}

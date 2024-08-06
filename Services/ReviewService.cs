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

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Review>> GetAllReviews()
        {
            throw new NotImplementedException();
        }

        public Task<Review> GetReviewById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Review>> GetReviewByServiceId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Review>> GetReviewByUserId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Review> Save(Review review)
        {
            throw new NotImplementedException();
        }

        public Task<Review> Update(int id, Review review)
        {
            throw new NotImplementedException();
        }
    }
}

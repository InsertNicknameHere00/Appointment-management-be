using AppointmentAPI.Data;
using AppointmentAPI.Entities;

namespace AppointmentAPI.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly HaircutSalonDbContext _context;
        public ReviewRepository(HaircutSalonDbContext context) {
            _context = context;
        }
        public Task<Review> AddReview(Review review)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteReview(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Review>> GetAllReviews()
        {
            throw new NotImplementedException();
        }

        public Task<Review> GetReviewById(int reviewId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Review>> GetReviewByServiceId(int serviceId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Review>> GetReviewByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Review> UpdateReview(int id, Review review)
        {
            throw new NotImplementedException();
        }
    }
}

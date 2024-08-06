using AppointmentAPI.Data;
using AppointmentAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppointmentAPI.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly HaircutSalonDbContext _context;
        public ReviewRepository(HaircutSalonDbContext context)
        {
            _context = context;
        }
        public async Task<Review> AddReview(Review review)
        {
            var newReview = new Review
            {
                ServiceId = review.ServiceId,
                UserId = review.UserId,
                ReviewDescription = review.ReviewDescription
            };

            await _context.Review.AddAsync(newReview);
            await _context.SaveChangesAsync();
            return newReview;
        }

        public async Task<bool> DeleteReview(int id)
        {
            var review = await _context.Review.FindAsync(id);
            if (review == null)
            {
                return false;
            }
            _context.Review.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Review>> GetAllReviews()
        {
            return await _context.Review.ToListAsync();
        }

        public async Task<Review> GetReviewById(int reviewId)
        {
            return await _context.Review.FindAsync(reviewId);
        }

        public async Task<List<Review>> GetReviewByServiceId(int serviceId)
        {
            return await _context.Review
            .Where(r => r.ServiceId == serviceId)
            .ToListAsync();
        }

        public async Task<List<Review>> GetReviewByUserId(int userId)
        {
            return await _context.Review
            .Where(r => r.UserId == userId)
            .ToListAsync();
        }

        public async Task<Review> UpdateReview(int id, Review review)
        {
            var existingReview = await _context.Review.FindAsync(id);
            if (existingReview == null)
            {
                return null;
            }
            existingReview.ReviewDescription = review.ReviewDescription;
            existingReview.ServiceId = review.ServiceId;
            existingReview.UserId = review.UserId;

            await _context.SaveChangesAsync();
            return existingReview;
        }
    }
}
    

namespace AppointmentAPI.Repository.Interfaces
{
    public interface IPromoCodeRepository
    {
        Task<decimal> ApplyPromoCodeToOrder(int orderId, string promoCode);
        Task<List<string>> GeneratePromoCodes(int numberOfCodes, int discountPercentage);
    }
}

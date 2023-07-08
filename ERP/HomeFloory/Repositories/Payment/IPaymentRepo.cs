using HomeFloory.Models;

namespace HomeFloory.Repositories.Payment
{
    public interface IPaymentRepo
    {
        Task<Korpa> CreateOrUpdatePaymentIntent(decimal idKorpa);

        Task<Korpa> UpdatePaymentIntentSucceeded(string paymentIntent);

        Task<Korpa> UpdatePaymentIntentFailed(string paymentIntent);
    
    }
}

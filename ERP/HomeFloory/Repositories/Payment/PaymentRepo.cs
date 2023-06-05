using HomeFloory.Models;
using HomeFloory.Repositories.DostavaRepo;
using HomeFloory.Repositories.KorpaRepo;
using HomeFloory.Repositories.ProizvodRepo;
using Stripe;

namespace HomeFloory.Repositories.Payment
{
    public class PaymentRepo : IPaymentRepo
    {
        private readonly IKorpaRepo _korpaRepo;
        private readonly IConfiguration _config;
        private readonly IDostavaRepo _dostavaRepo;
        private readonly IProizvodRepo _proizvodRepo;

        public PaymentRepo(IKorpaRepo korpaRepo, IConfiguration config, IDostavaRepo dostavaRepo, IProizvodRepo proizvodRepo )
        {
            _korpaRepo = korpaRepo;
            _config = config;
            _dostavaRepo = dostavaRepo;
            _proizvodRepo = proizvodRepo;
        }    

        public async Task<Korpa> CreateOrUpdatePaymentIntent(decimal idKorpa)
        {
            StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];
            var korpa = await _korpaRepo.GetKorpa(idKorpa);
            var cenaDostave = 0;

            if(korpa.IdDostava != null)
            {
                var idDostava = await _dostavaRepo.GetDostava(korpa.IdDostava);
                cenaDostave = (int)idDostava.CenaUsluge;
            }

            foreach(var item in korpa.DodatiProizvodi)
            {
                var productItem = await _proizvodRepo.GetProizvod(item.IdProizvod);
                if(item.Cena != productItem.CenaPoM2)
                {
                    item.Cena = productItem.CenaPoM2;
                }
            }

            var service = new PaymentIntentService();
            PaymentIntent intent;

            if (string.IsNullOrEmpty(korpa.PaymentIntent))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)korpa.DodatiProizvodi.Sum(i => i.Kolicina * (i.Cena * 100)) + (long)cenaDostave * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                intent = await service.CreateAsync(options);
                korpa.PaymentIntent = intent.Id;
                korpa.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)korpa.DodatiProizvodi.Sum(i => i.Kolicina * (i.Cena * 100)) + (long)cenaDostave * 100,

                };
                await service.UpdateAsync(korpa.PaymentIntent, options);
            }

            await _korpaRepo.UpdateKorpa(korpa.IdKorpa, korpa);
            return korpa;
        }

        /*public async Task<Korpa> UpdatePaymentIntentFailed(string paymentIntent)
        {
            var spec = new OrderByPaymentintent(paymentIntent);
            var korpa = await _korpaRepo.GetKorpa(spec);

            if (korpa == null) return null;

            
        }*/

    }
}

using HomeFloory.Models;
using HomeFloory.Repositories.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace HomeFloory.Controllers
{

    [ApiController]
    //[EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {
        private const string WhSecret = "whsec_6c158f363835cb35ed89a68aa78b0a763feb121d0d60b97c9e045faf6e24e877";
        private readonly IPaymentRepo _paymentRepo;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IPaymentRepo paymentRepo, ILogger<PaymentController> logger)
        {
            _paymentRepo = paymentRepo;
            _logger = logger;
        }

        [HttpPost("{IdKorpa}")]
        public async Task<ActionResult<Korpa>>CreateOrUpdatePaymentIntent(decimal IdKorpa)
        {
            return await _paymentRepo.CreateOrUpdatePaymentIntent(IdKorpa);
        }

        
        [HttpPost("Webhook")]
        //[EnableCors("AllowOrigin")]
        public async Task<ActionResult> StripeWebhook()
        {
            var json = await new StreamReader(Request.Body).ReadToEndAsync();
            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], WhSecret);

            PaymentIntent intent;
            Korpa order;

            switch (stripeEvent.Type)
            {
                case "payment_intent.succeeded":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Placanje uspesno: ", intent.Id);
                    order = await _paymentRepo.UpdatePaymentIntentSucceeded(intent.Id);
                    _logger.LogInformation("Porudzbina azurirana: placanje izvrseno", order.IdKorpa);
                    break;
                case "payment_intent.payment_failed":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Placanje neuspesno: ", intent.Id);
                    order = await _paymentRepo.UpdatePaymentIntentFailed(intent.Id);
                    _logger.LogInformation("Porudzbina azurirana: placanje neuspesno", order.IdKorpa);

                    break;
            }

            return new EmptyResult();
        
        }
    }
}

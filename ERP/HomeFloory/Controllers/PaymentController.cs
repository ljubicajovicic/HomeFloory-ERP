using HomeFloory.Models;
using HomeFloory.Repositories.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace HomeFloory.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {
        private const string WhSecret = "";
        private readonly IPaymentRepo _paymentRepo;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IPaymentRepo paymentRepo, ILogger<PaymentController> logger)
        {
            _paymentRepo = paymentRepo;
            _logger = logger;
        }

        [Authorize]
        [HttpPost("{IdKorpa}")]
        public async Task<ActionResult<Korpa>>CreateOrUpdatePaymentIntent(decimal IdKorpa)
        {
            return await _paymentRepo.CreateOrUpdatePaymentIntent(IdKorpa);
        }

        [HttpPost]
        public async Task<ActionResult> StripeWebhook()
        {
            var json = await new StreamReader(Request.Body).ReadToEndAsync();
            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], WhSecret);

            PaymentIntent intent;
            Korpa korpa;

            switch (stripeEvent.Type)
            {
                case "payment_intent.succeeded":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Placanje uspesno: ", intent.Id);
                    //update the order with status
                    break;
                case "payment_intent.payment_failed":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Placanje neuspesno: ", intent.Id);
                   
                    break;
            }

            return new EmptyResult();
        
        }
    }
}

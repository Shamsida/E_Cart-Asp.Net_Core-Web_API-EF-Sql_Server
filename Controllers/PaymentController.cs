using E_cart.Data;
using E_cart.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.Net;

namespace E_cart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private Response response;
        private readonly IConfiguration _configuration;
        private readonly DataContext context;
        public PaymentController(IConfiguration configuration,DataContext context)
        {
            _configuration = configuration;
            this.context = context;
            response = new();
        }

        [HttpPost]
        public async Task <ActionResult<Response>> MakePayment(int cartId)
        {
            var cart = await context.Carts.FirstOrDefaultAsync(c => c.Id == cartId);
            if (cart == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.Success = false;
                response.Error = "item not found";
                return BadRequest(response);
            }
            StripeConfiguration.ApiKey = _configuration["Stripe:Secretkey"];
            decimal amount = cart.TotalPrice;
            PaymentIntentCreateOptions intent = new PaymentIntentCreateOptions()
            {
                Amount = (long?)amount,
                Currency = "Inr",
                PaymentMethodTypes = new List<string>
                  {
                    "card",
                  },
            };
            PaymentIntentService service = new PaymentIntentService();
            PaymentIntent paymentIntent = service.Create(intent);
            cart.StripePaymentIntentId = paymentIntent.Id;
            cart.ClientSecret = paymentIntent.ClientSecret;
            response.Success = true;
            response.StatusCode = HttpStatusCode.OK;
            return Ok(response);
        }
    }
}

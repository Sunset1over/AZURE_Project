using API.BLL.Services.Pay;
using API.DAL.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MlkPwgen;

namespace WineProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonateController : ControllerBase
    {
        private readonly PayService _payService;
        private readonly ApplicationContext _context;
        public DonateController(PayService payService, ApplicationContext context)
        {
            _payService = payService;
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<PayButtonModel>> GetDonate(int amount)
        {
            var orderId = PasswordGenerator.Generate(length: 10, allowed: Sets.Alphanumerics);
            var payButton = _payService.GetPayButton(new PayOptions()
            {
                Amount = amount.ToString(),
                Currency = "UAH",
                Description = "Get VIP",
                Version = "3",
                OrderId = orderId
            });
            await _context.SaveChangesAsync();
            return payButton;
        }
    }
}

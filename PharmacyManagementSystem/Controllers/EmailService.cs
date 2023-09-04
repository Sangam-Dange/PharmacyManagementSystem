using Microsoft.AspNetCore.Mvc;
using PharmacyManagementSystem.Dtos.EmailDto;
using PharmacyManagementSystem.Services.EmailService;

namespace PharmacyManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailService : ControllerBase
    {
        private readonly IEmailService _emailService;
        public EmailService(IEmailService emailService)
        {
            this._emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(EmailDto request)
        {
            try
            {
                //await _emailService.SendEmail(request);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }
    }
}

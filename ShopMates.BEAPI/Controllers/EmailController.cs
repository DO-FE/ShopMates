using Microsoft.AspNetCore.Mvc;
using MimeKit;
using ShopMates.Application.Mail;
using ShopMates.ViewModels.Mail;
using System.Net.Mail;

namespace ShopMates.BEAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : Controller
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService) 
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody]EmailViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var email = await _emailService.SendMail(request);
            if (email == -1)
            {
                return BadRequest("Can not send mail");
            }
            return Ok();
        }
    }
}

using ECommerce.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ECommerce.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMailer _mailer;

        public UserController(IMailer mailer)
        {
            _mailer = mailer;
        }
 

        [HttpPost]
        public async Task<bool> Post(string email, string subject, string body)
        {
            return await _mailer.SendEmailAsync(email, subject, body);
        }
    }

    
}

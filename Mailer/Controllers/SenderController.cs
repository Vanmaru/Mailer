using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Mailer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SenderController : ControllerBase
    {
        private readonly ILogger<SenderController> _logger;
        public SenderController(ILogger<SenderController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public SendStatus[] Get(string messageText, string messageSubject, string recipients)
        {
            string[] addresses = System.IO.File.ReadAllLines(@$"{recipients}");
            SendStatus[] sendStatuses = new SendStatus[addresses.Length];
            int sendedCount = 0;
            foreach (var item in addresses)
            {
                var fromAddress = new MailAddress("ken.abaragi@gmail.com");
                var toAddress = new MailAddress(item);
                string password = "12qw34er56ty";
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(fromAddress.Address, password),
                    Timeout = 20000
                };
                var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = messageSubject,
                    Body = messageText
                };
                bool res = true;
                try
                {
                    smtp.Send(message);
                }
                catch (Exception e)
                {
                    res = false;
                    throw new SmtpException("email not sent", e);
                }
                sendStatuses[sendedCount] = new SendStatus
                {
                    Result = res,
                    Recipient = toAddress.Address
                };
            }
            return sendStatuses;
        }
    }
}

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

        static MailAddress senderAddress = new("ken.abaragi@gmail.com");
        static string password = "12qw34er56ty";
        private NetworkCredential sender = new(senderAddress.Address, password);
        public SenderController(ILogger<SenderController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public bool Get(string login, string p)
        {
            bool resultInfo;
            try
            {
                MailAddress mail = new(login);
                senderAddress = mail;
                password = p;
                resultInfo = true;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                resultInfo = false;
            }
            return resultInfo;
        }
        [HttpGet]
        public SendStatus[] Get(string messageText, string messageSubject, string recipients)
        {
            string[] addresses = System.IO.File.ReadAllLines(@$"{recipients}");
            SendStatus[] sendStatuses = new SendStatus[addresses.Length];
            int sendedCount = 0;
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = sender,
                Timeout = 20000
            };
            foreach (var item in addresses)
            {
                MailAddress toAddress = new(item);
                MailMessage message = new(senderAddress, toAddress)
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

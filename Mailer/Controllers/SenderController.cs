using Mailer.Models;
using Mailer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
namespace Mailer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SenderController : ControllerBase
    {
        private readonly ISendingRepository _sendingRepository;

        static MailAddress senderAddress = new("ken.abaragi@gmail.com");
        static string _password = "12qw34er56ty";
        private NetworkCredential sender = new(senderAddress.Address, _password);
        public SenderController(ISendingRepository sendingRepository)
        {
            _sendingRepository = sendingRepository;
        }
        [HttpGet("{login}")]
        public string Get(string login, string password)
        {
            try
            {
                MailAddress mail = new(login);
                senderAddress = mail;
                _password = password;
                NetworkCredential credential = new(login, password);
                sender = credential;
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return "Logged in";
        }
        [HttpGet]
        public async Task<IEnumerable<SendStatus>> GetSendingInfo()
        {
            return await _sendingRepository.Get();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SendStatus>> GetSendingInfo(int id)
        {
            return await _sendingRepository.Get(id);
        }
        [HttpPost]
        public async Task<ActionResult<SendStatus>> PostSending([FromBody]SendStatus sendStatus)
        {
            var newSending = await _sendingRepository.Create(sendStatus);
            return CreatedAtAction(nameof(GetSendingInfo), new { id = newSending.Id }, newSending);
        }
        //[HttpDelete]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    var toDelete = await _sendingRepository.Delete(id); //Broken
        //    if (toDelete==true)
        //    {
        //        return NotFound();
        //    }
        //    await _sendingRepository.Delete(toDelete.Id);
        //    return NoContent();
        //}
        //[HttpPost("{message}")]
        //public async Task<IEnumerable<SendStatus>> PostMessage(string messageText, string messageSubject, string recipients)
        //{
        //    var smtp = new SmtpClient
        //    {
        //        Host = "smtp.gmail.com",
        //        Port = 587,
        //        EnableSsl = true,
        //        DeliveryMethod = SmtpDeliveryMethod.Network,
        //        Credentials = sender,
        //        Timeout = 20000
        //    };
            
        //    using (SendingContext db = new())
        //    {
        //        foreach (var item in addresses)
        //        {
        //            MailAddress toAddress = new(item);
        //            MailMessage message = new(senderAddress, toAddress)
        //            {
        //                Subject = messageSubject,
        //                Body = messageText
        //            };
        //            bool res = true;
        //            sendStatuses[sendedCount] = new SendStatus
        //            {
        //                Id = sendedCount + 1,
        //                Result = res,
        //                Recipient = toAddress.Address
        //            };
        //            db.Sendings.Add(sendStatuses[sendedCount]);
        //            db.SaveChanges();
        //            //sendStatuses[sendedCount].Status = Status.Sending;
        //            try
        //            {
        //                smtp.Send(message);
        //                //sendStatuses[sendedCount].Status = Status.Sended;
        //            }
        //            catch (Exception e)
        //            {
        //                res = false;
        //                //sendStatuses[sendedCount].Status = Status.Not_sent;
        //                throw new SmtpException("email not sent", e);
        //            }
        //            sendedCount++;
        //        }
        //    }

        //    return await _sendingRepository.Get();
        //}
    }
}

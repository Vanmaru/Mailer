using System;

namespace Mailer
{
    public enum Status { Preparing, Sending, Sended, Not_sent }
    public class SendStatus
    {
        public int Id { get; set; }
        public bool Result { get; set; }
        public string Recipient { get; set; }
        public Status Status => Status.Preparing;
    }
}

using System;

namespace Mailer
{
    public class SendStatus
    {
        public bool Result { get; set; }
        public string Recipient { get; set; }
        public string Status => Result ? "Sended" : "Error";
    }
}

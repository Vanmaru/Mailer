using System;

namespace Mailer
{
    public class SendStatus
    {
        public bool Result { get; set; }
        public string Recipient { get; set; }
        public string Status => Result ? "Sended" : "Error";

        //public DateTime Date { get; set; }

        //public int TemperatureC { get; set; }

        //public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        //public string Summary { get; set; }
    }
}

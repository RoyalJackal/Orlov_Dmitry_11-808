using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcSn.Sender
{
    public class SmsSender : IMessageSender
    {
        public void Send()
        {
            Console.WriteLine("Check phone");
        }
    }
}

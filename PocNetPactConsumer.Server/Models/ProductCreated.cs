using System;
using System.Collections.Generic;
using System.Text;

namespace PocNetPactConsumer.Server.Models
{
    public class ProductCreated
    {
        public string Message { get; set; }

        public Dictionary<string, MessageAttribute> MessageAttributes { get; set; }
    }

    public class MessageAttribute
    {
        public string DataType { get; set; }
        public string StringValue { get; set; }
    }
}

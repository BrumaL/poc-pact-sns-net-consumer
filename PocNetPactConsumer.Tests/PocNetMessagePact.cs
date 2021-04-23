//using PactNet;
//using PactNet.Infrastructure.Outputters;
//using PactNet.PactMessage;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using Xunit.Abstractions;

//namespace PocNetPactConsumer.Tests
//{
      // PactNet message-pact feature branch class
//    public class PocNetMessagePact : IDisposable
//    {
//        private readonly IMessagePactBuilder _messagePactBuilder;
//        public IMessagePact MessagePact;

//        public PocNetMessagePact() 
//        {
//            _messagePactBuilder = new MessagePactBuilder(new PactConfig
//            {
//                SpecificationVersion = "3.0.0",
//                LogDir = $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}logs{Path.DirectorySeparatorChar}",
//                PactDir = $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}pacts{Path.DirectorySeparatorChar}",
//            })
//            .ServiceConsumer("MartinsNetMessageConsumer")
//            .HasPactWith("MartinsMessageProvider");

//            MessagePact = _messagePactBuilder.InitializePactMessage();
//        }

//        public void Dispose()
//        {
//            _messagePactBuilder.Build(); //NOTE: Will save the pact file once finished
//        }
//    }
//}

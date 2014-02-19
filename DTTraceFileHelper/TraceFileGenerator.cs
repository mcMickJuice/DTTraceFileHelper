using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTTraceFileHelper;

namespace DTTraceFileHelper
{
    public class TraceFileGenerator
    {
        private IAddressProvider _addressProvider;
        private string _incomingFileName;

        public TraceFileGenerator(IAddressProvider provider, string incomingFileName)
        {
            _addressProvider = provider;
            _incomingFileName = incomingFileName;
        }

        public string GetTraceResults()
        {
            var stringBuilder = new StringBuilder();
            var firstBlock = true;
            
            foreach (var line in System.IO.File.ReadAllLines(_incomingFileName))
            {
                ITraceResultBuilder traceResultBuilder;
                if (!firstBlock)
                {
                    stringBuilder.Append("\r\n");
                }
                var traceAddress = new TraceAddress(line);

                if (traceAddress.IsSuccessfulTrace)
                {
                    traceResultBuilder = new SuccessfulTraceResultBuilder(traceAddress);
                }
                else
                {
                    traceResultBuilder = new UnsuccessfulTraceResultBuilder(traceAddress);
                }

                stringBuilder.Append(traceResultBuilder.GetTraceResultBlock(_addressProvider));


                firstBlock = false;
            }

            return stringBuilder.ToString();
        }
    }
}

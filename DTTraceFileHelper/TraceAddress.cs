using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTTraceFileHelper.Helpers;

namespace DTTraceFileHelper
{
    public class TraceAddress
    {
        public string MatterBlock { get; set; }
        public string Matter { get; set; }
        public string DahlId { get; set; }
        public string AddressInfoId { get; set; }
        public string Name { get; set; }
        public string AddressToTrace { get; set; }

        public bool IsSuccessfulTrace
        {
            get
            {
                var result = RandomGenerator.GetRandomNumber(0, 3);
                return result < 2; //66% chance of success
            }
        }

        private readonly string _rawTraceAddress;

        private static Dictionary<string, TraceAddressFieldSpec> _importStringMap = new Dictionary<string, TraceAddressFieldSpec>()
    {
            {"MatterBlock", new TraceAddressFieldSpec {Order = 1, Position = 49, Length = 30}},
            //{"DahlId", new TraceAddressFieldSpec {Order = 1, Position = 1, Length = 1}},
            //{"AddressInfoId", new TraceAddressFieldSpec {Order = 1, Position = 1, Length = 1}},
            {"Name", new TraceAddressFieldSpec {Order = 1, Position = 79, Length = 27}},
            {"AddressToTrace", new TraceAddressFieldSpec {Order = 1, Position = 107, Length = 58}}

        };

        public TraceAddress(string rawTraceAddress)
        {
            _rawTraceAddress = rawTraceAddress;
            MapRawStringToObject();
        }

        private void MapRawStringToObject()
        {
            MatterBlock = GetSubstring(_importStringMap[HelperMethods.GetPropertyName(() => MatterBlock)]);
            //DahlId = GetSubstring(_importStringMap[HelperMethods.GetPropertyName(() => DahlId)]);
            //AddressInfoId = GetSubstring(_importStringMap[HelperMethods.GetPropertyName(() => AddressInfoId)]);
            Name = GetSubstring(_importStringMap[HelperMethods.GetPropertyName(() => Name)]);
            AddressToTrace = GetSubstring(_importStringMap[HelperMethods.GetPropertyName(() => AddressToTrace)]);

            SplitMatterBlock();
        }

        private string GetSubstring(TraceAddressFieldSpec fieldSpec)
        {
            return _rawTraceAddress.Substring(fieldSpec.Position, fieldSpec.Length);
        }

        private void SplitMatterBlock()
        {
            var splitBlock = MatterBlock.Split('_');
            Matter = splitBlock[0];
            DahlId = splitBlock[1];
            AddressInfoId = splitBlock[2].Replace(" ", "");
        }
    }
}

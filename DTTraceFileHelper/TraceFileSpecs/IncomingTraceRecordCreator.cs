using DTTraceFileHelper;
using DTTraceFileHelper.Helpers;

namespace DTTraceFileHelper.TraceFileSpecs
{
    public class IncomingTraceRecordCreator
    {
        private int _numberOfAddresses;
        private TraceAddress _traceAddress;
        private IAddressProvider _addressProvider;
        const string FirstStaticString = "H01EXPRESS0000382NX    UM00X20000";
        const string SecondStaticString = "0000000000X0         5833                   19770813        QYMNAB1 ";
        const string ThirdStaticString = "       36                  ";
        private const string AddressStaticString = "           1401 D                                                ";

        public IncomingTraceRecordCreator(int numberOfAddresses, TraceAddress traceAddress, IAddressProvider addressProvider)
        {
            _numberOfAddresses = numberOfAddresses;
            _traceAddress = traceAddress;
            _addressProvider = addressProvider;
        }

        public string GetTraceHeader()
        {
            var numberString = "0" + _numberOfAddresses;
            return FirstStaticString + numberString + SecondStaticString + _traceAddress.MatterBlock + ThirdStaticString;
        }


        public string GetAddressRecord(int recordNumber)
        {
            var address = _addressProvider.GetAddress();

            var returnAddress = "";

            if (recordNumber == 0)
            {
                returnAddress += "A0" + recordNumber + _traceAddress.Name.TruncateOrPad(27);
            }
            else
            {
                returnAddress += "A0" + recordNumber + HelperMethods.GenerateRepeatCharacter(' ', 27);
            }

            returnAddress += FormatAddress(address) + AddressStaticString;

            return returnAddress;
        }

        private string FormatAddress(Address addressToFormat)
        {
            string address = addressToFormat.Address1.TruncateOrPad(39);
            string city = addressToFormat.City.TruncateOrPad(15);
            string state = addressToFormat.State.TruncateOrPad(2);
            string zip = addressToFormat.Zip.TruncateOrPad(9);

            return address + city + state + zip; 
        }
    }
}

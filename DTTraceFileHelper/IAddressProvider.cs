using System.Collections.Generic;

namespace DTTraceFileHelper
{
    public interface IAddressProvider
    {
        Address GetAddress();
        List<Address> GetAddresses();
    }
}

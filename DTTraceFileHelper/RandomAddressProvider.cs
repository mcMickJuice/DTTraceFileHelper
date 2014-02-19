using System;
using System.Collections.Generic;
using System.Linq;
using DTTraceFileHelper.Helpers;

namespace DTTraceFileHelper
{
    public class RandomAddressProvider : IAddressProvider
    {
        private List<string> _addresses;
        private List<string> _cities;
        private List<string> _states;
        private int _maxRandomAddresses;

        public RandomAddressProvider()
        {
            _addresses = Config.GetValue("Addresses").Split(',').ToList();
            _cities = Config.GetValue("Cities").Split(',').ToList();
            _states = Config.GetValue("States").Split(',').ToList();
            _maxRandomAddresses = Int32.Parse(Config.GetValue("MaxNumberOfRandomAddresses"));
        }

        public Address GetAddress()
        {
            var address = new Address
            {
                Address1 = StreetNumberCreator(isZip: false) + ' '  + _addresses.SelectRandom(),
                City = _cities.SelectRandom(),
                State = _states.SelectRandom(),
                Zip = StreetNumberCreator(isZip: true)
            };

            return address;
        }

        public List<Address> GetAddresses()
        {
            var addressList = new List<Address>();

            for (var i = 0; i < _maxRandomAddresses; i++)
            {
                addressList.Add(GetAddress());
            }

            return addressList;
        }

        private string StreetNumberCreator(bool isZip)
        {
            string result = String.Empty;

            if (isZip)
            {
                result = RandomGenerator.GetRandomNumber(10000, 99999).ToString();
            }
            else
            {
                result = RandomGenerator.GetRandomNumber(1, 9999).ToString();
            }

            return result;
        }
    }
}

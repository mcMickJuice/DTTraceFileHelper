using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DTTraceFileHelper;
using DTTraceFileHelper.Helpers;
using DTTraceFileHelper.TraceFileSpecs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private IAddressProvider _addressProvider = new RandomAddressProvider();

        [TestMethod]
        public void CreateTraceAddress()
        {
            string outgoingTraceRecord =
                "XNNNNNNNU N00 3N3 1        00NN        00QYMNAB1 ROQ_00005931_3194             ANTHONY M PONCE            5175 N FELAND AVE UNIT 129      FRESNO         CA937115909570735833 ";

            var traceAddress = new TraceAddress(outgoingTraceRecord);

            Assert.AreEqual("ROQ",traceAddress.Matter);
            Assert.AreEqual("00005931",traceAddress.DahlId);
            Assert.AreEqual("3194",traceAddress.AddressInfoId);
        }

        [TestMethod]
        public void CreateTraceHeader()
        {
            string outgoingTraceRecord =
                "XNNNNNNNU N00 3N3 1        00NN        00QYMNAB1 ROQ_00005931_3194             ANTHONY M PONCE            5175 N FELAND AVE UNIT 129      FRESNO         CA937115909570735833 ";

            var traceAddress = new TraceAddress(outgoingTraceRecord);
            var traceRecordHelper = new IncomingTraceRecordCreator(5, traceAddress, _addressProvider);

            var header = traceRecordHelper.GetTraceHeader();

            Assert.AreEqual(160, header.Length);
            Assert.AreEqual(103,header.IndexOf("ROQ"));
        }

        [TestMethod]
        public void GetPropertyName()
        {
            string outgoingTraceRecord =
                "XNNNNNNNU N00 3N3 1        00NN        00QYMNAB1 ROQ_00005931_3194             ANTHONY M PONCE            5175 N FELAND AVE UNIT 129      FRESNO         CA937115909570735833 ";

            var traceAddress = new TraceAddress(outgoingTraceRecord);

            var propertyName = HelperMethods.GetPropertyName(() => traceAddress.AddressToTrace);

            Assert.AreEqual("AddressToTrace",propertyName);
        }

        [TestMethod]
        public void GenerateRepeatCharacter_SUCCESS()
        {
            var expectedResult = "GGG";
            var generatedResult = HelperMethods.GenerateRepeatCharacter('G', 3);

            Assert.AreEqual(expectedResult, generatedResult);
        }

        [TestMethod]
        public void GenerateRepeatCharacter_FAIL()
        {
            var expectedResult = "GGG";
            var generatedResult = HelperMethods.GenerateRepeatCharacter('G', 10);

            Assert.AreNotEqual(expectedResult, generatedResult);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GenerateRepeatCharacter_EXCEPTION()
        {
            var generatedResult = HelperMethods.GenerateRepeatCharacter('G', -1);
        }

        [TestMethod]
        public void GetConfigValue_SUCCESS()
        {
            var expectedResult = "TestValue";
            var actualResult = Config.GetValue("Test");

            Assert.AreEqual(expectedResult,actualResult);
        }

        [TestMethod]
        public void GetConfigValue_FAILURE()
        {
            var expectedResult = "rarg";
            var actualResult = Config.GetValue("Test");

            Assert.AreNotEqual(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void GetConfigValue_EXCEPTION()
        {
            Config.GetValue("Blarg");
        }

        [TestMethod]
        public void GetRandomCollectionValue_Success()
        {
            var genericList = new List<Address>
            {
                new Address(),
                new Address(),
                new Address()
            };

            var result = genericList.SelectRandom();
            var elementType = genericList.ElementAt(0).GetType();

            Assert.IsInstanceOfType(result, elementType);
        }

        [TestMethod]
        public void GetRandomCollectionValue_EMPTYCOLLECTION()
        {
            var genericList = new List<Address>
            {
            };

            var result = genericList.SelectRandom();

            Assert.IsTrue(object.Equals(result, default(Address)));
        }

        [TestMethod]
        public void AddressProviderGetAddress_SUCCESS()
        {
            var addressProvider = new RandomAddressProvider();
            var address = addressProvider.GetAddress();

            Assert.IsNotNull(address);
        }

        [TestMethod]
        public void TruncateOrPadString_TRUNCATE_SUCCESS()
        {
            string strToTest = "Thisisaverylongstringthat will be truncated";
            int length = 10;
            string expected = strToTest.Substring(0, length);

            string result = strToTest.TruncateOrPad(length);

            Assert.AreEqual(expected,result);
        }

        [TestMethod]
        public void TruncateOrPadString_PAD_SUCCESS()
        {
            string strToTest = "Short String";
            int length = 300;
            string expected = strToTest + HelperMethods.GenerateRepeatCharacter(' ', length - strToTest.Length);

            string result = strToTest.TruncateOrPad(length);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetAddressRecord_SUCCESS()
        {
            var traceAddress =
                new TraceAddress(
                    "XNNNNNNNU N00 3N3 1        00NN        00QYMNAB1 ROQ_00005931_3194             ANTHONY M PONCE            5175 N FELAND AVE UNIT 129      FRESNO         CA937115909570735833 ");
            var incomingAddressCreator = new IncomingTraceRecordCreator(4, traceAddress, _addressProvider);

            var incomingAddress = incomingAddressCreator.GetAddressRecord(1);

            var expectedLength = 160;
            var resultLength = incomingAddress.Length;

            Assert.AreEqual(expectedLength,resultLength);
        }

        [TestMethod]
        public void GetRandomNumber_SUCCESS()
        {
            var result = RandomGenerator.GetRandomNumber(0,10);

            Assert.IsInstanceOfType(result, typeof(int));
        }

        [TestMethod]
        public void GetRandomNumber_Exception()
        {
            try
            {
                var result = RandomGenerator.GetRandomNumber(5,1);
                Assert.Fail();
            }
            catch (IndexOutOfRangeException)
            {
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
    }
}

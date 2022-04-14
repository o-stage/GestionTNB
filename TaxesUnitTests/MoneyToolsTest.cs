using System;
using NUnit.Framework;
using TaxesV1;

namespace TaxesUnitTests
{
    [TestFixture]
    public class MoneyToolsTest
    {
        [Test]
        public void TestConvertToWords()
        {
            Assert.AreEqual("quinze", MoneyTools.ConvertToWords(15).Trim().ToLower());
            Assert.AreEqual("vingt cinq", MoneyTools.ConvertToWords(25).Trim().ToLower());
            Assert.AreEqual("quatre-vingt six", MoneyTools.ConvertToWords(86).Trim().ToLower());
            Assert.AreEqual("dix millions un mille quatre cent soixante neuf", MoneyTools.ConvertToWords(10001469).Trim().ToLower());
            Assert.AreEqual("quinze", MoneyTools.ConvertToWords(15).Trim().ToLower());
            Assert.AreEqual("quinze  et quatre-vingt seize  centimes", MoneyTools.ConvertToWords(15.96f).Trim().ToLower());        }
    }
}
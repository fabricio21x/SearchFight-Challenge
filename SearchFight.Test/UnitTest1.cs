using System;
using SearchFight.Test.Mocks;
using NUnit.Framework;

namespace SearchFight.Test
{
    [TestFixture]
    public class UnitTest1
    {

        private SearchEngineMock _runner;
        private SearchResultParserMock _parser;
        private SearchClientMock _client;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            _parser = new SearchResultParserMock();
            _client = new SearchClientMock();
            _runner = new SearchEngineMock(_parser, _client);
        }

        [TestFixtureTearDown]
        public void TestTearDown()
        {
            _runner = null;
            _parser = null;
            _client = null;
        }

        [SetUp]
        public void Setup()
        {         
                      
        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        public void SearchResultTest()
        {
            long result = _runner.ProcessQuery();
            Assert.AreEqual(11210000000, result,"Result is not being retrieved correctly");
        }

        [Test]
        public void ParseResultTest()
        {
            string resultString = _client.GetResultString();
            string result = _parser.Parse(resultString);
            Assert.AreEqual("11,210,000,000", result,"Result is not being parsed correctly");
        }
    }
}

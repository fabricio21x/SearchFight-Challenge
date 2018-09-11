using NUnit.Framework;
using SearchFight.Model;
using SearchFight.Model.Interfaces;
using SearchFight.Test.Mocks;

namespace SearchFight.Test.UnitTests
{
    [TestFixture]
    public class UnitTest1
    {

        private ISearchEngine _runner;
        private ISearchParser _parser;
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
            // this query is not relevant for the test as we are loading a response from a txt
            var query = new Query();
            var result = _runner.ProcessQuery(query);
            Assert.AreEqual(11210000000, result,"Result is not being retrieved correctly");
        }

        [Test]
        public void ParseResultTest()
        {
            var resultString = _client.GetResultString();
            var result = _parser.Parse(resultString);
            Assert.AreEqual("11,210,000,000", result,"Result is not being parsed correctly");
        }
    }
}

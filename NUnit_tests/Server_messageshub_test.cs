using NUnit.Framework;


namespace NUnit_tests
{
    [TestFixture]
    public class Messages_Tests
    {
        Server.MessagesHub a;
        /// <summary>
        /// before avery test
        /// </summary>
        [SetUp]
        public void Setup()
        {
            a = new Server.MessagesHub();
        }

        [Test]
        public void Test1()
        {
            a.Create_map(1, 50, 1, 1);

        }
        /// <summary>
        /// affter every test
        /// </summary>
        [TearDown]
        public void CleanUp()
        {
        }
    }
}
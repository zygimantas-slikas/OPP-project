using Client;
using NUnit.Framework;

namespace NUnit_tests
{
    [TestFixture]
    public class Rooms_list_view_objTests
    {
        [Test]
        public void TestMethod1()
        {
            // Arrange
            var rooms_list_view_obj = new Rooms_list_view_obj("1", "player", "20");

            // Act


            // Assert
            Assert.AreEqual(rooms_list_view_obj.map_id, "1");
            Assert.AreEqual(rooms_list_view_obj.players, "player");
            Assert.AreEqual(rooms_list_view_obj.map_size, "20");
        }
    }
}

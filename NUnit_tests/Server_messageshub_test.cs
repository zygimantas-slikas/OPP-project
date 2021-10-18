using NUnit.Framework;
using Server;
using System;
using System.Threading.Tasks;


namespace NUnit_tests
{
    [TestFixture]
    public class Messages_Tests
    {
        MessagesHub a;
        Room r1;
        /// <summary>
        /// before avery test
        /// </summary>
        [SetUp]
        public void Setup()
        {
            a = new MessagesHub();
            r1 = new Room(1, 4, 10, 1, 1);
        }

        [Test]
        [TestCase(2, 10000, 1, 1)]
        public void Create_map_test(Int32 players_count, Int32 map_size, Int32 level, int type)
        {
            Task map = a.Create_map(players_count, map_size, level, type);
            Assert.AreEqual(map.IsCompleted, true);

        }
        [Test]
        [TestCase(1, "player1")]
        public void Join_map_test(Int32 id, string name)
        {
            Task join = a.Join_map(id, name);
            Assert.AreEqual(join.IsCompleted, true);
        }
        [Test]
        [TestCase(1, 10, 10)]
        public void Move_test(Int32 map_id, Int32 x, Int32 y)
        {
             //Program.rooms
            Assert.AreEqual(r1.Id, map_id);
            Task move = a.Move(map_id, x, y);
            
            Assert.AreEqual(move.IsCompleted, true);
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
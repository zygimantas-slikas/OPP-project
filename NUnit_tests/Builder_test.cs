using NUnit.Framework;
using Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit_tests
{
    
    [TestFixture]
    class Builder_test
    {
        Room r1;
        /// <summary>
        /// before avery test
        /// </summary>
        [SetUp]
        public void Setup()
        {
            r1 = new Room(1, 4, 10, 1, 1);
        }
        [Test]
        [TestCase(10, 2)]
        public void MapBuilder_test(int map_size, int level)
        {
            r1.map.
            for (int i = 0; i < 4; i++)
            {
                r1.Add_player(con_id, name);
                Assert.AreEqual(r1.current_players, i + 1);
                Assert.AreEqual(r1.players.Count, i + 1);
            }
            Assert.That(r1.map[0, 0].Player_Standing, Is.Not.Null);
            Assert.That(r1.map[0, 9].Player_Standing, Is.Not.Null);
            Assert.That(r1.map[9, 0].Player_Standing, Is.Not.Null);
            Assert.That(r1.map[9, 9].Player_Standing, Is.Not.Null);
        }
    }
}

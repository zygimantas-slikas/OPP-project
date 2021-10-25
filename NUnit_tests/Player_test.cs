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
    public class Player_test
    {
        [Test]
        [TestCase("1", "player1", 10, 10)]
        public void Player_constructor_test(string id, string name, int x, int y)
        {
            LinkedList<Item> items = new LinkedList<Item>();
            Player player = new Player(id, name, x, y, items);
            Assert.AreEqual(player.Con_id, id);
            Assert.AreEqual(player.Name, name);
            Assert.AreEqual(player.X, x);
            Assert.AreEqual(player.Y, y);
            Assert.AreEqual(player.Inventory, items);
        }
    }
}

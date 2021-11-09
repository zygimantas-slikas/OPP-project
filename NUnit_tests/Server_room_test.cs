using NUnit.Framework;
using Server;

namespace NUnit_tests
{
    [TestFixture]
    public class Room_Tests
    {
        Room r1;
        /// <summary>
        /// before every test
        /// </summary>
        [SetUp]
        public void Setup()
        {
            r1 = new Room(1, 4, 10, 1, 1);
        }
        [Test]
        [TestCase(1, 4, 50, 1, 1)]
        [TestCase(1, 2, 20, 2, 2)]
        public void Constructor_test(int id, int players_count, int map_size, int level, int type)
        {
            Room r2 = new Room(id, players_count, map_size, level, type);
            Assert.AreEqual(r2.map.Length, map_size * map_size);
            Assert.AreEqual(r2.players.Capacity, players_count);
            Assert.AreEqual(id, r2.Id);
            Assert.AreEqual(players_count, r2.max_players);
            Assert.AreEqual(map_size, r2.map_size);
            Assert.AreEqual(r2.state, Room.Room_satate.empty);
            Assert.AreEqual(r2.level, level);
            if (level == 1)
            {
                for (int i = 0; i < map_size; i++)
                {
                    for (int j = 0; j < map_size; j++)
                    {
                        if (r2.map[i, j].Loot != null)
                        {
                            Assert.That(r2.map[i, j].Loot.Type, Is.Not.EqualTo("RedBerry"));
                            Assert.That(r2.map[i, j].Loot.Type, Is.Not.EqualTo("RedGun"));
                            Assert.That(r2.map[i, j].Loot.Type, Is.Not.EqualTo("RedMedicKit"));
                        }
                    }
                }
            }
            else if (level == 2)
            {
                for (int i = 0; i < map_size; i++)
                {
                    for (int j = 0; j < map_size; j++)
                    {
                        if (r2.map[i, j].Loot != null)
                        {
                            Assert.That(r2.map[i, j].Loot.Type, Is.Not.EqualTo("BlueBerry"));
                            Assert.That(r2.map[i, j].Loot.Type, Is.Not.EqualTo("BlueGun"));
                            Assert.That(r2.map[i, j].Loot.Type, Is.Not.EqualTo("BlueMedicKit"));
                        }
                    }
                }
            }
        }
        [Test]
        [TestCase("connection_id", "name")]
        public void Add_player_test(string con_id, string name)
        {
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
        [Test]
        public void Remove_player_test()
        {
            r1.Add_player("connection_id_1", "name");
            r1.Remove_player("connection_id_1");
            Assert.That(r1.map[0, 0].Player_Standing, Is.Null);
            Assert.AreEqual(r1.current_players, 0);
            Assert.AreEqual(r1.players.Count, 0);
        }
        [Test]
        public void To_json_test()
        {
            r1.Add_player("connection_id_1", "name");
            string json = r1.To_Json();
            Assert.That(json, Does.Not.Match("\"Con_id\":"));
        }
        [Test]
        public void Players_to_json_test()
        {
            r1.Add_player("connection_id_1", "name");
            string json = r1.Players_to_Json();
            Assert.That(json, Does.Not.Match("\"Con_id\":"));
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
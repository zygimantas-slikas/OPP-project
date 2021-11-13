using NUnit.Framework;
using Server;


namespace NUnit_tests
{
    [TestFixture]
    public class Room_Tests
    {
        Room r1;
        /// <summary>
        /// Parengimo metodas vykdomas prieš kiekvieną testavimo metodą
        /// Sukuria naują Room klasės objektą kurį vėliau testuoja kiti metodai
        /// </summary>
        [SetUp]
        public void Setup()
        {
            r1 = new Room(1, 4, 10, 1, 1);
        }
        /// <summary>
        /// Testuojamas naujo Room objekto sukūrimas naudojant klasės knostruktorių
        /// Tikrinama ar sukūrus naują objektą jo parametrai atitinka tuos kurie buvo paduoti metodui
        /// Tikrinama ar nesukurta netinkamo tipo Item objektų 
        /// </summary>
        /// <param name="id">identifikacinis numeris</param>
        /// <param name="players_count">maksimalus žaidėjų skaičius kurį prima objektas</param>
        /// <param name="map_size">kuriamo žemėlapio dydis</param>
        /// <param name="level">žaidimo lygis- žemėlapyje išmėtytų objektų spalva</param>
        /// <param name="type">žemėlapio langelių tipų pasiskirstymo tipas</param>
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
        /// <summary>
        /// Testas tikrina ar į žemėlapį pridedant naujus žaidėjus jų objektai padedami į 
        /// tinkamas pozicijas. Kievienas žaidėjas žaidimą pradeda viename iš 4 kampų
        /// pridėjus 4 žaidėjus visi kampai turėtų laikyti ne null objektą.
        /// </summary>
        /// <param name="con_id">prisijungusio žaidėjo identifikavimo kodas</param>
        /// <param name="name">žaidėjo pasirinktas vardas</param>
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
        /// <summary>
        /// Testas tikirna ar pridėjus ir pašalinus žaidėją is Room objekto visi parametrai 
        /// atstatomi taisingai. Kampiniai langeliai turi vėl neturėti žaidėjo - null, 
        /// Room objekto žaidėjų sarašas turi laikyti 0 objektų.
        /// </summary>
        [Test]
        public void Remove_player_test()
        {
            r1.Add_player("connection_id_1", "name");
            r1.Remove_player("connection_id_1");
            Assert.That(r1.map[0, 0].Player_Standing, Is.Null);
            Assert.AreEqual(r1.current_players, 0);
            Assert.AreEqual(r1.players.Count, 0);
        }
        /// <summary>
        /// Testuojamas žemėlapio serilizavimas į JSON formatą. Išserilizuojame tekste
        /// neturėtų būti žaidėjų dauomenų lauko "Con_id" ir jo reikšmės, nes tai gali 
        /// pakenkti saugumui (žaidėjai SignalR identifikuojami pagal Con_id todėl jis 
        /// gali būti panaudotas prisijungimui pri kito žaidėjo paskyros)
        /// Testas patikrina ar Regex šablonas neranda atitikimų JSON rezultate.
        /// </summary>
        [Test]
        public void To_json_test()
        {
            r1.Add_player("connection_id_1", "name");
            string json = r1.To_Json();
            Assert.That(json, Does.Not.Match("\"Con_id\":"));
        }
        /// <summary>
        /// Testuojamas žaidėjų sąršo serilizavimas i JSON formatą.
        /// Testas patikrina ar Regex šablonas neranda atitikimų JSON rezultate.
        /// </summary>
        [Test]
        public void Players_to_json_test()
        {
            r1.Add_player("connection_id_1", "name");
            string json = r1.Players_to_Json();
            Assert.That(json, Does.Not.Match("\"Con_id\":"));
        }
        /// <summary>
        /// I map ideda nauja zaideja naudojant jo moka
        /// </summary>
        [Test]
        public void Add_player_moq_test()
        {
            var player_mock = new Moq.Mock<Player>("connection_1", "name_1");
            player_mock.Setup(p => p.Health).Returns(-50);
            r1.Add_player(player_mock.Object);
            Assert.AreEqual(r1.Check_players_helath(), 1);
            player_mock.Verify(p => p.Health, Moq.Times.Once());

        }
        /// <summary>
        /// Resursų atlaisvinimas po kiekvieno testo
        /// </summary>
        [TearDown]
        public void CleanUp()
        {
        }
    }
}
using NUnit.Framework;
using Server;
using System;
using System.Collections.Generic;
using AutoMoq;
using Moq;
namespace NUnit_tests
{
    [TestFixture]
    public class ItemTests
    {
        [Test]
        public void Clone_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            Berry berry = new Berry();
            MedicKit medickit = new MedicKit();
            Gun gun = new Gun();
            Trap trap = new Trap();

            // Act
            var result_berry = (Berry)berry.Clone();
            var result_medickit = (MedicKit)medickit.Clone();
            var result_gun = (Gun)gun.Clone();
            var result_trap = (Trap)trap.Clone();

            
            Assert.AreEqual(result_berry.Points, berry.Points);
            Assert.AreNotEqual(result_berry.GetHashCode(), berry.GetHashCode());

            Assert.AreEqual(result_medickit.Heal, medickit.Heal);
            Assert.AreNotEqual(result_medickit.GetHashCode(), medickit.GetHashCode());

            Assert.AreEqual(result_gun.Damage, gun.Damage);
            Assert.AreEqual(result_gun.Ammo, gun.Ammo);
            Assert.AreNotEqual(result_gun.GetHashCode(), gun.GetHashCode());

            Assert.AreEqual(result_trap.Damage, trap.Damage);
            Assert.AreNotEqual(result_trap.GetHashCode(), trap.GetHashCode());
        }
        [Test]
        public void PickUpEffect_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            List<Item> items = new List<Item>();
            BlueBerry berry = new BlueBerry();
            var player = new Player(1, "1", "player1", 10, 10, items);
            var player2 = new Player(1, "1", "player2", 10, 10, items);

            // Act
            berry.PickupEffect(player);

            // Assert
            Assert.AreNotEqual(player2.Points, player.Points);
        }
        [Test]
        public void PickUpEffectHealth_StateUnderTest_ExpectedBehavior()
        {
            List<Item> items = new List<Item>();
            BlueMedicKit medickit = new BlueMedicKit();
            var player = new Player(1, "1", "player1", 10, 10, items);
            var player2 = new Player(1, "1", "player2", 10, 10, items);

            medickit.PickupEffect(player);
            Assert.AreNotEqual(player.Health, player2.Health);
        }

        [Test]
        public void Add_item_moq_test()
        {
            List<Item> items = new List<Item>();
            BlueBerry berry = new BlueBerry();

            var player = new Player(1, "1", "player1", 10, 10, items);

            berry.PickupEffect(player);

            var mockPlayer = new Mock<Player>("1", "player");

            mockPlayer.Object.Addpoints(200);

            Assert.AreEqual(mockPlayer.Object.Points, player.Points);
        }
    }
}

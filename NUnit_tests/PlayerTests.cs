using NUnit.Framework;
using Server;
using System;
using System.Collections.Generic;

namespace NUnit_tests
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void addItem_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            List<Item> items = new List<Item>();
            Berry berry = new Berry();
            var player = new Player("1", "player1", 10, 10, items);
            Item item = berry;

            // Act
            player.addItem(
                item);

            // Assert
            Assert.IsNotEmpty(player.Inventory);
        }

        [Test]
        public void Addpoints_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var player = new Player("1", "player1", 10);
            int pts = 150;

            // Act
            player.Addpoints(
                pts);

            // Assert
            Assert.AreEqual(player.Points, 150);
        }

        [Test]
        public void AddDamage_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var player = new Player("1", "player1", 10);
            int hp = 30;

            // Act
            player.AddDamage(
                hp);

            // Assert
            Assert.AreEqual(player.Health, 70);
        }

        [Test]
        public void AddHealth_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var player = new Player("1", "player1", 10);
            int hp = 100;

            // Act
            player.AddHealth(
                hp);

            // Assert
            Assert.AreEqual(player.Health, 200);
        }
    }
}

using Client;
using Client.Strategy;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit_tests
{
    [TestFixture]
    class MoveAlgorithmTests
    {
        [Test]
        public void StepOnWaterTest()
        {
            // Arrange
            Player player = new Player("TestPlayer");
            GameSettings settings = GameSettings.GetInstance();
            // Act
            player.setStrategy(new MoveOnWater());
            player.move();
            // Assert
            Assert.AreEqual(settings.PlayerDelaySpeed, 800);
        }

        [Test]
        public void StepOutOfWaterTest()
        {
            // Arrange
            Player player = new Player("TestPlayer");
            GameSettings settings = GameSettings.GetInstance();
            // Act
            player.setStrategy(new MoveOnWater());
            player.move();
            player.setStrategy(new MoveOnGrass());
            player.move();
            // Assert
            Assert.AreEqual(settings.PlayerDelaySpeed, 100);
        }
    }
}

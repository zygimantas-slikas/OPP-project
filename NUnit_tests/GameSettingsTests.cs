using Client;
using NUnit.Framework;
using System;

namespace NUnit_tests
{
    [TestFixture]
    public class GameSettingsTests
    {
        [Test]
        public void SetSpeed_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var gameSettings = new GameSettings();
            int newSpeed = 150;

            // Act
            gameSettings.SetSpeed(
                newSpeed);

            // Assert
            Assert.AreEqual(gameSettings.PlayerDelaySpeed, 150);
        }
    }
}

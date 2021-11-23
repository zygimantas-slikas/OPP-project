using AutoMoq;
using Moq;
using NUnit.Framework;
using Server;
using System;
using System.Collections.Generic;

namespace NUnit_tests
{
    [TestFixture]
    public class PlayerTestsMock
    {
        [Test]
        public void Addpoints()
        {
            var player_mock = new Mock<Player>("1", "player1");
            int pts = 150;

            player_mock.Object.Addpoints(pts);

            Assert.AreEqual(player_mock.Object.Points, 150);
        }
    }
}

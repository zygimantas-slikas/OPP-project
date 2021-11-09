using Client;
using Client.Decorator;
using Client.Adapter;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace NUnit_tests
{
    [TestFixture]
    class TrapTests
    {
        [Test]
        public void TrapDamageTest()
        {
            // Arrange
            Player player = new Player("TestPlayer"); //start health = 100
            int expectedDamage = 0;
            Item item;
            // Act
            item = new VisibleTrapAdapter(new VisibleTrap());
            item.PickupEffect(player);
            expectedDamage += new VisibleTrapAdapter(new VisibleTrap()).Damage;

            item = new InvisibleTrapAdapter(new InvisibleTrap());
            item.PickupEffect(player);
            expectedDamage += new InvisibleTrapAdapter(new InvisibleTrap()).Damage;
            // Assert
            Assert.AreEqual(player.Health, 100 - expectedDamage);
        }

        [Test]
        public void FireDamageTest()
        {
            // Arrange
            Player player = new Player("TestPlayer"); //start health = 100
            int timesStepped = 0;
            int expectedDamage = 0;
            Item item;
            // Act
            item = new HighFireDecorator(new Fire(timesStepped++));
            item.PickupEffect(player);
            expectedDamage += new HighFireDecorator(new Fire(timesStepped)).Damage;

            item = new MediumFireDecorator(new Fire(timesStepped++));
            item.PickupEffect(player);
            expectedDamage += new MediumFireDecorator(new Fire(timesStepped)).Damage;

            item = new LowFireDecorator(new Fire(timesStepped++));
            item.PickupEffect(player);
            expectedDamage += new LowFireDecorator(new Fire(timesStepped)).Damage;
            // Assert
            Assert.AreEqual(player.Health, 100 - expectedDamage);
        }

        [Test]
        public void FireStepsTest()
        {
            // Arrange
            Item fireItem1;
            Player player = new Player("TestPlayer");
            // Act
            fireItem1 = new HighFireDecorator(new Fire(0));
            fireItem1.PickupEffect(player);
            fireItem1.PickupEffect(player);
            fireItem1.PickupEffect(player);
            // Assert
            Assert.AreEqual(fireItem1.GetNumber(), 3);
        }
    }
}

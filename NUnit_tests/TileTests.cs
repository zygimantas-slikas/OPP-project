using NUnit.Framework;
using Server;

namespace NUnit_tests
{
    [TestFixture]
    public class TileTests
    {
        [Test]
        public void Clone_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var tile = new Tile(Tile.Tile_type.grass);

            // Act
            var result = tile.Clone();

            // Assert
            //Assert.AreEqual(result.Surface, tile.Surface);
            //Assert.AreNotEqual(result.GetHashCode(), tile.GetHashCode());
        }
    }
}

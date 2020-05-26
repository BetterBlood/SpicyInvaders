using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpicyInvader_V_01;

namespace SpicyInvader_V_01Tests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void IsLostTest()
        {
            //Arrange
            Game game = new Game();
            bool obtenu;

            //Act
            obtenu = game.IsLost();

            //Assert
            Assert.IsFalse(obtenu);
        }
    }
}

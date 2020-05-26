using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpicyInvader_V_01;

namespace SpicyInvader_V_01Tests
{
    [TestClass]
    public class InvaderTests
    {
        [TestMethod]
        public void IsAMissileNotFiredTest()
        {
            //Arrange
            Invader invader = new Invader("salut4je4suisuneshape", new Position(0, 0), 3, true);
            bool obtenu;

            //Act
            obtenu = invader.IsAMissileNotFired();

            //Assert
            Assert.IsTrue(obtenu);
        }

        [TestMethod]
        public void GetMissilesCapacityTest()
        {
            //Arrange
            int expected = 3;
            Invader invader = new Invader("salut4je4suisuneshape", new Position(0, 0), expected, true);
            int obtenu;

            //Act
            obtenu = invader.GetMissilesCapacity();

            //Assert
            Assert.AreEqual(expected, obtenu, "le nombre n'est pas correct !");
        }

        [TestMethod]
        public void HowManyMissilesLeftTest()
        {
            //Arrange
            int expected = 3;
            Invader invader = new Invader("salut4je4suisuneshape", new Position(0, 0), expected, true);
            int obtenu;

            //Act
            obtenu = invader.HowManyMissilesLeft();

            //Assert
            Assert.AreEqual(expected, obtenu, "le nombre n'est pas correct !");
        }

        [TestMethod]
        public void GetXTest()
        {
            //Arrange
            int expected = 5;
            Invader invader = new Invader("salut4je4suisuneshape", new Position(expected, 0), 3, true);
            int obtenu;

            //Act
            obtenu = invader.GetX();

            //Assert
            Assert.AreEqual(expected, obtenu, "le nombre n'est pas correct !");
        }

        [TestMethod]
        public void GetYTest()
        {
            //Arrange
            int expected = 5;
            Invader invader = new Invader("salut4je4suisuneshape", new Position(0, expected), 3, true);
            int obtenu;

            //Act
            obtenu = invader.GetY();

            //Assert
            Assert.AreEqual(expected, obtenu, "le nombre n'est pas correct !");
        }

        [TestMethod]
        public void GetHeightTest()
        {
            //Arrange
            int expected = 3;
            Invader invader = new Invader("salut4je4suisuneshape", new Position(0, 0), 4, true);
            int obtenu;

            //Act
            obtenu = invader.GetHeight();

            //Assert
            Assert.AreEqual(expected, obtenu, "le nombre n'est pas correct !");
        }

        [TestMethod]
        public void ToStringTest()
        {
            //Arrange
            string expected = "Enemy";
            Invader invader = new Invader("salut4je4suisuneshape", new Position(0, 0), 4, true);
            string obtenu;

            //Act
            obtenu = invader.ToString();

            //Assert
            Assert.AreEqual(expected, obtenu, "le nombre n'est pas correct !");
        }
    }
}

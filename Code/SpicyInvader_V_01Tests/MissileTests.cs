/*
 * ETML
 * Auteur : Jeremiah, Adrian, Laetitia et Toine
 * Date : de Février à Mars 2020
 * Desciption : la classe de Test sur la classe Missile
 */

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpicyInvader_V_01;

namespace SpicyInvader_V_01Tests
{
    [TestClass]
    public class MissileTests
    {
        [TestMethod]
        public void GetXTest()
        {
            //Arrange
            int expected = -1;
            Missile missile = new Missile(EnumMissileType.Normal, EnumDirection.DOWN);
            int obtenu;

            //Act
            obtenu = missile.GetX();

            //Assert
            Assert.AreEqual(expected, obtenu, "le nombre n'est pas correct !");
        }

        [TestMethod]
        public void GetYTest()
        {
            //Arrange
            int expected = -1;
            Missile missile = new Missile(EnumMissileType.Normal, EnumDirection.DOWN);
            int obtenu;

            //Act
            obtenu = missile.GetY();

            //Assert
            Assert.AreEqual(expected, obtenu, "le nombre n'est pas correct !");
        }
    }
}

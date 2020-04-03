/*
 * ETML
 * Auteur : Jeremiah, Adrian, Laetitia et Toine
 * Date : Mars 2020
 * Desciption : la classe Ally
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    /// <summary>
    /// Class Ally
    /// </summary>
    abstract public class Ally : Entity
    {
        private int _maxLifePoint;

        /// <summary>
        /// Constructeur renseigné
        /// </summary>
        /// <param name="a_shape"></param>
        /// <param name="a_position"></param>
        /// <param name="a_nbrMissile"></param>
        public Ally(string a_shape, Position a_position, int a_nbrMissile) : this (a_shape, a_position, a_nbrMissile, 3) { }

        /// <summary>
        /// Constructeur renseigné
        /// </summary>
        /// <param name="a_shape"></param>
        /// <param name="a_position"></param>
        /// <param name="a_nbrMissile"></param>
        /// <param name="a_lifePoint"></param>
        public Ally(string a_shape, Position a_position, int a_nbrMissile, int a_lifePoint) : base(a_shape, a_position, a_nbrMissile, EnumDirection.UP)
        {
            _lifePoints = a_lifePoint;
            _maxLifePoint = _lifePoints;
        }

        /// <summary>
        /// Retourne l'état d'une flotte
        /// </summary>
        /// <param name="a_fleet"></param>
        /// <returns></returns>
        public bool IsDead(Fleet a_fleet) // TODO implémenter ça dans Ally je pense !
        {
            foreach (Enemy enemy in a_fleet.GetMembers())
            {
                if (enemy.GetPositions()[0].Y >= _position.Y)
                {
                    return true;
                }
            }

            return _lifePoints <= 0 ? true : false;
        }

        /// <summary>
        /// retourne une chaine de caractère
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Ally";
        }

        /// <summary>
        /// retourne une chaine de caractère contanant les info sur l'état de l'unité
        /// </summary>
        /// <param name="a_separator">séparateur à utiliser si plusieurs infos sont retournées</param>
        /// <returns></returns>
        public string GetSaveStat(string a_separator)
        {
            string innerSeparator = ".";

            //exemple : 
            // string retour = "";
            // retour += "life" + innerSeparator + _lifePoints;
            // retour += a_separator;
            // retour += "missileNumber" + innerSeparator + _missiles.Size;

            return "life" + innerSeparator + _lifePoints;
        }

        public void UpgradLife()
        {
            _maxLifePoint++;
        }

        public void Heal()
        {
            if(_lifePoints < _maxLifePoint)
            {
                _lifePoints++;
            }
        }

        public int GetMaxLife()
        {
            return _maxLifePoint;
        }
    }
}

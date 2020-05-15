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
        public bool IsDead(Fleet a_fleet)
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
            string innerSeparator1 = "/";
            string innerSeparator2 = ".";

            //exemple : 
            // string retour = "";
            // retour += "life" + innerSeparator + _lifePoints;
            // retour += a_separator;
            // retour += "missileNumber" + innerSeparator + _missiles.Size;

            // TODO faudrai aussi ajouter la puissance des missiles etc etc...
            return  "life" + innerSeparator2 + _lifePoints + innerSeparator1 + 
                    "missile_number" + innerSeparator2 + _missiles.Count + innerSeparator1 + 
                    "max_life" + innerSeparator2 + _maxLifePoint; 
        }

        /// <summary>
        /// Augemnt le nombre de points de vie
        /// </summary>
        public void UpgradLife()
        {
            _maxLifePoint++;
        }

        /// <summary>
        /// Soigne le joueur
        /// </summary>
        public void Heal()
        {
            if(_lifePoints < _maxLifePoint)
            {
                _lifePoints++;
            }
        }

        /// <summary>
        /// Retourne le nombre maximal de PV
        /// </summary>
        /// <returns></returns>
        public int GetMaxLife()
        {
            return _maxLifePoint;
        }

        /// <summary>
        /// Retourne la position de tire
        /// </summary>
        /// <returns></returns>
        protected override Position GetFirePosition() // TODO : voir pour utiliser cette méthode quand l'allié tire (permet d'avoir des vaisseaux de différentes tailles)
        {
            return new Position(_position.X + GetHorizontalHightSize() / 2, _position.Y - 1);
        }
    }
}

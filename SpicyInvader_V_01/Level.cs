/*
 * ETML
 * Auteur : Jeremiah, Adrian, Laetitia et Toine
 * Date : Mars 2020
 * Desciption : la classe Level
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    /// <summary>
    /// Class Level
    /// </summary>
    public class Level
    {
        /// <summary>
        /// Attributs
        /// </summary>
        private int _lvlNumber;
        private Fleet _fleet;

        private bool _bossStage;

        /// <summary>
        /// Constructeur renseigné
        /// </summary>
        /// <param name="a_lvl"></param>
        public Level(int a_lvl)
        {
            _lvlNumber = a_lvl;
            
            if (_lvlNumber % 5 == 0)
            {
                _bossStage = true;
            }
            else
            {
                _bossStage = false;
            }

            _fleet = new Fleet(a_lvl, _bossStage);

        }

        /// <summary>
        /// Retourne un objet de type Fleet
        /// </summary>
        /// <returns></returns>
        public Fleet GetFleet()
        {
            return _fleet;
        }

        /// <summary>
        /// Retourne le level actuel
        /// </summary>
        /// <returns></returns>
        public int GetLevel()
        {
            return _lvlNumber;
        }

        /// <summary>
        /// Augmentation du level
        /// </summary>
        public void LevelUP()
        {
            _lvlNumber++;

            if (_lvlNumber % 5 == 0)
            {
                _bossStage = true;
            }
            else
            {
                _bossStage = false;
            }

            _fleet = new Fleet(_lvlNumber, _bossStage);
        }
    }
}

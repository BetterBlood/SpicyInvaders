/*
 * ETML
 * Auteur : Jeremiah, Adrian, Laetitia et Toine
 * Date : Janvier à Mai 2020
 * Desciption : la classe Level
 */

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
        /// construceteur par défaut appellant le constructeur renseigné avec 1 comme paramètre
        /// </summary>
        public Level() : this (1) { }

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

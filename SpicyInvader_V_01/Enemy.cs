/*
 * ETML
 * Auteur : Jeremiah, Adrian, Laetitia et Toine
 * Date : Janvier à Mai 2020
 * Desciption : la classe enemy
 */
using System;
using System.Collections.Generic;

namespace SpicyInvader_V_01
{
    /// <summary>
    /// Class Enemy
    /// </summary>
    abstract public class Enemy : Entity
    {
        /// <summary>
        /// Attributs
        /// </summary>
        protected int _pointNumber; // protected car réassigné suivant les points de l'ennemi : Ex :Invader = 1 pts, Boss = lvl*3 pts
        private int _upgradeLvl;
        private static bool _rightDirection;
        private int _startYPosition;
        public const int _MAX_FIRE_RANGE = 39;

        private bool _isInFrontLane;

        private static bool _isChange = false;

        private static Random _random;

        /// <summary>
        /// Constructeur renseigné
        /// </summary>
        /// <param name="a_shape"></param>
        /// <param name="a_position"></param>
        /// <param name="a_nbrMissile"></param>
        /// <param name="a_rightDirection"></param>
        public Enemy(string a_shape, Position a_position, int a_nbrMissile, bool a_rightDirection) : base(a_shape, a_position, a_nbrMissile, EnumDirection.DOWN) 
        {
            _upgradeLvl = 0;
            _pointNumber = 1;
            _rightDirection = a_rightDirection;
            _startYPosition = a_position.Y;

            _isInFrontLane = false;
            _random = new Random();
        }

        /// <summary>
        /// Retourne le nombre de points que vaut l'ennemy
        /// </summary>
        /// <returns></returns>
        public int GetPoint()
        {
            return _pointNumber;
        }

        /// <summary>
        /// Retourne le texte "Enemy"
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Enemy";
        }

        /// <summary>
        /// Méthode détectant les bordures de l'écran
        /// </summary>
        public void DetectorBridge()
        {
            if (_rightDirection && _position.X + GetHorizontalHightSize() >= Console.WindowWidth - 1 || !_rightDirection && _position.X <= 1) // détecte si on arrive proche de l'écran
            {
                _isChange = true;
            }
        }

        /// <summary>
        /// Change de direction
        /// </summary>
        static public void ChangMove()
        {
            if (_isChange)
            {
                _rightDirection = _rightDirection ? false : true;
                Fleet.GoDown();
                _isChange = false;
            }
        }

        /// <summary>
        /// Provoque le déplacement de l'ennemi
        /// </summary>
        public void Move()
        {
            EnumDirection direction;

            if (_rightDirection)
            {
                direction = EnumDirection.RIGHT;
            }
            else
            {
                direction = EnumDirection.LEFT;
            }

            PrivateMove(direction);

            if (_position.Y - _startYPosition != Fleet._yFleet)
            {
                _position.Y++;
            }
        }

        /// <summary>
        /// Permet d'optenir la position du charactère de gauche de l'ennemi
        /// </summary>
        /// <returns></returns>
        public List<Position> GetPositions()
        {
            List<Position> positions = new List<Position>();

            for (int i = 0; i < GetHorizontalHightSize(); i++)
            {
                positions.Add(new Position(_position.X + i, _position.Y));
            }

            return positions;
        }

        /// <summary>
        /// Tentative de lancer de missile par un ennemi
        /// </summary>
        public void TryToFire()
        {
            if (_isInFrontLane)
            {
                if (_random.Next(10)%10 == 0)
                {
                    Fire(GetFirePosition());
                }
            }
        }

        /// <summary>
        /// Détermine si les ennemis sont en première ligne
        /// </summary>
        /// <param name="a_isAbleToFire"></param>
        public void SetFireStatue(bool a_isAbleToFire)
        {
            _isInFrontLane = a_isAbleToFire;
        }

        /// <summary>
        /// Retourne la position de tire
        /// </summary>
        /// <returns></returns>
        protected override Position GetFirePosition()
        {
            return new Position(_position.X + GetHorizontalHightSize()/2, _position.Y + GetHeight()); 
        }
        
        /// <summary>
        ///  Color l'ennemi selon sa position
        /// </summary>
        public override void Draw() // TODO : ptetre enlever ça c'est pour le débug
        {
            if (_isInFrontLane)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            
            base.Draw();
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        /// <summary>
        /// Augement la puissance des ennemis en fonction du niveau
        /// </summary>
        public void Upgrad()
        {
            switch(_upgradeLvl)
            {
                case 0: // première upgrade (correspond normalement au lvl 10) les ennemies ont maintenant 2 vies
                    _upgradeLvl++;
                    _lifePoints++;
                    break;

                case 1: // seconde upgrade (correspond au lvl 20) les ennemies ont maintenant 2 missiles simultanément
                    _upgradeLvl++;
                    UpgradWeaponSlot();
                    break;
                case 2: // première upgrade (correspond normalement au lvl 30) les ennemies ont maintenant 3 vies
                    _upgradeLvl++;
                    _lifePoints++;
                    break;
                default:
                    // nothing to do ?
                    break;
            }
        }

    }
}
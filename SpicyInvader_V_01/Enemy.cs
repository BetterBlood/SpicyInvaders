/*
 * ETML
 * Auteur : Jeremiah, Adrian, Laetitia et Toine
 * Date : Mars 2020
 * Desciption : la classe enemy
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _pointNumber = 1;
            _rightDirection = a_rightDirection;
            _startYPosition = a_position.Y;

            _isInFrontLane = false; // TODO : initialiser a false ! mais d'abord faire une méthode qui mets automatiquement le premier space ivader en frontlane !
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
        /// 
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
        /// 
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
        protected virtual Position GetFirePosition()
        {
            return new Position(_position.X + 2, _position.Y + 1); // TODO : redéfinir ça dans boss mot clé : new !
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
    }
}
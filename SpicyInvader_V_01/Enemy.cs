using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    abstract public class Enemy : Entity
    {
        protected int _pointNumber; // protected car réassigné suivant les points de l'ennemi : Ex :Invader = 1 pts, Boss = lvl*3 pts
        private static bool _rightDirection;
        private int _startYPosition;

        private bool _isInFrontLane;

        private static bool _isChange = false;

        private static Random _random;

        public Enemy(string a_shape, Position a_position, int a_nbrMissile, bool a_rightDirection) : base(a_shape, a_position, a_nbrMissile, EnumDirection.DOWN) 
        {
            _pointNumber = 1;
            _rightDirection = a_rightDirection;
            _startYPosition = a_position.Y;

            _isInFrontLane = false; // TODO : initialiser a false ! mais d'abord faire une méthode qui mets automatiquement le premier space ivader en frontlane !
            _random = new Random();
        }


        public int GetPoint()
        {
            return _pointNumber;
        }

        public override string ToString()
        {
            return "Enemy";
        }

        public void DetectorBridge()
        {
            if (_rightDirection && _position.X + GetHorizontalHightSize() >= Console.WindowWidth - 1 || !_rightDirection && _position.X <= 1) // détecte si on arrive proche de l'écran
            {
                _isChange = true;
            }
        }
        static public void ChangMove()
        {
            if (_isChange)
            {
                _rightDirection = _rightDirection ? false : true;
                Fleet.GoDown();
                _isChange = false;
            }
        }
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

        public List<Position> GetPositions()
        {
            List<Position> positions = new List<Position>();

            for (int i = 0; i < GetHorizontalHightSize(); i++)
            {
                positions.Add(new Position(_position.X + i, _position.Y));
            }

            return positions;
        }

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

        public void SetFireStatue(bool a_isAbleToFire)
        {
            _isInFrontLane = a_isAbleToFire;
        }

        protected virtual Position GetFirePosition()
        {
            return new Position(_position.X + 2, _position.Y + 1); // TODO : redéfinir ça dans boss mot clé : new !
        }
        
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
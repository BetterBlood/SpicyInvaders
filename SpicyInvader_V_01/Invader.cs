using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    public class Invader
    {
        static public int _xSize = 4;
        private int _ySize;

        private Position _position;
        private int _pointNumber;

        private bool _rightDirection;

        private Shape _shape;
        private int _speed;

        public Invader() : this(5, 0) { }

        public Invader(int a_xPos, int a_yPos) : this (a_xPos, a_yPos, true) { }

        public Invader(int a_xPos, int a_yPos, bool a_rightDisplay)
        {
            // TODO : implémenter des missile pour les invaders, voir pour faire qu'ils ne se tirent pas à travers et pour faire qu'ils puissent toucher le vaisseau
            _xSize = 4;
            _ySize = 1;

            _position = new Position(a_xPos, a_yPos);
            _pointNumber = 1;

            _shape = new Shape("=()=");

            _speed = 1;
            _rightDirection = a_rightDisplay;
        }

        public void Move()
        {
            Clear();

            string direction = "";

            if (_rightDirection)
            {
                direction = "right";
            }
            else
            {
                direction = "left";
            }

            if (_rightDirection && _position.X + _speed + _xSize -1 >= Console.WindowWidth || !_rightDirection && _position.X - _speed < 0)
            {
                _rightDirection = _rightDirection ? false : true;
                direction = "down";
            }

            PrivateMove(direction);
        }

        private void PrivateMove(string a_direction)
        {
            // TODO : on va surement enlever la speed et gérer la vitesse depuis le main ou le Game par rapport au lvl

            if (a_direction.Equals("right"))
            {
                for (int i = 0; i < _speed; i++)
                {
                    _position.X++;
                }
            }
            else if (a_direction.Equals("left"))
            {
                for (int i = 0; i < _speed; i++)
                {
                    _position.X--;
                }
            }
            else if (a_direction.Equals("down"))
            {
                for (int i = 0; i < _speed; i++)
                {
                    _position.Y++;
                }
            }
            else
            {
                //normalement les ennemis ne remonte pas mais au cas ou c'est ici qu'il faudrait gérer ça
            }
        }

        public void Draw()
        {
            _shape.Draw(new Position(_position.X, _position.Y));
        }

        public void Clear()
        {
            _shape.Clear(new Position(_position.X, _position.Y));
        }

        public List<Position> GetPositions()
        {
            List<Position> positions = new List<Position>();

            for (int i = 0; i < _xSize; i++)
            {
                positions.Add(new Position(_position.X + i, _position.Y));
            }

            return positions;
        }

        public int GetPoint()
        {
            return _pointNumber;
        }

    }
}

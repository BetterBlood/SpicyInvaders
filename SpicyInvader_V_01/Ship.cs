using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    public class Ship : Ally
    {
        private int _xPos;
        private int _yPos;

        private Shape _shape;
        private int _xSize;

        private int _speed;

        private List<Missile> _missiles;

        public Ship() : base ("_/-\\_4 \\*/ ", new Position(50, 26), 2)
        {
            _xPos = 50;
            _yPos = 21;

            _shape = new Shape("_/-\\_4 \\*/ ");

            _xSize = 5;
            _speed = 1;

            _missiles = new List<Missile>();

            InitBasesMissiles(2);
        }

        private void InitBasesMissiles(int a_missileNumber)
        {
            for (int i = 0; i < a_missileNumber; i++)
            {
                _missiles.Add(new Missile(EnumDirection.UP));
            }
        }

        public bool IsDead(Fleet a_fleet)
        {
            foreach(Invader invader in a_fleet.GetMembers())
            {
                if (invader.GetPositions()[0].Y >= _yPos)
                {
                    return true;
                }
            }

            return false;
        }

        public void PrivateMove(string a_direction)
        {
            Clear();

            if (a_direction.Equals("right"))
            {
                for (int i = 0; i < _speed; i++)
                {
                    if (_xPos + _xSize < Console.WindowWidth - 1)
                    {
                        _xPos++;
                    }
                }
            }
            else if (a_direction.Equals("left"))
            {
                for (int i = 0; i < _speed; i++)
                {
                    if (_xPos > 0)
                    {
                        _xPos--;
                    }
                }
            }
            else
            {
                //normalement pas possible de se déplacer de haut en bas, à voir....
            }
        }

        public int GetX()
        {
            return _xPos;
        }

        public int GetY()
        {
            return _yPos;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    class Ship
    {
        private int _xPos;
        private int _yPos;

        private string _shape;
        private int _xSize;

        private int _speed;

        private List<Missile> _missiles;

        public Ship()
        {
            _xPos = 50;
            _yPos = 21;

            _shape = "_/-\\_";
            _xSize = 4;
            _speed = 1;

            _missiles = new List<Missile>();

            InitBasesMissiles(2);
        }

        private void InitBasesMissiles(int a_missileNumber)
        {
            for (int i = 0; i < a_missileNumber; i++)
            {
                _missiles.Add(new Missile());
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

        public bool IsAMissileNotFired()
        {
            foreach (Missile missile in _missiles)
            {
                if (!missile.IsFired())
                {
                    Console.SetCursorPosition(30, 23);
                    return true;
                }
            }

            return false;
        }

        public int GetMissileYPos()
        {
            foreach (Missile missile in _missiles)
            {
                if (missile.IsFired())
                {
                    return missile.GetY();
                }
            }
            return -1;
        }

        public int GetMissilesCapacity()
        {
            return _missiles.Count();
        }

        public int HowManyMissilesLeft()
        {
            int nbrMissilesLeft = 0;

            foreach (Missile missile in _missiles)
            {
                if (!missile.IsFired())
                {
                    nbrMissilesLeft++;
                }
            }

            return nbrMissilesLeft;
        }

        public void Fire()
        {
            foreach (Missile missile in _missiles)
            {
                if (!missile.IsFired())
                {
                    missile.Fire(new Position(_xPos + 2, _yPos - 1));
                    new SoundPlayer("Sounds//LazerFire.wav").Play();
                    return;
                }
                else
                {
                    // nothing to do
                }
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

        public void Draw()
        {
            Console.SetCursorPosition(_xPos, _yPos);
            Console.Write(_shape);
        }

        public void Clear()
        {
            Console.SetCursorPosition(_xPos, _yPos);
            // Console.Write("     "); // TODO : modifier ça en fonction de la taille du vaisseau
            for (int i = 0; i < _shape.Length; i++)
            {
                Console.Write(" ");
            }
        }

        public void UpdateMissile(Fleet a_fleet)
        {
            foreach (Missile missile in _missiles)
            {
                if (missile.IsFired())
                {
                    if (missile.Move(a_fleet)) // on dessin le missile seulement s'il n'a pas explosé
                    {
                        missile.Draw();
                    }
                }
                else
                {
                    //missile.Rearmed();
                    // ne fait rien car le missile n'est pas lancé
                }
            }
        }
    }
}

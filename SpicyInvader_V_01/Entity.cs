using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    public abstract class Entity : ICanFire, IDrawable, IMovebale
    {
        private Shape _shape;
        private Position _position;

        private List<Missile> _missiles;

        private int _lifePoints; // TODO : ajouter les point de vie ici et surtout dans la méthode qui dit si le truc touché est mort

        public Entity() : this(EnumDirection.DOWN) { }

        public Entity(EnumDirection a_missileDirection) : this (" ceci4estun4 test", new Position(0, 0), 0, a_missileDirection) { }

        public Entity(string a_shape, Position a_position, int a_nbrMissile, EnumDirection a_missileDirection)
        {
            _shape = new Shape(a_shape);
            _position = a_position;
            
            InitBasesMissiles(a_nbrMissile, a_missileDirection);
        }

        public void InitBasesMissiles(int a_missileNumber, EnumDirection a_missileDirection)
        {
            _missiles = new List<Missile>();

            for (int i = 0; i < a_missileNumber; i++)
            {
                _missiles.Add(new Missile(a_missileDirection));
            }
        }

        public bool IsAMissileNotFired()
        {
            foreach (Missile missile in _missiles)
            {
                if (!missile.IsFired())
                {
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
                    missile.Fire(new Position(_position.X + 2, _position.Y - 1));
                    new SoundPlayer("..//..//Sounds//LazerFire.wav").Play();
                    return;
                }
                else
                {
                    // nothing to do
                }
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
                    // ne fait rien car le missile n'est pas lancé
                }
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

        public void PrivateMove(EnumDirection a_direction)
        {
            Clear();

            switch (a_direction)
            {
                case EnumDirection.RIGHT:
                    if (_position.X + _shape.GetHorizontalHightSize() < Console.WindowWidth - 1)
                    {
                        _position.X++;
                    }
                    break;
                case EnumDirection.LEFT:
                    if (_position.X > 0)
                    {
                        _position.X--;
                    }
                    break;
                case EnumDirection.UP:
                    if (_position.Y > 0)
                    {
                        _position.Y--;
                    }
                    break;
                case EnumDirection.DOWN:
                    if (_position.Y + _shape.GetSizes().Count < Console.WindowHeight - 1)
                    {
                        _position.Y++;
                    }
                    break;
                default:
                    //normalement pas possible de se déplacer dans d'autres directions, à voir....diagonales ?
                    break;
            }
        }
    }
}

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
        private HitBox _hitBox;

        protected Position _position;

        protected List<Missile> _missiles;

        protected int _lifePoints; // TODO : ajouter les point de vie dans la méthode qui dit si le truc touché est mort

        public Entity() : this(EnumDirection.DOWN) { }

        public Entity(EnumDirection a_missileDirection) : this (" ceci4estun4 test", new Position(0, 0), 0, a_missileDirection) { }

        public Entity(string a_shape, Position a_position, int a_nbrMissile, EnumDirection a_missileDirection)
        {
            _shape = new Shape(a_shape);
            _hitBox = new HitBox(a_position, a_shape);

            _position = a_position;

            _lifePoints = 1;

            InitBasesMissiles(a_nbrMissile, a_missileDirection);
        }

        public List<Missile> GetMissiles()
        {
            return _missiles;
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

        public int GetMissileYPos() // TODO : il y a un truc bizarre dans cette méthode voir a quoi elle sert vraiment et la nommer en conséquence !
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
                    missile.Fire(new Position(_position.X + 2, _position.Y - 1)); // position de départ de missile peut être voir pour modifier selon le vaisseau
                    new SoundPlayer("..//..//Sounds//LazerFire.wav").Play();
                    return;
                }
                else
                {
                    // nothing to do
                }
            }
        }

        public void Fire(Position a_firePosition)
        {
            foreach (Missile missile in _missiles)
            {
                if (!missile.IsFired())
                {
                    missile.Fire(a_firePosition);
                    // TODO : ptetre faire un son différent pour les ennemis
                    return;
                }
                else
                {
                    // nothing to do
                }
            }
        }

        public void Draw()
        {
            _shape.Draw(_position);
        }

        public void Clear()
        {
            _shape.Clear(_position);
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

        public int GetX()
        {
            return _position.X;
        }

        public int GetY()
        {
            return _position.Y;
        }

        public int GetHorizontalHightSize()
        {
            return _shape.GetHorizontalHightSize();
        }

        public HitBox GetHitBox()
        {
            return _hitBox;
        }

        public override string ToString()
        {
            return "Entity";
        }

        public void TakeDamage(int a_damage)
        {
            _lifePoints -= a_damage;
        }

        public bool IsDead()
        {
            return _lifePoints > 0 ? false : true;
        }
    }
}

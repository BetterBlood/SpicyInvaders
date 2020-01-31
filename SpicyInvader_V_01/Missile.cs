using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    class Missile
    {
        private EnumMissileType _missileType;
        private Position _position;

        private int _speed;
        private string _shape; // TODO : modifier en Shape

        private bool _missileFired;

        public Missile() : this (EnumMissileType.Normal) { }

        public Missile(EnumMissileType a_missileType)
        {
            _position = new Position(-1, -1);
            _missileType = a_missileType;

            switch (a_missileType)
            {
                case EnumMissileType.Normal:
                    InitNormalMissiles();
                    break;
                case EnumMissileType.Lazer:
                    InitLazerMissiles();
                    break;
                default:
                    break;
            }
        }

        private void InitNormalMissiles()
        {
            _speed = 1;
            _shape = "*";
            _missileFired = false;
        }

        private void InitLazerMissiles()
        {
            _speed = -1;
            _shape = "||";
            _missileFired = false;
        }

        public bool Move(Fleet a_fleet) // return true si le mouvement a eut lieu sans rencontrer qqch
        {
            Clear();
            for (int i = 0; i < _speed; i++)
            {
                _position.Y--;
            }

            if (IsInvaderHit(a_fleet))
            {
                // TODO : afficher une explosion ??
                Rearmed();
                return false;
            }

            if (_position.Y == 0 || _position.Y == 0)
            {
                Rearmed();
                return false;
            }

            return true;
        }

        private bool IsInvaderHit(Fleet a_fleet)
        {
            List<Invader> invaders = a_fleet.GetMembers();

            foreach (Invader invader in invaders)
            {
                foreach(Position position in invader.GetPositions())
                {
                    if (position.Equals(_position))
                    {
                        Game._score += invader.GetPoint();
                        // TODO : appel d'une méthode d'explosion des invader ?? ( au lieu de clear, mais pas compatible avec invaders.Remove(invader);)
                        invader.Clear();
                        invaders.Remove(invader);
                        return true;
                    }
                }
            }

            return false;
        }

        private void Clear()
        {
            if (_position.X >= 0 && _position.Y >= 0)
            {
                Console.SetCursorPosition(_position.X, _position.Y);
                Console.Write(" "); // voir en fonction de la taille du missile
            }
        }

        public void Draw()
        {
            if (_position.Y != 0)
            {
                Console.SetCursorPosition(_position.X, _position.Y);
                Console.Write(_shape);
            }
        }

        public void Fire(Position a_position)
        {
            _missileFired = true;
            _position = new Position(a_position.X, a_position.Y);
        }

        public void Rearmed()
        {
            Clear();
            _missileFired = false;
        }

        public int GetX()
        {
            return _position.X;
        }

        public int GetY()
        {
            return _position.Y;
        }

        public bool IsFired()
        {
            return _missileFired;
        }
    }
}

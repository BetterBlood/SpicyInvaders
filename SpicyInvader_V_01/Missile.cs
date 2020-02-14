using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
        private EnumDirection _missileDirection; // true signifie que le missile monte, false qu'il descend

        public Missile(EnumDirection a_direction) : this (EnumMissileType.Normal, a_direction) { }

        public Missile(EnumMissileType a_missileType, EnumDirection a_direction)
        {
            _position = new Position(-1, -1);
            _missileDirection = a_direction;
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
            // TODO : modifier en List de vaisseau ou bien d'iunvader ou de boss a attaquer (genre une liste d'entity)

            Clear();
            switch (_missileDirection)
            {
                case EnumDirection.UP:
                    _position.Y--;
                    break;
                case EnumDirection.DOWN:
                    _position.Y++;
                    break;
                case EnumDirection.LEFT:
                    break;
                case EnumDirection.RFIGHT:
                    break;
                default:
                    break;
            }
            _position.Y--;
            

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
                        new SoundPlayer("..//..//Sounds//EnnemyDeath.wav").Play();
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

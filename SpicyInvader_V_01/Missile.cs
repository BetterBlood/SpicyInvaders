using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    public class Missile
    {
        private EnumMissileType _missileType;
        private Position _position;

        private string _shape; // TODO : modifier en Shape
        private int _power;

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

        public int Power
        {
            get { return _power; }
            set { _power = value; }
        }

        private void InitNormalMissiles()
        {
            _shape = "*";
            _missileFired = false;
            _power = 1;
        }

        private void InitLazerMissiles()
        {
            _shape = "||";
            _missileFired = false;
            _power = 3;
        }

        public void UpdateMissile(List<Entity> a_entities, Fleet a_fleet, Game a_game)
        {
            if (IsFired())
            {
                if (Move(a_entities, a_fleet, a_game)) // on dessin le missile seulement s'il n'a pas explosé
                {
                    Draw();
                }
            }
            else
            {
                // ne fait rien car le missile n'est pas lancé
            }
        }

        public bool Move(List<Entity> a_entities, Fleet a_fleet, Game a_game) // return true si le mouvement a eut lieu sans rencontrer qqch
        {
            // TODO : modifier en List de vaisseau ou bien d'invader ou de boss a attaquer (genre une liste d'entity)

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
                    // normallement pas possible mais on verra
                    break;
                case EnumDirection.RIGHT:
                    // normallement impossible mais on verra
                    break;
                default:
                    break;
            }


            if (IsEntityHit(a_entities, a_fleet, a_game))
            {
                // TODO : afficher une explosion 
                Rearmed();
                return false;
            }

            if (_position.Y <= 0)
            {
                Rearmed();
                return false;
            }

            return true;
        }

        private bool IsEntityHit(List<Entity> a_entities, Fleet a_fleet, Game a_game)
        {
            List<Enemy> enemies = new List<Enemy>();
            bool enemyIsHit = false;
            bool enemyIsDead = false;

            foreach (Entity entity in a_entities)
            {
                if (entity.GetHitBox().IsTouch(_position))
                {
                    if (entity.ToString().Equals("Enemy"))
                    {
                        enemyIsHit = true;

                        enemies.Add((Enemy)entity);
                        
                        entity.TakeDamage(_power);

                        if (entity.IsDead())
                        {
                            enemyIsDead = true;
                            a_game.IncreasePoint(((Enemy)entity).GetPoint());
                            a_fleet.RemoveMember((Enemy)entity);
                        }
                    }
                    else // ici c'est donc le vaisseau allié qui est touché
                    {
                        a_game.AllyIsHit(_power);
                    }
                }
            }

            if (enemyIsDead)
            {
                foreach(Enemy enemy in enemies)
                {
                    a_entities.Remove(enemy);
                }
                new SoundPlayer("..//..//Sounds//EnnemyDeath.wav").Play();
                // TODO : appel d'une méthode d'explosion des invader ?? ( au lieu de clear, mais pas compatible avec invaders.Remove(invader);)
            }

            if (enemyIsHit)
            {
                return true;
            }

            return false;
        }

        private void Clear()
        {
            if (_position.X >= 0 && _position.Y >= 0 && _position.Y <= 27) // TODO : mettre 27 en constant quelque part, ptetre dans ship
            {
                Console.SetCursorPosition(_position.X, _position.Y);
                Console.Write(" "); // voir en fonction de la taille du missile
            }
            

        }

        public void Draw()
        {
            if (_position.Y != 0 && _position.Y <= 27) // TODO : mettre 27 en constant quelque part, ptetre dans ship
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

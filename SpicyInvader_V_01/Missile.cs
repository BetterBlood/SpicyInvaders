/*
 * ETML
 * Auteur : Jeremiah, Adrian, Laetitia et Toine
 * Date : Mars 2020
 * Desciption : la classe Missile
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SpicyInvader_V_01
{
    /// <summary>
    /// Class Missile
    /// </summary>
    public class Missile
    {
        /// <summary>
        /// Attributs
        /// </summary>
        private EnumMissileType _missileType;
        private Position _position;

        private Shape _shape;
        private int _power;

        private bool _missileFired;
        private EnumDirection _missileDirection;

        /// <summary>
        /// Propriétés
        /// </summary>
        public int Power
        {
            get { return _power; }
            set { _power = value; }
        }

        /// <summary>
        /// Constructeur renseigné
        /// </summary>
        /// <param name="a_direction"></param>
        public Missile(EnumDirection a_direction) : this (EnumMissileType.Normal, a_direction) { }

        /// <summary>
        /// Constructeur renseigné
        /// </summary>
        /// <param name="a_missileType"></param>
        /// <param name="a_direction"></param>
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

        /// <summary>
        /// Caractéristique du missile de base
        /// </summary>
        private void InitNormalMissiles()
        {
            _shape = new Shape("*");
            _missileFired = false;
            _power = 1;
        }

        /// <summary>
        /// Caraactéristique du missile laser
        /// </summary>
        private void InitLazerMissiles()
        {
            _shape = new Shape("||");
            _missileFired = false;
            _power = 3;
        }

        /// <summary>
        /// Mise à jour d'un missile tiré
        /// </summary>
        /// <param name="a_entities"></param>
        /// <param name="a_fleet"></param>
        /// <param name="a_game"></param>
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

        /// <summary>
        /// Déplacement du missile
        /// </summary>
        /// <param name="a_entities"></param>
        /// <param name="a_fleet"></param>
        /// <param name="a_game"></param>
        /// <returns></returns>
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

            if (_position.Y <= 0 || _position.Y >= Enemy._MAX_FIRE_RANGE)
            {
                Rearmed();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Vérifie si le missile touche une entity, et l'état de cette dernière
        /// </summary>
        /// <param name="a_entities"></param>
        /// <param name="a_fleet"></param>
        /// <param name="a_game"></param>
        /// <returns></returns>
        private bool IsEntityHit(List<Entity> a_entities, Fleet a_fleet, Game a_game)
        {
            List<Enemy> enemies = new List<Enemy>();
            bool enemyIsHit = false;
            bool allyIsHit = false;
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
                        allyIsHit = true;
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
                //new SoundPlayer("..//..//Sounds//EnnemyDeath.wav").Play();
                // TODO : appel d'une méthode d'explosion des invader ?? ( au lieu de clear, mais pas compatible avec invaders.Remove(invader);)
                Thread _threadListener = new Thread(new ThreadStart(PlaySound));
                _threadListener.Name = "ennemyDeath";
                _threadListener.Start();
            }

            if (enemyIsHit || allyIsHit)
            {
                return true;
            }

            return false;
        }

        public void PlaySound()
        {
            if (Menu.SoundIsON())
            {
                new SoundPlayer("..//..//Sounds//EnnemyDeath.wav").Play();
            }
        }

        /// <summary>
        /// Supprime un missile
        /// </summary>
        private void Clear()
        {
            if (_position.X >= 0 && _position.Y >= 0 && _position.Y <= Enemy._MAX_FIRE_RANGE)
            {
                _shape.Clear(_position);
            }
        }

        /// <summary>
        /// Dessine un missile
        /// </summary>
        public void Draw()
        {
            if (_position.Y != 0 && _position.Y <= Enemy._MAX_FIRE_RANGE)
            {
                _shape.Draw(_position);
            }
        }

        /// <summary>
        /// Tire un missile
        /// </summary>
        /// <param name="a_position"></param>
        public void Fire(Position a_position)
        {
            _missileFired = true;
            _position = new Position(a_position.X, a_position.Y);
        }

        /// <summary>
        /// Rend possible un nouveau tire
        /// </summary>
        public void Rearmed()
        {
            Clear();
            _missileFired = false;
        }

        /// <summary>
        /// Retourne la position en X
        /// </summary>
        /// <returns></returns>
        public int GetX()
        {
            return _position.X;
        }

        /// <summary>
        /// Retourne la position en Y
        /// </summary>
        public int GetY()
        {
            return _position.Y;
        }

        /// <summary>
        /// Retourne l'état du missile
        /// </summary>
        /// <returns></returns>
        public bool IsFired()
        {
            return _missileFired;
        }
    }
}

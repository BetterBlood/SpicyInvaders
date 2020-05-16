/*
 * ETML
 * Auteur : Jeremiah, Adrian, Laetitia et Toine
 * Date : Mars 2020
 * Desciption : la classe entity
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
    /// Class Entity
    /// </summary>
    public abstract class Entity : ICanFire, IDrawable, IMovebale
    {
        /// <summary>
        /// Attributs
        /// </summary>
        private Shape _shape;
        private HitBox _hitBox;
        /// <summary>
        /// hdeksfhesk
        /// </summary>
        protected Position _position;
        protected List<Missile> _missiles;
        protected int _lifePoints;
        private int _weaponSlot;
        private EnumDirection _missileDirection;

        /// <summary>
        /// Constructeur par défaut
        /// </summary>
        public Entity() : this(EnumDirection.DOWN) { }

        /// <summary>
        /// Constructeur renseigné
        /// </summary>
        /// <param name="a_missileDirection"></param>
        public Entity(EnumDirection a_missileDirection) : this (" ceci4estun4 test", new Position(0, 0), 0, a_missileDirection) { }

        /// <summary>
        /// Constructeur renseigné
        /// </summary>
        /// <param name="a_shape"></param>
        /// <param name="a_position"></param>
        /// <param name="a_nbrMissile"></param>
        /// <param name="a_missileDirection"></param>
        public Entity(string a_shape, Position a_position, int a_nbrMissile, EnumDirection a_missileDirection)
        {
            _shape = new Shape(a_shape);
            _hitBox = new HitBox(a_position, a_shape);

            _position = a_position;

            _lifePoints = 1;
            _weaponSlot = a_nbrMissile;
            _missileDirection = a_missileDirection;

            InitBasesMissiles(_weaponSlot, _missileDirection);
        }

        /// <summary>
        /// Retourne la liste des missiles de l'entité
        /// </summary>
        /// <returns></returns>
        public List<Missile> GetMissiles()
        {
            return _missiles;
        }

        /// <summary>
        /// Ajoute le nombre nombre de missile possédé par une entité dans sa liste
        /// </summary>
        /// <param name="a_weaponSlot"></param>
        /// <param name="a_missileDirection"></param>
        public void InitBasesMissiles(int a_weaponSlot, EnumDirection a_missileDirection)
        {
            _missiles = new List<Missile>();

            for (int i = 0; i < a_weaponSlot; i++)
            {
                _missiles.Add(new Missile(a_missileDirection));
            }
        }

        /// <summary>
        /// Vérifie s'il y a un missile non tiré
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Position verticale du missile
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Retourne le nombre de missiles présent dans la liste
        /// </summary>
        /// <returns></returns>
        public int GetMissilesCapacity()
        {
            return _missiles.Count();
        }

        /// <summary>
        /// Nombre de missiles disponible
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Son de tire d'un missile
        /// </summary>
        /// <param name="a_bossStage"></param>
        public void PlayAttackSound(bool a_bossStage)
        {
            if (UseFull.SoundIsON() && this is Ally && !a_bossStage)
            {
                new SoundPlayer("..//..//Sounds//LazerFire.wav").Play();
            }
            else if (UseFull.SoundIsON() && this is Enemy && !a_bossStage)
            {
                // TODO : ptetre faire un son différent pour les ennemis
            }
            else
            {
                // le son est désactivé
            }
            
        }

        /// <summary>
        /// Tire un missile si les conditions sont remplies
        /// </summary>
        public void Fire(bool a_bossStage)
        {
            foreach (Missile missile in _missiles)
            {
                if (!missile.IsFired())
                {
                    if (this is Ally)
                    {
                        missile.Fire(new Position(_position.X + _shape.GetHorizontalHightSize() / 2, _position.Y));
                    }
                    else
                    { // TODO : vérifier les positions de lancement : ptetre voir pour les trouver par rapport aux tailles de shape
                        missile.Fire(new Position(_position.X + 2, _position.Y - 1)); // position de départ de missile peut être voir pour modifier selon le style d'ennemy
                    }

                    PlayAttackSound(a_bossStage);
                    return;
                }
                else
                {
                    // nothing to do
                }
            }
        }

        /// <summary>
        /// Lance un missile depuis une position donnée
        /// </summary>
        /// <param name="a_firePosition"></param>
        public void Fire(Position a_firePosition)
        {
            foreach (Missile missile in _missiles)
            {
                if (!missile.IsFired())
                {
                    missile.Fire(a_firePosition);
                    PlayAttackSound(false);
                    return;
                }
                else
                {
                    // nothing to do
                }
            }
        }

        /// <summary>
        /// Dessine l'entité
        /// </summary>
        public virtual void Draw()
        {
            _shape.Draw(_position);
        }

        /// <summary>
        /// Supprime le visuel de l'entité
        /// </summary>
        public void Clear()
        {
            _shape.Clear(_position);
        }

        /// <summary>
        /// Détérmine la nouvelle position de l'entité
        /// </summary>
        /// <param name="a_direction"></param>
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

        /// <summary>
        /// Position en X
        /// </summary>
        /// <returns></returns>
        public int GetX()
        {
            return _position.X;
        }

        /// <summary>
        /// Position en Y
        /// </summary>
        /// <returns></returns>
        public int GetY()
        {
            return _position.Y;
        }

        /// <summary>
        /// Retourne la hauteur de l'entité
        /// </summary>
        /// <returns></returns>
        public int GetHorizontalHightSize()
        {
            return _shape.GetHorizontalHightSize();
        }

        /// <summary>
        /// Retourne la largeur de l'entité
        /// </summary>
        /// <returns></returns>
        public int GetHeight()
        {
            return _shape.GetVerticalLenght();
        }

        /// <summary>
        /// Retourne un objet de classe HitBox
        /// </summary>
        /// <returns></returns>
        public HitBox GetHitBox()
        {
            return _hitBox;
        }

        /// <summary>
        /// Retourne le text "Entity"
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Entity";
        }

        /// <summary>
        /// Enleve des point de vie à l'entité
        /// </summary>
        /// <param name="a_damage"></param>
        public void TakeDamage(int a_damage)
        {
            _lifePoints -= a_damage;
        }

        /// <summary>
        /// Détermine si l'entité est morte ou en vie
        /// </summary>
        /// <returns></returns>
        public bool IsDead()
        {
            return _lifePoints > 0 ? false : true;
        }

        /// <summary>
        /// Donne le nombre de point de vie de l'entité
        /// </summary>
        /// <returns></returns>
        public int GetLife()
        {
            return _lifePoints;
        }

        /// <summary>
        /// Augment le nombre de missiles de reserve
        /// </summary>
        public void UpgradWeaponSlot()
        {
            if (_weaponSlot < 4)
            {
                _weaponSlot++;
                InitBasesMissiles(_weaponSlot, _missileDirection);
            }
        }

        /// <summary>
        /// Modifie les charactéristique pour le hard mode
        /// </summary>
        public void SetHardMode()
        {
            _weaponSlot = 1;
            InitBasesMissiles(_weaponSlot, _missileDirection);
        }

        /// <summary>
        /// Retourne la position de tire
        /// </summary>
        /// <returns></returns>
        protected virtual Position GetFirePosition()
        {
            return new Position(_position.X + GetHorizontalHightSize() / 2, _position.Y + GetHeight());
        }
    }
}

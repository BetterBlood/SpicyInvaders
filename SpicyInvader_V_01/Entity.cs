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

        protected Position _position;

        protected List<Missile> _missiles;

        protected int _lifePoints; // TODO : ajouter les point de vie dans la méthode qui dit si le truc touché est mort

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

            InitBasesMissiles(a_nbrMissile, a_missileDirection);
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
        /// <param name="a_missileNumber"></param>
        /// <param name="a_missileDirection"></param>
        public void InitBasesMissiles(int a_missileNumber, EnumDirection a_missileDirection)
        {
            _missiles = new List<Missile>();

            for (int i = 0; i < a_missileNumber; i++)
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
        /// 
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
        /// 
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
        /// Tire un missile si les conditions sont remplies
        /// </summary>
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
    }
}

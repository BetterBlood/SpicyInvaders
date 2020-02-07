﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    abstract class Entity : ICanFire, IDrawable, IMovebale
    {
        private Shape _shape;
        private Position _position;

        private List<Missile> _missiles;

        public Entity() : this (" ceci4estun4 test", new Position(0, 0), 0) { }

        public Entity(string a_shape, Position a_position, int a_nbrMissile)
        {
            _shape = new Shape(a_shape);
            _position = a_position;
            
            InitBasesMissiles(a_nbrMissile);
        }

        public void InitBasesMissiles(int a_missileNumber)
        {
            _missiles = new List<Missile>();

            for (int i = 0; i < a_missileNumber; i++)
            {
                _missiles.Add(new Missile());
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

        public void PrivateMove(string a_direction)
        {
            Clear();

            if (a_direction.Equals("right"))
            {
               
                if (_position.X + _xSize < Console.WindowWidth - 1) // TODO : réglé ce problème
                {
                    _position.X++;
                }
                
            }
            else if (a_direction.Equals("left"))
            {
                
                if (_position.X > 0)
                {
                    _position.X--;
                }
                
            }
            else
            {
                //normalement pas possible de se déplacer de haut en bas, à voir....
            }
        }
    }
}
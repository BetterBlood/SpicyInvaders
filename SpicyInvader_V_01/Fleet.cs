using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    class Fleet
    {
        private int _numberOfInvader;

        List<Invader> _invaders;

        public Fleet()
        {
            _invaders = new List<Invader>();

            InitInvaders(3);
        }

        public void InitInvaders(int a_fleet_lvl)
        {
            _numberOfInvader = 3 + a_fleet_lvl*2;
            
            int invaderSize = 4;
            int y = 0;
            int decalage = 0;
            int gap = 2;

            bool rightDisplay = true;

            for (int i = 0, j = 0; i < _numberOfInvader; i++, j++)
            {
                if (rightDisplay && j * (invaderSize + 1) + invaderSize + 1 >= Console.WindowWidth)
                {
                    j = 0;
                    y++;
                    rightDisplay = false;
                    decalage = 5;
                }
                else if (!rightDisplay && Console.WindowWidth - (j * (invaderSize + 1) + invaderSize - 2) < 0)
                {
                    j = 0;
                    y++;
                    rightDisplay = true;
                    decalage = 0;
                }

                if (rightDisplay)
                {
                    _invaders.Add(new Invader(j * (invaderSize + 1), y, rightDisplay));
                }
                else
                {
                    _invaders.Add(new Invader(Console.WindowWidth - (j * (invaderSize + 1) + invaderSize - 2), y, rightDisplay));
                }
            }
        }

        public void Update()
        {
            foreach(Invader invader in _invaders)
            {
                invader.Move();
                invader.Draw();
            }
        }

        public List<Invader> GetMembers()
        {
            return _invaders;
        }

        public bool IsDefeated()
        {
            if (_invaders.Count() == 0)
            {
                return true;
            }

            return false;
        }
    }
}

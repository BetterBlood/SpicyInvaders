//Auteur : JMY
//Date   : 03.09.2018
//Lieu   : ETML
//Description : Squelette pour SpaceInvaders en console

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SpicyInvader_V_01
{
    public class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(); // salut
            
            int tics = 0;

            game.Begin(); // appel le premier menu 

            while (true)
            {
                game.Update(tics);

                //Temporisation FPS
                Thread.Sleep(10);

                tics++;

                if (tics == Int32.MaxValue)
                {
                    tics = 0;
                }
            }

        }
    }
}
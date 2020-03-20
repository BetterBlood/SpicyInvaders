/*
 * ETML
 * Auteur : Jeremiah, Adrian, Laetitia et Toine
 * Date : Mars 2020
 * Desciption : la classe program
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SpicyInvader_V_01
{
    /// <summary>
    /// Classe Program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Méthode mais
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Game game = new Game();
            
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
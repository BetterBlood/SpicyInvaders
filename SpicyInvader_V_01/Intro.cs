
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    class Intro
    {
        string[] text = new string[20];

        public Intro()
        {

            text[0] = "  _________                               .___                            .___            ";
            text[1] = " /   _____/_____________   ___        _   |   | _______  _______        __| _/___________ ";
            text[2] = " \\_____  \\____ \\__  \\_/ ___\\/ __ \\        |   |/    \\   \\ / / \\__ \\   / __ |/ __ \\_  __ \\";
            text[3] = " /        \\  |_> > __ \\  \\__\\  ___/       |   |   |  \\    /  / __ \\_ / /_/ \\  ___/|  | \\/";
            text[4] = "/_______  /   __(____  /\\___  >___ >      |___|___|  /\\ _/  (____  /  \\____ |\\___  >__|    ";
            text[5] = "        \\/|__|       \\/     \\/   \\/            \\/            \\/         \\/      \\/      ";
        }

        public void Testicules()
        {
            for (int x = 0; x <= 5; x++)
            {
                Console.WriteLine(text[x]);
            }
        }
    }
}

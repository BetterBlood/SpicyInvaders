/*
 * ETML
 * Auteur : Jeremiah, Adrian, Laetitia et Toine
 * Date : Mars 2020
 * Desciption : l'interface IMovebale
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    /// <summary>
    /// Interface IMovebale
    /// </summary>
    public interface IMovebale
    {
        void PrivateMove(EnumDirection a_direction);
    }
}

/*
 * ETML
 * Auteur : Jeremiah, Adrian, Laetitia et Toine
 * Date : Janvier à Mai 2020
 * Desciption : L'interface IDrawable
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    /// <summary>
    /// Interface IDrawable
    /// </summary>
    public interface IDrawable
    {
        /// <summary>
        /// Dessine la forme
        /// </summary>
        void Draw();

        /// <summary>
        /// Efface la forme
        /// </summary>
        void Clear();
    }
}

/*
 * ETML
 * Auteur : Jeremiah, Adrian, Laetitia et Toine
 * Date : de Février à Mars 2020
 * Desciption : la clase HitBox
 */
using System.Collections.Generic;

namespace SpicyInvader_V_01
{
    /// <summary>
    /// Class HitBox
    /// </summary>
    public class HitBox
    {
        /// <summary>
        /// Attributs
        /// </summary>
        private Position _position;

        private List<List<bool>> _hitBox;

        /// <summary>
        /// Constructeur renseigné
        /// </summary>
        /// <param name="a_position"></param>
        /// <param name="a_shapes"></param>
        public HitBox(Position a_position, string a_shapes)
        {
            _position = a_position;

            InitHitBox(a_shapes);
        }

        /// <summary>
        /// Je crois que j'ai pas envie de comprendre ce que cette méthode fait
        /// </summary>
        /// <param name="a_shapes"></param>
        private void InitHitBox(string a_shapes)
        {
            string[] shapeBoard = a_shapes.Split(UseFull.STRING_SHAPE_SEPARATOR); // ATTENTION : voir la classe Menu si l'on souhaite utiliser un séparateur différent

            _hitBox = new List<List<bool>>();

            for (int i = 0; i < shapeBoard.Length; i++) // initialise chaque List de _hitBox
            {
                _hitBox.Add(new List<bool>());
            }

            bool begin;
            string tmp;

            for (int i = 0; i < shapeBoard.Length; i++) // parcourt chaque ligne du tableau shapeBoeard
            {
                begin = true; // début de la ligne
                tmp = "";

                foreach (char charInString in shapeBoard[i]) // parcourt chaque charactère de la ligne donnée
                {
                    if (begin && charInString.Equals(' ')) // si on est au début de la ligne et que c'est un espace cela ne compte pas dans la hit box !
                    {
                        _hitBox[i].Add(false);
                    }
                    else if (begin) // premier charactère différent d'un espace
                    {
                        _hitBox[i].Add(true);
                        begin = false; // on est plus au début dela ligne
                    }
                    else if (charInString.Equals(' ')) // on sauvegarde les espaces car on ne sais pas si on est encore à l'intérieur de l'entité
                    {
                        tmp += " ";
                    }
                    else // si différent d'un espace ça veut dire que les espaces précédents sont dans l'entité, on les ajoute donc :
                    {
                        foreach (char charTmp in tmp)
                        {
                            _hitBox[i].Add(true);
                        }
                        _hitBox[i].Add(true); // on ajoute un true de plus pour le charactère présent
                        tmp = ""; // on remet la liste tmp à 0
                    }
                }

                for (int k = tmp.Length; k > 0; k--) // ajoute des false à la fin de la ligne si jamais il y a des espaces en trop
                {
                    _hitBox[i].Add(false);
                }
            }
        }

        /// <summary>
        /// Détéecte les collisions entre les entités
        /// </summary>
        /// <param name="a_position"></param>
        /// <returns></returns>
        public bool IsTouch(Position a_position)
        {
            if (a_position.X < _position.X || a_position.Y < _position.Y) // renvoie faux si la position passé en param est trop a gauche ou en haut de l'entité
            {
                return false;
            }
            else
            {
                Position relativePosition = new Position(a_position.X - _position.X, a_position.Y - _position.Y); // cré la position relative à la position de la hitBox (le coin en haut à gauche)

                if (relativePosition.X >= _hitBox[0].Count || relativePosition.Y >= _hitBox.Count) // si la position relative est en dehors de la hitBox on return false
                {
                    return false;
                }
                else
                {
                    return _hitBox[relativePosition.Y][relativePosition.X]; // sinon on return la valeur à cette position
                }
            }
        }
    }
}

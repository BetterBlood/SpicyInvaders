/*
 * ETML
 * Auteur : Jeremiah, Adrian, Laetitia et Toine
 * Date : Mars 2020
 * Desciption : la classe Shape
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    /// <summary>
    /// Class Shape
    /// </summary>
    public class Shape
    {
        /// <summary>
        /// Propriétés
        /// </summary>
        
        //on considère ici qu'une silhouhaite est réctangulaire
        private List<List<string>> _shapes;
        private List<int> _sizes; // la taille de la liste est sa hauteur // chaque nombre de la liste est la taille de la ligne
        //private HitBox _hitbox; // TODO : faire une class hitbox contenant un tableau de boolean

        private int _hightWidth;

        //public Shape()
        //{
        //    _shape = new List<List<string>>();
        //}

        /// <summary>
        /// Constructeur renseigné
        /// </summary>
        /// <param name="a_shapes"></param>
        public Shape(List<List<string>> a_shapes)
        {
            _shapes = a_shapes;
            InitSize();
        }

        /// <summary>
        /// Constructeur renseigné
        /// </summary>
        /// <param name="a_shapes"></param>
        public Shape(string a_shapes)
        {
            _shapes = new List<List<string>>();
            InitShape(a_shapes);
            InitSize();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_shapes"></param>
        private void InitShape(string a_shapes)
        {
            string[] shapeBoard = a_shapes.Split(Menu.STRING_SHAPE_SEPARATOR); // ATTENTION : voir la classe Menu si l'on souhaite utiliser un séparateur différent

            for (int i = 0; i < shapeBoard.Length; i++)
            {
                _shapes.Add(new List<string>());
            }

            for (int i = 0; i < shapeBoard.Length; i++)
            {
                foreach (char charInString in shapeBoard[i])
                {
                    _shapes[i].Add(charInString.ToString());
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitSize()
        {
            _sizes = new List<int>();

            for (int i = 0; i < _shapes.Count; i++)
            {
                _sizes.Add(_shapes[i].Count);

                if (_hightWidth < _shapes[i].Count)
                {
                    _hightWidth = _shapes[i].Count;
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_row"></param>
        /// <returns></returns>
        public int GetHorizontalLenght(int a_row)
        {
            if (a_row > _shapes.Count - 1 || a_row < 0)
            {
                return 0;
            }
            else
            {
                return _shapes[0].Count();
            }
            
        }

        /// <summary>
        /// Retourne le nombre d'élément dans la liste _shapes
        /// </summary>
        /// <returns></returns>
        public int GetVerticalLenght()
        {
            return _shapes.Count();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetHorizontalHightSize()
        {
            return _hightWidth;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<List<string>> GetShape()
        {
            List<List<string>> tmpShapes = new List<List<string>>();

            int i = 0;

            // constructeur de copie
            foreach (List<string> horizontalShapeTmp in _shapes)
            {
                tmpShapes.Add(new List<string>());
                foreach (string charInHori in horizontalShapeTmp)
                {
                    tmpShapes[i].Add(charInHori);
                }
                i++;
            }

            return tmpShapes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<int> GetSizes()
        {
            List<int> tmpSizes = new List<int>();
            
            // constructeur de copie
            foreach (int horizontalSize in _sizes)
            {
                tmpSizes.Add(horizontalSize);
            }

            return tmpSizes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        public void Draw(Position position)
        {
            int i = 0;

            foreach (List<string> horizontalShapeTmp in _shapes)
            {
                Console.SetCursorPosition(position.X, position.Y + i);
                foreach (string charInHori in horizontalShapeTmp)
                {
                    Console.Write(charInHori);
                }
                i++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        public void Clear(Position position)
        {
            int i = 0;

            foreach (List<string> horizontalShapeTmp in _shapes)
            {
                Console.SetCursorPosition(position.X, position.Y + i);
                foreach (string charInHori in horizontalShapeTmp)
                {
                    Console.Write(" ");
                }
                i++;
            }
        }
    }
}

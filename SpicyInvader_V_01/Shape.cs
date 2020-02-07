using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpicyInvader_V_01
{
    class Shape
    {
        //on considère ici qu'une silhouhaite est réctangulaire
        private List<List<string>> _shapes;
        private List<int> _sizes; // la taille de la liste est sa hauteur // chaque nombre de la liste est la taille de la ligne
        //private HitBox _hitbox; // TODO : faire une class hitbox ou bien faire un tableau de boolean

        private int _hightSize;

        //public Shape()
        //{
        //    _shape = new List<List<string>>();
        //}

        public Shape(List<List<string>> a_shapes)
        {
            _shapes = a_shapes;
            InitSize();
        }

        public Shape(string a_shapes)
        {
            _shapes = new List<List<string>>();
            InitShape(a_shapes);
            InitSize();
        }

        private void InitShape(string a_shapes)
        {
            string[] test = a_shapes.Split('4'); // ATTENTION : on ne peut donc pas utiliser le chiffre 4 pour la construction de silhouette 

            for (int i = 0; i < test.Length; i++)
            {
                _shapes.Add(new List<string>());
            }

            for (int i = 0; i < test.Length; i++)
            {
                foreach (char charInString in test[i])
                {
                    _shapes[i].Add(charInString.ToString());
                }
            }
        }

        private void InitSize()
        {
            _sizes = new List<int>();

            for (int i = 0; i < _shapes.Count; i++)
            {
                _sizes.Add(_shapes[i].Count);

                if (_hightSize < _shapes[i].Count)
                {
                    _hightSize = _shapes[i].Count;
                }
            }

        }

        public int GetHorizontalLenght()
        {
            return _shapes[0].Count();
        }

        public int GetVerticalLenght()
        {
            return _shapes.Count();
        }

        public int GetHorizontalHightSize()
        {
            return _hightSize;
        }

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

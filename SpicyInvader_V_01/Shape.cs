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
        private List<int> _tailles; // la taille de la liste est sa hauteur // chaque nombre de la liste est la taille de la ligne
        //private HitBox _hitbox; // TODO : faire une class hitbox ou bien faire un tableau de boolean

        //public Shape()
        //{
        //    _shape = new List<List<string>>();
        //}

        public Shape(List<List<string>> a_shapes)
        {
            _shapes = a_shapes;
            InitTaille();
        }

        public Shape(string a_shapes)
        {
            _shapes = new List<List<string>>();
            InitShape(a_shapes);
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

        private void InitTaille()
        {
            _tailles = new List<int>(); // ICI !!!!!!
        }

        public int GetHorizontalLenght()
        {
            return _shapes[0].Count();
        }

        public int GetVerticalLenght()
        {
            return _shapes.Count();
        }

        public List<List<string>> GetShape()
        {
            List<List<string>> tmp = new List<List<string>>();

            int i = 0;

            // constructeur de copie
            foreach (List<string> horizontalShapeTmp in _shapes)
            {
                tmp.Add(new List<string>());
                foreach (string charInHori in horizontalShapeTmp)
                {
                    tmp[i].Add(charInHori);
                }
                i++;
            }

            return tmp;
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

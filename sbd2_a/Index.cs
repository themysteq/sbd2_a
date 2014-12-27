using System;
using System.Collections.Generic;
using System.Collections.ArrayList;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbd2_a
{
    class Index
    {
        public enum InsertType { PRIMARY, OVERFLOW  };
        private String _index_file_name;    
        public struct IndexElement { public int key; public int offset;};
        private List<IndexElement> _index_body;
        public Index(String index_file_name )
        {
            this._index_file_name = index_file_name;
            _index_body = null;
        }
        /*
        private void sortIndex()
        {
            List<IndexElement> sorted = this._index_body.OrderBy(o => o.key).ToList();
            _index_body = sorted;
        }
         * */
        public int getNearestPageNumberToInsert(int key_value)
        {
            /*(metoda insert powinna ogarniać, że tam gdzie będziemy chcieli wstawić może juz cos byc
             * i będzie trzeba biegać do overflow i tam przepisywać node'y (robić typowego insert między klucze)
             * jesli nie ma zadnej innej wartosci (czyli ogolnie zaczynamy) to wstawiamy po prostu*/

            if (_index_body == null)
            {

            }
            else //jesli jednak jakiś indeks juz istnieje
            {
                for (int i = 0; i < _index_body.Count; i++ )
                {
                    if (key_value == _index_body[i].key)
                    {
                        throw new InvalidOperationException("Key must not be duplicated!");
                    }
                    if (key_value < _index_body[i].key)
                    {
                        //wez poprzednika, powinien byc mniejszy-
                        if(_index_body[i-1].key < key_value)
                        {

                        }
                        else
                        {
                            //wiekszy ale nie mniejszy od poprzedniego, wtf rzuc wyjatkiem!
                            throw new InvalidOperationException("Key is greater but not less?! Paradox");
                        }
                    }
                }
            }
            return 0;
        }
    }
}

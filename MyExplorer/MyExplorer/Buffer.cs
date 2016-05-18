using System.Collections.Generic;

namespace MyExplorer
{
    enum Operation { none, move, copy }

    class buffer
    {  
        public List<string> pathColl = new List<string>();
        public Operation operation;

        public bool Empty { get { return pathColl.Count == 0; } }
    }
}

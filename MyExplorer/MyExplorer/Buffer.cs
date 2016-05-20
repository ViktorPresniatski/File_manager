using System.Collections.Generic;

namespace MyExplorer
{
    enum Operation { none, move, copy }

    struct BufferFile
    {
        public List<string> pathColl;
        public Operation operation;

        public bool Empty { get { return pathColl.Count == 0; } }

    }
}

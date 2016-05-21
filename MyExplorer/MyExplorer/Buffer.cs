using System.Collections.Generic;

namespace MyExplorer
{
    public enum Operation { none, move, copy }

    public struct BufferFile
    {
        public List<string> pathColl;
        public Operation operation;

        public bool Empty { get { return pathColl.Count == 0; } }

    }
}

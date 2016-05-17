using System;

namespace MyExplorer
{
    enum Operation { none, move, copy }

    class buffer
    {  
        public string path;
        public Operation operation;
    }
}

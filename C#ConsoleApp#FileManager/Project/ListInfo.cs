using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    struct ListInfo
    {
        public string path;

        public int page;
        
        public int pos;

        public ListInfo(int a)
        {
             path = "";
             page = 0;
             pos = 0;
        }

    }

}

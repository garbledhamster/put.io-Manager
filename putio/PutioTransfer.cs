using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace putio
{
    class PutioTransfer
    {
        public string name;
        public string file_id;
        public string source;
        public string status;
        public string type;
        public string save_parent_id;
        public string finished_at;
        public string error_message;
        public string started;
        public string size;

        public PutioTransfer(string inStrName, string inStrFileId)
        {
            name = inStrName;
            file_id = inStrFileId;
        }
    }
}

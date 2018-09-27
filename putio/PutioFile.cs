using System;
using System.Windows.Forms;
using System.Net;

namespace putio
{
    public class PutioFile
    {
        public readonly string id;
        public string name;
        public string size;
        public string file_type;
        public string folder_type;
        public string content_type;
        public string parent_id;
        public bool is_hidden;
        public bool is_mp4_available;
        public bool is_shared;

        public string download_path;
        public bool downloaded;
        public Uri downloadlink;
        public DataGridViewRow rowinque;
        public TreeNode file;
        public WebClient webcilent;

        public string autodownload_extensions;
        public Int64 autodownload_minsize;

        public PutioFile(string inStrFileID, string inStrFileName)
        {
            id = inStrFileID;
            name = inStrFileName;
        }

        protected virtual void Dispose()
        {
            Dispose();
            GC.SuppressFinalize(this);
        }

        public override string ToString()
        {
            return string.Format("{0};{1};{2}", name, id, content_type);
        }

    }
}

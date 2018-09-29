using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace putio
{
    public partial class FormPutioManager
    {

        private string magnet(string inStrUrl)
        {
            return inStrUrl.Split('=')[2].Replace("&tr", "").Replace("+", " ");
        }

        private string GetFileExtension(string inStrFileName)
        {
            return Path.GetExtension(inStrFileName.ToLower());
        }

    }
}

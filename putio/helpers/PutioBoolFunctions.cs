using System;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq.Expressions;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Diagnostics;
using PutioApi;

namespace putio
{
    public partial class FormPutioManager
    {
        private bool AlreadyDownloading(PutioFile putiofile)
        {
            foreach (DataGridViewRow row in dataGridViewDownloads.Rows)
            {
                if (row.Cells["ColumnFile"].Value.ToString() == putiofile.name)
                {
                    //Console.WriteLine("file " + putiofile.name + " is already downloading");
                    return true;
                }
            }
            return false;
        }

        private bool DownloadInProgress()
        {
            foreach (DataGridViewRow row in dataGridViewDownloads.Rows)
            {
                if (row.Cells["ColumnStatus"].Value.ToString() != "COMPLETE")
                {
                    return true;
                }
            }
            return false;
        }
    }
}

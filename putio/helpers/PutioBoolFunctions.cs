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
                    return true;
                }
            }
            return false;
        }

        private bool DownloadInProgress()
        {
            List<string> allowed = new List<string> { "complete", "failed", "error" };
            foreach (DataGridViewRow row in dataGridViewDownloads.Rows)
            {
                string value = row.Cells["ColumnStatus"].Value.ToString().ToLower();
                if (allowed.FirstOrDefault(x => x.Contains(value)) == null)
                    return true;
            }
            return false;
        }
    }
}

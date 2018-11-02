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

        private async void GetTransfers()
        {
            dataGridViewTransfers.Rows.Clear();
            var transfers = await trfrmgr.List();
            foreach (JObject transfer in transfers)
            {

                string name = transfer["name"].ToString();
                string id = transfer["id"].ToString();
                string peers = transfer["peers_connected"].ToString();
                string uploaded = ((Convert.ToInt64(transfer["uploaded"]) / 1024) / 1024).ToString() + " Mb";
                string status = transfer["status"].ToString();
                string parentid = transfer["save_parent_id"].ToString();
                string source = transfer["source"].ToString();
                string started = transfer["created_at"].ToString();
                string size = transfer["size"].ToString();

                var putiotransfer = new PutioTransfer(name, id);
                putiotransfer.save_parent_id = parentid;
                putiotransfer.source = source;
                putiotransfer.status = status;
                putiotransfer.started = started;
                putiotransfer.size = size;

                size = ((Convert.ToInt64(size) / 1024) / 1024).ToString() + " Mb";

                int rowindex = dataGridViewTransfers.Rows.Add(name, size, peers, uploaded, started, status);
                var row = dataGridViewTransfers.Rows[rowindex];
                row.Tag = putiotransfer;

            }

            dataGridViewTransfers.Sort(dataGridViewTransfers.Columns[4], ListSortDirection.Descending);

        }
    }
}

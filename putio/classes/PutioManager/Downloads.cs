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
        private void CheckAutoDownloads()
        {
            foreach (TreeNode rootfolder in treeViewAutoDownloads.Nodes)
            {
                AutoDownloadFiles(rootfolder.Tag as PutioFile);
            }
        }

        private void DownloadFile()
        {
            if (FileDownloads.Any())
            {
                foreach (WebClient wc in WebClients)
                {
                    if (!wc.IsBusy)
                    {
                        var putiofile = FileDownloads.Dequeue();
                        putiofile.rowinque.Cells["ColumnStatus"].Value = "Queued";
                        putiofile.webcilent = wc;

                        ContextMenuStrip cstrip = new ContextMenuStrip();
                        cstrip.Items.Add("Cancel").Click += (s, e1) => CstripDownload_Click(s, e1, putiofile);
                        putiofile.rowinque.ContextMenuStrip = cstrip;
                        wc.DownloadFileAsync(putiofile.downloadlink, putiofile.download_path, putiofile);
                        return;
                    }
                }
            }
            return;
        }

        private void QueueDownload(PutioFile putiofile)
        {
            if (!AlreadyDownloading(putiofile))
            {
                string started = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                string path = Properties.Settings.Default.DownloadDirectory + @"\" + putiofile.name;

                dataGridViewDownloads.Rows.Add(putiofile.name, putiofile.file_type, started, "", "Not Started");
                DataGridViewRow newDownloadRow = dataGridViewDownloads.Rows[dataGridViewDownloads.Rows.Count - 1];

                putiofile.rowinque = newDownloadRow;
                putiofile.download_path = path;

                FileDownloads.Enqueue(putiofile);
                DownloadFile();
            }
        }

        private async void AutoDownloadFiles(PutioFile putiofile)
        {
            var response = await filemgr.List(putiofile.id);
            List<string> allowed_extensions = new List<string>();
            allowed_extensions.AddRange(putiofile.autodownload_extensions.Split(',').ToArray());

            foreach (JObject jobject in response)
            {
                var file = SetPutioFile(jobject);
                file.autodownload_extensions = putiofile.autodownload_extensions;
                file.autodownload_minsize = putiofile.autodownload_minsize;
                if (file.file_type == "FOLDER")
                {
                    AutoDownloadFiles(file);
                }
                else
                {
                    var match = allowed_extensions.FirstOrDefault(filext => filext.Contains(GetFileExtension(file.name)));
                    if (match != null & file.autodownload_minsize <= Convert.ToInt64(file.size))
                        QueueDownload(file);
                }
            }
        }

    }
}

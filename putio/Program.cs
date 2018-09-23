using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Net;

namespace putio
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ServicePointManager.DefaultConnectionLimit = Properties.Settings.Default.ConcurrentDownloads;
            Application.Run(new FormPutioManager());
        }

        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs e)
        {
            //MessageBox.Show(e.Name.Split(',')[0].Trim());
            return Load(typeof(Program).Namespace + ".lib." + e.Name.Split(',')[0].Trim() + ".dll");
        }

        public static Assembly Load(string AssemblyName)
        {
            byte[] ba = null;
            string resource = AssemblyName;
            Assembly curAsm = Assembly.GetExecutingAssembly();
            using (Stream stm = curAsm.GetManifestResourceStream(resource))
            {
                ba = new byte[(int)stm.Length];
                stm.Read(ba, 0, (int)stm.Length);

                return Assembly.Load(ba);
            }
        }

    }
}

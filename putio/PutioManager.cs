using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Newtonsoft.Json.Linq;

namespace putio
{
    class PutioManager
    {
        string authtoken;

        public IList<PutioFile> listOfFiles = new List<PutioFile> { };
        
        public PutioManager(string inStrOauthToken)
        {
            authtoken = inStrOauthToken;
            GetFiles("0");
        }

        public void GetFiles(string ParentID)
        {
            Url putioapi = "https://api.put.io/v2".AppendPathSegment("files/list").SetQueryParams(new[] { "parent_id="+ParentID, "oauth_token="+authtoken});
            //Console.WriteLine(putioapi);
            JArray files = (JArray)InvokeResponse(putioapi)["files"];

            foreach (JObject file in files)
            {
                //Console.WriteLine(file);
                //Console.WriteLine("file_type={0}; name={1}; id={2}; parent_id={3}", file["file_type"], file["name"], file["id"], file["parent_id"]);
                SetNewFile(file);
            }
        }

        public JObject InvokeResponse(string inStrUrl)
        {
            Flurl.Http.FlurlClient fclient = new Flurl.Http.FlurlClient();
            string bodyResponse = fclient.HttpClient.GetStringAsync(inStrUrl).Result;
            JObject response = JObject.Parse(bodyResponse);
            return response;
        }

        private void SetNewFile(JObject file)
        {
            string content_type = file["content_type"].ToString();
            string crc32 = file["crc32"].ToString();
            string created_at = file["created_at"].ToString();
            string extension = file["extension"].ToString();
            string file_type = file["file_type"].ToString();
            string first_accessted_at = file["first_accessed_at"].ToString();
            string folder_type = file["folder_type"].ToString();
            string icon = file["icon"].ToString();
            string id = file["id"].ToString();
            bool is_hidden = Convert.ToBoolean(file["is_hidden"]);
            bool is_mp4_available = Convert.ToBoolean(file["is_mp4_available"]);
            bool is_shared = Convert.ToBoolean(file["is_shared"].ToString());
            string name = file["name"].ToString();
            string opensubtitles_hash = file["opensubtitles_hash"].ToString();
            string parent_id = file["parent_id"].ToString();
            string screenshot = file["screenshot"].ToString();
            string size = file["size"].ToString();

            PutioFile putioFile = new PutioFile(
                content_type, crc32, created_at,
                extension, file_type, first_accessted_at,
                folder_type, icon, id,
                is_hidden, is_mp4_available, is_shared,
                name, opensubtitles_hash, parent_id,
                screenshot, size);

            listOfFiles.Add(putioFile);

        }

        public void DeleteFile(string FileID)
        {
            Url putioapi = "https://api.put.io/v2".AppendPathSegment("files/delete").SetQueryParams(new[] { "file_ids=" + FileID, "oauth_token=" + authtoken });
            Console.WriteLine(putioapi);
            Console.WriteLine(InvokeResponse(putioapi));
        }

        public class PutioFile
        {
            public readonly string content_type;
            public readonly string crc32;
            public readonly string created_at;
            public readonly string extension;
            public readonly string file_type;
            public readonly string first_accessted_at;
            public readonly string folder_type;
            public readonly string icon;
            public readonly string id;
            public readonly bool is_hidden;
            public readonly bool is_mp4_available;
            public readonly bool is_shared;
            public readonly string name;
            public readonly string opensubtitles_hash;
            public readonly string parent_id;
            public readonly string screenshot;
            public readonly string size;

            public PutioFile(
                string in_content_type, string in_crc32, string in_created_at,
                string in_extension, string in_file_type, string in_first_accessted_at,
                string in_folder_type, string in_icon, string in_id, bool in_is_hidden,
                bool in_is_mp4_available, bool in_is_shared, string in_name,
                string in_opensubtitles_hash, string in_parent_id, string in_screenshot,
                string in_size)
            {
                content_type = in_content_type;
                crc32 = in_crc32;
                created_at = in_created_at;
                extension = in_extension;
                file_type = in_file_type;
                first_accessted_at = in_first_accessted_at;
                folder_type = in_folder_type;
                icon = in_icon;
                id = in_id;
                is_hidden = in_is_hidden;
                is_mp4_available = in_is_mp4_available;
                is_shared = in_is_shared;
                name = in_name;
                opensubtitles_hash = in_opensubtitles_hash;
                parent_id = in_parent_id;
                screenshot = in_screenshot;
                size = in_size;
            }

            public override string ToString()
            {
                return id;
            }

        }

    }
}

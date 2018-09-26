using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;
using Flurl;
using Flurl.Http;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace putio
{
    public class PutioManager
    {

        private readonly HttpClient httpClient = new HttpClient();
        private const string urlPutioApi = "https://api.put.io/v2/";
        private string oAuthToken;

        public PutioManager(string inStrOAuthToken)
        {
            oAuthToken = inStrOAuthToken;
        }

        public async Task<JObject> AccountInfo()
        {
            var pathsegment = string.Format("account/info");
            object query = new[] { "oauth_token=" + oAuthToken };

            string response = await urlPutioApi.AppendPathSegment(pathsegment)
                .SetQueryParams(query)
                .GetStringAsync();
            return (JObject)JObject.Parse(response)["info"];
        }

        public async Task<JArray> List(string inStrParentId)
        {
            var pathsegment = string.Format("files/list");
            object query = new[] { "parent_id"+inStrParentId,"oauth_token="+oAuthToken };
        
            string response = await urlPutioApi.AppendPathSegment(pathsegment)
                .SetQueryParams(query)
                .GetStringAsync();
            return (JArray)JObject.Parse(response)["files"];
        }

        public async Task<JObject> Get(string inStrParentId)
        {
            var pathsegment = string.Format("files/{0}", inStrParentId);
            object query = new[] { "oauth_token=" + oAuthToken };

            string response = await urlPutioApi.AppendPathSegment(pathsegment)
                .SetQueryParams(query)
                .GetStringAsync();
            return (JObject)JObject.Parse(response)["file"];
        }

        public async Task<string> Delete(string inStrFileIds)
        {
            string pathsegment = "files/delete";
            object query = new[] { "oauth_token=" + oAuthToken };
            object post = new { file_ids=inStrFileIds };

            var response = await urlPutioApi.AppendPathSegment(pathsegment)
                .SetQueryParams(query)
                .PostJsonAsync(post);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> Rename(string inStrFileId, string inStrName)
        {
            string pathsegment = "files/rename";
            object query = new[] { "oauth_token="+oAuthToken };
            object post = new { file_id=inStrFileId,name=inStrName };

            string CurrentName = (await Get(inStrFileId)).ToString();

            Console.WriteLine("currentname=" + CurrentName + "; newname=" + inStrName);

            if (inStrName != CurrentName)
            {
                var response = await urlPutioApi.AppendPathSegment(pathsegment)
                    .SetQueryParams(query)
                    .PostJsonAsync(post);
                return await response.Content.ReadAsStringAsync();
            }
            return null;
        }

        public async Task<string> CreateZip(string inStrFileIds)
        {
            string pathsegment = "zips/create";
            object query = new[] { "oauth_token=" + oAuthToken };
            object post = new { file_ids = inStrFileIds };

            var response = await urlPutioApi.AppendPathSegment(pathsegment)
                .SetQueryParams(query)
                .PostJsonAsync(post);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<JArray> ListZips()
        {
            var pathsegment = "zips/list";
            object query = new[] { "oauth_token=" + oAuthToken };

            string response = await urlPutioApi.AppendPathSegment(pathsegment)
                .SetQueryParams(query)
                .GetStringAsync();
            return (JArray)JObject.Parse(response)["file"];
        }

        public async Task<JObject> GetZip(string inStrZipID)
        {
            var pathsegment = string.Format("zips/{0}", inStrZipID);
            object query = new[] { "oauth_token=" + oAuthToken };

            string response = await urlPutioApi.AppendPathSegment(pathsegment)
                .SetQueryParams(query)
                .GetStringAsync();
            return JObject.Parse(response);
        }

        public async Task DownloadFile(string inStrFileId, string inStrFilePath, string inStrFileName = null)
        {
            var download = await urlPutioApi.AppendPathSegment("files")
                .DownloadFileAsync(inStrFilePath, inStrFileName);
        }

    }
}

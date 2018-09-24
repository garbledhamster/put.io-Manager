using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;

namespace putio
{
    class PutioManager
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
            string urlGetAccountInfo = string.Format(urlPutioApi + "account/info?{0}={1}",
                "oauth_token", oAuthToken);
            //Console.WriteLine("[FILE URL] " + urlGetListFiles);
            string response = await httpClient.GetStringAsync(urlGetAccountInfo);
            return (JObject)JObject.Parse(response)["info"];
        }

        public async Task<JArray> List(string inStrParentId)
        {
            string urlGetListFiles = string.Format(urlPutioApi + "files/list?{0}={1}&{2}={3}",
                "parent_id", inStrParentId,
                "oauth_token", oAuthToken);
            //Console.WriteLine("[LIST FILES URL] " + urlGetListFiles);
            string response = await httpClient.GetStringAsync(urlGetListFiles);
            return (JArray)JObject.Parse(response)["files"];
        }

        public async Task Search(string inStrQuery, string inStrPageNo)
        {

        }

        public async Task<JObject> Get(string inStrParentId)
        {
            string urlGetListFiles = string.Format(urlPutioApi + "files/{0}?{1}={2}",
                inStrParentId,
                "oauth_token", oAuthToken);
            //Console.WriteLine("[FILE URL] " + urlGetListFiles);
            string response = await httpClient.GetStringAsync(urlGetListFiles);
            return (JObject)JObject.Parse(response)["file"];
        }

        public async Task<string> Delete(string inStrFileIds)
        {
            Uri urlPostDeleteFile = new Uri(urlPutioApi + "files/delete");
            var Parameters = new Dictionary<string, string>();
            Parameters.Add("file_ids", inStrFileIds);
            Parameters.Add("oauth_token", oAuthToken);

            HttpContent content = new FormUrlEncodedContent(Parameters);
            var response = await httpClient.PostAsync(urlPostDeleteFile, content);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> Rename(string inStrFileId, string inStrName)
        {
            Uri urlPostRenameFile = new Uri(urlPutioApi + "files/rename");
            var Parameters = new Dictionary<string, string>();
            Parameters.Add("file_id", inStrFileId);
            Parameters.Add("name", inStrName);
            Parameters.Add("oauth_token", oAuthToken);

            HttpContent content = new FormUrlEncodedContent(Parameters);
            var response = await httpClient.PostAsync(urlPostRenameFile, content);

            string message = await response.Content.ReadAsStringAsync();
            return message;
        }

        public async Task<string> CreateZip(string inStrFileIds)
        {
            Uri urlPostCreateZip = new Uri(urlPutioApi + "zips/create");
            var Parameters = new Dictionary<string, string>();
            Parameters.Add("file_ids", inStrFileIds);
            Parameters.Add("oauth_token", oAuthToken);

            HttpContent content = new FormUrlEncodedContent(Parameters);
            var response = await httpClient.PostAsync(urlPostCreateZip, content);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<JArray> ListZips()
        {
            string urlGetZipFiles = string.Format(urlPutioApi + "zips/list?{0}={1}",
                "oauth_token", oAuthToken);
            //Console.WriteLine("[LIST ZIPS URL] " + urlGetZipFiles);
            string response = await httpClient.GetStringAsync(urlGetZipFiles);
            return (JArray)JObject.Parse(response)["files"];

        }

        public async Task<JObject> GetZip(string inStrZipID)
        {
            string urlGetZipFile = string.Format(urlPutioApi + "zips/{0}?{1}={2}",
                inStrZipID,
                "oauth_token", oAuthToken);
            //Console.WriteLine("[ZIP URL] " + urlGetZipFile);
            string response = await httpClient.GetStringAsync(urlGetZipFile);
            return JObject.Parse(response);
        }



    }
}

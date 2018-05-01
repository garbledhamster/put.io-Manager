using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace put.io_manager
{
    static class Authentication
    {
        static string OAuthRequestURL(string clientID, string responseCode, string redirectURI)
        {
            return "https://api.put.io/v2/oauth2/authenticate"+"?client_id="+clientID+"&response_type="+responseCode+"&redirect_uri="+redirectURI;
        }
    }
}
